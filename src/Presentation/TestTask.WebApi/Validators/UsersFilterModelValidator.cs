using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UsersFilterModelValidator : AbstractValidator<UsersFilterModel>
{
	public UsersFilterModelValidator()
	{
		RuleFor(e => e.Value).NotEmpty();
		RuleFor(e => e.FilterBy).NotEmpty().IsEnumName(typeof(UsersFilterProperties), false).WithMessage(e => $"Unable to filter by {e.FilterBy}.");
		RuleFor(e => e.Operator).NotEmpty().IsEnumName(typeof(Operators), false).WithMessage(e => $"Invalid operator {e.Operator}.");
	}
}
