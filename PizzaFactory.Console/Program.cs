using PizzaFactory.Core.Builder;
using PizzaFactory.Core.Factory;
using PizzaFactory.Core.Models;

// Sets the output encoding to UTF-8 to support special characters
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Prints a header for the console application
Console.WriteLine("════════════════════════════════════════");
Console.WriteLine("  PIZZA FACTORY — OOP & BUILDER DEMO  ");
Console.WriteLine("════════════════════════════════════════\n");

// ── PART A: Classic OOP + Factory Pattern ─────────────────────────────────
// This section demonstrates the classic object-oriented approach to creating and managing orders and pizzas.
Console.WriteLine("── PART A: Classic OOP + Factory Pattern ──\n");

// Creates a new order with the customer name "Alice".
var orderA = new Order("Alice");

// Pizza 1: Large with 2 cheese and 1 ham (requirement 4 demo)
var pizza1 = PizzaFactory.Create(PizzaSize.Large);
pizza1.AddTopping(ToppingFactory.Create(ToppingType.Cheese, "Mozzarella"));
pizza1.AddTopping(ToppingFactory.Create(ToppingType.Cheese, "Cheddar"));
pizza1.AddTopping(ToppingFactory.Create(ToppingType.Ham, "Smoked Ham"));
orderA.AddPizza(pizza1);

// Pizza 2: Small with pepperoni
var pizza2 = PizzaFactory.Create(PizzaSize.Small);
pizza2.AddTopping(ToppingFactory.Create(ToppingType.Pepperoni, "Classic Pepperoni"));
orderA.AddPizza(pizza2);

// Apply coupon
orderA.ApplyCoupon("SAVE10", 10);

Console.WriteLine("── Pizza Descriptions ──");
Console.WriteLine(pizza1.Describe());
Console.WriteLine(pizza2.Describe());

Console.WriteLine("── Full Receipt ──");
Console.WriteLine(orderA.PrintReceipt());

// ── PART B: Builder Pattern ────────────────────────────────────────────────
Console.WriteLine("\n── PART B: Builder Pattern ──\n");

var orderB = new Order("Bob");
var builder = new PizzaBuilder();
var director = new PizzaDirector(builder);

// Use director for named recipes
var meatLover = director.BuildMeatLover(PizzaSize.Large);
var margherita = director.BuildMargherita(PizzaSize.Medium);

orderB.AddPizza(meatLover);
orderB.AddPizza(margherita);

// Custom pizza using fluent builder directly
var customPizza = new PizzaBuilder()
    .WithSize(PizzaSize.Small)
    .AddTopping(ToppingType.Cheese, "Mozzarella")
    .AddTopping(ToppingType.Cheese, "Parmesan")
    .AddTopping(ToppingType.Cheese, "Gouda")
    .Build();

orderB.AddPizza(customPizza);
orderB.ApplyCoupon("BUILDER20", 20);

Console.WriteLine("── Full Receipt ──");
Console.WriteLine(orderB.PrintReceipt());
