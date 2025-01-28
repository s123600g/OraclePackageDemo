namespace OracleDb;

using System.Data;
using global::Dapper;
using Oracle.ManagedDataAccess.Client;

public class DataAccessFactory(string dbConnectionStr)
{
    public IDbConnection GetDbEntity()
    {
        IDbConnection dbConnectionEntiity = new OracleConnection()
        {
            ConnectionString = dbConnectionStr
        };

        // 開啟欄位名稱有底線自動對應處理
        // https://stackoverflow.com/questions/34533349/how-to-get-dapper-to-ignore-remove-underscores-in-field-names-when-mapping
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        // SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.Date);
        // SqlMapper.AddTypeMap(typeof(DateTimeOffset), System.Data.DbType.DateTimeOffset);

        return dbConnectionEntiity;
    }
}