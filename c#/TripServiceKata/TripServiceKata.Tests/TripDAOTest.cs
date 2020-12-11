using TripServiceKata.Exception;
using TripServiceKata.Trip;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripDAOTest
    {
        [Fact]
        public void ShouldThrowExceptionWhenRetrievingUserTrips()
        {
            Assert.Throws<DependendClassCallDuringUnitTestException>(
                () => new TripDAO().tripsBy(new User.User()));
        }
    }
}
