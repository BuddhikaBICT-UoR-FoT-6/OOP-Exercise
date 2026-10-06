namespace PizzaFactory.API.Data.Entities;

/// <summary>
/// Persistence entity for an Order. Stored in the 'Orders' table.
/// Separate from the domain Order model so EF concerns don't bleed into Core.
/// </summary>
public class OrderEntity
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CouponCode { get; set; }
    public decimal DiscountPercent { get; set; }

    // Navigation property — EF will load related pizzas via JOIN
    public List<PizzaEntity> Pizzas { get; set; } = new();
}
