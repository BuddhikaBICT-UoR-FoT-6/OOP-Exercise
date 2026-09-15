using PizzaFactory.Core.Models;

namespace PizzaFactory.Core.Builder;

// what this class does is it will construct the pizza using the builder
// based on the type of pizza we want to build
public class PizzaDirector {
    private readonly IPizzaBuilder _builder; // dependency injecting the builder 

    // constructor dependency injection - what this does is it will allow us to 
    // create an instance of the PizzaDirector class with the builder we want to use
    public PizzaDirector(IPizzaBuilder builder){
        _builder = builder;
    }

    // this is the method that will be used to build the meat lover pizza
    public Pizza BuildMeatLover(PizzaSize size){
        _builder.Reset(); // reset the builder before using it
        // returning the builder with the size and toppings set
        return _builder
            .WithSize(size)
            .AddTopping(ToppingType.Ham, "Smoked Ham")
            .AddTopping(ToppingType.Pepperoni, "Classic Pepperoni")
            .AddTopping(ToppingType.Pepperoni, "Spicy Pepperoni")
            .Build();
    }

    public Pizza BuildMargherita(PizzaSize size)
    {
        _builder.Reset();
        return _builder
            .WithSize(size)
            .AddTopping(ToppingType.Cheese, "Mozzarella")
            .AddTopping(ToppingType.Cheese, "Parmesan")
            .Build();
    }

    public Pizza BuildFourCheese(PizzaSize size)
    {
        _builder.Reset();
        return _builder
            .WithSize(size)
            .AddTopping(ToppingType.Cheese, "Mozzarella")
            .AddTopping(ToppingType.Cheese, "Cheddar")
            .AddTopping(ToppingType.Cheese, "Parmesan")
            .AddTopping(ToppingType.Cheese, "Gouda")
            .Build();
    } 

}
