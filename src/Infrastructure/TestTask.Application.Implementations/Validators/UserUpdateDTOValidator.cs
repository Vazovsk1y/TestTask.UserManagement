using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementations.Validators;

public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
{
	public UserUpdateDTOValidator()
	{
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0).WithMessage("Age must be positive number.");
		RuleFor(e => e.FullName).NotEmpty();
	}
}