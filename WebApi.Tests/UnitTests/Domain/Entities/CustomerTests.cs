using WebApi.Domain.ValueObjects;
using WebApi.Tests.Builders;

namespace WebApi.Tests.UnitTests.Domain.Entities;

public class CustomerTests
{
    [Fact]
    public void 正常系_インスタンス生成()
    {
        // Given
        var customer = CustomerBuilder.New().Build();

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
            CustomerBuilder.New().WithName("").Build()
        );
    }

    [Fact]
    public void 異常系_メールアドレスが空_ArgumentException()
    {
        // Then
        Assert.Throws<ArgumentNullException>(() =>
            CustomerBuilder.New().WithEmail("").Build()
        );
    }

    [Fact]
    public void 正常系_ChangeEmail()
    {
        // Given
        var customer = CustomerBuilder.New().Build();

        // When
        customer.ChangeEmail(new Email("test2@example.com"));

        // Then
        Assert.Equal("test2@example.com", customer.Email.Value);
    }

    [Fact]
    public void 異常系_ChangeEmail_メールアドレスが空_ArgumentException()
    {
        // Given
        var customer = CustomerBuilder.New().Build();

        // Then
        Assert.Throws<ArgumentNullException>(() =>
            customer.ChangeEmail(new Email(""))
        );
    }
}