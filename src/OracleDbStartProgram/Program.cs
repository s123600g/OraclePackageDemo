namespace OracleDbStartProgram;

using Microsoft.Extensions.DependencyInjection;
using OracleDb.Dapper.Entity;
using OracleDb.Dapper.Repository;
using OracleDbStartProgram.Init;

class Program
{
    private static IDepartmentRepository _departmentRepository;

    static async Task Main(string[] args)
    {
        var configInstance = InitConfig.Startup();

        var logger = configInstance._logger;
        var serviceProvider = configInstance.serviceProvider;

        _departmentRepository = serviceProvider.GetService<IDepartmentRepository>() ?? throw new ArgumentNullException(nameof(IDepartmentRepository));

        await RunCreateDepartment();

        logger.Info("Done.\n-------------------------------------------------------------\n");
    }

    private static async Task RunCreateDepartment()
    {
        await _departmentRepository.CreateDepartmentAsync(new DepartmentEntity());
    }
}