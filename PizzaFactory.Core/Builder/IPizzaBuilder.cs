using PizzaFactory.Core.Models;

namespace PizzaFactory.Core.Builder;

// Builder pattern is used to create a complex object step by step
// The builder interface defines methods to construct a complex object.
// This allows for flexible construction with various optional parameters.
public interface IPizzaBuilder {
    // Method to set the size of the pizza
    IPizzaBuilder WithSize(PizzaSize size);
    // Method to add a topping to the pizza
    IPizzaBuilder AddTopping(ToppingType type, string name);

    // Method to build the pizza
    Pizza Build();
    // Method to reset the builder
    void Reset();
}

