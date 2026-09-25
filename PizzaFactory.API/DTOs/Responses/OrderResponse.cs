namespace PizzaFactory.API.DTOs.Responses;

public record OrderResponse(
    Guid OrderId,
    string? CustomerName,
    List<PizzaResponse> Pizzas,
    decimal Subtotal,
    string? CouponCode,
    decimal DiscountPercent,
    decimal Discount,
    decimal Total,
    string Receipt
);
