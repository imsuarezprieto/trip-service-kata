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

		[Fact]
		public void Get_no_trips_for_logged_user_with_no_friends_and_no_trips()
		{
			static List<Trip> NoTrips(User _) => new List<Trip>();
			User         user        = new User();
			IUserSession userSession = new UserSessionStub {LoggedUser = user};
			TripService  suo         = new TripService(userSession, NoTrips);

			List<Trip> getSuoTrips = suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Equal(new List<Trip>());
		}

		[Fact]
		public void Get_no_trips_for_logged_user_with_no_friends_and_trips()
		{
			static List<Trip> Trips(User _) => new List<Trip> {new Trip()};
			User         user        = new User();
			IUserSession userSession = new UserSessionStub {LoggedUser = user};
			TripService  suo         = new TripService(userSession, Trips);

			List<Trip> getSuoTrips = suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Equal(new List<Trip>());
		}

		[Fact]
		public void Get_no_trips_for_logged_user_with_friends_and_trips()
		{
			static List<Trip> Trips(User _) => new List<Trip> {new Trip()};
			User         user        = new User();
			IUserSession userSession = new UserSessionStub {LoggedUser = user};
			TripService  suo         = new TripService(userSession, Trips);
			user.AddFriend(new User());

			List<Trip> getSuoTrips = suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Equal(new List<Trip>());
		}

		[Fact]
		public void Get_no_trips_for_no_logged_user_with_no_friends_and_no_trips()
		{
			static List<Trip> NoTrips(User _) => new List<Trip>();
			User         user        = new User();
			User         loggedUser  = new User();
			IUserSession userSession = new UserSessionStub {LoggedUser = loggedUser};
			TripService  suo         = new TripService(userSession, NoTrips);

			List<Trip> getSuoTrips = suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Equal(new List<Trip>());
		}

		[Fact]
		public void Get_no_trips_for_no_logged_user_with_friends_and_no_trips()
		{
			static List<Trip> NoTrips(User _) => new List<Trip>();
			User         user        = new User();
			User         loggedUser  = new User();
			IUserSession userSession = new UserSessionStub { LoggedUser = loggedUser };
			TripService  suo         = new TripService(userSession, NoTrips);
			user.AddFriend(new User());

			List<Trip> getSuoTrips = suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Equal(new List<Trip>());
		}

		[Fact]
		public void Get_no_trips_for_no_logged_user_with_no_friends_and_trips()
		{
			static List<Trip> Trips(User _) => new List<Trip>(){new Trip()};
			User         user        = new User();
			User         loggedUser  = new User();
			IUserSession userSession = new UserSessionStub { LoggedUser = loggedUser };
			TripService  suo         = new TripService(userSession, Trips);

			List<Trip> getSuoTrips = suo.GetTripsByUser(user);

			getSuoTrips
					.Should()
					.Equal(new List<Trip>());
		}
	}
}