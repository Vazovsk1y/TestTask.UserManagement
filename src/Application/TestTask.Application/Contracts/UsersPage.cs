using MikesPaging.AspNetCore.Common;

namespace TestTask.Application.Contracts;

public record UsersPage : Page<UserDTO, SortingOptions<UsersSortingEnum>, FilteringOptions<UsersFilteringEnum>>
{
	public UsersPage(IReadOnlyCollection<UserDTO> items,
		int totalItemsCount,
		SortingOptions<UsersSortingEnum>? sortingOptions,
		FilteringOptions<UsersFilteringEnum>? filteringOptions,
		PagingOptions? pagingOptions) : base(items,
		totalItemsCount,
		sortingOptions,
		filteringOptions,
		pagingOptions)
	{
	}
}