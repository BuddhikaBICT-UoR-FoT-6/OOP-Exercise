# Changelog

All notable changes to the **Pizza Factory** project are documented here, organised by date.

---

## [2026-10-05] — Frontend Completion

The frontend application reached its final form with the addition of the checkout flow. A dedicated **Order page** was introduced to display a completed order when the customer navigates to checkout. The **home page** was improved with a cleaner layout, better state management, and a smoother transition between the name-entry screen and the main ordering view. A polished **Receipt component** was also built, rendering the full order breakdown — including pizzas, toppings, pricing, coupon details, and total — in a styled paper-ticket format with a print button.

---

## [2026-10-02] — Core Frontend Components Built

The bulk of the interactive UI was developed during this phase. Each part of the pizza ordering experience was broken down into focused, reusable React components:

- A **Size Selector** component allows customers to choose between Small, Medium, and Large pizzas displayed as clickable cards showing the base price.
- A **Topping Selector** was built to support adding multiple toppings of any type — including multiple instances of the same topping — via a suggestion list and a custom name input field.
- A **Preset Selector** fetches named recipes (e.g., Meat Lover, Margherita, Four Cheese) from the backend's `/api/meta/presets` endpoint and auto-fills the builder form on selection.
- The **Pizza Builder Form** composes all three selectors into a single unified form that submits a pizza to the current order.
- An **Order Summary** panel displays all added pizzas in real-time, showing per-pizza cost breakdowns and the running total.
- A **Coupon Input** form lets the customer apply a discount code and percentage, which is reflected immediately in the summary.

The **global CSS** design system was also finalised, establishing the dark colour palette, gradient accents, card styles, and component tokens used throughout the UI. The **API client module** (`lib/api.ts`) was completed, centralising all `fetch` calls to the .NET backend with consistent error handling.

---

## [2026-09-28] — API Wiring & Frontend Foundation

The backend was fully connected and made ready for frontend consumption. The **Program.cs** entry point was configured with dependency injection, CORS policy (to allow requests from the Next.js dev server), and Swagger documentation. Application settings were updated to bind the API to `http://localhost:5000`.

Two API controllers were created:
- The **Orders Controller** exposes endpoints to create an order, add pizzas, apply coupons, retrieve an order, and trigger checkout.
- The **Meta Controller** provides reference data endpoints for pizza sizes, topping types, and named preset recipes — allowing the frontend to be fully data-driven without hardcoded values.

A **Console Demo** project was also completed, showcasing both Part A (Classic OOP with the Factory Pattern) and Part B (Builder Pattern) in a terminal environment, printing formatted receipts directly to the console.

The **TypeScript types module** (`lib/types.ts`) was created on the frontend side, mirroring all API request and response contracts to provide end-to-end type safety.

---

## [2026-09-25] — Service Layer & DTOs

The **Order Service** was implemented as the core business logic layer of the API. It manages all active orders in memory, uses the `PizzaBuilder` internally to construct pizzas from incoming API requests, and maps domain objects to serializable response DTOs. The service implements a clean `IOrderService` interface, keeping the controllers thin and the logic testable.

All **Data Transfer Objects (DTOs)** were finalised — covering request payloads (create order, add pizza, apply coupon) and response shapes (topping, pizza, and full order responses) that form the API's public contract.

---

## [2026-09-15] — Domain Models, Factory & Builder Patterns

This was the largest development phase, establishing the entire domain layer of the application:

The **core Pizza model hierarchy** was built using abstract base classes and inheritance. An abstract `Pizza` class defines the shared structure — size, base price, a list of toppings, `Cost()` calculation, and `Describe()` output — while `SmallPizza`, `MediumPizza`, and `LargePizza` override the size and base price properties ($10, $12, and $14 respectively). The **Order model** was created to hold a customer's collection of pizzas, manage coupon application, calculate subtotals and discounts, and produce a fully formatted receipt.

The **Factory Pattern** was implemented via two static factory classes: `PizzaFactory`, which instantiates the correct pizza size subclass from a `PizzaSize` enum, and `ToppingFactory`, which creates the appropriate topping subclass from a `ToppingType` enum and a name string. This centralises object creation and eliminates scattered `new` calls across the codebase.

The **Builder Pattern** was then layered on top. The `IPizzaBuilder` interface defines the fluent construction contract. `PizzaBuilder` implements it with a step-by-step approach — setting the size and adding toppings one by one before calling `Build()` to produce the final pizza. A `PizzaDirector` class was also added to encapsulate pre-configured recipes, allowing named pizzas like "Meat Lover" or "Four Cheese" to be built in a single method call.

---

## [2026-09-11] — Topping Models

The concrete topping classes were created: `CheeseTopping`, `HamTopping`, and `PepperoniTopping`. Each inherits from the abstract `Topping` base class (which enforces a `Name`, `Type`, and fixed `Cost` of $2.00) and accepts a name string in its constructor, allowing multiple varieties of the same topping type to be distinguished (e.g., "Mozzarella" vs. "Cheddar" are both Cheese toppings).

---

## [2026-09-10] — Project Initialisation

The project was scaffolded from the ground up. The .NET 10 solution was created with three projects: a `PizzaFactory.Core` class library for shared domain logic, a `PizzaFactory.API` Web API project, and a `PizzaFactory.Console` demo project. The Next.js 15 frontend was also initialised using the App Router with TypeScript and a custom CSS setup.

The foundational domain enums were created: `PizzaSize` (Small, Medium, Large) and `ToppingType` (Cheese, Ham, Pepperoni), and the abstract `Topping` base class was defined to establish the common structure all concrete toppings must implement.
