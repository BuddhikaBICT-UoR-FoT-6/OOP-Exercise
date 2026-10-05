# Pizza Factory

A full-stack web application built to demonstrate Object-Oriented Programming (OOP) concepts, specifically the **Factory Pattern** and **Builder Pattern**. It allows users to order pizzas of various sizes with multiple toppings, apply discounts, and generate detailed receipts.

## 🚀 Tech Stack

- **Backend:** .NET 10 Web API
- **Frontend:** Next.js 15 (App Router, TypeScript, Vanilla CSS)
- **Architecture:** Multi-project Solution (Core Domain, API, Console Demo)

## 📁 Project Structure

```
PizzaFactory/
├── PizzaFactory.Core/       # Shared domain library (Models, Enums, Factories, Builders)
├── PizzaFactory.API/        # ASP.NET Core 10 Web API (Controllers, Services, DTOs)
├── PizzaFactory.Console/    # Console app demonstrating both OOP & Builder patterns
└── pizza-factory-ui/        # Next.js 15 frontend application
```

## ✨ Features

1. **Pizza Customization:** 
   - Sizes: Small ($10), Medium ($12), Large ($14).
   - Toppings: Cheese, Ham, Pepperoni ($2 each).
2. **Multiple Toppings:** Customers can add multiple instances of the same topping (e.g., 2 portions of Cheese).
3. **Multi-Pizza Orders:** A customer can add multiple pizzas to a single order.
4. **Coupons:** Apply a discount code to the total order price.
5. **Detailed Receipts:** Automatically generates a comprehensive breakdown of pizzas, toppings, discounts, and total cost at checkout.
6. **Design Patterns Applied:**
   - **Factory Pattern:** Centralized creation of pizza sizes and toppings (`PizzaFactory`, `ToppingFactory`).
   - **Builder Pattern:** Fluent interface to step-by-step assemble a custom pizza (`PizzaBuilder`, `PizzaDirector`).

## 🛠️ How to Run

### 1. Backend (API)

Open a terminal in the root directory and run the API:

```bash
cd PizzaFactory.API
dotnet run
```
The API will start at `http://localhost:5000`. You can view the Swagger UI at `http://localhost:5000/swagger`.

### 2. Frontend (Next.js UI)

Open another terminal and start the Next.js development server:

```bash
cd pizza-factory-ui
npm install
npm run dev
```
The UI will be accessible at `http://localhost:3000`.

### 3. Console Demo (Optional)

If you'd prefer to see a terminal-based demonstration of the OOP and Builder patterns:

```bash
cd PizzaFactory.Console
dotnet run
```

## 📚 OOP Concepts Demonstrated

- **Abstraction:** Base `Pizza` and `Topping` classes.
- **Inheritance:** Specific pizza sizes and topping types inherit from their respective base classes.
- **Polymorphism:** Method overriding for `Cost()` and `Describe()`.
- **Encapsulation:** Protected properties and internal lists.
- **Creational Patterns:** Factory Method and Builder Patterns to decouple object creation from its representation.
