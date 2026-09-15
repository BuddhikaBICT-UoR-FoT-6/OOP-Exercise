namespace PizzaFactory.Core.Models.Pizzas;

public class LargePizza : Pizza
{
    public override PizzaSize Size => PizzaSize.Large;
    public override decimal BasePrice => 14.00m;
}
