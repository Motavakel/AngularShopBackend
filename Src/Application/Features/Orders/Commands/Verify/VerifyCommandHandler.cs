using Application.Contracts;
using Application.Dtos.OrderDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Order;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Application.Features.Orders.Commands.Verify;

public class VerifyCommand : IRequest<OrderVerifyDto>
{
    public string Authority { get; set; }
    public string PaymentStatus { get; set; }
    public string invoiceID { get; set; }

    public VerifyCommand(string authority, string InvoiceID, string paymentStatus)
    {
        Authority = authority;
        PaymentStatus = paymentStatus;
        invoiceID = InvoiceID;
    }
}

public class VerifyCommandHandler : IRequestHandler<VerifyCommand, OrderVerifyDto>
{
    private readonly IUnitOWork _uow;
    private readonly IMapper _mapper;
    private readonly IBasketRepository _basketRepository;

    public VerifyCommandHandler(
        IUnitOWork uow,
        IConfiguration configuration,
        IBasketRepository basketRepository,
        IMapper mapper
        ) 
    {
        _uow = uow;
        _mapper = mapper;
        _basketRepository = basketRepository;
    }

    public async Task<OrderVerifyDto> Handle(
        VerifyCommand request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderByAuthoritySpecification(request.Authority);
        var order = await _uow.Repository<Order>().GetEntityBySpecAsync(spec, cancellationToken);

        if (order == null)
        {
            return new OrderVerifyDto
            {
                OrderStatus = OrderStatus.PaymentFailed,
                
            };
        }

        string basketId = order.BasketId;
        int amount = order.SubTotal * 10;
        var paymentResult = await CheckPayment(amount, request.Authority);

        await _uow.BeginTransactionAsync(cancellationToken);
        
        if (request.PaymentStatus == "NOK" || !paymentResult.StartsWith("پرداخت موفق"))
        {
            order.PaymentDate = DateTime.UtcNow;
            order.OrderStatus = OrderStatus.PaymentFailed;

            try
            {
                await _uow.SaveAsync(cancellationToken);
                await _uow.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync(cancellationToken);
                return new OrderVerifyDto
                {
                    OrderStatus = OrderStatus.PaymentFailed,
                };
            }

            return new OrderVerifyDto
            {
                OrderStatus = OrderStatus.PaymentFailed,
            };
        }

        if (!string.IsNullOrEmpty(basketId) && basketId != string.Empty)
        {
            await _basketRepository.DeleteBasketAsync(basketId);
        }

        order.PaymentDate = DateTime.UtcNow;
        order.OrderStatus = OrderStatus.PaymentSuccess;
        order.IsFinally = true;
        try
        {
            await _uow.SaveAsync(cancellationToken);
            await _uow.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _uow.RollbackTransactionAsync(cancellationToken);
            return new OrderVerifyDto
            {
                OrderStatus = OrderStatus.PaymentFailed,
            };
        }

        return _mapper.Map<OrderVerifyDto>(order);
    }


    public async Task<string> CheckPayment(int amount, string authority)
    {
        try
        {
            string merchant = "test";

            var client = new RestClient(new RestClientOptions { Timeout = TimeSpan.FromSeconds(30) });
            var request = new RestRequest("https://api.novinopay.com/payment/ipg/v2/verification", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var body = new
            {
                merchant_id = merchant,
                amount = amount,
                authority = authority
            };

            request.AddJsonBody(body);

            RestResponse response = await client.ExecuteAsync(request).ConfigureAwait(false);


            if (!response.IsSuccessStatusCode)
            {
                return $"خطا در برقراری ارتباط با درگاه: {response.StatusCode} - {response.ErrorMessage}";
            }

            var result = JsonConvert.DeserializeObject<PaymentResponse>(response.Content);

            if (result == null || result.status != "100")
            {
                return $"پرداخت تأیید نشد: {result?.message ?? "پاسخ نامعتبر از درگاه"}";
            }

            return $"پرداخت موفق - شماره پیگیری: {result.data.ref_id}";
        }
        catch (Exception ex)
        {
            return $"خطای سیستمی رخ داده: {ex.Message}";
        }
    }
}



public class PaymentResponse
{
    public string status { get; set; }
    public string message { get; set; }
    public PaymentData data { get; set; }
}

public class PaymentData
{
    public long trans_id { get; set; }
    public string ref_id { get; set; }
    public string authority { get; set; }
    public string card_pan { get; set; }
    public int amount { get; set; }
    public string buyer_ip { get; set; }
    public long payment_time { get; set; }
}
