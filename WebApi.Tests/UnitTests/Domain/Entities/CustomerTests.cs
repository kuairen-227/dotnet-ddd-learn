using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class CustomerTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var customer = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test@example.com"));

        // Then
        Assert.NotNull(customer);
        Assert.Equal("テスト 太郎", customer.Name);
        Assert.Equal("test@example.com", customer.Email.Value);
    }

    [Fact]
    public void 異常系_名前が空_ArgumentException()
    {
        // Then
        Assert.Throws<ArgumentNullException>(() =>
            new Customer(Guid.NewGuid(), "", new Email("test@example.com"))
        );
    }

    [Fact]
    public void 異常系_メールアドレスが空_ArgumentException()
    {
        // Then
        Assert.Throws<ArgumentNullException>(() =>
            new Customer(Guid.NewGuid(), "テスト 太郎", new Email(""))
        );
    }

    [Fact]
    public void 正常系_ChangeEmail()
    {
        // Given
        var customer = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test@example.com"));

        // When
        customer.ChangeEmail(new Email("test2@example.com"));

        // Then
        Assert.Equal("test2@example.com", customer.Email.Value);
    }

    [Fact]
    public void 異常系_ChangeEmail_メールアドレスが空_ArgumentException()
    {
        // Given
        var customer = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test@example.com"));

        // Then
        Assert.Throws<ArgumentNullException>(() =>
            customer.ChangeEmail(new Email(""))
        );
    }
}