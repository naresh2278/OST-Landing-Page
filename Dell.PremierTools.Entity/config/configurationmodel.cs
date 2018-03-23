using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.PremierTools.Entity.Model.config
{
    public class configurationmodel
    {

        public configurationmodel()
        {
        }
        public string displayid { get; set; }
        public string status { get; set; }
        public string ordercodes { get; set; }
        public bool isfullcatalog { get; set; }
    }
}
