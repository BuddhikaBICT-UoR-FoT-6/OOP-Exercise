namespace PizzaFactory.Core.Models.Toppings;

public class HamTopping : Topping {
    private readonly string _name;

    public HamTopping(string name) => _name = name;

    public override string Name => _name;

    public override ToppingType Type => ToppingType.Ham;
}

