namespace TestTask.Application.Responses;

public record PagingOptions
{
	public int PageSize { get; }

	public int PageIndex { get; }

	private PagingOptions(int pageIndex, int pageSize)
	{
		PageSize = pageSize;
		PageIndex = pageIndex;
	}

	public static Result<PagingOptions> Create(int pageIndex, int pageSize)
	{
		if (pageIndex <= 0)
		{
			return Result.Failure<PagingOptions>("Page index must be greater than zero.");
		}

		if (pageSize <= 0)
		{
			return Result.Failure<PagingOptions>("Page size must be greater than zero.");
		}

		return new PagingOptions(pageIndex, pageSize);
	}
}