using PizzaFactory.API.DTOs.Requests;
using PizzaFactory.API.DTOs.Responses;

namespace PizzaFactory.API.Services;

public interface IOrderService
{
    Task<Guid> CreateOrderAsync(string? customerName);
    Task AddPizzaAsync(Guid orderId, AddPizzaRequest request);
    Task ApplyCouponAsync(Guid orderId, ApplyCouponRequest request);
    Task<OrderResponse> GetOrderAsync(Guid orderId);
    Task<OrderResponse> CheckoutAsync(Guid orderId);
}
