namespace PizzaFactory.Core.Models.Toppings;

public class PepperoniTopping : Topping {
    private readonly string _name;

    public PepperoniTopping(string name) => _name = name;

    public override string Name => _name;
    public override ToppingType Type => ToppingType.Pepperoni;
}
