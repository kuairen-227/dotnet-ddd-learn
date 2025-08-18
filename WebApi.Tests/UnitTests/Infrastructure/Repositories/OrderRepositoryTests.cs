using WebApi.Infrastructure.Repositories;
using WebApi.Tests.Builders;

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
        var customer = CustomerBuilder.New().Build();
        var order = OrderBuilder.New().WithCustomer(customer).Build();

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
        var order1 = OrderBuilder.New().WithCustomer(
            CustomerBuilder.New().WithName("テスト 太郎").WithEmail("test1@example.com").Build()
        ).Build();
        var order2 = OrderBuilder.New().WithCustomer(
            CustomerBuilder.New().WithName("テスト 次郎").WithEmail("test2@example.com").Build()
        ).Build();

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
        var order = OrderBuilder.New().Build();
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