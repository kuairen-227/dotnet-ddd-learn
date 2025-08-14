using WebApi.Domain.Common;
using WebApi.Infrastructure.Contexts;

namespace WebApi.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        // Application 層

        // Infrastructure 層
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}