namespace WebApi.Application.DTOs;

public sealed record OrderDto
(
    Guid Id,
    Guid CustomerId,
    DateTime OrderDate,
    decimal TotalAmount
);

public sealed record OrderItemDto
(
    Guid Id,
    Guid ProductId,
    int Quantity
);

public sealed record CreateOrderDto
(
    Guid CustomerId,
    IReadOnlyList<CreateOrderItemDto> Items
);

public sealed record CreateOrderItemDto
(
    Guid ProductId,
    int Quantity
);