
namespace TestTask.Application.Responses;

public record UsersPage : Page<UserDTO>
{
	public UsersPage(IReadOnlyCollection<UserDTO> items, int totalItemsCount, PagingOptions? pagingOptions = null) : base(items, totalItemsCount, pagingOptions)
	{
	}
}