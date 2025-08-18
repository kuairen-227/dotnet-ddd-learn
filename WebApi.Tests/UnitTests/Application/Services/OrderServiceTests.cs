using Moq;
using WebApi.Application.DTOs;
using WebApi.Application.Services;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using WebApi.Domain.ValueObjects;
using WebApi.Tests.Builders;

namespace WebApi.Tests.UnitTests.Application.Services;

public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IOrderRepository> _orderMock;
    private readonly Mock<ICustomerRepository> _customerMock;
    private readonly Mock<IProductRepository> _productMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _orderMock = new Mock<IOrderRepository>();
        _customerMock = new Mock<ICustomerRepository>();
        _productMock = new Mock<IProductRepository>();

        _unitOfWork.Setup(u => u.Orders).Returns(_orderMock.Object);
        _unitOfWork.Setup(u => u.Customers).Returns(_customerMock.Object);
        _unitOfWork.Setup(u => u.Products).Returns(_productMock.Object);

        _service = new OrderService(_unitOfWork.Object);
    }

    [Fact]
    public async Task 正常系_GetOrdersAsync_全件取得できる()
    {
        // Given
        var orders = new List<Order>
        {
            OrderBuilder.New().WithCustomer(CustomerBuilder.New().Build()).Build(),
            OrderBuilder.New().WithCustomer(CustomerBuilder.New().Build()).Build()
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
        var order = OrderBuilder.New().Build();
        _orderMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // When
        var result = await _service.GetOrderAsync(order.Id, CancellationToken.None);

        // Then
        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal(order.CustomerId, result.CustomerId);
        Assert.Equal(order.OrderDate, result.OrderDate);
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
    public async Task 正常系_CreateOrderAsync()
    {
        // Given
        var customer = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test@example.com"));
        _customerMock.Setup(r => r.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);
        var product1 = new Product(Guid.NewGuid(), new ProductName("商品A"), new Price(100));
        var product2 = new Product(Guid.NewGuid(), new ProductName("商品B"), new Price(200));
        _productMock.Setup(r => r.GetByIdAsync(product1.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product1);
        _productMock.Setup(r => r.GetByIdAsync(product2.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product2);

        var dto = new CreateOrderDto(customer.Id, new List<CreateOrderItemDto>
        {
            new CreateOrderItemDto(product1.Id, 2),
            new CreateOrderItemDto(product2.Id, 1)
        });

        // When
        var result = await _service.CreateOrderAsync(dto, CancellationToken.None);

        // Then
        Assert.NotNull(result);
        Assert.Equal(400, result.TotalAmount);
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
        var dto = new CreateOrderDto(customerId, new List<CreateOrderItemDto>());

        // Then
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CreateOrderAsync(dto, CancellationToken.None));
    }

    [Fact]
    public async Task 異常系_CreateOrderAsync_商品が存在しない場合_InvalidOperationException()
    {
        // Given
        var customer = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test@example.com"));
        _customerMock.Setup(r => r.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);
        var product = new Product(Guid.NewGuid(), new ProductName("商品A"), new Price(100));
        _productMock.Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var dto = new CreateOrderDto(customer.Id, new List<CreateOrderItemDto>
        {
            new CreateOrderItemDto(product.Id, 2),
            new CreateOrderItemDto(Guid.NewGuid(), 1) // 存在しない商品ID
        });

        // Then
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CreateOrderAsync(dto, CancellationToken.None));
    }
}