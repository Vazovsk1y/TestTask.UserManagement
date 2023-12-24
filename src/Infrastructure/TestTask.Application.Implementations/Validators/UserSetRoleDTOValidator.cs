using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Validators;

public class UserSetRoleDTOValidator : AbstractValidator<UserSetRoleDTO>
{
	public UserSetRoleDTOValidator(IValidator<UserId> userIdValidator, IValidator<RoleId> roleIdValidator)
	{
		RuleFor(e => e.FromId).NotNull().SetValidator(userIdValidator).NotEqual(e => e.ToId).WithMessage("User can't set role to himself.");
		RuleFor(e => e.ToId).NotNull().SetValidator(userIdValidator);
		RuleFor(e => e.RoleId).NotNull().SetValidator(roleIdValidator);
	}
}
