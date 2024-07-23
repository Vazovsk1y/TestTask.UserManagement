using MikesPaging.AspNetCore.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public class UsersFilteringEnum : FilteringEnum
{
    public static readonly UsersFilteringEnum ByAge = new(nameof(User.Age), [nameof(User.Age), "user_age"]);
	
    public static readonly UsersFilteringEnum ByFullName = new(nameof(User.FullName), [nameof(User.FullName), "user_fullname"]);
	
    public static readonly UsersFilteringEnum ByRoles = new(nameof(User.Roles), [nameof(User.Roles) ]);
	
    public static readonly UsersFilteringEnum ByEmail = new(nameof(User.Email), [nameof(User.Email) ]);
    private UsersFilteringEnum(string propertyName, IReadOnlyCollection<string> allowedNames) : base(propertyName, allowedNames)
    {
    }
}