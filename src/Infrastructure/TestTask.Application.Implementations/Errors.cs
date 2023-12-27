namespace TestTask.Application.Implementations;

internal static class Errors
{
	public static string EntityWithPassedIdIsNotExists(string entityName) => $"{entityName} with passed id is not exists.";

	internal static class User
	{
		public const string EmailIsAlreadyTaken = "Email is already taken.";
	}

	internal static class Auth
	{
		public const string InvalidCredentials = "Incorrect password or email passed.";

		public const string AccessDenided = "You have not enough rights to perform this action.";
	}
}
