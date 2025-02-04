namespace OraclePackageDemoApp;

using Microsoft.Extensions.DependencyInjection;
using NLog;
using OracleDb.Entity;
using OracleDb.Repository;

class Program
{
    private static IDepartmentRepository _departmentRepository;
    private static Logger _logger;

    static async Task Main(string[] args)
    {
        var configInstance = InitConfig.Startup();

        var logger = configInstance._logger;
        _logger = logger;

        var serviceProvider = configInstance.serviceProvider;

        _departmentRepository = serviceProvider.GetService<IDepartmentRepository>() ?? throw new ArgumentNullException(nameof(IDepartmentRepository));

        try
        {
            // 取得全部資料
            await RunGetDepartmentsAsync();

            //// 取得單一資料
            // await RunGetSingleDepartmentAsync(3);

            //// 建立資料
            // await RunCreateDepartmentAsync(
            //     new DepartmentEntity
            //     {
            //         Name = "Marketing",
            //         Location = "Boston",
            //     }
            // );

            //// 更新單一資料
            // await RunUpdateDepartmentAsync(
            //     new DepartmentEntity
            //     {
            //         DeptNo = 5,
            //         Name = "Sam",
            //         Location = "Kaohsiung",
            //     }
            // );

            //// 刪除單一資料
            // await RunDeleteDepartmentAsync(5);
        }
        catch (Exception ex)
        {
            logger.Error(
                exception: ex
                , message: ex.Message
            );

            throw;
        }

        logger.Info("Done.\n-------------------------------------------------------------\n");
    }

    private static async Task RunGetDepartmentsAsync()
    {
        var data = await _departmentRepository.GetDepartmentsAsync();

        if (
            data.Count != 0
        )
        {
            foreach (var item in data)
            {
                _logger.Info($"Department No: {item.DeptNo}, Name: {item.Name}, Location: {item.Location}");
            }
        }
    }

    private static async Task RunGetSingleDepartmentAsync(
        int depNo
    )
    {
        var data = await _departmentRepository.GetSingleDepartmentAsync(depNo);

        if (
            data is not null
        )
        {
            _logger.Info($"Department No: {data.DeptNo}, Name: {data.Name}, Location: {data.Location}");
        }
    }

    private static async Task RunCreateDepartmentAsync(
        DepartmentEntity input
    )
    {
        await _departmentRepository.CreateDepartmentAsync(input);

        _logger.Info("Department created successfully");
    }

    private static async Task RunUpdateDepartmentAsync(
        DepartmentEntity input
    )
    {
        await _departmentRepository.UpdateDepartmentAsync(input);

        _logger.Info("Department updated successfully");
    }

    private static async Task RunDeleteDepartmentAsync(
        int depNo
    )
    {
        await _departmentRepository.DeleteDepartmentAsync(depNo);

        _logger.Info("Department deleted successfully");
    }
}