using PizzaFactory.Core.Models;

namespace PizzaFactory.API.DTOs.Requests;


// allow to send the pizza size and toppings to the AddPizza method
public record ToppingRequest(ToppingType Type, string Name);

// allow to send the pizza size and toppings to the AddPizza method
public record AddPizzaRequest(PizzaSize Size, List<ToppingRequest> Toppings);
