using TripServiceKata.Entity;
using TripServiceKata.Service;

namespace TripServiceKata.Tests
{
	class UserSessionStub : IUserSession
	{
		internal User LoggedUser { private get; set;  }

		public User GetLoggedUser() => LoggedUser;
	}
}