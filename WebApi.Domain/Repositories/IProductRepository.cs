using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}