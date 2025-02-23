using Application.Contracts.Specification;
using Domain.Entities.Order;

namespace Application.Features.Orders.Queries;

public class DeliveryMethodSpecification : BaseSpecification<DeliveryMethod>
{
    public DeliveryMethodSpecification(int Id) : base(x => x.Id == Id)
    {

    }
}
