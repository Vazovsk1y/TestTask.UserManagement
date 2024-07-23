namespace TestTask.Domain.Shared;

public sealed record Error
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public string Code { get; }
    public string Text { get; }

    public Error(string code, string text)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(text);
        Text = text;
        Code = code;
    }

    public static implicit operator Error((string code, string message) value) => new(value.code, value.message);
}