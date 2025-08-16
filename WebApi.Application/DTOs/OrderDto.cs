namespace WebApi.Application.DTOs;

public sealed record OrderDto
(
    Guid Id,
    Guid CustomerId,
    DateTime OrderDate,
    string Currency,
    decimal TotalAmount
);

public sealed record OrderItemDto
(
    Guid Id,
    Guid ProductId,
    decimal UnitPrice,
    int Quantity
);

public sealed record CreateOrderDto
(
    Guid CustomerId,
    string Currency,
    IReadOnlyList<CreateOrderItemDto> Items
);

public sealed record CreateOrderItemDto
(
    Guid ProductId,
    decimal UnitPrice,
    int Quantity
);