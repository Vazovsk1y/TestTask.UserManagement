namespace TestTask.Application.Responses;

public record UsersSortingOptions(SortDirection SortDirection, UsersSortingProperties SortBy) : ISortingOptions<UsersSortingProperties>;

public enum UsersSortingProperties
{
	FullName,
	Age,
	RolesCount,
	Email,
}

