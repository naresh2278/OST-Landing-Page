using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dell.B2BOnlineTools.Common.EF.Models
{
    //[Table("BasicCache")]
    public class BasicCache
    {
        [Key, Column(Order = 0)]
        public string Key { get; set; }
        [Column(Order =1)]
        public string Value { get; set; }
    }
}
