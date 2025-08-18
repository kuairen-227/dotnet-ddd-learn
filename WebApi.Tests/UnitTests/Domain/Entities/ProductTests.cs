using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void 正常系_インスタンスが生成できる()
    {
        // Given
        var id = Guid.NewGuid();
        var name = new ProductName("商品A");
        var price = new Price(100);

        // When
        var product = new Product(id, name, price);

        // Then
        Assert.NotNull(product);
        Assert.Equal(id, product.Id);
        Assert.Equal(name, product.Name);
        Assert.Equal(price, product.Price);
    }
}