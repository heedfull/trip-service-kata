using Xunit;

namespace TripServiceKata.Tests
{
    public class UserTest
    {
        private User.User BOB = new User.User();
        private User.User PAUL = new User.User();

        [Fact]
        public void ShouldInformWhenUsersAreNotFriends()
        {
            var user = UserBuilder.AUser()
                .FriendsWith(BOB)
                .Build();

            Assert.False(user.isFriendsWith(PAUL));
        }

        [Fact]
        public void ShouldInformWhenUsersAreFriends()
        {
            var user = UserBuilder.AUser()
                .FriendsWith(BOB, PAUL)
                .Build();

            Assert.True(user.isFriendsWith(PAUL));
        }
    }
}
