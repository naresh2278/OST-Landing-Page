using System;
using System.Web;
using NLog;
using System.Diagnostics;

namespace Dell.PremierTools.Common.Log
{

    public sealed class NLogger : ILogger
    {
        private static NLog.Logger logger;
        private static volatile NLogger _instance;
        private static object syncLock = new object();

        public NLogger GetInstance
        {
            get
            {
                if(_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                            _instance = new NLogger();
                    }
                }
                return _instance;
            }
        }


        static NLogger() => logger = NLog.LogManager.GetCurrentClassLogger();
        
        public void LogError<T>(T sender, Exception ex, string message="")
        {        
            LogEventInfo logEventInfo = LogEventInfo.Create(LogLevel.Error, logger.Name, MsgFormatter(sender, ex, message));
            GetCallingClass(ex, ref logEventInfo);
            GetEventProperties(ref logEventInfo);          
            logEventInfo.Exception = ex;
            logger.Log(sender.GetType(), logEventInfo);
        }

        public void LogInfo(object sender, string message) => Log(LogLevel.Info, sender, null, message);
        public void LogWarn(object sender, Exception ex, string message) => Log(LogLevel.Warn, sender, ex, message);
        public void LogDebug(object sender, Exception ex, string message) => Log(LogLevel.Debug, sender, ex, message);


        private void GetCallingClass(Exception ex, ref LogEventInfo logEventInfo)
        {
            if (ex == null) return;
            StackTrace stackTrace = new StackTrace(ex);
            if (stackTrace.FrameCount > 0)
            {
                logEventInfo.Properties["Class"] = stackTrace.GetFrame(0).GetMethod().ReflectedType.ToString();
                logEventInfo.Properties["Method"] = stackTrace.GetFrame(0).GetMethod().ToString();
            }
        }        
        private void GetEventProperties(ref LogEventInfo logEventInfo)
        {
            if (Trace.CorrelationManager.ActivityId == Guid.Empty) Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            logEventInfo.Properties["ActivityId"] = Trace.CorrelationManager.ActivityId;
            logEventInfo.Properties["AppDomain"] = AppDomain.CurrentDomain.FriendlyName;
         
            //if (HttpContext.Current != null)
            //{
            //    logEventInfo.Properties["IP"] = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //    if (HttpContext.Current.Request != null) logEventInfo.Properties["URL"] = HttpContext.Current.Request.Url.ToString();
            //}
        }
        private string MsgFormatter<T>(T sender, Exception ex, string message) => string.Format(@"{{""msgBody"":{0},""msgErr"":""{1}""}}", message ?? "",
                (ex != null && !string.IsNullOrEmpty(ex.Message)) ? ex.Message : string.Empty);

        private void Log <T>(LogLevel level, T sender, Exception ex, string message)
        {
            LogEventInfo logEvent = LogEventInfo.Create(level, logger.Name, MsgFormatter(sender, ex, message));
            GetEventProperties(ref logEvent);
            if (ex != null)
            {
                GetCallingClass(ex, ref logEvent);
                logEvent.Exception = ex;
            } 
            logEvent.Level = level;
            logger.Log(sender.GetType(), logEvent);
        }
    }
}