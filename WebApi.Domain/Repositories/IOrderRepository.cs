using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    void Add(Order order);
    void Update(Order order);
    void Remove(Order order);
}