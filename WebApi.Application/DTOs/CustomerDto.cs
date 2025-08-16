namespace WebApi.Application.DTOs;

public sealed record CustomerDto
(
    Guid Id,
    string Name,
    string Email
);