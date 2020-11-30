using Xunit;
using TripServiceKata.Trip;
using TripServiceKata.Exception;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        const User.User GUEST = null;
        const User.User UNUSED_USER = null;

        [Fact]
        public void ShouldThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            var sut = new TestableTripService();
            sut.LoggedInUser = GUEST;

            Assert.Throws<UserNotLoggedInException>(() => sut.GetTripsByUser(UNUSED_USER));
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
