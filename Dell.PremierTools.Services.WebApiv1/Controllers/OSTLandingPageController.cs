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
                return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
