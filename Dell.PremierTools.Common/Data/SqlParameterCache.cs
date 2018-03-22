using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Dell.Premier.Common.Retry.Database;

namespace Dell.PremierTools.Common.Data
{
    internal sealed class SqlHelperParameterCache
    {
        // Fields
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        // Methods
        private SqlHelperParameterCache()
        {
        }

        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }
            string str = connectionString + ":" + commandText;
            paramCache[str] = commandParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            var parameterArray = new SqlParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (SqlParameter)((ICloneable)originalParameters[index]).Clone();
                index++;
            }
            return parameterArray;
        }

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (string.IsNullOrEmpty(spName))
            {
                throw new ArgumentNullException("spName");
            }
            var command = new SqlCommand(spName, connection) {CommandType = CommandType.StoredProcedure};
            connection.OpenWithRetry();
            SqlCommandBuilder.DeriveParameters(command);
            connection.Close();
            if (!includeReturnValueParameter)
            {
                command.Parameters.RemoveAt(0);
            }
            var parameterArray = new SqlParameter[command.Parameters.Count];
            command.Parameters.CopyTo((Array)parameterArray, 0);
            foreach (SqlParameter parameter in parameterArray)
            {
                parameter.Value = DBNull.Value;
            }
            return parameterArray;
        }

        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }
            var str = connectionString + ":" + commandText;
            var originalParameters = paramCache[str] as SqlParameter[];
            return originalParameters == null ? null : CloneParameters(originalParameters);
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            using (var connection2 = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(connection2, spName, includeReturnValueParameter);
            }
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(spName))
            {
                throw new ArgumentNullException("spName");
            }
            using (var connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }
        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (string.IsNullOrEmpty(spName))
            {
                throw new ArgumentNullException("spName");
            }
            var str = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            var originalParameters = paramCache[str] as SqlParameter[];
            if (originalParameters == null)
            {
                var parameterArray2 = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[str] = parameterArray2;
                originalParameters = parameterArray2;
            }
            return CloneParameters(originalParameters);
        }
    }

}