using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void 正常系_正しいメールアドレス_インスタンスが生成できる()
    {
        // Given
        var validEmail = "test@example.com";

        // When
        var email = new Email(validEmail);

        // Then
        Assert.Equal(validEmail, email.Value);
    }

    [Fact]
    public void 異常系_空のメールアドレス_ArgumentException()
    {
        // Given
        var invalidEmail = "";

        // Then
        Assert.Throws<ArgumentNullException>(() => new Email(invalidEmail));
    }

    [Fact]
    public void 異常系_不正なメールアドレス_ArgumentException()
    {
        // Given
        var invalidEmail = "invalid-email";

        // Then
        Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
    }

    [Fact]
    public void 正常系_同じ値のEmailは等しい()
    {
        // Given
        var email1 = new Email("test@example.com");
        var email2 = new Email("test@example.com");

        // When
        var areEqual = email1.Equals(email2);

        // Then
        Assert.True(areEqual);
        Assert.Equal(email1.GetHashCode(), email2.GetHashCode());
    }

    [Fact]
    public void 正常系_異なる値のEmailは等しくない()
    {
        // Given
        var email1 = new Email("test@example.com");
        var email2 = new Email("test2@example.com");

        // When
        var areEqual = email1.Equals(email2);

        // Then
        Assert.False(areEqual);
        Assert.NotEqual(email1.GetHashCode(), email2.GetHashCode());
    }

    [Fact]
    public void 正常系_値を文字列として返す()
    {
        // Given
        var email = new Email("test@example.com");

        // When
        var emailString = email.ToString();

        // Then
        Assert.Equal("test@example.com", emailString);
    }
}