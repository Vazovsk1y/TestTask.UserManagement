using FluentValidation;
using TestTask.Application.Responses;

namespace TestTask.Application.Implementations.Validators;

internal class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
{
	public UserUpdateDTOValidator()
	{
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0).WithMessage("Age must be positive number.");
		RuleFor(e => e.Id).NotNull();
		RuleFor(e => e.Id.Value).NotEqual(e => Guid.Empty);
		RuleFor(e => e.FullName).NotEmpty();
	}
}