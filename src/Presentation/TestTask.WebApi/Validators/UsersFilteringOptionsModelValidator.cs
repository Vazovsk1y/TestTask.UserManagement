using FluentValidation;
using TestTask.Application.Contracts.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UsersFilteringOptionsModelValidator : AbstractValidator<UsersFilteringOptionsModel>
{
	public UsersFilteringOptionsModelValidator(IValidator<UsersFilterModel> validator)
	{
		RuleFor(e => e.Logic).NotEmpty().IsEnumName(typeof(Logic), false).WithMessage("Invalid logic operator.");
		RuleFor(e => e.Filters).NotEmpty().Must(OnlyUniqueValues).WithMessage("Filters must be unique.");
		RuleForEach(e => e.Filters).SetValidator(validator);
	}

	private bool OnlyUniqueValues(IEnumerable<UsersFilterModel> filters)
	{
		var hashSet = new HashSet<UsersFilterModel>(filters);
		return hashSet.Count == filters.Count();
	}
}