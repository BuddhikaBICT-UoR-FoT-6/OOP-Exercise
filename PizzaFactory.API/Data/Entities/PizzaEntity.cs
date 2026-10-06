namespace PizzaFactory.API.Data.Entities;

/// <summary>
/// Persistence entity for a Pizza. Stored in the 'Pizzas' table.
/// Size is stored as a string ("Small", "Medium", "Large") for readability.
/// </summary>
public class PizzaEntity
{
    public Guid Id { get; set; }
    public string Size { get; set; } = string.Empty;   // e.g. "Large"

    // Foreign key back to the parent Order
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; } = null!;

    // Navigation property — EF will load related toppings
    public List<ToppingEntity> Toppings { get; set; } = new();
}
