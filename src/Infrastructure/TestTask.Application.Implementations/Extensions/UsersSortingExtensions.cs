using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Extensions;

public static class UsersSortingExtensions
{
	public static IOrderedQueryable<User> ApplySorting(this IQueryable<User> users, UsersSortingOptions sortingOptions)
	{
		return sortingOptions.SortBy switch
		{
			UsersSortingProperties.FullName => users.Sort(sortingOptions.SortDirection, e => e.FullName),
			UsersSortingProperties.Age => users.Sort(sortingOptions.SortDirection, e => e.Age),
			UsersSortingProperties.RolesCount => users.Sort(sortingOptions.SortDirection, e => e.Roles.Count),
			UsersSortingProperties.Email => users.Sort(sortingOptions.SortDirection, e => e.Email),
			_ => throw new KeyNotFoundException(),
		};
	}
}

