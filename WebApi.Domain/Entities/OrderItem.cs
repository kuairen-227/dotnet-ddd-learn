using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { } // EF Core用
    public OrderItem(Guid id, Guid productId, decimal unitPrice, int quantity)
    {
        Id = id;
        ProductId = productId;
        UnitPrice = unitPrice > 0 ? unitPrice : throw new ArgumentOutOfRangeException(nameof(unitPrice), "単価は1以上でなければなりません");
        Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity), "数量は1以上でなければなりません");
    }

    public decimal GetTotalPrice() => UnitPrice * Quantity;
}