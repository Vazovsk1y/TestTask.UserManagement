namespace TestTask.Application.Contracts.Common;

public interface IFilteringOptions
{

}

public interface IFilter
{

}

public interface IFilteringOptions<T> : IFilteringOptions where T : class, IFilter
{
    IReadOnlyCollection<T> Filters { get; }

    Logic Logic { get; }
}

public interface IFilter<T> : IFilter where T : Enum
{
    T FilterBy { get; init; }

    Operators Operator { get; init; }

    string Value { get; init; }
}

public enum Operators
{
    NotEqual,
    LessThanOrEqual,
    GreaterThanOrEqual,
    LessThan,
    GreaterThan,
    Contains,
    StartsWith
}

public enum Logic
{
    And,
    Or,
}