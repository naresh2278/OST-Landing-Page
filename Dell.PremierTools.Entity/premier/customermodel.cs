using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.PremierTools.Entity.Model.premier
{
    public class customermodel
    {
        public int segmentid { get; set; }
        public string segmentname { get; set; }
        public string customername { get; set; }
        public bool istestpage { get; set; }
        public int regionid { get; set; }
        public int customerid { get; set; }
        public string createdate { get; set; }
        public string catalog { get; set; }
        public string language { get; set; }
        public bool isgp { get; set; }
        public string isdirect { get; set; }
        public string ischannel { get; set; }
        public bool isb2b { get; set; }
        public bool isbhc { get; set;  }
        public bool isshc { get; set; }
    }
}
