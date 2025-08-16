using WebApi.Application.DTOs;
using WebApi.Application.Mappings;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;

namespace WebApi.Application.Services;

public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto> PlaceOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId, cancellationToken)
            ?? throw new InvalidOperationException("指定された顧客が存在しません");
        var order = new Order(Guid.NewGuid(), customer.Id, DateTime.UtcNow, dto.Currency);

        foreach (var itemDto in dto.Items)
        {
            var orderItem = new OrderItem(Guid.NewGuid(), itemDto.ProductId, itemDto.UnitPrice, itemDto.Quantity);
            order.AddItem(orderItem);
        }

        _unitOfWork.Orders.Add(order);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return order.ToDto();
    }
}