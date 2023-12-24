using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementations.Validators;

public class UsersFilterValidator : AbstractValidator<UsersFilter>
{
	public UsersFilterValidator()
	{
		RuleFor(e => e.Value).NotEmpty().WithMessage("Filter value is required property.");
	}
}