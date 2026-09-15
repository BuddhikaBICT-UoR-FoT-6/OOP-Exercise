using System.Text;

namespace PizzaFactory.Core.Models;

public abstract class Pizza
{
    private readonly List<Topping> _toppings = new();

    public abstract PizzaSize Size { get; }
    public abstract decimal BasePrice { get; }

    public IReadOnlyList<Topping> Toppings => _toppings.AsReadOnly();

    public void AddTopping(Topping topping)
    {
        ArgumentNullException.ThrowIfNull(topping);
        _toppings.Add(topping);
    }

    public decimal Cost() => BasePrice + (_toppings.Count * 2.00m);

    public string Describe()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"┌─ {Size} Pizza ─────────────────────");
        sb.AppendLine($"│  Base Price : ${BasePrice:F2}");
        sb.AppendLine($"│  Toppings   : {_toppings.Count}");

        foreach (var t in _toppings)
            sb.AppendLine($"│    • {t.Describe()}");

        sb.AppendLine($"│  Total Cost : ${Cost():F2}");
        sb.AppendLine("└────────────────────────────────────");
        return sb.ToString();
    }
}
