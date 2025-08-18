using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class OrderItemTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var product = new Product(
            Guid.NewGuid(),
            new ProductName("テスト 商品"),
            new Price(100)
        );
        var orderItem = new OrderItem(Guid.NewGuid(), product, 2);

        // Then
        Assert.NotNull(orderItem);
        Assert.Equal(2, orderItem.Quantity);
    }

    [Fact]
    public void 異常系_数量が0以下_ArgumentOutOfRangeException()
    {
        // Given
        var product = new Product(
            Guid.NewGuid(),
            new ProductName("テスト 商品"),
            new Price(100)
        );
        var invalidQuantity = 0;

        // When
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new OrderItem(Guid.NewGuid(), product, invalidQuantity)
        );
    }

    [Fact]
    public void 正常系_合計金額の取得()
    {
        // Given
        var product = new Product(
            Guid.NewGuid(),
            new ProductName("テスト 商品1"),
            new Price(100)
        );
        var orderItem = new OrderItem(Guid.NewGuid(), product, 2);
        var totalPrice = orderItem.GetTotalPrice();

        // Then
        Assert.Equal(100 * 2, totalPrice);
    }
}