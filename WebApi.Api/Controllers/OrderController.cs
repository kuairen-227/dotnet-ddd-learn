using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs;
using WebApi.Application.Services;

namespace WebApi.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetOrdersAsync(cancellationToken);
        return Ok(orders);
    }

    [HttpGet("id:{guid}")]
    public async Task<IActionResult> GetOrder(Guid guid, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderAsync(guid, cancellationToken);
        return order is not null ? Ok(order) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var orderDto = await _orderService.CreateOrderAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetOrder), new { guid = orderDto.Id }, orderDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}