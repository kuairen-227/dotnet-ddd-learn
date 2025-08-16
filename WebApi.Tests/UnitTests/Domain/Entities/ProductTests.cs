using WebApi.Domain.Entities;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void 正常系_インスタンスが生成できる()
    {
        // Given
        var id = Guid.NewGuid();
        var name = "商品A";
        var price = 100;

        // When
        var product = new Product(id, name, price);

        // Then
        Assert.NotNull(product);
        Assert.Equal(id, product.Id);
        Assert.Equal(name, product.Name);
        Assert
        .Equal(price, product.Price);
    }

    [Fact]
    public void 異常系_名前が空_ArgumentException()
    {
        // Given
        var id = Guid.NewGuid();
        var name = "";
        var price = 100;

        // Then
        Assert.Throws<ArgumentNullException>(() => new Product(id, name, price));
    }

    [Fact]
    public void 異常系_価格が0以下_ArgumentOutOfRangeException()
    {
        // Given
        var id = Guid.NewGuid();
        var name = "商品A";
        var price = 0;

        // Then
        Assert.Throws<ArgumentOutOfRangeException>(() => new Product(id, name, price));
    }
}