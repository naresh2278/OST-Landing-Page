using Dell.Premier.Common.Retry.Database;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace Dell.PremierTools.Common.Data
{
    public static class DataAccessHelper
    {
        #region " iDAL code "

        static private readonly CacheManager ValueCache;
        static readonly CacheManagerFactory Cm;
        static int _iRetryInterval = 1000, _iMaxRetries = 1;

        
        static DataAccessHelper()
        {
            try
            {
                IConfigurationSource cs = new SystemConfigurationSource();
                Cm = new CacheManagerFactory(cs);
                ValueCache = (CacheManager)Cm.CreateDefault();
            }
            catch
            {
                throw;
            }
        }


        ///<summary>
        ///Function to retrieve an open SqlConnection to the currently active Heartbeat database server.
        /// </summary>
        /// <returns>SqlConnection</returns>
        private static SqlConnection GetActiveConnection()
        {
            SqlConnection cn = null;
            try
            {

                #region " Execution Logic "

                //Logic explained:
                //Establish a connection to the currently active Heartbeat server
                //Once the connection is opened determine the server name by finding the datasource.
                //Compare it to the value of the ActiveAppDBServer. If they are not the same, a server switch has occured.
                //Log this change as an error and assign the new value to ActiveAppDBServer.
                //All new connections would be routed to the newly active server till the newly active server faces any issues.
                //If the newly active server begins experiencing issues, the same process as outlined above will occur in reverse.

                #endregion

                // cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn_Heartbeat"].ConnectionString);
                cn = new SqlConnection(PCFSetting.ConnectionStringsValue("conn_Heartbeat"));
                PCFSetting.AppSettingValue("portalpagekey");
                cn.OpenWithRetry();
                
                return cn;
            }
            catch 
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
                throw;
            }
        }       

        /// <summary>
        /// This method removes a named item from the valuecache so that this item's current value will be fetched from the database instead from the cache.
        /// </summary>
        /// <param name="itemName">The name of the item to be removed from the cache.
        /// If the item to be removed is a connectionstring, this parameter name should be the connection string name.</param>
        /// <remarks>This method is to be used when a value is updated in the database so that the updated value is fetched and cached.</remarks>
        public static void RemoveItemFromCache(string itemName)
        {
            try
            {
                ValueCache.Remove(itemName);
            }
            catch
            {
                throw;
            }
        }

        public static void FlushCache()
        {
            ValueCache.Flush();
        }

        private static string GetConnectionStringBasedOnServiceAccount(string premierOwnedCatalog)
        {
            try
            {
                if(premierOwnedCatalog == "OST")
                    return PCFSetting.ConnectionStringsValue("metadatacontext");                
                else
                    return PCFSetting.ConnectionStringsValue("conn_Heartbeat");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieve Database name for the input key
        /// </summary>
        /// <param name="key">Connectionstring ame </param>
        /// <returns></returns>
        //private static string GetPremierOwnedCatalog(string key)
        //{
        //    string initialCatalog = null;

        //    switch (key)
        //    {
        //        case "DB_ICU":
        //        case "DB_PREMIER":
        //        case "DB_RLCM_STORESTAGING":
        //        case "DB_GLOBALPREMIER":
        //        case "DB_GLOBALRLCM_STORESTAGING":
        //        case "DB_GLOBALICU":
        //        case "DB_MasterPREMIERCE":
        //        case "conn_WorkflowApp":
        //        case "conn_PMU":
        //        case "conn_PCE":
        //        case "conn_ICU":
        //        case "conn_SS":
        //        case "conn_WorkflowApp_Staging":
        //        case "conn_PCE_Staging":
        //        case "conn_ICU_Staging":
        //        case "conn_SS_Staging":
        //        case "conn_PMU_Staging":
        //        case "dsn_Premier":
        //        case "HedgeRatesConnectionString":
        //        case "PremierCEConnectionString":
        //        case "conn_PCE_LongRunningQuery":
        //            {
        //                initialCatalog = "PremierCE";
        //                break;
        //            }

        //        case "conn_MyDell":
        //        case "conn_MyDell_Staging":
        //            {
        //                initialCatalog = "MyDell";
        //                break;
        //            }
        //        case "DB_CACHE_ACTIVE":
        //        case "DB_CACHE_US":
        //        case "DB_CACHE_EMEA":
        //        case "conn_DsnXmlCache":
        //        case "conn_CACHE":
        //        case "DB_CACHE_APJ":
        //            {
        //                initialCatalog = "XMLcache";
        //                break;
        //            }
        //        case "DB_CACHE_STAGING":
        //        case "DB_CACHE_GlobalStaging":
        //        case "conn_STGCACHE":
        //            {
        //                initialCatalog = "XMLCachePreview";
        //                break;
        //            }
        //        case "DB_APPLICATION":
        //        case "conn_ApplicationData":
        //            {
        //                initialCatalog = "ApplicationData";
        //                break;
        //            }
        //        case "MetadataContext":
        //            {
        //                initialCatalog = "OST";
        //                break;
        //            }

        //        default:
        //            initialCatalog = null;
        //            break;
        //    }

        //    return initialCatalog;
        //}

        /// <summary>
        /// Retrieves the connection string associated with the passed in Sql Server connectionstring name
        /// </summary>
        /// <param name="connectionStringIdentifier">The name of the connection string that needs to be retrieved</param>
        /// <returns>String ConnectionString</returns>
        private static String GetSQLServerConnectionString(string connectionStringIdentifier)
        {
            //Resetting the retry parameter values to default prior to each connection attempt.
            _iMaxRetries = 1;
            _iRetryInterval = 1000;
            SqlDataReader dr;
            SqlCommand cmd;
            string premierOwnedCatalog = null;

            // Below code added for the Service Account Login
            premierOwnedCatalog = connectionStringIdentifier;  //GetPremierOwnedCatalog(connectionStringIdentifier);

            //If the Retry Interval and the Maximum number of retries are cached, get them from the cache.
            if (ValueCache[connectionStringIdentifier + "RetryInterval"] != null && ValueCache[connectionStringIdentifier + "MaxRetries"] != null)
            {
                _iRetryInterval = Convert.ToInt32(ValueCache[connectionStringIdentifier + "RetryInterval"]);
                _iMaxRetries = Convert.ToInt32(ValueCache[connectionStringIdentifier + "MaxRetries"]);
            }
            //Else fetch them from the database and assign them to the cache.
            else
            {
                // Condition added by Ajesh for the Service Account Login
                if (!string.IsNullOrEmpty(premierOwnedCatalog))
                {
                    _iRetryInterval = ConfigurationManager.AppSettings["PremierDBRetryInterval"] == null ? _iRetryInterval : Convert.ToInt32(ConfigurationManager.AppSettings["PremierDBRetryInterval"]);
                    _iMaxRetries = ConfigurationManager.AppSettings["PremierDBMaxRetries"] == null ? _iMaxRetries : Convert.ToInt32(ConfigurationManager.AppSettings["PremierDBMaxRetries"]);

                    ValueCache.Add(connectionStringIdentifier + "RetryInterval", _iRetryInterval);
                    ValueCache.Add(connectionStringIdentifier + "MaxRetries", _iMaxRetries);
                }
                else
                {
                    using (SqlConnection retryConnection = GetActiveConnection())
                    {
                        cmd = new SqlCommand("proc_sel_connection_retry_parameters", retryConnection) { CommandType = CommandType.StoredProcedure };

                        //The active application server name will also be used to determine the set of information that the following code should fetch from the database.
                        //This logic will be implemented in the SP.
                        cmd.Parameters.Add(new SqlParameter("ConnectionStringName", connectionStringIdentifier) { Direction = ParameterDirection.Input });


                        using (dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            //Code to assemble the connection string.
                            if (dr.Read())
                            {
                                if (!string.IsNullOrEmpty(dr["RetryInterval"].ToString()))
                                {
                                    _iRetryInterval = (Int32)dr["RetryInterval"];
                                }



                                if (!string.IsNullOrEmpty(dr["MaxRetries"].ToString()))
                                {
                                    _iMaxRetries = (Int32)dr["MaxRetries"];

                                }
                            }

                        }
                        ValueCache.Add(connectionStringIdentifier + "RetryInterval", _iRetryInterval);
                        ValueCache.Add(connectionStringIdentifier + "MaxRetries", _iMaxRetries);
                    }
                }
            }

            #region " Code to assemble connectionstring from the database "

            //if the connectionstring is already cached, get it from the cache.
            if (ValueCache.GetData(connectionStringIdentifier) != null)
            {
                return ValueCache[connectionStringIdentifier].ToString();
            }
            //else, get it from the database.
            else
            {
                var connectionString = new SqlConnectionStringBuilder();
                dr = null;

                /* ***************************** Start - Code by Ajesh ***************************** */
                /* This code implemented for the sql server service account login,
                 * The new connection string build using heartbeat connection string */
                if (!string.IsNullOrEmpty(premierOwnedCatalog))
                {
                    connectionString.ConnectionString = GetConnectionStringBasedOnServiceAccount(premierOwnedCatalog);
                    connectionString.InitialCatalog = premierOwnedCatalog;

                    //Add the connectionstring to the cache so that future requests do not need to hit the database.
                    int refreshInterval = ConfigurationManager.AppSettings["connectionstringCacheExpirationInterval"] != null ? Int32.Parse(ConfigurationManager.AppSettings["connectionstringCacheExpirationInterval"]) : 600;
                    ValueCache.Add(connectionStringIdentifier, connectionString.ToString(), CacheItemPriority.Normal, null, new AbsoluteTime(new TimeSpan(0, 0, refreshInterval)));

                    return connectionString.ToString();
                }
                /* ***************************** End - Code by Ajesh ***************************** */

                using (var heartbeatConnection = GetActiveConnection())
                {
                    cmd = new SqlCommand("proc_sel_sqlconnectionstringparameters", heartbeatConnection) { CommandType = CommandType.StoredProcedure };


                    //The active application server name will also be used to determine the set of information that the following code should fetch from the database.
                    //This logic will be implemented in the SP.
                    cmd.Parameters.Add(new SqlParameter("ConnectionStringName", connectionStringIdentifier) { Direction = ParameterDirection.Input });


                    using (dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        //Code to assemble the connection string.
                        if (dr.Read())
                        {
                            //Build the sql server connectionstring
                            connectionString.DataSource = dr["DatabaseServerName"].ToString();
                            connectionString.InitialCatalog = dr["DatabaseName"].ToString();
                            if (dr["FailoverPartner"] != null && dr["FailoverPartner"].ToString().Length != 0)
                            {
                                connectionString.FailoverPartner = dr["FailoverPartner"].ToString();
                            }
                            connectionString.UserID = dr["UserID"].ToString();
                            connectionString.Password = dr["Password"].ToString();
                            if (dr["ConnectTimeout"] != null && dr["ConnectTimeout"].ToString().Length != 0)
                            //Cannot use the ternary operator here as Int32 and null value are incompatible options.
                            {
                                connectionString.ConnectTimeout = (Int32)dr["ConnectTimeout"];

                            }

                            string strConnectionString;
                            strConnectionString = connectionString.ToString();

                            if (dr.GetSchemaTable().Columns.Contains("MultiSubnetFailover"))
                            {
                                if (dr["MultiSubnetFailover"] != null && dr["MultiSubnetFailover"].ToString().Length != 0)
                                {
                                    bool multiSubnetFailover;
                                    bool.TryParse(dr["MultiSubnetFailover"].ToString(), out multiSubnetFailover);
                                    strConnectionString.Insert(connectionString.ToString().Length - 1, " ;MultiSubnetFailover=" + multiSubnetFailover);
                                }
                            }
                            //Add the connectionstring to the cache so that future requests do not need to hit the database.
                            var refreshInterval = ConfigurationManager.AppSettings["connectionstringCacheExpirationInterval"] != null
                                                      ? Int32.Parse(ConfigurationManager.AppSettings["connectionstringCacheExpirationInterval"])
                                                      : 600;
                            ValueCache.Add(connectionStringIdentifier, strConnectionString, CacheItemPriority.Normal, null,
                                           new AbsoluteTime(new TimeSpan(0, 0, refreshInterval)));
                            // Above code will be used later
                            return connectionString.ToString();
                        }
                        //There is no data corresponding to the supplied connectionstring name. 
                        //Throw an exception to indicate that the connectionstring name is invalid.
                        //If this exception is thrown, it means that the heartbeat database has not been updated / the passed in connectionstring name is incorrect.
                        throw new DataException("There is no connectionstring named " + connectionStringIdentifier + " defined.");
                    }
                }
            }

            #endregion
        } 
        

        /// <summary>
        /// This method checks for database failover and returns a connection to the requested database on the current principal server.
        /// </summary>
        /// <param name="db">The SQL Database instance passed by ref to associate the connection to.</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        private static void CheckForFailover(ref SqlDatabase db, string connectionStringName)
        {
            db = new SqlDatabase(GetSQLServerConnectionString(connectionStringName));

            try
            {
                using (var cn = (SqlConnection)db.CreateConnection())
                {
                    cn.OpenWithRetry();
                }
                return;
            }
            catch 
            {
                throw;
            }

            throw new DataException("Could not connect to the database");
        }

        /// <summary>
        /// This method is thin wrapper to the GetSpParameterSet method of the SqlHelperParameterCache class
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="procName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        private static SqlParameter[] CreateSqlParameters(SqlConnection cn, string procName, params object[] parameterValues)
        {
            SqlParameter[] sqlParamerValues = SqlHelperParameterCache.GetSpParameterSet(cn, procName);
            int paramCount = parameterValues.Length;
            for (int i = 0; i < paramCount; i++)
            {
                sqlParamerValues[i].Value = parameterValues[i];
            }
            return sqlParamerValues;
        }

        /// <summary>
        /// Do not use.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static String GetStringValue(String identifier)
        {
            if (ValueCache.GetData(identifier) != null)
            {
                return ValueCache[identifier].ToString();
            }
            SqlDataReader dr;
            SqlCommand cmd = null;

            using (var heartbeatConnection = GetActiveConnection())
            {
                cmd = new SqlCommand("proc_sel_stringvalue", heartbeatConnection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("identifier", identifier) { Direction = ParameterDirection.Input });

                using (dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (dr.Read())
                    {

                        //Add the connectionstring to the cache so that future requests do not need to hit the database.
                        ValueCache.Add(identifier, dr["stringvalue"].ToString());
                        return dr[0].ToString();
                    }
                    else
                    {
                        //There is no data corresponding to the supplied identifier name. 
                        //Throw an exception to indicate that the identifier name is invalid.
                        //If this exception is thrown, it means that the heartbeat database has not been updated / the passed in identifier name is incorrect.
                        throw new DataException("There is no setting named " + identifier + " defined.");
                    }
                }
            }
        }

        /// <summary>
        /// This method adds a ConnectionString to the ApplicationDatabase (aka HeartBeat database) 
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <param name="connectionString"></param>
        public static void CreateConnectionString(String connectionStringName, String connectionString)
        {
            SqlConnectionStringBuilder scb = new SqlConnectionStringBuilder(connectionString);
            //TODO: Add code to add the Connectionstring to the database.
            //Remove both the connectionstring and primary server name from the cache so that current values from the database.
            RemoveItemFromCache(connectionStringName);
            RemoveItemFromCache(connectionStringName + "Server");
        }
        #endregion

        /// <summary>
        /// This method creates a connection to the database pointed to by the connectionstringname
        /// </summary>
        /// <returns>SqlConnection</returns>
        public static SqlConnection CreateConnection(String connectionStringName)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                var connection = (SqlConnection)db.CreateConnection();
                
                return connection;
            }
            catch 
            {              
     
                throw;
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues">Stored procedure parameters</param>
        /// <returns>Dataset</returns>
        public static DataSet ExecuteDataSet(string connectionStringName, string procName, params object[] parameterValues)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                var dataset = db.ExecuteDataSet(procName, parameterValues);
                
                return dataset;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues">Stored procedure parameters</param>
        /// <returns>Dataset</returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, string procName, params object[] parameterValues)
        {
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                var sqlparameterValues = CreateSqlParameters(transaction.Connection, procName, parameterValues);
                command.Parameters.AddRange(sqlparameterValues);
                var da = new SqlDataAdapter(command);
                var ds = new DataSet();
                da.Fill(ds);
                
                return ds;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionStringName, string procName, params SqlParameter[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);

                dbCommand = db.GetStoredProcCommand(procName);

                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var parameter in parameterValues)
                {
                    if (parameter != null)
                    {
                        parameter.Value = parameter.Value ?? DBNull.Value;

                    }
                }
                dbCommand.Parameters.AddRange(parameterValues);
                var dataSet = db.ExecuteDataSet(dbCommand);
                
                return dataSet;
            }
            catch 
            {     
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has no input/out parameters
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="procName">The name of the stored procedure to be executed.</param>
        /// <returns>
        /// Dataset
        /// </returns>
        public static DataSet ExecuteDataSet(string connectionStringName, string procName)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                dbCommand = db.GetStoredProcCommand(procName);
                var dataSet = db.ExecuteDataSet(dbCommand);
                
                return dataSet;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }

        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters types
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues">List of Parameters.</param>
        /// <returns>
        /// integer value
        /// </returns>
        public static DataSet ExecuteDataSet(string connectionStringName, string procName, params DBParameter[] parameterValues)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);

                var dbCommand = db.GetStoredProcCommand(procName);
                if (parameterValues != null)
                {
                    foreach (var parameter in parameterValues)
                    {
                        switch (parameter.Direction)
                        {
                            case ParameterDirection.Input:
                                db.AddInParameter(dbCommand, parameter.Name, parameter.DataType, parameter.Value);
                                break;
                            case ParameterDirection.Output:
                                db.AddOutParameter(dbCommand, parameter.Name, parameter.DataType, parameter.Size);
                                break;
                        }
                    }
                }

                var ds = db.ExecuteDataSet(dbCommand);

                if (parameterValues != null)
                    foreach (var parameter in parameterValues.Where(t => t.Direction == ParameterDirection.Output))
                    {
                        parameter.Value = db.GetParameterValue(dbCommand, parameter.Name);
                    }
                
                return ds;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Executes a proc with optional parameters and ands the resulting DataTable to the DataSet naming it according to the tablename parameter
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="dataset">The dataset.</param>
        /// <param name="parameterValues">The parameter values.</param>
        public static void LoadDataSet(string connectionStringName, string procName, string tablename, DataSet dataset, params object[] parameterValues)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                using (var dbCommand = (parameterValues == null ? db.GetStoredProcCommand(procName) : db.GetStoredProcCommand(procName, parameterValues)))
                {
                    db.LoadDataSet(dbCommand, dataset, tablename);
                }
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Wraps around a derived class's implementation of the GetStoredProcCommandWrapper method and adds functionality for using this method with UpdateDataSet. 
        /// The GetStoredProcCommandWrapper method that takes a params array expects the array to be filled with VALUES for the parameters. 
        /// This method differs from the GetStoredProcCommandWrapper method in that it allows a user to pass in a string array. 
        /// It will also dynamically discover the parameters for the stored procedure and set the parameter's SourceColumns to the strings that are passed in. 
        /// It does this by mapping the parameters to the strings IN ORDER. Thus, order is very important. 
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="sourceColumns">The list of DataFields for the procedure.</param>
        /// <returns></returns>
        public static DbCommand GetStoredProcCommand(string connectionStringName, string storedProcedureName, params string[] sourceColumns)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                return db.GetStoredProcCommandWithSourceColumns(storedProcedureName, sourceColumns);
            }
            catch 
            {
               

                throw;
            }
        }

        /// <summary>
        /// Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="dataset">The dataset.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="updateBehavior">The update behavior.</param>
        public static int UpdateDataset(string connectionStringName, DataSet dataset, string tableName, DbCommand insertCommand, DbCommand deleteCommand, DbCommand updateCommand, UpdateBehavior updateBehavior)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                return db.UpdateDataSet(dataset, tableName, insertCommand, deleteCommand, updateCommand, updateBehavior);
            }
            catch 
            {
               
                throw;
            }
        }

        /// <summary>
        /// Gets the SP parameters.
        /// </summary>
        /// <param name="connectionStringName">The name of the ConnectionString that is used to access the database.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <returns></returns>
        public static SqlParameter[] GetSPParameters(String connectionStringName, String procName)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connectionStringName);
                var cn = (SqlConnection)db.CreateConnection();
                var paramSet = SqlHelperParameterCache.GetSpParameterSet(cn, procName);
                
                return paramSet;
            }
            catch 
            {
              
                throw;
            }
        }

        /// <summary>
        /// Ignore this method for now
        /// 
        /// </summary>
        /// 
        /// <returns>integer value</returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, String procName, SqlParameter[] parameterValues)
        {
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(parameterValues);
                var da = new SqlDataAdapter(command);
                var ds = new DataSet();
                da.Fill(ds);
                
                return ds;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has no input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param>        
        /// <returns>integer value</returns>
        public static int ExecuteNonQuery(string connection, string procName)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                dbCommand = db.GetStoredProcCommand(procName);
                var nonQuery = db.ExecuteNonQuery(dbCommand);
                
                return nonQuery;
            }
            catch 
            {
                
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static int ExecuteNonQuery(string connection, string procName, params object[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName, parameterValues);
                var nonQuery = db.ExecuteNonQuery(dbCommand);
                
                return nonQuery;
            }
            catch 
            {                  

                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static int ExecuteNonQuery(string connection, string procName, params SqlParameter[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                dbCommand = db.GetStoredProcCommand(procName);
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var parameter in parameterValues)
                {
                    if (parameter != null)
                    {
                        parameter.Value = parameter.Value ?? DBNull.Value;
                    }
                }
                dbCommand.Parameters.AddRange(parameterValues);
                var nonQuery = db.ExecuteNonQuery(dbCommand);
                return nonQuery;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
        {
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                var command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var parameter in parameterValues.Where(sp => sp != null))
                {
                    parameter.Value = parameter.Value ?? DBNull.Value;
                }
                command.Parameters.AddRange(parameterValues);
                var nonQuery = command.ExecuteNonQuery();
                
                return nonQuery;
            }
            catch 
            {
                throw;
            }
        }

        public static int ExecuteNonQuery(string connectionString, string procName, int timeout, IEnumerable<string> customersetList)
        {
            SqlConnection connection = null;
            try
            {
                int nonQuery;
                using (connection = CreateConnection(connectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.OpenWithRetry();
                    }
                    SqlCommand command;
                    using (command = connection.CreateCommand())
                    {
                        command.CommandText = procName;
                        command.CommandType = CommandType.StoredProcedure;

                        var parameter = command.Parameters.AddWithValue("@CustomerSet", CreateDataTable(customersetList));

                        if (parameter != null)
                        {
                            parameter.SqlDbType = SqlDbType.Structured;
                            parameter.TypeName = "dbo.CustomerSetTableType";
                        }

                        nonQuery = command.ExecuteNonQuery();
                    }
                }
                return nonQuery;
            }
            catch 
            {
                     
                throw;
            }
        }

        private static DataTable CreateDataTable(IEnumerable<string> customerSetIds)
        {
            var table = new DataTable();
            table.Columns.Add("customer_set_id", typeof(string));
            if (customerSetIds != null)
                foreach (var customersetId in customerSetIds)
                {
                    table.Rows.Add(customersetId);
                }
            return table;
        }


        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, string procName, params object[] parameterValues)
        {
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                SqlParameter[] sqlparameterValues = CreateSqlParameters(transaction.Connection, procName, parameterValues);
                command.Parameters.AddRange(sqlparameterValues);
                
                //command.Parameters.AddRange(parameterValues);
                var nonQuery = command.ExecuteNonQuery();
                
                return nonQuery;

            }
            catch 
            {
                    
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static int ExecuteNonQuery(string connection, string procName, int commandTimeout, params object[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName, parameterValues);
                dbCommand.CommandTimeout = commandTimeout;
                var nonQuery = db.ExecuteNonQuery(dbCommand);
                return nonQuery;
            }
            catch 
            {        
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters types
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param> 
        /// <param name="parameterValues">List of Parameters.</param>
        /// <returns>integer value</returns>
        public static object ExecuteNonQuery(string connection, string procName, params DBParameter[] parameterValues)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                DbCommand dbCommand = db.GetStoredProcCommand(procName);
                if (parameterValues != null)
                {
                    foreach (var parameter in parameterValues)
                    {
                        switch (parameter.Direction)
                        {
                            case ParameterDirection.Input:
                                db.AddInParameter(dbCommand, parameter.Name, parameter.DataType,
                                                  parameter.Value);
                                break;
                            case ParameterDirection.Output:
                                db.AddOutParameter(dbCommand, parameter.Name, parameter.DataType, parameter.Size);
                                break;
                        }
                    }
                }

                object obj = db.ExecuteNonQuery(dbCommand);

                if (parameterValues != null)
                    foreach (var parameter in
                        parameterValues.Where(parameter => parameter.Direction == ParameterDirection.Output))
                    {
                        parameter.Value = db.GetParameterValue(dbCommand, parameter.Name);
                    }
                
                return obj;
            } 
            catch 
            {
                 
                throw;
            }
        }

        /// <summary>
        /// Wrapper function for Executing a SQL stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static object ExecuteScalar(string connection, string procName, params object[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName, parameterValues);
                var scalar = db.ExecuteScalar(dbCommand);
                
                return scalar;
            }
            catch 
            {                
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing a SQL stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static object ExecuteScalar(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
        {
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };

                command.Parameters.AddRange(parameterValues);

                var scalar = command.ExecuteScalar();
                
                return scalar;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing a SQL stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static object ExecuteScalar(SqlTransaction transaction, string procName, params object[] parameterValues)
        {
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };

                SqlParameter[] sqlparameterValues = CreateSqlParameters(transaction.Connection, procName, parameterValues);
                command.Parameters.AddRange(sqlparameterValues);
                //command.Parameters.AddRange(parameterValues);

                var scalar = command.ExecuteScalar();
                
                return scalar;
            } 
            catch 
            {
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing a SQL stored procedure which has input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param>
        /// <param name="parameterValues"></param>
        /// <returns>integer value</returns>
        public static object ExecuteScalar(string connection, string procName, params SqlParameter[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                dbCommand = db.GetStoredProcCommand(procName);
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var parameter in parameterValues.Where(sp => sp != null))
                {
                    parameter.Value = parameter.Value ?? DBNull.Value;
                }
                dbCommand.Parameters.AddRange(parameterValues);
                var scalar = db.ExecuteScalar(dbCommand);
                
                return scalar;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has input/out parameters types
        /// </summary>
        /// <param name="connection">A named connectionstring to the database</param>
        /// <param name="procName">Stored Procedure name</param> 
        /// <param name="parameterValues">List of Parameters.</param>
        /// <returns>integer value</returns>
        public static object ExecuteScalar(string connection, string procName, params DBParameter[] parameterValues)
        {
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                var dbCommand = db.GetStoredProcCommand(procName, parameterValues);
                if (parameterValues != null)
                {
                    foreach (var parameter in parameterValues)
                    {
                        switch (parameter.Direction)
                        {
                            case ParameterDirection.Input:
                                db.AddInParameter(dbCommand, parameter.Name, parameter.DataType,
                                                  parameter.Value);
                                break;
                            case ParameterDirection.Output:
                                db.AddOutParameter(dbCommand, parameter.Name, parameter.DataType, parameter.Size);
                                break;
                        }
                    }
                }

                var obj = db.ExecuteScalar(dbCommand);

                if (parameterValues != null)
                    foreach (var parameter in parameterValues)
                    {
                        if (parameter.Direction == ParameterDirection.Output)
                        {
                            parameter.Value = db.GetParameterValue(dbCommand, parameter.Name);
                        }
                    }
                
                return obj;
            }
            catch 
            {
            
                throw;
            }
        }

        /// <summary>
        /// Wrapper function for Executing the stored procedure which has no input/out parameters 
        /// </summary>
        /// <param name="connection">Connection to the database</param>
        /// <param name="procName">Stored Procedure name</param>        
        /// <returns>integer value</returns>
        public static object ExecuteScalar(string connection, string procName)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName);
                var scalar = db.ExecuteScalar(dbCommand);
                
                return scalar;
            } 
            catch 
            {
            
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the stored procedure that is passed in as the argument and returns a Datareader that contains the results.
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <returns>IDataReader with the results.</returns>
        public static IDataReader ExecuteReader(string connection, string procName)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                {
                    dbCommand = db.GetStoredProcCommand(procName);
                    var reader = db.ExecuteReader(dbCommand);
                    
                    return reader;
                }
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the sql command text that is passed in as the argument and returns a Datareader that contains the results.
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="sqlText">The sql statement that needs to be executed.</param>
        /// <returns></returns>
        public static IDataReader ExecuteReaderDynamicSql(string connection, string sqlText)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetSqlStringCommand(sqlText);
                var reader = db.ExecuteReader(dbCommand);
                
                return reader;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }


        /// <summary>
        /// Executes the stored procedure that is passed in as the argument and returns a Datareader that contains the results.
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <param name="parameterValues">Collection of Parameters to be passed into the Stored Procedure.</param>
        /// <returns>IDataReader with the results.</returns>
        public static IDataReader ExecuteReader(string connection, string procName, params object[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName, parameterValues);
                var reader = db.ExecuteReader(dbCommand);
                
                return reader;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the stored procedure that is passed in as the argument and returns a Datareader that contains the results.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string connection, string procName, params SqlParameter[] parameterValues)
        {
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                dbCommand = db.GetStoredProcCommand(procName);
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var sp in parameterValues)
                {
                    if (sp != null)
                    {
                        sp.Value = sp.Value ?? DBNull.Value;
                    }
                }
                dbCommand.Parameters.AddRange(parameterValues);
                var reader = db.ExecuteReader(dbCommand);
                
                return reader;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the stored procedure that is passed in as the argument and returns a Datareader that contains the results.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
        {
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (SqlParameter sp in parameterValues.Where(sp => sp != null))
                {
                    sp.Value = sp.Value ?? DBNull.Value;
                }
                command.Parameters.AddRange(parameterValues);
                var reader = command.ExecuteReader();
                
                return reader;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }


        /// <summary>
        /// Executes the stored procedure that is passed in as the argument and returns a Datareader that contains the results.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(SqlTransaction transaction, string procName, params object[] parameterValues)
        {
            SqlCommand command = null;

            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                SqlParameter[] sqlparameterValues = CreateSqlParameters(transaction.Connection, procName, parameterValues);
                command.Parameters.AddRange(sqlparameterValues);
                var reader = command.ExecuteReader();
                
                return reader;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (command != null) command.Parameters.Clear();
            }
        }


        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <returns>XmlReader with results</returns>
        public static XmlReader ExecuteXmlReader(string connection, string procName)
        {
            XmlReader xmlReader = null;
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName);
                xmlReader = db.ExecuteXmlReader(dbCommand);
                
                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();

                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <param name="parameterValues">Collection of Parameters to be passed into the Stored Procedure.</param>
        /// <returns>XmlReader with results</returns>

        public static XmlReader ExecuteXmlReader(string connection, string procName, params object[] parameterValues)
        {
            XmlReader xmlReader = null;
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);
                dbCommand = db.GetStoredProcCommand(procName, parameterValues);
                xmlReader = db.ExecuteXmlReader(dbCommand);

                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
                throw;
            }
            finally
            {

                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }
        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <param name="procParameters">Collection of Parameters to be passed into the Stored Procedure.</param>
        /// <returns>XmlReader with results</returns>

        public static XmlReader ExecuteXmlReaderTVP(string connection, string procName, Hashtable procParameters)
        {
            XmlReader xmlReader = null;            
            SqlDatabase db = null;
            SqlCommand dbCommand = null;
            try       
            {
                SqlConnection conection = null;
                using (conection = CreateConnection(connection))
                {
                    CheckForFailover(ref db, connection);
                    if (conection.State != ConnectionState.Open)
                    {
                        conection.OpenWithRetry();
                    }
                    
                    using (dbCommand = conection.CreateCommand())
                    {
                        dbCommand.CommandText = procName;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        foreach (DictionaryEntry param in procParameters)
                        {
                            dbCommand.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                        }
                        xmlReader = db.ExecuteXmlReader(dbCommand);
                    }
                }
                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
                throw;
            }
            finally
            {

                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <param name="parameterValues">Collection of SqlParameters to be passed into the Stored Procedure.</param>
        /// <returns>XmlReader with results</returns>

        public static XmlReader ExecuteXmlReader(string connection, string procName, params SqlParameter[] parameterValues)
        {
            XmlReader xmlReader = null;
            DbCommand dbCommand = null;
            SqlDatabase db = null;
            try
            {
                CheckForFailover(ref db, connection);

                dbCommand = db.GetStoredProcCommand(procName);
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var parameter in parameterValues.Where(sp => sp != null))
                {
                    parameter.Value = parameter.Value ?? DBNull.Value;
                }
                dbCommand.Parameters.AddRange(parameterValues);
                xmlReader = db.ExecuteXmlReader(dbCommand);
                dbCommand.Parameters.Clear();
                
                return xmlReader;

            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
                throw;
            }
            finally
            {

                //This eliminates the "The SqlParameter is already contained by another SqlParameterCollection." error.
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="procName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string procName, params SqlParameter[] parameterValues)
        {
            XmlReader xmlReader = null;
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                //The following code block handles SqlParameters with null values but replacing them with DBNull.

                foreach (var parameter in parameterValues.Where(sp => sp != null))
                {
                    parameter.Value = parameter.Value ?? DBNull.Value;
                }
                command.Parameters.AddRange(parameterValues);
                xmlReader = command.ExecuteXmlReader();
                command.Parameters.Clear();
                
                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
             
                throw;
            }
            finally
            {

                if (command != null) command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string procName, params object[] parameterValues)
        {
            XmlReader xmlReader = null;
            SqlCommand command = null;
            try
            {
                if (transaction.Connection.State != ConnectionState.Open)
                {
                    transaction.Connection.OpenWithRetry();
                }
                command = new SqlCommand(procName, transaction.Connection, transaction) { CommandType = CommandType.StoredProcedure };
                //command.Parameters.AddRange(parameterValues);
                SqlParameter[] sqlparameterValues = CreateSqlParameters(transaction.Connection, procName, parameterValues);
                command.Parameters.AddRange(sqlparameterValues);
                xmlReader = command.ExecuteXmlReader();
                command.Parameters.Clear();
                
                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
                throw;
            }
            finally
            {

                if (command != null) command.Parameters.Clear();
            }
        }

        #region "New methods added on R1 Launch per Mayank's request"

        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <returns>XmlReader with results</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, string procName)
        {
            XmlReader xmlReader = null;
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = new SqlCommand(procName, connection) { CommandType = CommandType.StoredProcedure };
                xmlReader = dbCommand.ExecuteXmlReader();
                
                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
                throw;
            }
            finally
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <param name="parameterValues">Collection of Parameters to be passed into the Stored Procedure.</param>
        /// <returns>XmlReader with results</returns>

        public static XmlReader ExecuteXmlReader(SqlConnection connection, string procName, params object[] parameterValues)
        {
            XmlReader xmlReader = null;
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = new SqlCommand(procName, connection) { CommandType = CommandType.StoredProcedure };
                dbCommand.Parameters.AddRange(CreateSqlParameters(connection, procName, parameterValues));
                xmlReader = dbCommand.ExecuteXmlReader();
                
                return xmlReader;
            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
                throw;
            }
            finally
            {

                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }


        /// <summary>
        /// Executes the StoredProcedure that is passed in and returns the results in an XmlReader
        /// </summary>
        /// <param name="connection">The name of the connection string to use to establish connection to the database.</param>
        /// <param name="procName">The name of the Stored Procedure that needs to be executed.</param>
        /// <param name="parameterValues">Collection of SqlParameters to be passed into the Stored Procedure.</param>
        /// <returns>XmlReader with results</returns>

        public static XmlReader ExecuteXmlReader(SqlConnection connection, string procName, params SqlParameter[] parameterValues)
        {
            XmlReader xmlReader = null;
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = new SqlCommand(procName, connection) { CommandType = CommandType.StoredProcedure };
                //The following code block handles SqlParameters with null values but replacing them with DBNull.
                foreach (var parameter in parameterValues.Where(sp => sp != null))
                {
                    parameter.Value = parameter.Value ?? DBNull.Value;
                }
                dbCommand.Parameters.AddRange(parameterValues);
                xmlReader = dbCommand.ExecuteXmlReader();
                dbCommand.Parameters.Clear();
                
                return xmlReader;

            }
            catch 
            {
                if (xmlReader != null)
                    xmlReader.Close();
               
                throw;
            }
            finally
            {

                //This eliminates the "The SqlParameter is already contained by another SqlParameterCollection." error.
                if (dbCommand != null) dbCommand.Parameters.Clear();
            }
        }
        #endregion

    }
}
