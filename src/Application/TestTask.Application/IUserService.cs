using MikesPaging.AspNetCore.Common;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;
using TestTask.Domain.Shared;

namespace TestTask.Application;

public interface IUserService
{
	Task<Result<UsersPage>> GetPageAsync(
		UserId requesterId,
		SortingOptions<UsersSortingEnum> sortingOptions, 
		PagingOptions? pagingOptions = null,
		FilteringOptions<UsersFilteringEnum>? filteringOptions = null,
		CancellationToken cancellationToken = default);

	Task<Result<UserDTO>> GetByIdAsync(
		UserId requesterId,
		UserId targetId, 
		CancellationToken cancellationToken = default);

	Task<Result<UserId>> RegisterAsync(UserRegisterDTO dto, CancellationToken cancellationToken = default);

	Task<Result> UpdateAsync(UserId requesterId, UserUpdateDTO dto, CancellationToken cancellationToken = default);
}
