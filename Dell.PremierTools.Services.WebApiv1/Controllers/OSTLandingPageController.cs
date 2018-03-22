using Dell.PremierTools.OstServices.Controllers;
using Dell.PremierTools.OstServices.WebAPi.App_Start;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dell.PremierTools.Services.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class OSTLandingPageController : BaseApiController
    {
        // GET api/values
        [HttpGet]
        [Route("example")]
        public IEnumerable<string> Get()
        {
            // throw new MyAppException("My Custom Exception");
            throw new System.Exception();
              //  return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
