using FluentValidation;
using TestTask.Application.Responses;

namespace TestTask.Application.Implementations.Validators;

internal class UserAddRoleDTOValidator : AbstractValidator<UserAddRoleDTO>
{
	public UserAddRoleDTOValidator()
	{
		RuleFor(e => e.FromId).NotNull().NotEqual(e => e.ToId).WithMessage("User can't add new role to himself.");
		RuleFor(e => e.ToId).NotNull();
		RuleFor(e => e.RoleId).NotNull();

		RuleFor(e => e.RoleId.Value).NotEqual(e => Guid.Empty);
		RuleFor(e => e.FromId.Value).NotEqual(e => Guid.Empty);
		RuleFor(e => e.ToId.Value).NotEqual(e => Guid.Empty);
	}
}
