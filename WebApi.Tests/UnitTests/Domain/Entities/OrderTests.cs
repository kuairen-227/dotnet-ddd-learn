using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class OrderTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var orderDate = DateTime.UtcNow;

        // When
        var order = new Order(orderId, customerId, orderDate);

        // Then
        Assert.NotNull(order);
        Assert.Equal(orderId, order.Id);
        Assert.Equal(customerId, order.CustomerId);
        Assert.Equal(orderDate, order.OrderDate);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void 正常系_AddItem()
    {
        // Given
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        var product = new Product(Guid.NewGuid(),
            new ProductName("テスト 商品"),
            new Price(100)
        );
        var item = new OrderItem(Guid.NewGuid(), product, 2);

        // When
        order.AddItem(item);

        // Then
        Assert.Single(order.Items);
        Assert.Equal(item, order.Items.First());
    }

    [Fact]
    public void 異常系_AddItem_ArgumentNullException()
    {
        // Given
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        // Then
        Assert.Throws<ArgumentNullException>(() => order.AddItem(null!));
    }

    [Fact]
    public void 正常系_GetTotalAmount()
    {
        // Given
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        var product1 = new Product(
            Guid.NewGuid(),
            new ProductName("商品A"),
            new Price(100)
        );
        order.AddItem(new OrderItem(Guid.NewGuid(), product1, 2));
        var product2 = new Product(
            Guid.NewGuid(),
            new ProductName("商品B"),
            new Price(200)
        );
        order.AddItem(new OrderItem(Guid.NewGuid(), product2, 1));

        // When
        var totalAmount = order.GetTotalAmount();

        // Then
        Assert.Equal(400, totalAmount);
    }
}