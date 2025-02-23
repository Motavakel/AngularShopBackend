using Application.Contracts.Specification;
using Domain.Entities.ProductEntity;

namespace Application.Features.Products.Queries.GetByBrandId;

public class ProductByBrandIdSpec : BaseSpecification<Product>
{

    public ProductByBrandIdSpec(int id):base(p => p.ProductBrandId == id)
    {
        AddOrderByDesc(x => x.Id);
        Take = 5;
    }
}
