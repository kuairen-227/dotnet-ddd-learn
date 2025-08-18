using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.ValueObjects;

public class PriceTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var validPrice = 100;

        // When
        var price = new Price(validPrice);

        // Then
        Assert.Equal(validPrice, price.Value);
    }

    [Fact]
    public void 異常系_価格が0以下_ArgumentOutOfRangeException()
    {
        // Given
        var invalidPrice = 0;

        // Then
        Assert.Throws<ArgumentOutOfRangeException>(() => new Price(invalidPrice));
    }

    [Fact]
    public void 正常系_同じ値のPriceは等しい()
    {
        // Given
        var price1 = new Price(100);
        var price2 = new Price(100);

        // When
        var areEqual = price1.Equals(price2);

        // Then
        Assert.True(areEqual);
        Assert.Equal(price1.GetHashCode(), price2.GetHashCode());
    }

    [Fact]
    public void 正常系_異なる値のPriceは等しくない()
    {
        // Given
        var price1 = new Price(100);
        var price2 = new Price(200);

        // When
        var areEqual = price1.Equals(price2);

        // Then
        Assert.False(areEqual);
        Assert.NotEqual(price1.GetHashCode(), price2.GetHashCode());
    }

    [Fact]
    public void 正常系_値を文字列として返す()
    {
        // Given
        var email = new Price(100);

        // When
        var nameString = email.ToString();

        // Then
        Assert.Equal("100", nameString);
    }
}