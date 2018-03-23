using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.PremierTools.Entity.Model.premier
{
    public class ostpagemodel
    {
        public int premierpageid { get; set; }
        public int roleid { get; set; }
        public string pagetitle { get; set; }
        public bool isfullcatalog { get; set; }
        public bool istestpage { get; set; }
        public string customerid { get; set; }
        public string createdate { get; set; }
        public string modifydate { get; set; }
        public string status { get; set; }
        public bool ismigrated { get; set; }
        public string region { get; set; }
        public string countrycode { get; set; }
        public bool isglobalportal { get; set; }
        public bool isactive { get; set; }      
    }
}
