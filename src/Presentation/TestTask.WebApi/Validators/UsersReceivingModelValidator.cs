using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UsersReceivingModelValidator : AbstractValidator<UsersReceivingModel>
{
	public UsersReceivingModelValidator(
		IValidator<UsersSortingOptionsModel> sortingValidator, 
		IValidator<PagingOptionsModel> pagingValidator,
		IValidator<UsersFilteringOptionsModel> filterValidator)
	{
		RuleFor(e => e.SortingOptions).SetValidator(sortingValidator);

		When(e => e.PagingOptions is not null, () =>
		{
			RuleFor(e => e.PagingOptions).SetValidator(pagingValidator!);
		});

		When(e => e.FilteringOptions is not null, () =>
		{
			RuleFor(e => e.FilteringOptions).SetValidator(filterValidator!);
		});
	}
}