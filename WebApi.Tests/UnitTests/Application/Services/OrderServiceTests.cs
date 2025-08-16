using Moq;
using WebApi.Application.DTOs;
using WebApi.Application.Services;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Application.Services;

public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IOrderRepository> _orderMock;
    private readonly Mock<ICustomerRepository> _customerMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _orderMock = new Mock<IOrderRepository>();
        _customerMock = new Mock<ICustomerRepository>();

        _unitOfWork.Setup(u => u.Orders).Returns(_orderMock.Object);
        _unitOfWork.Setup(u => u.Customers).Returns(_customerMock.Object);
        _service = new OrderService(_unitOfWork.Object);
    }

    [Fact]
    public async Task 正常系_GetOrdersAsync_全件取得できる()
    {
        // Given
        var orders = new List<Order>
        {
            new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "USD"),
            new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "JPY")
        };
        _orderMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        // When
        var result = await _service.GetOrdersAsync(CancellationToken.None);

        // Then
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task 正常系_GetOrderAsync_注文が存在する場合_注文情報が取得できる()
    {
        // Given
        var orderId = Guid.NewGuid();
        var order = new Order(orderId, Guid.NewGuid(), DateTime.UtcNow, "JPY");
        _orderMock.Setup(r => r.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // When
        var result = await _service.GetOrderAsync(orderId, CancellationToken.None);

        // Then
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task 正常系_GetOrderAsync_顧客が存在しない場合_NULLが返る()
    {
        // Given
        _orderMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        // When
        var result = await _service.GetOrderAsync(Guid.NewGuid(), CancellationToken.None);

        // Then
        Assert.Null(result);
    }

    [Fact]
    public async Task 正常系_CreateOrderAsync_顧客が存在する場合_注文が作成できる()
    {
        // Given
        var customerId = Guid.NewGuid();
        var customer = new Customer(customerId, "テスト 太郎", new Email("test@example.com"));
        _customerMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        // When
        var dto = new CreateOrderDto
        (
            customerId,
            "JPY",
            new List<CreateOrderItemDto>
            {
                new(Guid.NewGuid(), 100, 2),
                new(Guid.NewGuid(), 200, 1)
            }
        );

        _orderMock.Setup(r => r.Add(It.IsAny<Order>()));
        _unitOfWork.Setup(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Then
        var result = await _service.CreateOrderAsync(dto, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal(dto.Items.Sum(i => i.UnitPrice * i.Quantity), result.TotalAmount);
        _orderMock.Verify(r => r.Add(It.IsAny<Order>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task 異常系_CreateOrderAsync_顧客が存在しない場合_InvalidOperationException()
    {
        // Given
        var customerId = Guid.NewGuid();
        _customerMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        // When
        var dto = new CreateOrderDto(customerId, "JPY", new List<CreateOrderItemDto>());

        // Then
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CreateOrderAsync(dto, CancellationToken.None));
    }
}