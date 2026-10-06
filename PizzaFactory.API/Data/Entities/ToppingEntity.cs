namespace PizzaFactory.API.Data.Entities;

/// <summary>
/// Persistence entity for a Topping. Stored in the 'Toppings' table.
/// </summary>
public class ToppingEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;   // e.g. "Mozzarella"
    public string Type { get; set; } = string.Empty;   // e.g. "Cheese"
    public decimal Cost { get; set; } = 2.00m;

    // Foreign key back to the parent Pizza
    public Guid PizzaId { get; set; }
    public PizzaEntity Pizza { get; set; } = null!;
}
