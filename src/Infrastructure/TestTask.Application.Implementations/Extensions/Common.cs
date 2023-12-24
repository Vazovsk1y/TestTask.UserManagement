using System.Linq.Expressions;
using TestTask.Application.Contracts.Common;

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
}