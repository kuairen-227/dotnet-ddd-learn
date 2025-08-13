using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id);
    Task AddAsync(Customer customer);
}