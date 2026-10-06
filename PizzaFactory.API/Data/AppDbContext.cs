using Microsoft.EntityFrameworkCore;
using PizzaFactory.API.Data.Entities;

namespace PizzaFactory.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    public DbSet<PizzaEntity> Pizzas => Set<PizzaEntity>();
    public DbSet<ToppingEntity> Toppings => Set<ToppingEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ── Order ────────────────────────────────────────────────────────────
        modelBuilder.Entity<OrderEntity>(e =>
        {
            e.ToTable("Orders");
            e.HasKey(o => o.Id);
            e.Property(o => o.CustomerName).HasMaxLength(100);
            e.Property(o => o.CouponCode).HasMaxLength(50);
            e.Property(o => o.DiscountPercent).HasPrecision(5, 2);
        });

        // ── Pizza ─────────────────────────────────────────────────────────────
        modelBuilder.Entity<PizzaEntity>(e =>
        {
            e.ToTable("Pizzas");
            e.HasKey(p => p.Id);
            e.Property(p => p.Size).HasMaxLength(20).IsRequired();

            // One Order → many Pizzas
            e.HasOne(p => p.Order)
             .WithMany(o => o.Pizzas)
             .HasForeignKey(p => p.OrderId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ── Topping ───────────────────────────────────────────────────────────
        modelBuilder.Entity<ToppingEntity>(e =>
        {
            e.ToTable("Toppings");
            e.HasKey(t => t.Id);
            e.Property(t => t.Name).HasMaxLength(100).IsRequired();
            e.Property(t => t.Type).HasMaxLength(20).IsRequired();
            e.Property(t => t.Cost).HasPrecision(8, 2);

            // One Pizza → many Toppings
            e.HasOne(t => t.Pizza)
             .WithMany(p => p.Toppings)
             .HasForeignKey(t => t.PizzaId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
