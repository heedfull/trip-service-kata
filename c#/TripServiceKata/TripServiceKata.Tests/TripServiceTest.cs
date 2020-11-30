using Xunit;
using TripServiceKata.Trip;
using TripServiceKata.Exception;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        private readonly User.User GUEST;
        private readonly User.User UNUSED_USER;

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
