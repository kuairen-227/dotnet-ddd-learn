using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public ProductName Name { get; private set; }
    public Price Price { get; private set; }

    public Product(Guid id, ProductName name, Price price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public void Rename(ProductName newName) => Name = newName;
    public void RevisePrice(Price newPrice) => Price = newPrice;
}