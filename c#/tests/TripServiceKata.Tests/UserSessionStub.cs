using TripServiceKata.Entity;
using TripServiceKata.Service;

namespace TripServiceKata.Tests
{
	class UserSessionStub : IUserSession
	{
		internal User User { private get; set;  }

		public User GetLoggedUser() => User;
	}
}