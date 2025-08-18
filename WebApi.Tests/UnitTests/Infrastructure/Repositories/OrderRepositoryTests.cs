using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Repositories;

namespace WebApi.Tests.UnitTests.Infrastructure.Repositories;

public class OrderRepositoryTests
{
    [Fact]
    public async Task 正常系_Add_GetByIdAsync()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var customerRepo = new CustomerRepository(context);
        var orderRepo = new OrderRepository(context);
        var customer = new Customer(
            Guid.NewGuid(),
            "テスト 太郎",
            new Email("test@example.com")
        );
        var order = new Order(
            Guid.NewGuid(),
            customer.Id,
            DateTime.UtcNow
        );

        // When
        customerRepo.Add(customer);
        orderRepo.Add(order);
        await context.SaveChangesAsync();
        var result = await orderRepo.GetByIdAsync(order.Id);

        // Then
        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal(customer.Id, result.Customer.Id);
        Assert.Equal(order.OrderDate, result.OrderDate);
    }

    [Fact]
    public async Task 正常系_GetAllAsync()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new OrderRepository(context);
        var customer1 = new Customer(
            Guid.NewGuid(),
            "テスト 太郎",
            new Email("test1@example.com")
        );
        var order1 = new Order(Guid.NewGuid(), customer1.Id, DateTime.UtcNow);
        var customer2 = new Customer(
            Guid.NewGuid(),
            "テスト 次郎",
            new Email("test2@example.com")
        );
        var order2 = new Order(Guid.NewGuid(), customer2.Id, DateTime.UtcNow);

        // When
        repository.Add(order1);
        repository.Add(order2);
        await context.SaveChangesAsync();

        // Then
        var result = await repository.GetAllAsync();
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task 正常系_Remove()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new OrderRepository(context);
        var customer = new Customer(
            Guid.NewGuid(),
            "テスト 太郎",
            new Email("test@example.com")
        );
        var order = new Order(
            Guid.NewGuid(),
            customer.Id,
            DateTime.UtcNow
        );
        repository.Add(order);
        await context.SaveChangesAsync();

        // When
        repository.Remove(order);
        await context.SaveChangesAsync();
        var deleted = await repository.GetByIdAsync(order.Id);

        // Then
        Assert.Null(deleted);
    }
}