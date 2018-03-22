using Dell.B2BOnlineTools.Common.Models.QMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Extensions.QMsg
{
    public static partial class QMsgHelper
    {
        public static dynamic GetValue(this ArgInfo argInfo, object[] value)
        {
            if (value == null || value.Count() < 1) return null;
            if (value[argInfo.Index] == null)
                return null;
            else if (argInfo.IsValueEncrypted)
                return Convert.ChangeType(Convert.ToString(value[argInfo.Index]).Decrypt(), Type.GetType(argInfo.DataType));
            else
                return Convert.ChangeType(value[argInfo.Index], Type.GetType(argInfo.DataType));
        }
    }//End of Class
}
