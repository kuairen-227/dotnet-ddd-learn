using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    private Customer() { } // EF Core用
    public Customer(Guid id, string name, Email email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Nameは必須です");

        Id = id;
        Name = name;
        Email = email ?? throw new ArgumentNullException(nameof(email), "Emailは必須です");
    }

    public void ChangeEmail(Email newEmail)
    {
        Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail), "新しいEmailは必須です");
    }
}