using System;
using System.Collections.Generic;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
	public class TripService
	{
		private readonly Func<User, List<Trip>> findTripsByUser;
		private readonly UserSession userSession;

		public TripService()
		{
			userSession = UserSession.GetInstance();
			findTripsByUser = TripDAO.FindTripsByUser;
		}

		public TripService(UserSession userSession, Func<User, List<Trip>> findTripsByUser)
		{
			this.userSession = userSession;
			this.findTripsByUser = findTripsByUser;
		}

		public List<Trip> GetTripsByUser(User user)
		{
			var tripList = new List<Trip>();
			var loggedUser = userSession.GetLoggedUser();
			var isFriend = false;
			if (loggedUser != null)
			{
				foreach (var friend in user.GetFriends())
					if (friend.Equals(loggedUser))
					{
						isFriend = true;
						break;
					}

				if (isFriend) tripList = findTripsByUser(user);

				return tripList;
			}

			throw new UserNotLoggedInException();
		}
	}
}