using Microsoft.AspNetCore.Mvc;
using PizzaFactory.API.DTOs.Requests;
using PizzaFactory.API.DTOs.Responses;
using PizzaFactory.API.Services;

namespace PizzaFactory.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // POST api/orders
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var id = await _orderService.CreateOrderAsync(request.CustomerName);
        return CreatedAtAction(nameof(GetOrder), new { id }, new { orderId = id });
    }

    // GET api/orders/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        try
        {
            return Ok(await _orderService.GetOrderAsync(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST api/orders/{id}/pizzas
    [HttpPost("{id:guid}/pizzas")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPizza(Guid id, [FromBody] AddPizzaRequest request)
    {
        try
        {
            await _orderService.AddPizzaAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST api/orders/{id}/coupon
    [HttpPost("{id:guid}/coupon")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApplyCoupon(Guid id, [FromBody] ApplyCouponRequest request)
    {
        try
        {
            await _orderService.ApplyCouponAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // GET api/orders/{id}/checkout
    [HttpGet("{id:guid}/checkout")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Checkout(Guid id)
    {
        try
        {
            return Ok(await _orderService.CheckoutAsync(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
