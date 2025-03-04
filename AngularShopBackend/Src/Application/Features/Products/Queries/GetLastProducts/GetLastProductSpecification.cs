using Application.Contracts.Specification;
using Domain.Entities.ProductEntity;

namespace Application.Features.Products.Queries.GetLastProducts;

public class GetLastProductSpecification : BaseSpecification<Product>
{
    public GetLastProductSpecification():base()
    {
        
        AddOrderByDesc(x => x.Id);
        ApplyPaging(0, 5);
    }
}