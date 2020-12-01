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

        [Fact]
        public void ShouldThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            var sut = new TestableTripService();
            sut.LoggedInUser = GUEST;

            Assert.Throws<UserNotLoggedInException>(() => sut.GetTripsByUser(UNUSED_USER));
        }

        [Fact]
        public void ShouldNotReturnAnyTripsWhenUsersAreNotFriends()
        {
            var sut = new TestableTripService();
            sut.LoggedInUser = REGISTERED_USER;

            var friend = new User.User();
            friend.AddFriend(ANOTHER_USER);
            friend.AddTrip(TO_BRAZIL);

            var friendTrips = sut.GetTripsByUser(friend);

            Assert.Empty(friendTrips);
        }

        public class TestableTripService : TripService
        {
            public User.User LoggedInUser;

            public override User.User GetLoggedInUser()
            {
                return LoggedInUser;
            }

        }
    }
}
