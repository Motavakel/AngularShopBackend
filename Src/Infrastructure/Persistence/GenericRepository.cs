using Application.Contracts;
using Application.Contracts.Specification;
using Domain.Entities.Base;
using Domain.Entities.Order;
using Domain.Exceptions;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T dto, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(dto, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task Delete(T entity, CancellationToken cancellationToken)
    {
        var spec = new BaseSpecification<T>(x => x.Id == entity.Id);
        var record = await GetEntityBySpecAsync(spec,cancellationToken);

        if (record == null) throw new NotFoundEntityException("خطا در انجام عملیات حذف");

        record.IsDelete = true;
        Update(entity);
    }


    public async Task<T> GetEntityBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecToQuery(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetListBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecToQuery(spec).ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecToQuery(spec).CountAsync(cancellationToken);
    }

    public IQueryable<T> GetQueryBySpec(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return ApplySpecToQuery(spec);
    }

    private IQueryable<T> ApplySpecToQuery(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
    }
}




