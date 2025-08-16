using WebApi.Domain.Repositories;

namespace WebApi.Domain.Common;

public interface IUnitOfWork
{

    IOrderRepository Orders { get; }
    ICustomerRepository Customers { get; }
    IProductRepository Products { get; }

    Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
}