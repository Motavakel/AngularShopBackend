using Application.Contracts.Specification;
using Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries;

public class GetOrdersByUserSpecification :BaseSpecification<Order>
{
    public GetOrdersByUserSpecification(string UserId) : base(x => x.CreatedBy.Equals(UserId))
    {
        AddInclude(x => x.DeliveryMethod);
        AddInclude(x => x.OrderItems);
        AddOrderByDesc(x => x.Created);
    }
}
