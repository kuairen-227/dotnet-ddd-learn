using System.Text.RegularExpressions;

namespace WebApi.Domain.ValueObjects;

public sealed class Email
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("Emailは必須です");
        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException("Emailの形式が正しくありません");

        Value = value;
    }

    public override bool Equals(object? obj)
        => obj is Email other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    public override string ToString() => Value;
}