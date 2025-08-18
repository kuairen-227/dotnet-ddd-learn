namespace WebApi.Domain.ValueObjects;

public sealed class ProductName
{
    public string Value { get; }

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("商品名は必須です");

        Value = value;
    }

    public override bool Equals(object? obj)
        => obj is ProductName other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    public override string ToString() => Value;
}