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
}