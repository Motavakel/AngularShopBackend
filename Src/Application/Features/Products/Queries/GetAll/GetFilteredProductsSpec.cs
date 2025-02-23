using Application.Contracts.Specification;
using Application.Wrappers;
using Domain.Entities.ProductEntity;
using System.Linq.Expressions;

namespace Application.Features.Products.Queries.GetAll;

public class GetFilteredProductsSpec : BaseSpecification<Product>
{
    public GetFilteredProductsSpec(GetAllProductsQuery specParams) : base(Expression.ExpressionSpec(specParams))
    {
        AddInclude(x => x.ProductBrand);
        AddInclude(x => x.ProductType);

        switch (specParams.TypeSort)
        {
            case SortOptions.Newest:
                AddOrderByDesc(x => x.Id); 
                break;
            case SortOptions.PriceHighToLow:
                AddOrderByDesc(x => x.Price);
                break;
            case SortOptions.PriceLowToHigh:
                AddOrderBy(x => x.Price);
                break;
            case SortOptions.NameAToZ:
                AddOrderBy(x => x.Title);
                break;
            default:
                AddOrderByDesc(x => x.Id);
                break;
        }
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize, true);
    }

}
public class GetProductsCountSpec : BaseSpecification<Product>
{
    public GetProductsCountSpec(GetAllProductsQuery specParams) : base(Expression.ExpressionSpec(specParams))
    {
        IsPagingEnabled = false;
    }
}
public class GetProductByIdSpecification : BaseSpecification<Product>
{
    public GetProductByIdSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductBrand);
        AddInclude(x => x.ProductType);
    }
}
public class Expression
{
    public static Expression<Func<Product, bool>> ExpressionSpec(GetAllProductsQuery specParams)
    {
        return x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Title.Contains(specParams.Search))
            &&
            (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId.Value)
            &&
            (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId.Value);
    }
}
