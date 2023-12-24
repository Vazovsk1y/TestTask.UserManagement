using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestTask.Application.Contracts.Common;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Extensions;

public static class Common
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> collection, PagingOptions? pagingOptions)
    {
        if (pagingOptions is null)
        {
            return collection;
        }

        return collection
            .Skip((pagingOptions.PageIndex - 1) * pagingOptions.PageSize)
            .Take(pagingOptions.PageSize);
    }
    public static IOrderedQueryable<TFrom> Sort<TFrom, TBy>(this IQueryable<TFrom> collection, SortDirection sortDirection, Expression<Func<TFrom, TBy>> expression)
    {
        return sortDirection == SortDirection.Ascending ? collection.OrderBy(expression) : collection.OrderByDescending(expression);
    }

    internal static async Task<Role> GetRoleByTitleAsync(this IQueryable<Role> roles, string roleTitle)
    {
        return await roles.SingleAsync(e => e.Title == roleTitle);
    }

    internal static async Task<bool> IsEmailTakenAsync(this IQueryable<User> users, string email)
    {
        return await users.AnyAsync(e => e.Email == email);
	}
}