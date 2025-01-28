namespace OracleDb.Dapper.Repository;

using OracleDb.Dapper.Entity;

public interface IDepartmentRepository
{
    Task CreateDepartmentAsync(DepartmentEntity departmentEntity);
}