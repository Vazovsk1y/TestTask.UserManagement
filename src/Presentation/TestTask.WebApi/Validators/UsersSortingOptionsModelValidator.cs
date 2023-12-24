using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UsersSortingOptionsModelValidator : AbstractValidator<UsersSortingOptionsModel>
{
	public UsersSortingOptionsModelValidator()
	{
		RuleFor(e => e.SortDirection).NotEmpty().IsEnumName(typeof(SortDirection), false).WithMessage("Invalid sort direction.");
		RuleFor(e => e.SortBy).NotEmpty().IsEnumName(typeof(UsersSortingProperties), false).WithMessage(e => $"Unable to sort by {e.SortBy}.");
	}
}