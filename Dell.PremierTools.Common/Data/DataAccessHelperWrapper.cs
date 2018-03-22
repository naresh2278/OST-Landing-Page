using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Dell.PremierTools.Common.Data
{
    public class DataAccessHelperWrapper : IDataAccessHelperWrapper
    {
     
       #region IDataAccessHelperWrapper Members


        public object ExecuteScalar(string connection, string procName, params object[] parameterValues)
        {
            return DataAccessHelper.ExecuteScalar(connection, procName, parameterValues);
        }

       public DataSet ExecuteDataSet(string connectionStringName, string procName, params object[] parameterValues)
       { 
           return DataAccessHelper.ExecuteDataSet(connectionStringName, procName, parameterValues);
       }

       public DataSet ExecuteDataSet(SqlTransaction transaction, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteDataSet(transaction,  procName, parameterValues);
       }

       public DataSet ExecuteDataSet(string connectionStringName, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteDataSet(connectionStringName, procName, parameterValues);
       }

       public DataSet ExecuteDataSet(string connectionStringName, string procName)
       {
          return DataAccessHelper.ExecuteDataSet(connectionStringName, procName);
       }

       public DataSet ExecuteDataSet(string connectionStringName, string procName, params DBParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteDataSet(connectionStringName, procName, parameterValues);
       }

       public SqlParameter[] GetSPParameters(string connectionStringName, string procName)
       {
           return DataAccessHelper.GetSPParameters(connectionStringName, procName);
       }

       public DataSet ExecuteDataSet(SqlTransaction transaction, string procName, System.Data.SqlClient.SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteDataSet(transaction, procName, parameterValues);
       }

       public int ExecuteNonQuery(string connection, string procName)
       {
           return DataAccessHelper.ExecuteNonQuery(connection, procName);
       }

       public int ExecuteNonQuery(string connection, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteNonQuery(connection, procName, parameterValues);
       }

       public int ExecuteNonQuery(string connection, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteNonQuery(connection, procName, parameterValues);
       }

       public int ExecuteNonQuery(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteNonQuery(transaction, procName, parameterValues);
       }

       public int ExecuteNonQuery(SqlTransaction transaction, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteNonQuery(transaction, procName, parameterValues);
       }

       public int ExecuteNonQuery(string connection, string procName, int commandTimeout, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteNonQuery(connection, procName, commandTimeout, parameterValues);
       }

       public object ExecuteNonQuery(string connection, string procName, params DBParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteNonQuery(connection, procName, parameterValues);
       }

       public object ExecuteScalar(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteScalar(transaction, procName, parameterValues);
       }

       public object ExecuteScalar(SqlTransaction transaction, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteScalar(transaction, procName, parameterValues);
       }

       public object ExecuteScalar(string connection, string procName, params SqlParameter[] parameterValues)
       {
            return DataAccessHelper.ExecuteScalar(connection, procName, parameterValues);
       }

       public object ExecuteScalar(string connection, string procName, params DBParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteScalar(connection, procName, parameterValues);
       }

       public object ExecuteScalar(string connection, string procName)
       {
           return DataAccessHelper.ExecuteScalar(connection, procName);
       }

       public IDataReader ExecuteReader(string connection, string procName)
       {
           return DataAccessHelper.ExecuteReader(connection, procName);
       }

       public IDataReader ExecuteReaderDynamicSql(string connection, string sqlText)
       {
           return DataAccessHelper.ExecuteReaderDynamicSql(connection, sqlText);
       }

       public IDataReader ExecuteReader(string connection, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteReader(connection, procName, parameterValues);
       }

       public IDataReader ExecuteReader(string connection, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteReader(connection, procName, parameterValues);
       }

       public IDataReader ExecuteReader(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteReader(transaction, procName, parameterValues);
       }

       public IDataReader ExecuteReader(SqlTransaction transaction, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteReader(transaction, procName, parameterValues);
       }

       public XmlReader ExecuteXmlReader(string connection, string procName)
       {
           return DataAccessHelper.ExecuteXmlReader(connection, procName);
       }

       public XmlReader ExecuteXmlReader(string connection, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteXmlReader(connection, procName, parameterValues);
       }

       public XmlReader ExecuteXmlReader(string connection, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteXmlReader(connection, procName, parameterValues);
       }

       public XmlReader ExecuteXmlReader(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteXmlReader(transaction,  procName, parameterValues);
       }

       public XmlReader ExecuteXmlReader(SqlTransaction transaction, string procName, params object[] parameterValues)
       {
            return DataAccessHelper.ExecuteXmlReader(transaction, procName,parameterValues);
       }

       public XmlReader ExecuteXmlReader(SqlConnection connection, string procName)
       {
           return DataAccessHelper.ExecuteXmlReader(connection, procName);
       }

       public XmlReader ExecuteXmlReader(SqlConnection connection, string procName, params object[] parameterValues)
       {
           return DataAccessHelper.ExecuteXmlReader(connection, procName, parameterValues);
       }

       public XmlReader ExecuteXmlReader(SqlConnection connection, string procName, params SqlParameter[] parameterValues)
       {
           return DataAccessHelper.ExecuteXmlReader(connection, procName, parameterValues);
       }

       public SqlConnection CreateConnection(string connectionStringName)
       {
           return DataAccessHelper.CreateConnection(connectionStringName);
       }

       #endregion
    }
}
