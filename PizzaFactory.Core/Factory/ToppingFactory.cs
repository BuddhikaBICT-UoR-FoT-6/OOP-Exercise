using PizzaFactory.Core.Models;
using PizzaFactory.Core.Models.Toppings;

namespace PizzaFactory.Core.Factory;

/*
Factory pattern is used to create objects without specifying the exact class of object to be created.
In this case, the ToppingFactory class is used to create toppings based on the specified type.
*/
public static class ToppingFactory{
    // Static method to create a topping based on the specified type
    public static Topping Create(ToppingType type, string name) => type switch {
        ToppingType.Cheese => new CheeseTopping(name),
        ToppingType.Ham => new HamTopping(name),
        ToppingType.Pepperoni => new PepperoniTopping(name),
        _=> throw new ArgumentOutOfRangeException(nameof(type), $"Unknown topping type: {type}");
    };
}
