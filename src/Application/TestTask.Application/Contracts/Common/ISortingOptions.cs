namespace TestTask.Application.Contracts.Common;


public interface ISortingOptions
{

}

public interface ISortingOptions<T> : ISortingOptions where T : Enum
{
    SortDirection SortDirection { get; init; }

    T SortBy { get; init; }
}

public enum SortDirection
{
    Ascending,
    Descending,
}

