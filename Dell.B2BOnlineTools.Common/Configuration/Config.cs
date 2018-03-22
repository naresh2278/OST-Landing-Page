using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Pivotal.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Configuration
{
    public class Config
    {        
        public static T MapConfigServerData<T>() where T: class, new()
        {
            var opt = new T();
            ConfigurationBinder.Bind(Configuration, opt);
            return opt;
        } 

        public static T GetAppSettings<T>(string key, string defaultValue, Nullable<char> delimiter = null)
        {
            string value = string.Empty;
            if (Configuration[key] == null)
                value = ConfigurationManager.AppSettings[key] ?? defaultValue;
            else
                value = Configuration[key];

            if(delimiter.HasValue && !string.IsNullOrEmpty(value) && value.Contains(delimiter.Value.ToString()))
            {
                List<string> array = value.Split(delimiter.Value).ToList();                
                Type type = typeof(T);
                if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                {                    
                    Type itemType = type.GetGenericArguments()[0];
                    if (itemType != typeof(string))
                    {
                        Type listType = typeof(List<>).MakeGenericType(new[] { itemType });
                        IList list = (IList)Activator.CreateInstance(listType);
                        array.ForEach(x => list.Add(Convert.ChangeType(x, itemType)));
                        return (T)Convert.ChangeType(list, typeof(T));
                    }
                    return (T)Convert.ChangeType(array, typeof(T));
                }
            }
            return (T)Convert.ChangeType(value,typeof(T));
        }

        public static ConfigServerData ConfigServiceData() 
        {
            return MapConfigServerData<ConfigServerData>();
        }
        public static CloudFoundryServicesOptions CloudFoundryServices
        {
            get
            {
                var opt = new CloudFoundryServicesOptions();
                ConfigurationBinder.Bind(Configuration, opt);
                return opt;
            }
        }
        public static CloudFoundryApplicationOptions CloudFoundryApplication
        {
            get
            {
                var opt = new CloudFoundryApplicationOptions();
                ConfigurationBinder.Bind(Configuration, opt);
                return opt;
            }
        }
        public static ConfigServerClientSettingsOptions ConfigServerClientSettings
        {
            get
            {
                var opt = new ConfigServerClientSettingsOptions();
                ConfigurationBinder.Bind(Configuration, opt);
                return opt;               
            }
        }

        public static IConfigurationRoot Configuration { get; set; }
        public static bool IsPCF =>
            !string.IsNullOrWhiteSpace(GetEnvVariable(Constants.PCF.APPLICATION_ENV_VARIABLE_NAME)) ? true : false;

        public static string HOSTING_ENVIRONMENT
        {
            get
            {
                string value = string.Empty;
                if (IsPCF)
                    value = GetEnvVariable(Constants.HOSTING_ENVIRONMENT);
                else
                    value = Convert.ToString(ConfigurationManager.AppSettings[Constants.HOSTING_ENVIRONMENT]);
                return !string.IsNullOrWhiteSpace(value) ? value.ToLowerInvariant() : "development";
            }
        }

        public static bool IsRuntimeDotNetCore =>
            string.Compare(PlatformServices.Default.Application.RuntimeFramework.Identifier,
                                ".NETFramework", StringComparison.InvariantCultureIgnoreCase) == 0
                    && PlatformServices.Default.Application.RuntimeFramework.Version.Major == 4 ? false : true;

        private static string GetEnvVariable(string key)
        {
            string value = Environment.GetEnvironmentVariable(key);
            return !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
        }
    }
}
