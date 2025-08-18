using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Repositories;

namespace WebApi.Tests.UnitTests.Infrastructure.Repositories;

public class CustomerRepositoryTests
{
    [Fact]
    public async Task 正常系_Add_GetByIdAsync()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new CustomerRepository(context);
        var customer = new Customer(
            Guid.NewGuid(),
            "テスト 太郎",
            new Email("test@example.com")
        );

        // When
        repository.Add(customer);
        await context.SaveChangesAsync();
        var result = await repository.GetByIdAsync(customer.Id);

        // Then
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal(customer.Name, result.Name);
        Assert.Equal(customer.Email, result.Email);
    }

    [Fact]
    public async Task 正常系_GetAllAsync()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new CustomerRepository(context);
        var customer1 = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test1@example.com"));
        var customer2 = new Customer(Guid.NewGuid(), "テスト 次郎", new Email("test2@example.com"));

        // When
        repository.Add(customer1);
        repository.Add(customer2);
        await context.SaveChangesAsync();

        // Then
        var result = await repository.GetAllAsync();
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task 正常系_Update()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new CustomerRepository(context);
        var customer = new Customer(
            Guid.NewGuid(),
            "テスト 太郎",
            new Email("test@example.com")
        );
        repository.Add(customer);
        await context.SaveChangesAsync();

        // When
        customer.ChangeEmail(new Email("test-change@example.com"));
        repository.Update(customer);
        await context.SaveChangesAsync();

        // Then
        var updated = await repository.GetByIdAsync(customer.Id);
        Assert.NotNull(updated);
        Assert.Equal("test-change@example.com", updated.Email.Value);
    }

    [Fact]
    public async Task 正常系_Remove()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new CustomerRepository(context);
        var customer = new Customer(Guid.NewGuid(), "テスト 太郎", new Email("test@example.com"));
        repository.Add(customer);
        await context.SaveChangesAsync();

        // When
        repository.Remove(customer);
        await context.SaveChangesAsync();
        var deleted = await repository.GetByIdAsync(customer.Id);

        // Then
        Assert.Null(deleted);
    }
}