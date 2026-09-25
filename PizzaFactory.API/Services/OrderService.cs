using PizzaFactory.API.DTOs.Requests;
using PizzaFactory.API.DTOs.Responses;
using PizzaFactory.Core.Builder;
using PizzaFactory.Core.Models;

namespace PizzaFactory.API.Services;

public class OrderService : IOrderService
{
    private readonly Dictionary<Guid, Order> _orders = new();

    public Guid CreateOrder(string? customerName)
    {
        var id = Guid.NewGuid();
        _orders[id] = new Order(customerName);
        return id;
    }

    public void AddPizza(Guid orderId, AddPizzaRequest request)
    {
        var order = GetOrderOrThrow(orderId);

        var builder = new PizzaBuilder();
        builder.WithSize(request.Size);

        foreach (var t in request.Toppings)
            builder.AddTopping(t.Type, t.Name);

        order.AddPizza(builder.Build());
    }

    public void ApplyCoupon(Guid orderId, ApplyCouponRequest request)
    {
        var order = GetOrderOrThrow(orderId);
        order.ApplyCoupon(request.CouponCode, request.DiscountPercent);
    }

    public OrderResponse GetOrder(Guid orderId) =>
        MapToResponse(orderId, GetOrderOrThrow(orderId));

    public OrderResponse Checkout(Guid orderId) =>
        MapToResponse(orderId, GetOrderOrThrow(orderId));

    // ── Private helpers ────────────────────────────────────────────────────

    private Order GetOrderOrThrow(Guid orderId)
    {
        if (!_orders.TryGetValue(orderId, out var order))
            throw new KeyNotFoundException($"Order {orderId} not found.");
        return order;
    }

    private static OrderResponse MapToResponse(Guid id, Order order) =>
        new(
            OrderId:        id,
            CustomerName:   order.CustomerName,
            Pizzas:         order.Pizzas.Select(MapPizza).ToList(),
            Subtotal:       order.Subtotal(),
            CouponCode:     order.CouponCode,
            DiscountPercent:order.DiscountPercent,
            Discount:       order.Discount(),
            Total:          order.Total(),
            Receipt:        order.PrintReceipt()
        );

    private static PizzaResponse MapPizza(Pizza pizza)
    {
        var toppingResponses = pizza.Toppings
            .Select(t => new ToppingResponse(t.Name, t.Type.ToString(), t.Cost))
            .ToList();

        return new PizzaResponse(
            Size:         pizza.Size.ToString(),
            Toppings:     toppingResponses,
            BasePrice:    pizza.BasePrice,
            ToppingsCost: pizza.Toppings.Count * 2.00m,
            TotalCost:    pizza.Cost(),
            Description:  pizza.Describe()
        );
    }
}
