using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    void Add(Customer customer);
    void Update(Customer customer);
    void Remove(Customer customer);
}