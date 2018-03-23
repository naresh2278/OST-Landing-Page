using Dell.PremierTools.Entity.Model.config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.PremierTools.DataServices.PCS
{
    public interface iconfigurationdb
    {
        configurationmodel getpcslandingpagedetails(string customer_set_id);
    }
}
