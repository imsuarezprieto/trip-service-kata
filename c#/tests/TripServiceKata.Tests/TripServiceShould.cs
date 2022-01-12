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
			var user = new User();
			IUserSession userSession = new UserSessionStub();
			List<Trip> NoTrips(User _) => new List<Trip>();
			var suo = new TripService(userSession, NoTrips);

			Action getSuoTrips = () => suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Throw<UserNotLoggedInException>();
		}
	}
}