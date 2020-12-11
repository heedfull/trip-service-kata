using Xunit;
using TripServiceKata.Trip;
using TripServiceKata.Exception;
using System.Collections.Generic;
using static TripServiceKata.Tests.UserBuilder;


namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        const User.User GUEST = null;
        const User.User UNUSED_USER = null;
        private static readonly User.User REGISTERED_USER = new User.User();
        private static readonly User.User ANOTHER_USER = new User.User();
        private static readonly Trip.Trip TO_BRAZIL = new Trip.Trip();
        private Trip.Trip TO_LONDON = new Trip.Trip();

        private readonly TripService tripService;


        public TripServiceTest()
        {
            tripService = new TestableTripService(this);
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(UNUSED_USER, GUEST));
        }

        [Fact]
        public void ShouldNotReturnAnyTripsWhenUsersAreNotFriends()
        {
            var friend = AUser()
                .FriendsWith(ANOTHER_USER)
                .WithTrips(TO_BRAZIL)
                .Build();

            var friendTrips = tripService.GetTripsByUser(friend, REGISTERED_USER);

            Assert.Empty(friendTrips);
        }

        [Fact]
        public void ShouldReturnTripsWhenUsersAreFriends()
        {
            var friend = AUser()
                .FriendsWith(ANOTHER_USER, REGISTERED_USER)
                .WithTrips(TO_BRAZIL, TO_LONDON)
                .Build();

            var friendTrips = tripService.GetTripsByUser(friend, REGISTERED_USER);

            Assert.Equal(2, friendTrips.Count);
        }

        public class TestableTripService : TripService
        {
            private readonly TripServiceTest parent;

            public TestableTripService(TripServiceTest parent)
            {
                this.parent = parent;
            }

            protected override List<Trip.Trip> tripsBy(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
