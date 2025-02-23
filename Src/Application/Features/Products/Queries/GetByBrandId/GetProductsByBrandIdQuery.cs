using Application.Contracts;
using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.ProductEntity;
using MediatR;

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
        var products = await _uow.Repository<Product>().GetListBySpecAsync(spec, cancellationToken);
        return _mapper.Map<IEnumerable<ProductHeroSlider>>(products);
    }
}
