namespace OracleDb.Dapper.Entity.Udt;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

// 對應 Oracle 的 UDT 類別
[OracleCustomTypeMapping("T_DEPARTMENT")]
public class DepartmentUdt : OracleUdtBase<DepartmentUdt>
{
    [OracleObjectMapping("DEPT_NO")]
    public decimal? DeptNo { get; set; }

    [OracleObjectMapping("NAME")]
    public string Name { get; set; }

    [OracleObjectMapping("LOCATION")]
    public string Location { get; set; }

    public override void FromCustomObject(OracleConnection con, object udt)
    {
        if (
            DeptNo.HasValue
        )
        {
            SetValue(con, udt, "DEPT_NO", DeptNo.Value);
        }

        if (
            !string.IsNullOrEmpty(Name)
        )
        {
            SetValue(con, udt, "NAME", Name);
        }

        if (
            !string.IsNullOrEmpty(Location)
        )
        {
            SetValue(con, udt, "LOCATION", Location);
        }
    }

    public override void ToCustomObject(OracleConnection con, object udt)
    {
        DeptNo = GetValue<decimal?>(con, udt, "DEPT_NO");

        Name = GetValue<string>(con, udt, "NAME");

        Location = GetValue<string>(con, udt, "LOCATION");
    }
}