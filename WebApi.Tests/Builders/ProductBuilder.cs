using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.Builders;

public class ProductBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "商品テスト";
    private int _price = 100;

    public static ProductBuilder New() => new ProductBuilder();

    public ProductBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ProductBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProductBuilder WithPrice(int price)
    {
        _price = price;
        return this;
    }

    public Product Build()
    {
        return new Product(_id, new ProductName(_name), new Price(_price));
    }
}