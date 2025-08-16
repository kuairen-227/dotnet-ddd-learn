using WebApi.Domain.Common;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Repositories;

namespace WebApi.Infrastructure.Contexts;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IOrderRepository Orders { get; }
    public ICustomerRepository Customers { get; }
    public IProductRepository Products { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Orders = new OrderRepository(_context);
        Customers = new CustomerRepository(_context);
        Products = new ProductRepository(_context);
    }

    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}