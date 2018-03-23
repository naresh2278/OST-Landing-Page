using Dell.PremierTools.Entity.Model.config;
using Dell.PremierTools.Entity.Model.premier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.PremierTools.Entity.Model.viewmodel
{
   public class landingpageviewmodel
    {
        public customermodel _customermodel { get; set; }
        public ostpagemodel _ostpagemodel { get; set; }
        public usermodel _usermodel { get; set; }
        public configurationmodel _configurationmodel { get; set; }
    }
}
