namespace WebApi.Domain.ValueObjects;

public sealed class Price
{
    public int Value { get; }

    public Price(int value)
    {
        if (value < 1)
            throw new ArgumentOutOfRangeException("価格は1円以上でなければなりません");

        Value = value;
    }

    public override bool Equals(object? obj)
        => obj is Price other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}