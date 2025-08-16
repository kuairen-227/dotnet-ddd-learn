using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs;
using WebApi.Application.Services;

namespace WebApi.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _productService.GetProductsAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("id:{guid}")]
    public async Task<IActionResult> GetProduct(Guid guid, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductAsync(guid, cancellationToken);
        return product is not null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var productDto = await _productService.CreateProductAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetProduct), new { guid = productDto.Id }, productDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}