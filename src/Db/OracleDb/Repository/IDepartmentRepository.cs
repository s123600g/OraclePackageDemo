namespace OracleDb.Repository;

using OracleDb.Entity;

public interface IDepartmentRepository
{
    Task CreateDepartmentAsync(DepartmentEntity departmentEntity);
}