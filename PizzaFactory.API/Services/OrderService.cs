using Microsoft.EntityFrameworkCore;
using PizzaFactory.API.Data;
using PizzaFactory.API.Data.Entities;
using PizzaFactory.API.DTOs.Requests;
using PizzaFactory.API.DTOs.Responses;
using PizzaFactory.Core.Builder;
using PizzaFactory.Core.Models;
using PF = PizzaFactory.Core.Factory.PizzaFactory;
using TF = PizzaFactory.Core.Factory.ToppingFactory;

namespace PizzaFactory.API.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    // ── Create Order ──────────────────────────────────────────────────────────
    public async Task<Guid> CreateOrderAsync(string? customerName)
    {
        var order = new OrderEntity
        {
            Id = Guid.NewGuid(),
            CustomerName = customerName,
            DiscountPercent = 0
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return order.Id;
    }

    // ── Add Pizza ─────────────────────────────────────────────────────────────
    public async Task AddPizzaAsync(Guid orderId, AddPizzaRequest request)
    {
        var order = await GetEntityOrThrowAsync(orderId);

        // Use PizzaBuilder to validate the request (domain logic stays in Core)
        var builder = new PizzaBuilder();
        builder.WithSize(request.Size);
        foreach (var t in request.Toppings)
            builder.AddTopping(t.Type, t.Name);
        builder.Build(); // validates — throws if invalid

        // Persist as entity
        var pizzaEntity = new PizzaEntity
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Size = request.Size.ToString(),
            Toppings = request.Toppings.Select(t => new ToppingEntity
            {
                Id = Guid.NewGuid(),
                Name = t.Name,
                Type = t.Type.ToString(),
                Cost = 2.00m
            }).ToList()
        };

        _db.Pizzas.Add(pizzaEntity);
        await _db.SaveChangesAsync();
    }

    // ── Apply Coupon ──────────────────────────────────────────────────────────
    public async Task ApplyCouponAsync(Guid orderId, ApplyCouponRequest request)
    {
        var order = await GetEntityOrThrowAsync(orderId);
        order.CouponCode = request.CouponCode;
        order.DiscountPercent = request.DiscountPercent;
        await _db.SaveChangesAsync();
    }

    // ── Get Order ─────────────────────────────────────────────────────────────
    public async Task<OrderResponse> GetOrderAsync(Guid orderId)
    {
        var entity = await GetEntityWithDetailsAsync(orderId);
        return MapToResponse(orderId, entity);
    }

    // ── Checkout ──────────────────────────────────────────────────────────────
    public async Task<OrderResponse> CheckoutAsync(Guid orderId)
    {
        var entity = await GetEntityWithDetailsAsync(orderId);
        return MapToResponse(orderId, entity);
    }

    // ── Private Helpers ───────────────────────────────────────────────────────

    private async Task<OrderEntity> GetEntityOrThrowAsync(Guid orderId)
    {
        var order = await _db.Orders.FindAsync(orderId);
        if (order is null)
            throw new KeyNotFoundException($"Order {orderId} not found.");
        return order;
    }

    private async Task<OrderEntity> GetEntityWithDetailsAsync(Guid orderId)
    {
        var order = await _db.Orders
            .Include(o => o.Pizzas)
                .ThenInclude(p => p.Toppings)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null)
            throw new KeyNotFoundException($"Order {orderId} not found.");
        return order;
    }

    /// <summary>
    /// Reconstructs domain Order from entity data so we can reuse
    /// domain logic (Cost(), PrintReceipt()) without duplicating it.
    /// </summary>
    private static Order ReconstructDomainOrder(OrderEntity entity)
    {
        var domainOrder = new Order(entity.CustomerName);

        if (entity.CouponCode is not null)
            domainOrder.ApplyCoupon(entity.CouponCode, entity.DiscountPercent);

        foreach (var pizzaEntity in entity.Pizzas)
        {
            var size = Enum.Parse<PizzaSize>(pizzaEntity.Size);
            var builder = new PizzaBuilder();
            builder.WithSize(size);

            foreach (var t in pizzaEntity.Toppings)
                builder.AddTopping(Enum.Parse<ToppingType>(t.Type), t.Name);

            domainOrder.AddPizza(builder.Build());
        }

        return domainOrder;
    }

    private static OrderResponse MapToResponse(Guid id, OrderEntity entity)
    {
        // Reconstruct domain object to reuse Cost() and PrintReceipt()
        var domainOrder = ReconstructDomainOrder(entity);

        var pizzaResponses = entity.Pizzas.Zip(domainOrder.Pizzas, (e, d) =>
            new PizzaResponse(
                Size:         e.Size,
                Toppings:     e.Toppings.Select(t => new ToppingResponse(t.Name, t.Type, t.Cost)).ToList(),
                BasePrice:    d.BasePrice,
                ToppingsCost: e.Toppings.Count * 2.00m,
                TotalCost:    d.Cost(),
                Description:  d.Describe()
            )).ToList();

        return new OrderResponse(
            OrderId:         id,
            CustomerName:    entity.CustomerName,
            Pizzas:          pizzaResponses,
            Subtotal:        domainOrder.Subtotal(),
            CouponCode:      entity.CouponCode,
            DiscountPercent: entity.DiscountPercent,
            Discount:        domainOrder.Discount(),
            Total:           domainOrder.Total(),
            Receipt:         domainOrder.PrintReceipt()
        );
    }
}
