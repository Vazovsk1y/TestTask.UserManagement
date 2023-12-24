namespace TestTask.Application.Implementations;

internal static class Errors
{
	public static string EntityWithPassedIdIsNotExists(string entityName) => $"{entityName} with passed id is not exists.";

	internal static class User
	{
		public const string EmailIsAlreadyTaken = "Email is already taken.";
	}
}
