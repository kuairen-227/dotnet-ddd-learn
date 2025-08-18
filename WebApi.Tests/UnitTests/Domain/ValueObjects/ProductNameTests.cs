using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.ValueObjects;

public class ProductNameTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var validName = "商品名";

        // When
        var productName = new ProductName(validName);

        // Then
        Assert.Equal(validName, productName.Value);
    }

    [Fact]
    public void 異常系_空の商品名_ArgumentNullException()
    {
        // Given
        var invalidName = "";

        // Then
        Assert.Throws<ArgumentNullException>(() => new ProductName(invalidName));
    }

    [Fact]
    public void 正常系_同じ値のProductNameは等しい()
    {
        // Given
        var name1 = new ProductName("商品名");
        var name2 = new ProductName("商品名");

        // When
        var areEqual = name1.Equals(name2);

        // Then
        Assert.True(areEqual);
        Assert.Equal(name1.GetHashCode(), name2.GetHashCode());
    }

    [Fact]
    public void 正常系_異なる値のProductNameは等しくない()
    {
        // Given
        var name1 = new ProductName("商品名");
        var name2 = new ProductName("商品名2");

        // When
        var areEqual = name1.Equals(name2);

        // Then
        Assert.False(areEqual);
        Assert.NotEqual(name1.GetHashCode(), name2.GetHashCode());
    }

    [Fact]
    public void 正常系_値を文字列として返す()
    {
        // Given
        var email = new ProductName("商品名");

        // When
        var nameString = email.ToString();

        // Then
        Assert.Equal("商品名", nameString);
    }
}