using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;
using TestTask.Domain.Shared;

namespace TestTask.Application.Implementations.Extensions;

public static class Common
{
    internal static async Task<Role> GetRoleByTitleAsync(this IQueryable<Role> roles, string roleTitle)
    {
        return await roles.SingleAsync(e => e.Title == roleTitle);
    }

    internal static async Task<bool> IsEmailTakenAsync(this IQueryable<User> users, string email)
    {
        return await users.AnyAsync(e => e.Email == email);
	}

    internal static bool IsInRole(this User user, string roleTitle)
    {
        return user.Roles.Any(e => e.Role!.Title == roleTitle);
    }

    internal static bool IsInRole(this User user, RoleId roleId)
    {
        return user.Roles.Any(e => e.RoleId == roleId);
    }

    public static IEnumerable<Error> ToSharedErrors(this IEnumerable<ValidationFailure> errors)
    {
        return errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
    }
}