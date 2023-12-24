using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementations.Validators;

public class UsersFilteringOptionsValidator : AbstractValidator<UsersFilteringOptions>
{
	public UsersFilteringOptionsValidator(IValidator<UsersFilter> validator)
	{
		RuleFor(e => e.Filters).NotEmpty().Must(OnlyUniqueValues).WithMessage("Filters contains duplicates.");
		RuleForEach(e => e.Filters).SetValidator(validator);
	}

	private bool OnlyUniqueValues(IEnumerable<UsersFilter> filters)
	{
		var hashSet = new HashSet<UsersFilter>(filters);
		return hashSet.Count == filters.Count();
	}
}