namespace PizzaFactory.Core.Models.Pizzas;

public class SmallPizza : Pizza {
    public override PizzaSize Size => PizzaSize.Small;
    public override decimal BasePrice => 10.00m;
}
