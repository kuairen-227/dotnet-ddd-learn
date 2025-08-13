using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
}