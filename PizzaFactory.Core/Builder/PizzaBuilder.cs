using PizzaFactory.Core.Factory;
using PizzaFactory.Core.Models;

namespace PizzaFactory.Core.Builder;

//Implements the builder interface to construct a pizza
public class PizzaBuilder : IPizzaBuilder {
    // Private field to store the size of the pizza
    private PizzaSize _size;
    // Private list to store the toppings for the pizza
    private readonly List<(ToppingType Type, string Name)> _toppings = new();

    // Method to set the size of the pizza
    public IPizzaBuilder WithSize(PizzaSize size){
        _size = size;
        return this; // returns the builder itself, enabling method chaining
    }

    // Method to add a topping to the pizza
    public IPizzaBuilder AddTopping(ToppingType type, string name){
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Topping name cannot be empty", nameof(name));

        _toppings.Add((type, name));
        return this;
    }

    // Method to create the pizza using the builder
    public Pizza Build(){
        // Throws an exception if the pizza size has not been set
        if(_size == default)
            throw new InvalidOperationException("Pizza size must be set before calling Build()");

        // Creates the pizza using the factory
        var pizza = PizzaFactory.Create(_size);

        // Adds the toppings to the pizza using the factory
        foreach(var (type, name) in _toppings)
            pizza.AddTopping(ToppingFactory.Create(type, name));

        return pizza;
    }

    // Method to reset the builder
    public void Reset(){
        _size = default;
        _toppings.Clear();
    }
}