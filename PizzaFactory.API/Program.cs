using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PizzaFactory.API.Data;
using PizzaFactory.API.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Services ──────────────────────────────────────────────────────────────────

// JsonStringEnumConverter allows the API to accept enum values as strings
// (e.g. "Small", "Cheese") instead of integers.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Pizza Factory API", Version = "v1" });
});

// ── MySQL via Entity Framework Core ───────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Scoped (not Singleton) because DbContext is scoped per request
builder.Services.AddScoped<IOrderService, OrderService>();

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ── App / Middleware ──────────────────────────────────────────────────────────
var app = builder.Build();

// Auto-apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Factory API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowNextJs");
app.MapControllers();

app.Run();
