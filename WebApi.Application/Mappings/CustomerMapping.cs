using WebApi.Application.DTOs;
using WebApi.Domain.Entities;

namespace WebApi.Application.Mappings;

public static class CustomerMapping
{
    public static CustomerDto ToDto(this Customer customer) =>
        new CustomerDto(
            customer.Id,
            customer.Name,
            customer.Email.Value
        );
}