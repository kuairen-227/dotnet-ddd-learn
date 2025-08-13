using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Guid id, Guid productId, Money unitPrice, int quantity)
    {
        Id = id;
        ProductId = productId;
        UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice), "単価は必須です");
        Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity), "数量は1以上でなければなりません");
    }

    public Money GetTotalPrice() => new Money(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}