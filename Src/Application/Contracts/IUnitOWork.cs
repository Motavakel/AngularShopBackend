using Domain.Entities.Base;

namespace Application.Contracts;

public interface IUnitOWork
{
    Task<int> SaveAsync(CancellationToken cancellationToken);
    IGenericRepository<T> Repository<T>() where T : BaseEntity;

    //Transaction
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}