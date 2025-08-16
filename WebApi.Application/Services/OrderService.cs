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

   public async Task<IEnumerable<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken)
   {
       var orders = await _unitOfWork.Orders.GetAllAsync(cancellationToken);
       return orders.Select(o => o.ToDto());
   }

   public async Task<OrderDto?> GetOrderAsync(Guid id, CancellationToken cancellationToken)
   {
       var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
       return order?.ToDto();
   }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId, cancellationToken)
            ?? throw new InvalidOperationException("指定された顧客が存在しません");
        var order = new Order(Guid.NewGuid(), customer.Id, DateTime.UtcNow);

        foreach (var itemDto in dto.Items)
        {
            var orderItem = new OrderItem(Guid.NewGuid(), itemDto.ProductId, itemDto.Quantity);
            order.AddItem(orderItem);
        }

        _unitOfWork.Orders.Add(order);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return order.ToDto();
    }
}