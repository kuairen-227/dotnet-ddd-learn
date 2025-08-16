namespace WebApi.Application.DTOs;

public sealed record ProductDto
(
    Guid Id,
    string Name,
    int Price
);

public sealed record CreateProductDto
(
    string Name,
    int Price
);

public sealed record CreateProductItemDto
(
    Guid ProductId,
    string Name,
    int Price
);