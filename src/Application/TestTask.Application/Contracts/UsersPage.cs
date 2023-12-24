using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Contracts;

public record UsersPage : Page<UserDTO, UsersSortingOptions, UsersFilteringOptions>
{
	public UsersPage(
		IReadOnlyCollection<UserDTO> users, 
		int totalUsersCount, 
		UsersSortingOptions sortingOptions, 
		PagingOptions? pagingOptions = null, 
		UsersFilteringOptions? filteringOptions = null) 
		: base(users, totalUsersCount, sortingOptions, pagingOptions, filteringOptions)
	{
	}
}