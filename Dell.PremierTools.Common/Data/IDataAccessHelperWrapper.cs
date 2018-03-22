using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Dell.PremierTools.Common.Data
{
    public interface IDataAccessHelperWrapper
    {
        SqlConnection CreateConnection(String connectionStringName);
        DataSet ExecuteDataSet(string connectionStringName, string procName, params object[] parameterValues);
        DataSet ExecuteDataSet(SqlTransaction transaction, string procName, params object[] parameterValues);
        DataSet ExecuteDataSet(string connectionStringName, string procName, params SqlParameter[] parameterValues);
        DataSet ExecuteDataSet(string connectionStringName, string procName);
        DataSet ExecuteDataSet(string connectionStringName, string procName, params DBParameter[] parameterValues);
        SqlParameter[] GetSPParameters(String connectionStringName, String procName);
        DataSet ExecuteDataSet(SqlTransaction transaction, String procName, SqlParameter[] parameterValues);
        int ExecuteNonQuery(string connection, string procName);
        int ExecuteNonQuery(string connection, string procName, params object[] parameterValues);
        int ExecuteNonQuery(string connection, string procName, params SqlParameter[] parameterValues);
        int ExecuteNonQuery(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues);
        int ExecuteNonQuery(SqlTransaction transaction, string procName, params object[] parameterValues);
        int ExecuteNonQuery(string connection, string procName, int commandTimeout, params object[] parameterValues);
        object ExecuteNonQuery(string connection, string procName, params DBParameter[] parameterValues);
        object ExecuteScalar(string connection, string procName, params object[] parameterValues);
        object ExecuteScalar(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues);
        object ExecuteScalar(SqlTransaction transaction, string procName, params object[] parameterValues);
        object ExecuteScalar(string connection, string procName, params SqlParameter[] parameterValues);
        object ExecuteScalar(string connection, string procName, params DBParameter[] parameterValues);
        object ExecuteScalar(string connection, string procName);
        IDataReader ExecuteReader(string connection, string procName);
        IDataReader ExecuteReaderDynamicSql(string connection, string sqlText);
        IDataReader ExecuteReader(string connection, string procName, params object[] parameterValues);
        IDataReader ExecuteReader(string connection, string procName, params SqlParameter[] parameterValues);
        IDataReader ExecuteReader(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues);
        IDataReader ExecuteReader(SqlTransaction transaction, string procName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(string connection, string procName);
        XmlReader ExecuteXmlReader(string connection, string procName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(string connection, string procName, params SqlParameter[] parameterValues);
        XmlReader ExecuteXmlReader(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues);
        XmlReader ExecuteXmlReader(SqlTransaction transaction, string procName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(SqlConnection connection, string procName);
        XmlReader ExecuteXmlReader(SqlConnection connection, string procName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(SqlConnection connection, string procName, params SqlParameter[] parameterValues);
        
    }
}
