using WebApi.Application.DTOs;
using WebApi.Application.Mappings;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.Services;

public class CustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   public async Task<IEnumerable<CustomerDto>> GetCustomersAsync(CancellationToken cancellationToken)
   {
       var customers = await _unitOfWork.Customers.GetAllAsync(cancellationToken);
       return customers.Select(o => o.ToDto());
   }

   public async Task<CustomerDto?> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
   {
       var customer = await _unitOfWork.Customers.GetByIdAsync(id, cancellationToken);
       return customer?.ToDto();
   }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto, CancellationToken cancellationToken)
    {
        var customer = new Customer(Guid.NewGuid(), dto.Name, new Email(dto.Email));

        _unitOfWork.Customers.Add(customer);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return customer.ToDto();
    }
}