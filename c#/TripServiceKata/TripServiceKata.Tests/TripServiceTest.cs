using Xunit;
using TripServiceKata.Trip;
using TripServiceKata.User;
using TripServiceKata.Exception;


namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        const User.User GUEST = null;
        const User.User UNUSED_USER = null;
        private static readonly User.User REGISTERED_USER = new User.User();
        private static readonly User.User ANOTHER_USER = new User.User();
        private static readonly Trip.Trip TO_BRAZIL = new Trip.Trip();
        private User.User LoggedInUser;

        private TripService tripService;

        public TripServiceTest()
        {
            tripService = new TestableTripService(this);
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenUserIsNotLoggedIn()
        {            
            LoggedInUser = GUEST;

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(UNUSED_USER));
        }

        [Fact]
        public void ShouldNotReturnAnyTripsWhenUsersAreNotFriends()
        {
            LoggedInUser = REGISTERED_USER;

            var friend = new User.User();
            friend.AddFriend(ANOTHER_USER);
            friend.AddTrip(TO_BRAZIL);

            var friendTrips = tripService.GetTripsByUser(friend);

            Assert.Empty(friendTrips);
        }

        public class TestableTripService : TripService
        {
            private readonly TripServiceTest parent;

            public TestableTripService(TripServiceTest parent)
            {
                this.parent = parent;
            }

            public override User.User GetLoggedInUser()
            {
                return parent.LoggedInUser;
            }

        }
    }
}
