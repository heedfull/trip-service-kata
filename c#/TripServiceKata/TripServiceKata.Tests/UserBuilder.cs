using System.Collections.Generic;
using System.Linq;

namespace TripServiceKata.Tests
{
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
