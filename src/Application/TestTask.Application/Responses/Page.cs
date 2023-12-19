namespace TestTask.Application.Responses;

public abstract record Page<T>
{
	private const int __StartCountingFrom = 1;

	public IReadOnlyCollection<T> Items { get; private set; } = null!;

	public int TotalItemsCount { get; private set; }

	public int PageIndex { get; private set; }

	public int TotalPages { get; private set; }

	public bool HasNextPage => PageIndex < TotalPages;

	public bool HasPreviousPage => PageIndex > __StartCountingFrom;

	protected Page(IReadOnlyCollection<T> items, int totalItemsCount, PagingOptions? pagingOptions = null)
	{
		Initialize(items, totalItemsCount, pagingOptions);
	}

	private void Initialize(IReadOnlyCollection<T> items, int totalItemsCount, PagingOptions? pagingOptions)
	{
		if (items.Count > totalItemsCount)
		{
			throw new ArgumentException("Total items count can't be lower than current items count.");
		}

		if (pagingOptions is null && totalItemsCount != items.Count)
		{
			throw new ArgumentException("If paging options is not passed total items count must be equal to current items count.");
		}

		Items = items;
		TotalItemsCount = totalItemsCount;
		PageIndex = pagingOptions?.PageIndex is null ? __StartCountingFrom : pagingOptions.PageIndex;
		TotalPages = CalculateTotalPages();


		int CalculateTotalPages()
		{
			if (pagingOptions?.PageSize is null)
			{
				return __StartCountingFrom;
			}

			return totalItemsCount < pagingOptions.PageSize ? __StartCountingFrom : (int)Math.Ceiling(totalItemsCount / (double)pagingOptions.PageSize);
		}
	}
}
