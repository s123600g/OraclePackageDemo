namespace OraclePackageDemoApp.Extensions;

using Microsoft.Extensions.DependencyInjection;
using OracleDb.Repository;

public static class RepositoryServiceCollection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}