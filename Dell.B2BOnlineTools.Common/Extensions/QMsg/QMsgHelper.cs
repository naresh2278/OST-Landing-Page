
using Dell.B2BOnlineTools.Common.Models.QMsg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Extensions.QMsg
{
    public static partial class QMsgHelper
    {
        public static void Log(this IQMsgHandler qMsg, string message)
        {
            Console.WriteLine($"{qMsg.QInfo.QName}.{qMsg.QInfo.BatchId}[{qMsg.QInfo.BatchIndex}] {message}");
        }
        
    }//End of Class
}
