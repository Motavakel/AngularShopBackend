using Application.Contracts;
using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.ProductEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetLastProducts;

public class GetLastProductQuery : IRequest<IEnumerable<ProductDto>>
{

}

public class GetLastProductQueryHandler : IRequestHandler<GetLastProductQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOWork _uow;
    private readonly IMapper _mapper;
    public GetLastProductQueryHandler(IUnitOWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductDto>> Handle(GetLastProductQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetLastProductSpecification();
        var products = await _uow.Repository<Product>().GetListBySpecAsync(spec,cancellationToken);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
