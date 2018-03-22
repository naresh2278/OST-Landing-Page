using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Extensions
{
    public static class Helper
    {
        public static double ToDouble(this string value)
        {
            return Convert.ToDouble(value);
        }

        public static string ToGMTOffsetString(this DateTime now)
        {
            return now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss tt \"GMT\"zzz");
        }

        
    }//End of Class
}
