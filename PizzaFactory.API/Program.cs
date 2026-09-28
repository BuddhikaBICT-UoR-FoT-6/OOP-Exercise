using PizzaFactory.API.Services;

var builder = WebApplication.CreateBuilder(args); // Creates a WebApplicationBuilder 
// instance which is the entry point of the application.

// Add services to the container.
builder.Services.AddControllers(); // Adds controller services to the 
// dependency injection container.
builder.Services.AddEndpointsApiExplorer(); // Adds endpoint API explorer services 
// to the dependency injection container.
builder.Services.AddSwaggerGen(c => // Adds Swagger generation services to the 
// dependency injection container.
{
    // basic configuration for Swagger 
    c.SwaggerDoc("v1", new() { Title = "Pizza Factory API", Version = "v1" });
});

// singleton tells the application to use only one instance of the OrderService
// throughout the application's lifetime. This is a common pattern for services
// that manage state or resources.
builder.Services.AddSingleton<IOrderService, OrderService>();

// adding a CORS policy to allow the Next.js application to access the API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Builds the WebApplication instance and configures the middleware pipeline.
var app = builder.Build();

// enable the Swagger UI at the /swagger endpoint.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Factory API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowNextJs"); // Enables the CORS policy
app.MapControllers(); // Maps controller routes

app.Run();
