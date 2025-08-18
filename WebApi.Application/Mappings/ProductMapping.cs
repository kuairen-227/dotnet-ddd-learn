using WebApi.Application.DTOs;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.Mappings;

public static class ProductMapping
{
    public static ProductDto ToDto(this Product product) =>
        new ProductDto(
            product.Id,
            product.Name.Value,
            product.Price.Value
        );
}

public static class CreateProductMapping
{
    public static Product ToEntity(this CreateProductDto createProductDto)
    {
        return new Product(
            Guid.NewGuid(),
            new ProductName(createProductDto.Name),
            new Price(createProductDto.Price)
        );
    }
}