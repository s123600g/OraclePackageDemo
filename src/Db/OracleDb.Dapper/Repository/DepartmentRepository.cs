namespace OracleDb.Dapper.Repository;

using System.Data;
using global::Dapper;
using NLog;
using Oracle.ManagedDataAccess.Client;
using OracleDb.Dapper.Entity;
using OracleDb.Dapper.Entity.Udt;
using OracleDb.Dapper.Helper;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DataAccessFactory _dataAccessFactory;

    public DepartmentRepository(
        DataAccessFactory dataAccessFactory
    )
    {
        _dataAccessFactory = dataAccessFactory ?? throw new ArgumentNullException(nameof(dataAccessFactory));
    }

    public async Task CreateDepartmentAsync(DepartmentEntity departmentEntity)
    {
        using var db = _dataAccessFactory.GetDbEntity();

        // 建立部門 UDT 物件
        var department = new DepartmentUdt
        {
            DeptNo = 4,
            Name = "Marketing",
            Location = "Boston",
        };

        // 使用 OracleDynamicParameters 管理參數
        var dynamicParameters = new OracleDynamicParameters();
        // dynamicParameters.Add(
        //     name: "p_department",
        //     dbType: OracleDbType.Object,
        //     direction: ParameterDirection.Input,
        //     udtTypeName: "t_department", // Oracle 中的 UDT 名稱
        //     value: department
        // );

        var param = new OracleParameter()
        {
            ParameterName = "p_department",
            OracleDbType = OracleDbType.Object,
            Direction = ParameterDirection.Input,
            UdtTypeName = "t_department", // Oracle 中的 UDT 名稱
            Value = department
        };

        dynamicParameters.Add(param);

        // 使用 Dapper 傳遞參數並執行
        await db.ExecuteAsync(
            sql: "pkg_departments.create_department",
            param: dynamicParameters,
            commandType: CommandType.StoredProcedure
        );

        Console.WriteLine("Department created successfully");
    }
}