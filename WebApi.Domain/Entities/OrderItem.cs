namespace WebApi.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public Product Product { get; private set; } = null!;

    private OrderItem() { } // EF Core用
    public OrderItem(Guid id, Product product, int quantity)
    {
        Id = id;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity), "数量は1以上でなければなりません");
    }

    public decimal GetTotalPrice() => Product.Price * Quantity;
}