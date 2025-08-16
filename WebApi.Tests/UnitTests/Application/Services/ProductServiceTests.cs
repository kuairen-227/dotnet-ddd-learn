using Moq;
using WebApi.Application.DTOs;
using WebApi.Application.Services;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Application.Services;

public class ProductServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProductRepository> _productMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _productMock = new Mock<IProductRepository>();

        _unitOfWork.Setup(u => u.Products).Returns(_productMock.Object);
        _service = new ProductService(_unitOfWork.Object);
    }

    [Fact]
    public async Task 正常系_GetProductsAsync_全件取得できる()
    {
        // Given
        var products = new List<Product>
        {
            new Product(Guid.NewGuid(), "テスト 商品1", 100),
            new Product(Guid.NewGuid(), "テスト 商品2", 200)
        };
        _productMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        // When
        var result = await _service.GetProductsAsync(CancellationToken.None);

        // Then
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task 正常系_GetProductAsync_商品が存在する場合_商品情報が取得できる()
    {
        // Given
        var productId = Guid.NewGuid();
        var product = new Product(productId, "テスト 商品1", 100);
        _productMock.Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // When
        var result = await _service.GetProductAsync(productId, CancellationToken.None);

        // Then
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
    }

    [Fact]
    public async Task 正常系_GetProductAsync_商品が存在しない場合_NULLが返る()
    {
        // Given
        _productMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        // When
        var result = await _service.GetProductAsync(Guid.NewGuid(), CancellationToken.None);

        // Then
        Assert.Null(result);
    }

    [Fact]
    public async Task 正常系_CreateProductAsync_商品が作成できる()
    {
        // Given
        var dto = new CreateProductDto("テスト 商品1", 100);

        // When
        _productMock.Setup(r => r.Add(It.IsAny<Product>()));
        _unitOfWork.Setup(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Then
        var result = await _service.CreateProductAsync(dto, CancellationToken.None);
        Assert.NotNull(result);
        _productMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}