using Moq;
using WebApi.Application.DTOs;
using WebApi.Application.Mappings;
using WebApi.Application.Services;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using WebApi.Tests.Builders;

namespace WebApi.Tests.UnitTests.Application.Services;

public class CustomerServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ICustomerRepository> _customerMock;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _customerMock = new Mock<ICustomerRepository>();

        _unitOfWork.Setup(u => u.Customers).Returns(_customerMock.Object);
        _service = new CustomerService(_unitOfWork.Object);
    }

    [Fact]
    public async Task 正常系_GetCustomersAsync_全件取得できる()
    {
        // Given
        var customers = new List<Customer>
        {
            CustomerBuilder.New().WithName("テスト 太郎").WithEmail("test1@example.com").Build(),
            CustomerBuilder.New().WithName("テスト 次郎").WithEmail("test2@example.com").Build()
        };
        _customerMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers);

        // When
        var result = await _service.GetCustomersAsync(CancellationToken.None);

        // Then
        var customersList = customers.Select(c => c.ToDto()).ToList();
        Assert.Equal(customersList, result);
    }

    [Fact]
    public async Task 正常系_GetCustomerAsync_顧客が存在する場合_顧客情報が取得できる()
    {
        // Given
        var customer = CustomerBuilder.New().Build();
        _customerMock.Setup(r => r.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        // When
        var result = await _service.GetCustomerAsync(customer.Id, CancellationToken.None);

        // Then
        Assert.NotNull(result);
        Assert.Equal(customer.ToDto(), result);
    }

    [Fact]
    public async Task 正常系_GetCustomerAsync_顧客が存在しない場合_NULLが返る()
    {
        // Given
        _customerMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        // When
        var result = await _service.GetCustomerAsync(Guid.NewGuid(), CancellationToken.None);

        // Then
        Assert.Null(result);
    }

    [Fact]
    public async Task 正常系_CreateCustomerAsync_顧客が作成できる()
    {
        // Given
        var dto = new CreateCustomerDto("テスト 太郎", "test@example.com");

        // When
        _customerMock.Setup(r => r.Add(It.IsAny<Customer>()));
        _unitOfWork.Setup(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Then
        var result = await _service.CreateCustomerAsync(dto, CancellationToken.None);
        Assert.NotNull(result);
        _customerMock.Verify(r => r.Add(It.IsAny<Customer>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}