using WebApi.Domain.Common;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Repositories;

namespace WebApi.Infrastructure.Contexts;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Customers = new CustomerRepository(_context);
        Orders = new OrderRepository(_context);
    }

    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}