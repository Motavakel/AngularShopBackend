using Application.Contracts.Specification;
using Domain.Entities.Order;

namespace Application.Features.Orders.Commands;

public class GetDeliveryMethodByIdSpecification : BaseSpecification<DeliveryMethod>
{
   public GetDeliveryMethodByIdSpecification(int deliveryMethodId) : base(x => x.Id == deliveryMethodId) { }
}