using System;
using System.Configuration;
using Dell.PremierTools.Common.Log;

public static class PCFSetting
{
    static ILogger logger = new NLogger().GetInstance;

    public static string AppSettingValue(string key)
    {
        if (isInPCF) logger.LogDebug("PCFAccess Class", null, string.Format("Env[{0}]:{1}", key, PCFConfiguration.GetEnvVariable(key)));
        return isInPCF ? PCFConfiguration.GetEnvVariable(key) : Convert.ToString(ConfigurationManager.AppSettings[key]);
    }
    public static string ConnectionStringsValue(string key)
    {
        if (isInPCF) logger.LogDebug("PCFAccess Class", null, string.Format("Env[{0}]:{1}", key, PCFConfiguration.GetEnvVariable(key)));
        return isInPCF ? PCFConfiguration.GetEnvVariable(key) : Convert.ToString(ConfigurationManager.ConnectionStrings[key].ConnectionString);
    }


    public static bool isInPCF
    {
        get
        {
            return !string.IsNullOrWhiteSpace(PCFConfiguration.GetEnvVariable(PCFConfiguration.APPLICATION_ENV_VARIABLE_NAME)) ? true : false;
        }
    }

}