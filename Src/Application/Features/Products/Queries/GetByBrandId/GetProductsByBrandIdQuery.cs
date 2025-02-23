using Application.Contracts;
using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.ProductEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetByBrandId;

public class GetProductsByBrandIdQuery : IRequest<IEnumerable<ProductHeroSlider>>
{
    public int Id { get; set; }

    public GetProductsByBrandIdQuery(int id)
    {
        Id = id;
    }
}

public class GetProductsByBrandIdQueryHandler : IRequestHandler<GetProductsByBrandIdQuery, IEnumerable<ProductHeroSlider>>
{
    private readonly IUnitOWork _uow;
    private readonly IMapper _mapper;
    public GetProductsByBrandIdQueryHandler(IUnitOWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductHeroSlider>> Handle(GetProductsByBrandIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductByBrandIdSpec(request.Id);
        return await _uow.Repository<Product>()
            .GetQueryBySpec(spec, cancellationToken)
            .Select(p => _mapper.Map<ProductHeroSlider>(p)) // اینجا مپ رو استفاده کن
            .ToListAsync();
    }
}
