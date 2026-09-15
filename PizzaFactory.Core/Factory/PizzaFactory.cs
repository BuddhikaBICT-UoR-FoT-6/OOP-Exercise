using PizzaFactory.Core.Models;
using PizzaFactory.Core.Models.Pizzas;

namespace PizzaFactory.Core.Factory;

/*
Factory pattern is used to create objects without specifying the exact class of object to be created.
In this case, the PizzaFactory class is used to create pizzas based on the specified size.
*/
public static class PizzaFactory {
    // Static method to create a pizza based on the specified size
    public static Pizza Create(PizzaSize size) => size switch {
        PizzaSize.Small => new SmallPizza(),
        PizzaSize.Medium => new MediumPizza(),
        PizzaSize.Large => new LargePizza(),
        _ => throw new ArgumentOutOfRangeException(nameof(size), $"Unknown pizza size: {size}")
    };
}

