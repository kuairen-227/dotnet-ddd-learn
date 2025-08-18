using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Infrastructure.Contexts;

namespace WebApi.Tests.UnitTests.Infrastructure;

public static class DbContextFactory
{
    public static AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }
}