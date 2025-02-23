using Application.Contracts;
using Application.Dtos.OrderDto;
using Application.Features.Products.Queries.GetAll;
using AutoMapper;
using Domain.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery : IRequest<OrderDto>
{
    public int Id { get; set; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IUnitOWork _unitOWork;
    private readonly IMapper _mapper;


    public GetOrderByIdQueryHandler(IUnitOWork unitOWork, IMapper mapper)
    {
        _unitOWork = unitOWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetOrderByIdQuerySpecification(request.Id);
        var order = await _unitOWork.Repository<Order>().GetEntityBySpecAsync(spec,cancellationToken);
        return _mapper.Map<OrderDto>(order);
    }
}