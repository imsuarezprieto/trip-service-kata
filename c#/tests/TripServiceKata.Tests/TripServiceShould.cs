using System;
using System.Collections.Generic;
using FluentAssertions;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
	public class TripServiceShould
	{
		[Fact]
		public void Not_allow_not_logged_user()
		{
			static List<Trip> NoTrips(User _) => new List<Trip>();
			User         user        = new User();
			IUserSession userSession = new UserSessionStub();
			TripService  suo         = new TripService(userSession, NoTrips);

			Action getSuoTrips = () => suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Throw<UserNotLoggedInException>();
		}
	}
}