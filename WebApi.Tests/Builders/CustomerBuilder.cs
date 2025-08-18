using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Tests.Builders;

public class CustomerBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "テスト 太郎";
    private string _email = "test@example.com";

    public static CustomerBuilder New() => new CustomerBuilder();

    public CustomerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public CustomerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CustomerBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public Customer Build()
    {
        return new Customer(_id, _name, new Email(_email));
    }
}