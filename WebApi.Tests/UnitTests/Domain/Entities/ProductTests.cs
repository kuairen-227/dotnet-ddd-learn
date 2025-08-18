using WebApi.Tests.Builders;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void 正常系_インスタンスが生成できる()
    {
        // Given
        var product = ProductBuilder.New().Build();

        // Then
        Assert.NotNull(product);
        Assert.Equal("商品テスト", product.Name.Value);
        Assert.Equal(100, product.Price.Value);
    }
}