using Application.Contracts;
using Application.Features.Orders.Commands;
using Domain.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetDeliveryMethodById;

public class GetDeliveryMethodByIdQuery : IRequest<DeliveryMethod>
{
    public int Id { get; set; }

    public GetDeliveryMethodByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetDeliveryMethodByIdQueryHandler : IRequestHandler<GetDeliveryMethodByIdQuery, DeliveryMethod>
{
    private readonly IUnitOWork _uow;

    public GetDeliveryMethodByIdQueryHandler(IUnitOWork uow)
    {
        _uow = uow;
    }

    public async Task<DeliveryMethod> Handle(GetDeliveryMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new DeliveryMethodSpecification(request.Id);
        return await _uow.Repository<DeliveryMethod>().GetEntityBySpecAsync(spec, cancellationToken);
    }
}