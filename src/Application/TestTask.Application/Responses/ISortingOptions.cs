namespace TestTask.Application.Responses;


public interface ISortingOptions
{

}

public interface ISortingOptions<T> : ISortingOptions where T : Enum
{
	public SortDirection SortDirection { get; init; }

	T SortBy { get; init; }
}

public enum SortDirection
{
	Ascending,
	Descending,
}

