namespace PizzaFactory.API.DTOs.Requests;

// acts as a data transfer object for the CreateOrder method what this does is
// it will allow us to send the customer name to the CreateOrder method
public record CreateOrderRequest(string? CustomerName);
