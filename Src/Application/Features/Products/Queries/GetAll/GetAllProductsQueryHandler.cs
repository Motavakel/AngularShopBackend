using Application.Contracts;
using Application.Contracts.Specification;
using Application.Dtos.Products;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Entities.ProductEntity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginationResponse<ProductDto>>
{
    private readonly IUnitOWork _uow;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IUnitOWork uow, IMapper mapper, UserManager<User> userManager)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductDto>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new GetFilteredProductsSpec(request);
        var result = await _uow.Repository<Product>().GetListBySpecAsync(spec, cancellationToken);

        var allProductsQuery = _uow.Repository<Product>().GetAllQuery(cancellationToken);
        var minPrice = await allProductsQuery.MinAsync(p => p.Price, cancellationToken);
        var maxPrice = await allProductsQuery.MaxAsync(p => p.Price, cancellationToken);

        var count = await _uow.Repository<Product>().GetCountBySpecAsync(new GetProductsCountSpec(request), cancellationToken);
        var model = _mapper.Map<IEnumerable<ProductDto>>(result);

        return new PaginationResponse<ProductDto>(request.PageIndex, request.PageSize, count, model,minPrice,maxPrice);
    }
}

