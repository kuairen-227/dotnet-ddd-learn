using WebApi.Domain.Entities;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class OrderTests
{
    [Fact]
    public void 正常系_注文情報が正しい場合_インスタンスが生成できる()
    {
        // Given
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var orderDate = DateTime.UtcNow;
        var currency = "JPY";

        // When
        var order = new Order(orderId, customerId, orderDate, currency);

        // Then
        Assert.NotNull(order);
        Assert.Equal(orderId, order.Id);
        Assert.Equal(customerId, order.CustomerId);
        Assert.Equal(orderDate, order.OrderDate);
        Assert.Equal(currency, order.Currency);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void 正常系_注文明細の追加()
    {
        // Given
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "JPY");
        var item = new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 100, 2);

        // When
        order.AddItem(item);

        // Then
        Assert.Single(order.Items);
        Assert.Equal(item, order.Items.First());
    }

    [Fact]
    public void 異常系_注文明細の追加_ArgumentNullException()
    {
        // Given
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "JPY");

        // Then
        Assert.Throws<ArgumentNullException>(() => order.AddItem(null!));
    }

    [Fact]
    public void 正常系_合計金額の取得()
    {
        // Given
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "JPY");
        order.AddItem(new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 100, 2));
        order.AddItem(new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 200, 1));

        // When
        var totalAmount = order.GetTotalAmount();

        // Then
        Assert.Equal(400, totalAmount);
    }
}