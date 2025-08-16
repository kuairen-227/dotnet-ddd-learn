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
            order.Currency,
            order.GetTotalAmount()
        );
}

public static class OrderItemMapping
{
    public static OrderItemDto ToDto(this OrderItem item) =>
        new OrderItemDto(
            item.Id,
            item.ProductId,
            item.UnitPrice,
            item.Quantity
        );
}

public static class CreateOrderMapping
{
    public static Order ToEntity(this CreateOrderDto createOrderDto)
    {
        var order = new Order(Guid.NewGuid(), createOrderDto.CustomerId, DateTime.UtcNow, createOrderDto.Currency);

        foreach (var item in createOrderDto.Items)
        {
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                item.ProductId,
                item.UnitPrice,
                item.Quantity
            );
            order.AddItem(orderItem);
        }
        return order;
    }
}