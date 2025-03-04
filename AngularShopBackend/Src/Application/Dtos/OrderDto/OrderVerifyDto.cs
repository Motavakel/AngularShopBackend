using Application.Common.Mapping;
using Application.Dtos.Common;
using AutoMapper;
using Domain.Entities.Order;
using Domain.Enums;

namespace Application.Dtos.OrderDto;

public class OrderVerifyDto : IMapFrom<Order>
{
    public string InvoiceNumber { get; set; }
    public string TrackingCode { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderVerifyDto>();
           
    }
}
