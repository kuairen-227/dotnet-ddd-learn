using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Order order);
    void Update(Order order);
    void Remove(Order order);
}