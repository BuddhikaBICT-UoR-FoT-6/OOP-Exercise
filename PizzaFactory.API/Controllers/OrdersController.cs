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
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var id = _orderService.CreateOrder(request.CustomerName);
        return CreatedAtAction(nameof(GetOrder), new { id }, new { orderId = id });
    }

    // GET api/orders/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetOrder(Guid id)
    {
        try
        {
            return Ok(_orderService.GetOrder(id));
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
    public IActionResult AddPizza(Guid id, [FromBody] AddPizzaRequest request)
    {
        try
        {
            _orderService.AddPizza(id, request);
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
    public IActionResult ApplyCoupon(Guid id, [FromBody] ApplyCouponRequest request)
    {
        try
        {
            _orderService.ApplyCoupon(id, request);
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
    public IActionResult Checkout(Guid id)
    {
        try
        {
            return Ok(_orderService.Checkout(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
