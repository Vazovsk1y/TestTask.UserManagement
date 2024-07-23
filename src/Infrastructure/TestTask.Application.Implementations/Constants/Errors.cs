using TestTask.Domain.Shared;

namespace TestTask.Application.Implementations.Constants;

internal static class Errors
{
	internal static class User
	{
		public static readonly Error EmailIsAlreadyTaken = new Error("Auth.EmailIsAlreadyTaken", "Email is already taken.");
	}

	internal static class Auth
	{
		public static readonly Error InvalidCredentials = new ("Auth.InvalidCredentials","Incorrect password or email passed.");

		public static readonly Error AccessDenied = new Error("Auth.AccessDenied", "You have not enough rights to perform this action.");
	}
}
