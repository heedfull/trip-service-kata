using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            User.User loggedInUser = GetLoggedInUser();
            if (loggedInUser == null)
            {
                throw new UserNotLoggedInException();
            }

            List<Trip> tripList = new List<Trip>();

            if (user.isFriendsWith(loggedInUser))
            {
                tripList = tripsBy(user);
            }
            return tripList;

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
