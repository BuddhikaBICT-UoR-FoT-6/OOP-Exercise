namespace PizzaFactory.Core.Models;

public abstract class Topping{
    public abstract string Name { get; }
    public abstract ToppingType Type { get; }
    public decimal Cost => 2.00m;

    public string Describe() => $"{Name} ({Type}) - {Cost:C}";
}