
namespace TestTask.Application.Responses;

public record UsersPage : Page<UserDTO, UsersSortingOptions>
{
	public UsersPage(IReadOnlyCollection<UserDTO> items, int totalItemsCount, UsersSortingOptions sortingOptions, PagingOptions? pagingOptions = null) 
		: base(items, totalItemsCount, sortingOptions, pagingOptions)
	{
	}
}