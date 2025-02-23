using Application.Contracts.Specification;
using Domain.Entities.Base;
using System.Linq;

namespace Application.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(T dto, CancellationToken cancellationToken);
    void Update(T entity);
    Task Delete(T entity, CancellationToken cancellationToken);


    //Specification
    Task<T> GetEntityBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetListBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<int> GetCountBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken);

    //Handel Select and Projection
    IQueryable<T> GetQueryBySpec(ISpecification<T> spec, CancellationToken cancellationToken);
}
