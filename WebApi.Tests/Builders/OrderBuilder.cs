using WebApi.Domain.Entities;

namespace WebApi.Tests.Builders;

public class OrderBuilder
{
    private Guid _id = Guid.NewGuid();
    private Customer _customer = new CustomerBuilder().Build();
    private DateTime _orderDate = DateTime.UtcNow;
    private List<OrderItem> _items = new();

    public static OrderBuilder New() => new OrderBuilder();

    public OrderBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public OrderBuilder WithCustomer(Customer customer)
    {
        _customer = customer;
        return this;
    }

    public OrderBuilder WithOrderDate(DateTime date)
    {
        _orderDate= date;
        return this;
    }

    public OrderBuilder AddItem(Product product, int quantity)
    {
        var item = new OrderItem(Guid.NewGuid(), product, quantity);
        _items.Add(item);
        return this;
    }

    public Order Build()
    {
        var order = new Order(Guid.NewGuid(), _customer.Id, _orderDate);
        foreach (var item in _items)
        {
            order.AddItem(item);
        }
        return order;
    }
}