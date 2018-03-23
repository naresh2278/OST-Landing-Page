using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.PremierTools.Entity.Model.premier
{
    public class usermodel
    {
        public int premierpageid { get; set; }
        public int roleid { get; set; }
        public string rolename { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string pagetitle { get; set; }
        public string rostertypedescription { get; set; }
        public string country { get; set; }
        public string usertitle { get; set; }
        public string phonenumber { get; set; }
        public string category { get; set; }
        public bool isowner { get; set; }
        public bool ismanagepage { get; set; }
        public string countrycode { get; set; }
        public bool isactive { get; set; }
    }
}
