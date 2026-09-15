namespace PizzaFactory.Core.Models.Pizzas;

public class MediumPizza : Pizza
{
    public override PizzaSize Size => PizzaSize.Medium;
    public override decimal BasePrice => 12.00m;
}
