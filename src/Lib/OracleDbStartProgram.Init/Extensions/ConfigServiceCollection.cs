namespace OracleDbStartProgram.Init.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OracleDb.Dapper;

public static class ConfigServiceCollection
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("DemoDb");
        services.AddScoped<DataAccessFactory>(provider => new DataAccessFactory(connectionString));

        return services;
    }
}