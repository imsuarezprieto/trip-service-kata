using System;
using System.Collections.Generic;
using System.Linq;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
	public class TripService
	{
		private readonly Func<User, List<Trip>> findTripsByUser;
		private readonly IUserSession           userSession;

		public TripService()
		{
			userSession     = UserSession.GetInstance();
			findTripsByUser = TripDAO.FindTripsByUser;
		}

		public TripService(IUserSession userSession, Func<User, List<Trip>> findTripsByUser)
		{
			this.userSession     = userSession;
			this.findTripsByUser = findTripsByUser;
		}

		public List<Trip> GetTripsByUser(User user)
		{
			User loggedUser = userSession.GetLoggedUser();
			if (loggedUser == null) throw new UserNotLoggedInException();
			List<Trip> tripList = new List<Trip>();
			if (user.GetFriends().Any(friend => friend == loggedUser))
				tripList = findTripsByUser(user);
			return tripList;
		}
	}
}