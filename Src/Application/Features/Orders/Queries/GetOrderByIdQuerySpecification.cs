using Application.Contracts.Specification;
using Domain.Entities.Order;

namespace Application.Features.Products.Queries.GetAll;

public class GetOrderByIdQuerySpecification : BaseSpecification<Order>
{
    public GetOrderByIdQuerySpecification(int orderId) : base(x => x.Id == orderId) { }
}