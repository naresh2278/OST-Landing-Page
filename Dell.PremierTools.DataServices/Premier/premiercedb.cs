using Dell.PremierTools.Common.Data;
using Dell.PremierTools.Entity.Model.premier;
using Dell.PremierTools.Entity.Model.viewmodel;
using System.Data;
using System.Data.SqlClient;

namespace Dell.PremierTools.DataServices.Premier
{
   public class premiercepage : Ipremiercepage
    {
       public landingpageviewmodel getpremierlandingpagedetails(string emailaddress, string customer_set_id)
        {
            IDataReader objdatareader = null;
            landingpageviewmodel _landingpageviewmodel = new landingpageviewmodel();

            try
            {  
                string sprocname = "proc_get_ost_landing_page_detail";

                SqlParameter[] paramArray = new SqlParameter[2];
                paramArray[0] = new SqlParameter("@emailaddress", SqlDbType.VarChar);
                paramArray[0].Value = emailaddress;
                paramArray[1] = new SqlParameter("@customer_set_id", SqlDbType.VarChar);
                paramArray[1].Value = customer_set_id;

                 objdatareader = DataAccessHelper.ExecuteReader(CommonConstant.ConnPCE, sprocname, paramArray);

                try
                {           // User      
                    while (objdatareader.Read())
                    {
                        _landingpageviewmodel._usermodel.premierpageid = (int)objdatareader.GetValue(objdatareader.GetOrdinal("premierpageid"));
                        _landingpageviewmodel._usermodel.pagetitle = (string)objdatareader.GetValue(objdatareader.GetOrdinal("pagetitle"));
                        _landingpageviewmodel._usermodel.roleid = (int)objdatareader.GetValue(objdatareader.GetOrdinal("roleid"));
                        _landingpageviewmodel._usermodel.rolename = (string)objdatareader.GetValue(objdatareader.GetOrdinal("role"));
                        _landingpageviewmodel._usermodel.firstname = (string)objdatareader.GetValue(objdatareader.GetOrdinal("firstname"));
                        _landingpageviewmodel._usermodel.lastname = (string)objdatareader.GetValue(objdatareader.GetOrdinal("lastname"));
                        _landingpageviewmodel._usermodel.phonenumber = (string)objdatareader.GetValue(objdatareader.GetOrdinal("phone_number"));
                        _landingpageviewmodel._usermodel.isowner = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("ispageowner"));
                        _landingpageviewmodel._usermodel.ismanagepage = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("ismanagepage"));
                        _landingpageviewmodel._usermodel.category = (string)objdatareader.GetValue(objdatareader.GetOrdinal("category"));
                        _landingpageviewmodel._usermodel.usertitle = (string)objdatareader.GetValue(objdatareader.GetOrdinal("usertitle"));
                        _landingpageviewmodel._usermodel.countrycode = (string)objdatareader.GetValue(objdatareader.GetOrdinal("countrycode"));
                    }
                    // Page Info
                    if (objdatareader.NextResult())
                    {
                        while (objdatareader.Read())
                        {
                          _landingpageviewmodel._ostpagemodel.premierpageid = (int)objdatareader.GetValue(objdatareader.GetOrdinal("premierpageid"));
                           _landingpageviewmodel._ostpagemodel.pagetitle = (string)objdatareader.GetValue(objdatareader.GetOrdinal("pagetitle"));
                            _landingpageviewmodel._ostpagemodel.isfullcatalog = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isfullcatalog"));
                            _landingpageviewmodel._ostpagemodel.istestpage = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("istestpage"));
                            _landingpageviewmodel._ostpagemodel.customerid = (string)objdatareader.GetValue(objdatareader.GetOrdinal("customer_id"));
                            _landingpageviewmodel._ostpagemodel.createdate = (string)objdatareader.GetValue(objdatareader.GetOrdinal("create_date"));
                            _landingpageviewmodel._ostpagemodel.modifydate = (string)objdatareader.GetValue(objdatareader.GetOrdinal("modify_date"));
                            _landingpageviewmodel._ostpagemodel.ismigrated = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("ismigrated"));
                            _landingpageviewmodel._ostpagemodel.region = (string)objdatareader.GetValue(objdatareader.GetOrdinal("region"));
                            _landingpageviewmodel._ostpagemodel.countrycode = (string)objdatareader.GetValue(objdatareader.GetOrdinal("countrycode"));
                            _landingpageviewmodel._ostpagemodel.isglobalportal = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("Isglobalportal"));
                            _landingpageviewmodel._ostpagemodel.isactive = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isactive"));
                        }
                    }

                    //Customer
                    if (objdatareader.NextResult())
                    {
                        while (objdatareader.Read())
                        {
                            _landingpageviewmodel._customermodel.customerid = (int)objdatareader.GetValue(objdatareader.GetOrdinal("customer_id"));
                            _landingpageviewmodel._customermodel.customername = (string)objdatareader.GetValue(objdatareader.GetOrdinal("customer_name"));
                            _landingpageviewmodel._customermodel.segmentid = (int)objdatareader.GetValue(objdatareader.GetOrdinal("segment_id"));
                            _landingpageviewmodel._customermodel.segmentname = (string)objdatareader.GetValue(objdatareader.GetOrdinal("segment_name"));
                            _landingpageviewmodel._customermodel.regionid = (int)objdatareader.GetValue(objdatareader.GetOrdinal("region_id"));
                            _landingpageviewmodel._customermodel.createdate = (string)objdatareader.GetValue(objdatareader.GetOrdinal("catalog"));
                            _landingpageviewmodel._customermodel.language = (string)objdatareader.GetValue(objdatareader.GetOrdinal("lng"));
                            _landingpageviewmodel._customermodel.isgp = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isGP"));
                            _landingpageviewmodel._customermodel.isdirect = (string)objdatareader.GetValue(objdatareader.GetOrdinal("isdirect"));
                            _landingpageviewmodel._customermodel.ischannel = (string)objdatareader.GetValue(objdatareader.GetOrdinal("ischannel"));
                            _landingpageviewmodel._customermodel.isb2b = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isb2b"));
                            _landingpageviewmodel._customermodel.isbhc = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isbhc"));
                            _landingpageviewmodel._customermodel.isshc = (bool)objdatareader.GetValue(objdatareader.GetOrdinal("isshc"));
                        }
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
            return _landingpageviewmodel;
        }
    }
}
