using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository), "OrderRepositoryは必須です");
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository), "CustomerRepositoryは必須です");
    }

    public async Task<Order> PlaceOrderAsync(Guid customerId, (Guid productId, decimal price, int quantity)[] items)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
            ?? throw new ArgumentException("指定された顧客が存在しません", nameof(customerId));
        var order = new Order(Guid.NewGuid(), customerId, DateTime.UtcNow);

        foreach (var item in items)
        {
            var money = new Money(item.price, "JPY");
            var orderItem = new OrderItem(Guid.NewGuid(), order.Id, money, item.quantity);
            order.AddItem(orderItem);
        }

        await _orderRepository.AddAsync(order);
        return order;
    }
}