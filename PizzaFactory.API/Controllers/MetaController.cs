using Microsoft.AspNetCore.Mvc;
using PizzaFactory.Core.Models;

namespace PizzaFactory.API.Controllers;

// MetaController is used to get the meta information about the pizza factory. 
// This includes the available pizza sizes, topping types, and presets.
[ApiController]
[Route("api/meta")]
public class MetaController : ControllerBase
{
    // GET api/meta/sizes - Get all available pizza sizes
    [HttpGet("sizes")]
    public IActionResult GetSizes() =>
        Ok(Enum.GetNames<PizzaSize>()); // Returns an array 
        // of all available pizza sizes.

    // GET api/meta/toppings - Get all available topping types
    [HttpGet("toppings")]
    public IActionResult GetToppingTypes() =>
        Ok(Enum.GetNames<ToppingType>()); // Returns an array 
        // of all available topping types.

    // GET api/meta/presets - Get all available preset pizzas
    [HttpGet("presets")]
    public IActionResult GetPresets()
    {
        //  An array of preset pizzas
        var presets = new[]
        {
            new
            {
                name = "Meat Lover",
                toppings = new[]
                {
                    new { type = "Ham",       name = "Smoked Ham" },
                    new { type = "Pepperoni", name = "Classic Pepperoni" },
                    new { type = "Pepperoni", name = "Spicy Pepperoni" }
                }
            },
            new
            {
                name = "Margherita",
                toppings = new[]
                {
                    new { type = "Cheese", name = "Mozzarella" },
                    new { type = "Cheese", name = "Parmesan" }
                }
            },
            new
            {
                name = "Four Cheese",
                toppings = new[]
                {
                    new { type = "Cheese", name = "Mozzarella" },
                    new { type = "Cheese", name = "Cheddar" },
                    new { type = "Cheese", name = "Parmesan" },
                    new { type = "Cheese", name = "Gouda" }
                }
            }
        };

        return Ok(presets); // Returns an array of preset pizzas
    }
}
