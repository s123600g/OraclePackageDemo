namespace OracleDbStartProgram;

using Microsoft.Extensions.DependencyInjection;
using OracleDb.Entity;
using OracleDb.Repository;

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
        var input = new DepartmentEntity
        {
            Name = "Marketing",
            Location = "Boston",
        };

        await _departmentRepository.CreateDepartmentAsync(input);
    }
}