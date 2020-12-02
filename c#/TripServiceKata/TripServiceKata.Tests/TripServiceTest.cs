using Xunit;
using TripServiceKata.Trip;
using TripServiceKata.Exception;
using System.Collections.Generic;
using System.Linq;

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

        private User.User LoggedInUser;

        private readonly TripService tripService;


        public TripServiceTest()
        {
            tripService = new TestableTripService(this);
            LoggedInUser = REGISTERED_USER;
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
            var friend = UserBuilder.AUser()
            .FriendsWith(ANOTHER_USER)
            .WithTrips(TO_BRAZIL)
            .Build();

            var friendTrips = tripService.GetTripsByUser(friend);

            Assert.Empty(friendTrips);
        }

        [Fact]
        public void ShouldReturnTripsWhenUsersAreFriends()
        {
            var friend = UserBuilder.AUser()
                .FriendsWith(ANOTHER_USER, LoggedInUser)
                .WithTrips(TO_BRAZIL, TO_LONDON)
                .Build();

            var friendTrips = tripService.GetTripsByUser(friend);

            Assert.Equal(2, friendTrips.Count);
        }

        public class TestableTripService : TripService
        {
            private readonly TripServiceTest parent;

            public TestableTripService(TripServiceTest parent)
            {
                this.parent = parent;
            }

            protected override User.User GetLoggedInUser()
            {
                return parent.LoggedInUser;
            }

            protected override List<Trip.Trip> tripsBy(User.User user)
            {
                return user.Trips();
            }
        }
    }

    public class UserBuilder
    {
        private List<User.User> Friends = new List<User.User>();
        private List<Trip.Trip> Trips = new List<Trip.Trip>();

        internal static UserBuilder AUser()
        {
            return new UserBuilder();
        }

        internal User.User Build()
        {
            var user = new User.User();
            AddTripsTo(user);
            AddFriendsTo(user);
            return user;
        }

        private void AddFriendsTo(User.User user)
        {
            foreach (var friend in Friends)
            {
                user.AddFriend(friend);
            }
        }

        private void AddTripsTo(User.User user)
        {
            foreach (var trip in Trips)
            {
                user.AddTrip(trip);
            }
        }

        internal UserBuilder FriendsWith(params User.User[] friends)
        {
            Friends = friends.ToList();
            return this;
        }

        internal UserBuilder WithTrips(params Trip.Trip[] trips)
        {
            Trips = trips.ToList();
            return this;
        }
    }
}
