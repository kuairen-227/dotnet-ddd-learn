using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Customer customer);
    void Update(Customer customer);
    void Remove(Customer customer);
}