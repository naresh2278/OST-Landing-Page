using System;

namespace Dell.PremierTools.Common.Log
{

    public interface ILogger
    {
        void LogInfo(object sender, string message);
        void LogWarn(object sender, Exception ex, string message);
        void LogError<T>(T sender, Exception ex, string message);
        void LogDebug(object sender, Exception ex, string message);

    }
}