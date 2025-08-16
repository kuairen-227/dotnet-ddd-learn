using WebApi.Domain.Repositories;

namespace WebApi.Domain.Common;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }
    IOrderRepository Orders { get; }

    Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
}