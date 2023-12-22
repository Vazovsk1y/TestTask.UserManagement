using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UsersReceivingModelValidator : AbstractValidator<UsersReceivingModel>
{
	public UsersReceivingModelValidator(
		IValidator<UsersSortingModel> sortingValidator, 
		IValidator<PagingOptionsModel> pagingValidator)
	{
		RuleFor(e => e.SortingModel).SetValidator(sortingValidator);
		When(e => e.PagingOptionsModel is not null, () =>
		{
			RuleFor(e => e.PagingOptionsModel).SetValidator(pagingValidator!);
		});
	}
}