using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Repositories;
using WebApi.Tests.Builders;

namespace WebApi.Tests.UnitTests.Infrastructure.Repositories;

public class ProductRepositoryTests
{
    [Fact]
    public async Task 正常系_Add_GetByIdAsync()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new ProductRepository(context);
        var product = ProductBuilder.New().Build();

        // When
        repository.Add(product);
        await context.SaveChangesAsync();
        var result = await repository.GetByIdAsync(product.Id);

        // Then
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Price, result.Price);
    }

    [Fact]
    public async Task 正常系_GetAllAsync()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new ProductRepository(context);
        var product1 = ProductBuilder.New().WithName("商品A").WithPrice(100).Build();
        var product2 = ProductBuilder.New().WithName("商品B").WithPrice(200).Build();

        // When
        repository.Add(product1);
        repository.Add(product2);
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
        var repository = new ProductRepository(context);
        var product = ProductBuilder.New().WithName("商品（変更前）").Build();
        repository.Add(product);
        await context.SaveChangesAsync();

        // When
        product.Rename(new ProductName("商品（変更後）"));
        repository.Update(product);
        await context.SaveChangesAsync();

        // Then
        var updated = await repository.GetByIdAsync(product.Id);
        Assert.NotNull(updated);
        Assert.Equal("商品（変更後）", updated.Name.Value);
    }

    [Fact]
    public async Task 正常系_Remove()
    {
        // Given
        var context = DbContextFactory.CreateInMemoryDbContext();
        var repository = new ProductRepository(context);
        var product = ProductBuilder.New().Build();
        repository.Add(product);
        await context.SaveChangesAsync();

        // When
        repository.Remove(product);
        await context.SaveChangesAsync();
        var deleted = await repository.GetByIdAsync(product.Id);

        // Then
        Assert.Null(deleted);
    }
}