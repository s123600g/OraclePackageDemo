namespace OracleDb.Repository;

using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using OracleDb.Entity;
using OracleDb.Entity.Udt;
using OracleDb.Helper;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DataAccessFactory _dataAccessFactory;

    public DepartmentRepository(
        DataAccessFactory dataAccessFactory
    )
    {
        _dataAccessFactory = dataAccessFactory ?? throw new ArgumentNullException(nameof(dataAccessFactory));
    }

    public async Task<List<DepartmentEntity>> GetDepartmentsAsync()
    {
        const string sqlSyntax = @"
            SELECT 
                DEPT_NO
                , NAME
                , LOCATION
            FROM DEPARTMENTS
        ";

        var result = new List<DepartmentEntity>();

        using var db = _dataAccessFactory.GetDbEntity();

        var data = (
            await db.QueryAsync<DepartmentEntity>(
                sql: sqlSyntax
            )
        )?.ToList() ?? new List<DepartmentEntity>();

        if (data.Count != 0)
        {
            result = data;
        }

        return result;
    }

    public async Task<DepartmentEntity?> GetSingleDepartmentAsync(
        int deptNo
    )
    {
        const string sqlSyntax = @"
            SELECT 
                DEPT_NO
                , NAME
                , LOCATION
            FROM DEPARTMENTS
            WHERE DEPT_NO = :dept_no
        ";

        using var db = _dataAccessFactory.GetDbEntity();

        var dynamicParameters = new OracleDynamicParameters();

        dynamicParameters.Add(new OracleParameter()
        {
            ParameterName = "dept_no",
            OracleDbType = OracleDbType.Int32,
            Value = deptNo
        });

        var result = await db.QueryFirstOrDefaultAsync<DepartmentEntity>(
            sql: sqlSyntax,
            param: dynamicParameters
        );

        return result;
    }

    public async Task CreateDepartmentAsync(
        DepartmentEntity departmentEntity
    )
    {
        const string sqlSyntax = "pkg_departments.create_department";

        using var db = _dataAccessFactory.GetDbEntity();

        var dynamicParameters = new OracleDynamicParameters();

        dynamicParameters.Add(
            new OracleParameter()
            {
                ParameterName = "p_department", // 對照 Oracle 的 UDT 參數名稱
                OracleDbType = OracleDbType.Object,
                Direction = ParameterDirection.Input,
                UdtTypeName = "t_department", // Oracle 中的 UDT 名稱
                Value = new DepartmentUdt
                {
                    Name = departmentEntity.Name,
                    Location = departmentEntity.Location,
                }
            }
        );

        _ = await db.ExecuteAsync(
            sql: sqlSyntax,
            param: dynamicParameters,
            commandType: CommandType.StoredProcedure
        );

        Console.WriteLine("Department created successfully");
    }

    public async Task UpdateDepartmentAsync(
        DepartmentEntity departmentEntity
    )
    {
        const string sqlSyntax = "pkg_departments.update_department";

        using var db = _dataAccessFactory.GetDbEntity();

        var dynamicParameters = new OracleDynamicParameters();

        dynamicParameters.Add(
            new OracleParameter()
            {
                ParameterName = "p_department", // 對照 Oracle 的 UDT 參數名稱
                OracleDbType = OracleDbType.Object,
                Direction = ParameterDirection.Input,
                UdtTypeName = "t_department", // Oracle 中的 UDT 名稱
                Value = new DepartmentUdt
                {
                    DeptNo = departmentEntity.DeptNo,
                    Name = departmentEntity.Name,
                    Location = departmentEntity.Location,
                }
            }
        );

        _ = await db.ExecuteAsync(
            sql: sqlSyntax,
            param: dynamicParameters,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteDepartmentAsync(
        int deptNo
    )
    {
        const string sqlSyntax = "pkg_departments.delete_department";

        using var db = _dataAccessFactory.GetDbEntity();

        var dynamicParameters = new OracleDynamicParameters();

        dynamicParameters.Add(new OracleParameter()
        {
            ParameterName = "p_dept_no",
            OracleDbType = OracleDbType.Int32,
            Direction = ParameterDirection.Input,
            Value = deptNo
        });

        _ = await db.ExecuteAsync(
            sql: sqlSyntax,
            param: dynamicParameters,
            commandType: CommandType.StoredProcedure
        );
    }
}