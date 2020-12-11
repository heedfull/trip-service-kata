using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            if (GetLoggedInUser() == null)
            {
                throw new UserNotLoggedInException();
            }

            return user.isFriendsWith(GetLoggedInUser())
                ? tripsBy(user)
                : NoTrips();
        }

        private List<Trip> NoTrips()
        {
            return new List<Trip>();
        }

        protected virtual List<Trip> tripsBy(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        protected virtual User.User GetLoggedInUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
