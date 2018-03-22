
using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Configuration
{
    public class ConfigServerData
    {
        public string Bar { get; set; }
        public string Foo { get; set; }
        public Info Info { get; set; }

        // Optional data from vault
        public string Vault { get; set; }
    }

    public class Info
    {
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
