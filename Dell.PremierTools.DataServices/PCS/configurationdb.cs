using Dell.PremierTools.Common.Data;
using Dell.PremierTools.Entity.Model.config;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dell.PremierTools.DataServices.PCS
{
    public class configurationdb : iconfigurationdb
    {
      public  configurationmodel getpcslandingpagedetails(string customer_set_id)
        {
            IDataReader objdatareader = null;
            configurationmodel _configuration = new configurationmodel();

            try
            {
                string sprocname = "proc_get_pcs_landing_page_detail";
                SqlParameter[] paramArray = new SqlParameter[1];
                paramArray[0] = new SqlParameter("@customer_set_id", SqlDbType.VarChar);
                paramArray[0].Value = customer_set_id;

                objdatareader = DataAccessHelper.ExecuteReader(CommonConstant.ConnPCE, sprocname, paramArray);

                try
                {
                    while (objdatareader.Read())
                    {
                        _configuration.status = (string)objdatareader.GetValue(objdatareader.GetOrdinal("status"));
                        _configuration.displayid = (string)objdatareader.GetValue(objdatareader.GetOrdinal("display_id"));
                        _configuration.ordercodes = (string)objdatareader.GetValue(objdatareader.GetOrdinal("ordercodes"));
                        _configuration.isfullcatalog = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isfullcatalog"));
                    }

                }
                catch { }
            }
            finally
            {
                if (objdatareader != null)
                {
                    objdatareader.Close();
                }
            }
            return _configuration;
        }
    }
}