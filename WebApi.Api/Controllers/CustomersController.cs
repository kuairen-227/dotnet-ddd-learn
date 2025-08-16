using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs;
using WebApi.Application.Services;

namespace WebApi.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomersController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken)
    {
        var customers = await _customerService.GetCustomersAsync(cancellationToken);
        return Ok(customers);
    }

    [HttpGet("id:{guid}")]
    public async Task<IActionResult> GetCustomer(Guid guid, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetCustomerAsync(guid, cancellationToken);
        return customer is not null ? Ok(customer) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var customerDto = await _customerService.CreateCustomerAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetCustomer), new { guid = customerDto.Id }, customerDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}