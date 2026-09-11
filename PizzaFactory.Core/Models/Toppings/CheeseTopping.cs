namespace PizzaFactory.Core.Models.Toppings;

public class CheeseTopping : Topping {
    private readonly string _name;

    public CheeseTopping(string name) => _name = name;

    public override string Name => _name;
    public override ToppingType Type => ToppingType.Cheese;
}