using TestTask.Application.Responses;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations;

internal static class Mapper 
{
	public static UserDTO ToDTO(this User user)
	{
		return new UserDTO(
			user.Id, 
			user.FullName, 
			user.Email, 
			user.Age, 
			user.Roles.Select(r => new RoleDTO(r.RoleId, r.Role!.Title)).ToList());
	}
}
