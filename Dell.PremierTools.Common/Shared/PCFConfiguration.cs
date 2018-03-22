using System;

namespace Dell.PremierTools.Common.Log
{
    public class PCFConfiguration
    {
        public static string PORT_ENV_VARIABLE_NAME => "PORT";
        public static string IP_ENV_VARIABLE_NAME => "CF_INSTANCE_IP";
        public static string INSTANCE_GUID_ENV_VARIABLE_NAME => "INSTANCE_GUID";
        public static string INSTANCE_INDEX_ENV_VARIABLE_NAME => "CF_INSTANCE_INDEX";
        public static string BOUND_SERVICES_ENV_VARIABLE_NAME => "VCAP_SERVICES";
        public static string APPLICATION_ENV_VARIABLE_NAME => "VCAP_APPLICATION";

        public static string GetEnvVariable(string key)
        {
            string value = Environment.GetEnvironmentVariable(key);
            return !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
        }
    }
    
}