namespace WebApi.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }

    public Product(Guid id, string name, int price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "商品名は必須です");
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "価格は1円以上でなければなりません");

        Id = id;
        Name = name;
        Price = price;
    }
}