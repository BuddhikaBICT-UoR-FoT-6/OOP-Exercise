namespace PizzaFactory.Core.Models;

public abstract class Topping{
    public abstract string Name { get; }
    public abstract ToppingType Type { get; }
    public decimal cost => 2.00m;

    public string Describe() => $"{Name} ({Type}) - {cost:C}";
}