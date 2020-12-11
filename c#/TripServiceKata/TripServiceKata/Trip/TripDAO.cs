using System;
using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
    public class TripDAO
    {
        public static List<Trip> FindTripsByUser(User.User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public List<Trip> tripsBy(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
