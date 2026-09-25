using PizzaFactory.API.DTOs.Requests;
using PizzaFactory.API.DTOs.Responses;

namespace PizzaFactory.API.Services;

public interface IOrderService
{
    Guid CreateOrder(string? customerName);
    void AddPizza(Guid orderId, AddPizzaRequest request);
    void ApplyCoupon(Guid orderId, ApplyCouponRequest request);
    OrderResponse GetOrder(Guid orderId);
    OrderResponse Checkout(Guid orderId);
}
