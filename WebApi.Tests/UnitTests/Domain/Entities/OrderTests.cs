using WebApi.Domain.Entities;
using WebApi.Tests.Builders;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class OrderTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var order = OrderBuilder.New().Build();

        // Then
        Assert.NotNull(order);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void 正常系_AddItem()
    {
        // Given
        var order = OrderBuilder.New().Build();
        var product = ProductBuilder.New().Build();
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
        var order = OrderBuilder.New().Build();

        // Then
        Assert.Throws<ArgumentNullException>(() => order.AddItem(null!));
    }

    [Fact]
    public void 正常系_GetTotalAmount()
    {
        // Given
        var order = OrderBuilder.New().Build();
        var product1 = ProductBuilder.New().WithName("商品A").WithPrice(100).Build();
        order.AddItem(new OrderItem(Guid.NewGuid(), product1, 2));
        var product2 = ProductBuilder.New().WithName("商品B").WithPrice(200).Build();
        order.AddItem(new OrderItem(Guid.NewGuid(), product2, 1));

        // When
        var totalAmount = order.GetTotalAmount();

        // Then
        Assert.Equal(400, totalAmount);
    }
}