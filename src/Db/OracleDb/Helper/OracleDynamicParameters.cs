namespace OracleDb.Helper;

using System.Data;
using global::Dapper;
using Oracle.ManagedDataAccess.Client;

public class OracleDynamicParameters : SqlMapper.IDynamicParameters
{
    private readonly List<OracleParameter> _parameters = [];

    public void Add(string name, object value, OracleDbType dbType, ParameterDirection direction, string udtTypeName = null)
    {
        var parameter = new OracleParameter
        {
            ParameterName = name,
            Value = value,
            OracleDbType = dbType,
            Direction = direction
        };

        if (!string.IsNullOrEmpty(udtTypeName))
        {
            parameter.UdtTypeName = udtTypeName;
        }

        _parameters.Add(parameter);
    }

    public void Add(OracleParameter parameter)
    {
        _parameters.Add(parameter);
    }

    public OracleParameter[] ToArray()
    {
        return _parameters.ToArray();
    }

    public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
    {
        if (command is OracleCommand oracleCommand)
        {
            oracleCommand.Parameters.AddRange(_parameters.ToArray());
        }
    }
}