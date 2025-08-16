using WebApi.Domain.Entities;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class OrderItemTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var orderItem = new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 100, 2);

        // Then
        Assert.NotNull(orderItem);
        Assert.Equal(100, orderItem.UnitPrice);
        Assert.Equal(2, orderItem.Quantity);
    }

    [Fact]
    public void 異常系_単価が0以下_ArgumentOutOfRangeException()
    {
        // Given
        var invalidUnitPrice = 0;

        // Then
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new OrderItem(Guid.NewGuid(), Guid.NewGuid(), invalidUnitPrice, 2)
        );
    }

    [Fact]
    public void 異常系_数量が0以下_ArgumentOutOfRangeException()
    {
        // Given
        var invalidQuantity = 0;

        // When
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 100, invalidQuantity)
        );
    }

    [Fact]
    public void 正常系_合計金額の取得()
    {
        // Given
        var orderItem = new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 100, 2);
        var totalPrice = orderItem.GetTotalPrice();

        // Then
        Assert.Equal(100 * 2, totalPrice);
    }
}