using FluentValidation;
using TestTask.Application.Responses;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UsersSortingModelValidator : AbstractValidator<UsersSortingModel>
{
	public UsersSortingModelValidator()
	{
		RuleFor(e => e.SortDirection).NotEmpty().IsEnumName(typeof(SortDirection), false).WithMessage("Invalid sort direction.");
		RuleFor(e => e.SortBy).NotEmpty().IsEnumName(typeof(UsersSortingProperties), false).WithMessage(e => $"Unable to sort by {e.SortBy}.");
	}
}
