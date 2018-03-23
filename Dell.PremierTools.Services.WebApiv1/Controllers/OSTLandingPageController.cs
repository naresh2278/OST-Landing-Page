using Dell.PremierTools.DataServices.PCS;
using Dell.PremierTools.DataServices.Premier;
using Dell.PremierTools.Entity.Model.viewmodel;
using Dell.PremierTools.OstServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dell.PremierTools.Services.WebApi.Controllers
{
    [Route(CommonConstant.GETDetails)]
    public class OSTLandingPageController : BaseApiController
    {
        // GET api/values
        [HttpGet]
        public landingpageviewmodel Get(string emailaddress, string customer_set_id)
        {
            landingpageviewmodel _landingpageviewmodel = new landingpageviewmodel();
            Ipremiercepage _premiercedb = new premiercepage();
            _landingpageviewmodel = _premiercedb.getpremierlandingpagedetails(emailaddress, customer_set_id);

            iconfigurationdb _config = new configurationdb();
            _landingpageviewmodel._configurationmodel = _config.getpcslandingpagedetails(customer_set_id);
            return _landingpageviewmodel;

        }        
    }
}
