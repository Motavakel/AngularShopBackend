using Application.Contracts;
using Domain.Entities.Base;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence;

public class UnitOWork : IUnitOWork
{
    private readonly ApplicationDbContext _context;
    public UnitOWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        return new GenericRepository<T>(_context);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.RollbackTransactionAsync(cancellationToken);
    }
}