using TestTask.Application.Responses;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Services;

internal class UserService : IUserService
{
	public Task<Result> AddRoleAsync(UserAddRoleDTO addRoleToUserDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<UsersPage>> GetAsync(PagingOptions? pagingOptions = null, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<UserDTO>> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result> RegisterAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result> UpdateAsync(UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
