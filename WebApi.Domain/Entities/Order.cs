namespace WebApi.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }

    public Customer Customer { get; private set; } = null!;
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public Order(Guid id, Guid customerId, DateTime orderDate)
    {
        Id = id;
        CustomerId = customerId;
        OrderDate = orderDate;
    }

    public void AddItem(OrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item), "注文アイテムは必須です");
        _items.Add(item);
    }

    public decimal GetTotalAmount()
    {
        decimal total = 0;
        foreach (var item in _items)
        {
            total += item.GetTotalPrice();
        }
        return total;
    }
}