using System.Linq.Expressions;
using TestTask.Application.Responses;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations;

public static class Extensions
{
	internal static IQueryable<T> Page<T>(this IQueryable<T> collection, PagingOptions? pagingOptions)
	{
		if (pagingOptions is null)
		{
			return collection;
		}

		return collection
			.Skip((pagingOptions.PageIndex - 1) * pagingOptions.PageSize)
			.Take(pagingOptions.PageSize);
	}

	internal static IOrderedQueryable<User> Sort(this IQueryable<User> users, UsersSortingOptions sortingOptions)
	{
		return sortingOptions.SortBy switch
		{
			UsersSortingProperties.FullName => sortingOptions.SortDirection == SortDirection.Ascending ? users.OrderBy(e => e.FullName) : users.OrderByDescending(e => e.FullName),
			UsersSortingProperties.Age => sortingOptions.SortDirection == SortDirection.Ascending ? users.OrderBy(e => e.Age) : users.OrderByDescending(e => e.Age),
			UsersSortingProperties.RolesCount => sortingOptions.SortDirection == SortDirection.Ascending ? users.OrderBy(e => e.Roles.Count) : users.OrderByDescending(e => e.Roles.Count),
			UsersSortingProperties.Email => sortingOptions.SortDirection == SortDirection.Ascending ? users.OrderBy(e => e.Email) : users.OrderByDescending(e => e.Email),
			_ => throw new KeyNotFoundException(),
		};
	}

	//internal static IQueryable<User> Filter(this IQueryable<User> users, UsersFilteringOptions? filteringOptions)
	//{
	//	if (filteringOptions is null)
	//	{
	//		return users;
	//	}

	//	return filteringOptions.FilterBy switch
	//	{
	//		UsersFilteringProperties.FullName => users.Where(e => e.FullName.Contains(filteringOptions.FilterTerm)),
	//		UsersFilteringProperties.Age => users.Where(e => e.Age == int.Parse(filteringOptions.FilterTerm)),
	//		_ => throw new KeyNotFoundException(),
	//	};
	//}
}
