namespace OracleDbStartProgram.Init.Extensions;

using Microsoft.Extensions.DependencyInjection;
using NLog;
using OracleDb.Dapper;
using OracleDb.Dapper.Repository;

public static class RepositoryServiceCollection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}