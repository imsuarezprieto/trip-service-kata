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
		private readonly UserSession getInstance;

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
			var tripList = new List<Trip>();
			var loggedUser = getInstance.GetLoggedUser();
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