using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }

    public Customer(Guid id, string name, Email email)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name), "名前は必須です");
        Email = email ?? throw new ArgumentNullException(nameof(email), "Emailは必須です");
    }
}