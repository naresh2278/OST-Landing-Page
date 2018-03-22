using Dell.PremierTools.Common.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Configuration;

namespace Dell.PremierTools.OstServices.Controllers
{
    public abstract class BaseApiController : Controller
    {
        protected ILogger logger { get; private set; }
        protected bool isInPCF
        {
            get
            {
                return !string.IsNullOrWhiteSpace(PCFConfiguration.GetEnvVariable(PCFConfiguration.APPLICATION_ENV_VARIABLE_NAME)) ? true : false;   
            }
        }

        protected BaseApiController()
        {
            logger = new NLogger().GetInstance;
            if (isInPCF) logger.LogDebug(this, null, "......In PCF......");
        }
        
        protected string AppSettingValue(string key)
        {
            if (isInPCF) logger.LogDebug(this, null, string.Format("Env[{0}]:{1}", key, PCFConfiguration.GetEnvVariable(key)));
            return isInPCF ? PCFConfiguration.GetEnvVariable(key) : Convert.ToString(ConfigurationManager.AppSettings[key]);
        }

        protected string MsgFormatter(int sequence, string caption, string requestId, object obj)
        {
            obj = obj ?? "Null Object";
            var jsonobj = new 
            {
                requestId = requestId,
                sequence = sequence,                
                caption = caption,                
                contentType = obj.GetType().Name,
                content = obj
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonobj).ToString();
        }

    }
}
