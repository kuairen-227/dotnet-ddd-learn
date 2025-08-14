namespace WebApi.Domain.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
}