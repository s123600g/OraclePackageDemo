namespace OracleDb.Dapper.Entity.Udt;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using INullable = System.Data.SqlTypes.INullable;

public abstract class OracleUdtBase<T> : IOracleCustomType, IOracleCustomTypeFactory, INullable where T : OracleUdtBase<T>, new()
{
    private bool _innerIsNull;

    public bool IsNull => _innerIsNull;

    public static T Null => new()
    {
        _innerIsNull = true
    };

    public IOracleCustomType CreateObject()
    {
        return new T();
    }

    public abstract void FromCustomObject(OracleConnection con, object udt);

    public abstract void ToCustomObject(OracleConnection con, object udt);

    protected void SetValue<TValue>(OracleConnection con, object udt, string name, TValue value)
    {
        OracleUdt.SetValue(con, udt, name, value);
    }

    protected TReturn GetValue<TReturn>(OracleConnection con, object udt, string name)
    {
        return (TReturn)OracleUdt.GetValue(con, udt, name);
    }
}