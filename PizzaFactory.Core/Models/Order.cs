using System.Text;

namespace PizzaFactory.Core.Models;

// Represents a single pizza order
public class Order{
    // Private list to store pizzas in the order
    private readonly List<Pizza> _pizzas = new();
    
    // Publicly accessible read-only list of pizzas in the order
    // Publicly accessible read-only list of pizzas in the order
    public IReadOnlyList<Pizza> Pizzas => _pizzas.AsReadOnly();

    // Public property to store the customer's name
    public string? CustomerName {get; init;}
    
    // Public property to store the coupon code (settable only within the class)
    public string? CouponCode {get; private set;}
    
    // Public property to store the discount percentage (settable only within the class)
    public decimal DiscountPercent {get; private set;}

    // Constructor for creating an order with an optional customer name
    public Order(string? customerName = null){
        CustomerName = customerName;
    }

    // Method to add a pizza to the order
    public void AddPizza(Pizza pizza){
        ArgumentNullException.ThrowIfNull(pizza);
        _pizzas.Add(pizza);
    }

    // Method to apply a coupon to the order with validation
    public void ApplyCoupon(string code, decimal discountPercent){
        if(string.IsNullOrWhiteSpace(code)) 
            throw new ArgumentException("Coupon code cannot be empty", nameof(code));

        if(discountPercent is < 0 or > 100) 
            throw new ArgumentOutOfRangeException(nameof(discountPercent), "Discount must be 0-100");

        // Updates the coupon code and discount percentage for the order
        CouponCode = code;
        DiscountPercent = discountPercent;
    }

    // Method to calculate the subtotal (total cost of all pizzas before discount)
    public decimal Subtotal() => _pizzas.Sum(p => p.Cost());
    
    // Method to calculate the discount amount
    public decimal Discount() => Math.Round(Subtotal() * DiscountPercent / 100, 2);
    
    // Method to calculate the final total (subtotal minus discount)
    public decimal Total() => Subtotal() - Discount();

    // Method to print a formatted receipt of the order
    public string PrintReceipt()
    {
        // Creates a StringBuilder to efficiently build the receipt string
        var sb = new StringBuilder();
        // Appends a decorative header to the receipt
        sb.AppendLine("╔══════════════════════════════════════╗");
        sb.AppendLine("║         🍕  PIZZA FACTORY  🍕         ║");
        sb.AppendLine("╚══════════════════════════════════════╝");
        // Appends the customer's name if it exists 
        if (!string.IsNullOrEmpty(CustomerName))
            sb.AppendLine($"  Customer : {CustomerName}");
        sb.AppendLine($"  Pizzas   : {_pizzas.Count}");
        sb.AppendLine();
        // Iterates through each pizza in the order and appends its description
        for (int i = 0; i < _pizzas.Count; i++)
        {
            sb.AppendLine($"  Pizza #{i + 1}");
            sb.Append(_pizzas[i].Describe());
            sb.AppendLine();
        }
        // Appends a separator line before the summary
        sb.AppendLine("──────────────────────────────────────");
        // Appends the subtotal (total cost of all pizzas before discount)
        sb.AppendLine($"  Subtotal  : ${Subtotal():F2}");
        // If a coupon has been applied, appends the coupon details and discount amount
        if (CouponCode is not null)
        {
            sb.AppendLine($"  Coupon    : {CouponCode} (-{DiscountPercent}%)");
            sb.AppendLine($"  Discount  : -${Discount():F2}");
        }
        sb.AppendLine($"  TOTAL     : ${Total():F2}");
        sb.AppendLine("══════════════════════════════════════");
        return sb.ToString(); // Returns the formatted receipt
    }

}

