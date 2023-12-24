using System.Linq.Expressions;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Extensions;

public static class UsersFilteringExtensions
{
	public static IQueryable<User> ApplyFiltering(this IQueryable<User> users, UsersFilteringOptions? filter)
	{
		if (filter is null)
			return users;

		if (filter is { Filters.Count: 0 })
		{
			throw new ArgumentException($"Filters collection can't be empty.");
		}

		Expression<Func<User, bool>> compositeFilterExpression = filter.Logic switch
		{
			Logic.And => GetAndFilterExpression(filter.Filters),
			Logic.Or => GetOrFilterExpression(filter.Filters),
			_ => throw new KeyNotFoundException(),
		};
		return users.Where(compositeFilterExpression);
	}

	private static Expression<Func<User, bool>> GetAndFilterExpression(IReadOnlyCollection<UsersFilter> filters)
	{
		var parameter = Expression.Parameter(typeof(User), "x");
		Expression? andExpression = null;

		foreach (var filter in filters)
		{
			var filterExpression = BuildFilterExpression(filter, parameter);
			if (andExpression is null)
			{
				andExpression = filterExpression;
			}
			else
			{
				andExpression = Expression.AndAlso(andExpression, filterExpression);
			}
		}

		if (andExpression is null)
		{
			throw new InvalidOperationException("Filters were not applied.");
		}

		return Expression.Lambda<Func<User, bool>>(andExpression, parameter);
	}

	private static Expression<Func<User, bool>> GetOrFilterExpression(IReadOnlyCollection<UsersFilter> filters)
	{
		var parameter = Expression.Parameter(typeof(User), "x");
		Expression? orExpression = null;

		foreach (var filter in filters)
		{
			var filterExpression = BuildFilterExpression(filter, parameter);
			if (orExpression is null)
			{
				orExpression = filterExpression;
			}
			else
			{
				orExpression = Expression.OrElse(orExpression, filterExpression);
			}
		}

		if (orExpression is null)
		{
			throw new InvalidOperationException("Filters were not applied.");
		}

		return Expression.Lambda<Func<User, bool>>(orExpression, parameter);
	}

	private static Expression BuildFilterExpression(UsersFilter filter, ParameterExpression parameter)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(filter.Value);

		if (filter.FilterBy == UsersFilterProperties.Roles)
		{
			return BuildFilterByRolesExpression(filter, parameter);
		}

		var property = Expression.Property(parameter, filter.FilterBy.ToString());
		var convertionResult = Convert.ChangeType(filter.Value, property.Type)
			?? throw new InvalidCastException($"Unable cast [{filter.Value}] to [{property.Type}].");

		var constant = Expression.Constant(convertionResult);
		switch (filter.Operator)
		{
			case Operators.NotEqual:
				return Expression.NotEqual(property, constant);
			case Operators.LessThanOrEqual:
				return Expression.LessThanOrEqual(property, constant);
			case Operators.GreaterThanOrEqual:
				return Expression.GreaterThanOrEqual(property, constant);
			case Operators.LessThan:
				return Expression.LessThan(property, constant);
			case Operators.GreaterThan:
				return Expression.GreaterThan(property, constant);
			case Operators.Contains:
				var containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)]);
				return Expression.Call(property, containsMethod!, constant);
			case Operators.StartsWith:
				var startsWithMethod = typeof(string).GetMethod(nameof(string.StartsWith), [typeof(string)]);
				return Expression.Call(property, startsWithMethod!, constant);
			default:
				throw new ArgumentException($"Unsupported operator: [{filter.Operator}]."); ;
		}
	}

	private static MethodCallExpression BuildFilterByRolesExpression(UsersFilter filter, ParameterExpression parameter)
	{
		if (filter.Operator != Operators.Contains)
		{
			throw new ArgumentException($"Unable to apply [{filter.Operator}] to filter by [{filter.FilterBy}].");
		}

		var roleId = new RoleId(Guid.Parse(filter.Value));
		var property = Expression.Property(parameter, filter.FilterBy.ToString());

		var userRoleParameter = Expression.Parameter(typeof(UserRole), "e");
		var roleIdComparison = Expression.Equal(
		Expression.Property(userRoleParameter, nameof(UserRole.RoleId)),
		Expression.Constant(roleId)
		);

		var anyExpression = Expression.Call(
			typeof(Enumerable),
			nameof(Enumerable.Any),
			[typeof(UserRole)],
			property,
			Expression.Lambda<Func<UserRole, bool>>(roleIdComparison, userRoleParameter)
		);

		return anyExpression;
	}
}