using WebApi.Application.DTOs;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.Mappings;

public static class OrderMapping
{
    public static OrderDto ToDto(this Order order) =>
        new OrderDto(
            order.Id,
            order.CustomerId,
            order.OrderDate,
            order.GetTotalAmount()
        );
}

public static class OrderItemMapping
{
    public static OrderItemDto ToDto(this OrderItem item) =>
        new OrderItemDto(
            item.Id,
            item.ProductId,
            item.Quantity
        );
}

public static class CreateOrderMapping
{
    public static Order ToEntity(this CreateOrderDto createOrderDto, IEnumerable<Product> products)
    {
        var order = new Order(Guid.NewGuid(), createOrderDto.CustomerId, DateTime.UtcNow);

        foreach (var item in createOrderDto.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId)
                ?? throw new InvalidOperationException("指定された商品が存在しません");
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                product,
                item.Quantity
            );
            order.AddItem(orderItem);
        }
        return order;
    }
}