namespace WebApi.Application.DTOs;

public sealed record CustomerDto
(
    Guid Id,
    string Name,
    string Email
);

public sealed record CreateCustomerDto
(
    string Name,
    string Email
);
