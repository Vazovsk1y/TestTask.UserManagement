using MikesPaging.AspNetCore.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public class UsersSortingEnum : SortingEnum
{
    public static readonly UsersSortingEnum ByAge = new(nameof(User.Age), [nameof(User.Age), "user_age"]);
	
    public static readonly UsersSortingEnum ByFullName = new(nameof(User.FullName), [nameof(User.FullName), "user_fullname"]);
	
    public static readonly UsersSortingEnum ByRolesCount = new("RolesCount", [ "RolesCount" ]);
	
    public static readonly UsersSortingEnum ByEmail = new(nameof(User.Email), [ nameof(User.Email) ]);
    private UsersSortingEnum(string propertyName, IReadOnlyCollection<string> allowedNames) : base(propertyName, allowedNames)
    {
    }
}