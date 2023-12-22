using TestTask.Application.Responses;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IUserService
{
	Task<Result<UsersPage>> GetAsync(
		UsersSortingOptions sortingOptions, 
		PagingOptions? pagingOptions = null, 
		CancellationToken cancellationToken = default);

	Task<Result<UserDTO>> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);

	Task<Result> AddRoleAsync(UserAddRoleDTO addRoleToUserDTO, CancellationToken cancellationToken = default);

	Task<Result> RegisterAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken = default);

	Task<Result> UpdateAsync(UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken = default);
}
