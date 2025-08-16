using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Customer> Customers => Set<Customer>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Items);
        });
        modelBuilder.Entity<OrderItem>(builder =>
        {
            builder.HasKey(oi => oi.Id);
        });
        modelBuilder.Entity<Customer>(builder =>
        {
            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("email").IsRequired();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}