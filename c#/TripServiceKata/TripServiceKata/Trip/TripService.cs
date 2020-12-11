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

            return user.isFriendsWith(loggedInUser) 
                ? tripsBy(user) 
                : new List<Trip>();
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
