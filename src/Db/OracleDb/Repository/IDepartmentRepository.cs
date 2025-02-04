namespace OracleDb.Repository;

using OracleDb.Entity;

public interface IDepartmentRepository
{
    Task<List<DepartmentEntity>> GetDepartmentsAsync();

    Task<DepartmentEntity?> GetSingleDepartmentAsync(
        int deptNo
    );

    Task CreateDepartmentAsync(
        DepartmentEntity departmentEntity
    );

    Task UpdateDepartmentAsync(
        DepartmentEntity departmentEntity
    );

    Task DeleteDepartmentAsync(
        int deptNo
    );
}