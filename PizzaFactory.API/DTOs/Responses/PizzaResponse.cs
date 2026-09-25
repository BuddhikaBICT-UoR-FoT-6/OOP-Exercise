namespace PizzaFactory.API.DTOs.Responses;

public record PizzaResponse(
    string Size,
    List<ToppingResponse> Toppings,
    decimal BasePrice,
    decimal ToppingsCost,
    decimal TotalCost,
    string Description
);
