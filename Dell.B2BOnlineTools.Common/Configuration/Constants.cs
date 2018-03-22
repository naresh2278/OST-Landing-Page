
namespace Dell.B2BOnlineTools.Common.Configuration
{
    public class Constants
    {
        public const string HOSTING_ENVIRONMENT = "Hosting_Environment";

        public class PCF
        {
            public const string PORT_ENV_VARIABLE_NAME = "PORT";
            public const string IP_ENV_VARIABLE_NAME = "CF_INSTANCE_IP";
            public const string INSTANCE_GUID_ENV_VARIABLE_NAME = "INSTANCE_GUID";
            public const string INSTANCE_INDEX_ENV_VARIABLE_NAME = "CF_INSTANCE_INDEX";
            public const string BOUND_SERVICES_ENV_VARIABLE_NAME = "VCAP_SERVICES";
            public const string APPLICATION_ENV_VARIABLE_NAME = "VCAP_APPLICATION";
        }
    }
}
