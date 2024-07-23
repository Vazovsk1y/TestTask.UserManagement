using Microsoft.EntityFrameworkCore;
using TestTask.Application.Contracts;
using TestTask.Application.Implementations.Constants;
using TestTask.Application.Implementations.Extensions;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.Domain.Shared;

namespace TestTask.Application.Implementations;

internal class RoleService(TestTaskDbContext dbContext) : IRoleService
{
	public async Task<Result<IReadOnlyCollection<RoleDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();
		
		var roles = await dbContext
			.Roles
			.OrderBy(e => e.Title)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return roles;
	}

	public async Task<Result> AddToRoleAsync(UserId requesterId, UserAddToRoleDTO dto, CancellationToken cancellationToken = default)
	{
		var userId = new UserId(dto.TargetUserId);
		var roleId = new RoleId(dto.RoleId);
		
		if (requesterId == userId)
		{
			return Result.Failure(new Error("RoleService.AccessDenied", "You can't add new role to yourself."));
		}

		var requester = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == requesterId, cancellationToken);

		if (requester is null)
		{
			return Result.Failure(new Error("RoleService.RequesterNotFound", "Request not found."));
		}

		var role = await dbContext.Roles.SingleOrDefaultAsync(e => e.Id == roleId, cancellationToken);
		if (role is null)
		{
			return Result.Failure(new Error("RoleService.RoleNotFound", "Target role not found."));
		}

		var user = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == userId, cancellationToken);

		if (user is null)
		{
			return Result.Failure(new Error("RoleService.UserNotFound", "Target user not found."));
		}

		if (user.IsInRole(role.Id))
		{
			return Result.Success();
		}

		var actionPermitted = requester.IsInRole(Roles.SuperAdmin);
		if (!actionPermitted)
		{
			return Result.Failure(Errors.Auth.AccessDenied);
		}

		user.Roles.Add(new UserRole
		{
			RoleId = role.Id,
			UserId = user.Id,
		});

		await dbContext.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}