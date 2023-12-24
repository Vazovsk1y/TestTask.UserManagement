using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Validators;

public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
{
	public UserUpdateDTOValidator(IValidator<UserId> validator)
	{
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0).WithMessage("Age must be positive number.");
		RuleFor(e => e.Id).NotNull().SetValidator(validator);
		RuleFor(e => e.FullName).NotEmpty();
	}
}