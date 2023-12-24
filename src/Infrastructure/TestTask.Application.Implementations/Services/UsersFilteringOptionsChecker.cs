using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Services;

namespace TestTask.Application.Implementations.Services;

public class UsersFilteringOptionsChecker : IFilteringOptionsChecker<UsersFilteringOptions>
{
	public Result IsAppliсable(UsersFilteringOptions filter)
	{
        foreach (var item in filter.Filters)
        {
			var result = IsFilterApplicable(item);
			if (result.IsFailure)
			{
				return result;
			}
        }

		return Result.Success();
    }

	private static Result IsFilterApplicable(UsersFilter filter)
	{
		if (filter.FilterBy == UsersFilterProperties.Roles)
		{
			if (filter.Operator != Operators.Contains)
			{
				return Result.Failure($"Unable to apply [{filter.Operator}] to filter by [{filter.FilterBy}].");
			}

			if (!Guid.TryParse(filter.Value, out _))
			{
				return Result.Failure($"Unable to parse [{filter.Value}] to filter by [{filter.FilterBy}].");
			}
		}

		if (filter.FilterBy == UsersFilterProperties.Age && !int.TryParse(filter.Value, out _))
		{
			return Result.Failure($"Unable to parse [{filter.Value}] to filter by [{filter.FilterBy}].");
		}

        if (
			IsComparableOperator(filter.Operator) && filter.FilterBy != UsersFilterProperties.Age 
			|| 
			IsStringOperator(filter.Operator) && filter.FilterBy == UsersFilterProperties.Age
			)
		{
			return Result.Failure($"Unable to apply [{filter.Operator}] to filter by [{filter.FilterBy}].");
		}

		return Result.Success();
    }

	private static bool IsComparableOperator(Operators @operator)
	{
		return @operator switch
		{
			Operators.LessThanOrEqual or Operators.GreaterThanOrEqual or Operators.LessThan or Operators.GreaterThan => true,
			_ => false,
		};
	}

	private static bool IsStringOperator(Operators @operator)
	{
		return @operator switch
		{
			Operators.Contains or Operators.StartsWith => true,
			_ => false,
		};
	}
}
