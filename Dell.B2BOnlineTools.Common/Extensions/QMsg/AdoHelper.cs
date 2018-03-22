

using Dell.B2BOnlineTools.Common.Models.QMsg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dell.B2BOnlineTools.Common.Extensions.QMsg
{
    public static partial class QMsgHelper
    {
        public static string ConnectionString(this Ado ado)
        {
            if (ado.IsConnectionEncrypted)
                return ado.ConnectionString.Decrypt();
            return ado.ConnectionString;
        }
        public static string CommandText(this Ado ado)
        {
            if (ado.IsCommandEncrypted)
                return ado.CommandText.Decrypt();
            return ado.CommandText;
        }
        public static CommandType CommandType(this Ado ado)
        {
            if (string.IsNullOrEmpty(ado.CommandType)) return System.Data.CommandType.Text;
            if (string.Compare(ado.CommandType.ToUpper(), Constants.QMsg.Ado.CommandType.StoredProcedure.ToUpper()) == 0)
                return System.Data.CommandType.StoredProcedure;
            return System.Data.CommandType.Text;
        }

        public static List<DataTable> SqlClient(this Ado ado, ref SqlParameter[] sqlParameters)
        {
            switch (ado.Type.ToLowerInvariant())
            {
                case Constants.QMsg.Ado.Type.MsSql:
                    return ado.MsSqlClient(ref sqlParameters);
                case Constants.QMsg.Ado.Type.MySql:

                    break;
            }        
            return null;
        }

        public static List<DataTable> MsSqlClient(this Ado ado, ref SqlParameter[] sqlParameters)
        {
            List<DataTable> dataTables = new List<DataTable>();
            using (SqlConnection con = new SqlConnection(ado.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(ado.CommandText(), con))
                {
                    cmd.CommandType = ado.CommandType();
                    cmd.Parameters.AddRange(sqlParameters);
                    try
                    {
                        con.Open();
                        switch (ado.Execute.ToLowerInvariant())
                        {
                            case Constants.QMsg.Ado.Execute.Reader:
                                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                                dataTables = reader.GetDataTables();
                                reader.Close();
                                break;
                            case Constants.QMsg.Ado.Execute.Scalar:
                                DataSet ds = new DataSet();
                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                                { adapter.Fill(ds); }
                                foreach(DataTable table in ds.Tables)
                                { dataTables.Add(table); }
                                break;
                            case Constants.QMsg.Ado.Execute.NonQuery:
                                int rows = cmd.ExecuteNonQuery();
                                DataTable dt2 = new DataTable();
                                dt2.Columns.Add(new DataColumn("Number Of Affected Rows", typeof(int)));
                                dt2.Rows.Add(rows);
                                dataTables.Add(dt2);
                                break;
                        }                        
                    }
                    catch (SqlException ex)
                    {
                        dataTables = null;
                        Console.WriteLine($"{ex.ToString()}");
                        throw;
                    }
                    finally
                    {
                        cmd.Cancel();
                        con.Close();
                    }
                }
            }
            return dataTables;
        }

        
    }//End of Class
}
