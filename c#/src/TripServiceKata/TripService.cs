using System;
using System.Collections.Generic;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
    public class TripService
    {
	    private readonly UserSession getInstance;
	    private readonly Func<User, List<Trip>> findTripsByUser;

	    public TripService()
	    {
		    getInstance = UserSession.GetInstance();
		    findTripsByUser = TripDAO.FindTripsByUser;
	    }

		public TripService(UserSession getInstance, Func<User, List<Trip>> findTripsByUser)
		{
			this.getInstance = getInstance;
			this.findTripsByUser = findTripsByUser;
		}

		public List<Trip> GetTripsByUser(User user)
        {
            List<Trip> tripList = new List<Trip>();
            User loggedUser = getInstance.GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    tripList = findTripsByUser(user);
                }

                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}