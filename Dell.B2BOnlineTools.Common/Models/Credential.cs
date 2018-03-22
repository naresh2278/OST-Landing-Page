using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Models
{
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public bool UseDefaultCredential { get; set; } = false;
    }
}
