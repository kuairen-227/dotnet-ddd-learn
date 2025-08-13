namespace WebApi.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("金額は0以上である必要があります", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("通貨は必須です", nameof(currency));

        Amount = amount;
        Currency = currency;
    }

    public override bool Equals(object obj)
        => obj is Money other && Amount == other.Amount && Currency == other.Currency;
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    public override string ToString() => $"{Amount} {Currency}";

    public Money Add(Money other)
    {
        if (other.Currency != Currency)
            throw new ArgumentException("異なる通貨の金額を加算することはできません", nameof(other));

        return new Money(Amount + other.Amount, Currency);
    }
}