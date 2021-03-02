using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroApp
{
    static class Static
    {
        /// <summary>
        /// Static function for conversion of string to boolean value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            switch (value.ToLower())
            {
                case "true":
                    return true;
                case "t":
                    return true;
                case "1":
                    return true;
                case "0":
                    return false;
                case "false":
                    return false;
                case "f":
                    return false;
                default:
                    throw new InvalidCastException("Invalid Boolean Cast!");
            }
        }

        /// <summary>
        /// Converts Timestamp as Int32 to DateTime as String (for human reading purpose)
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDate(int timestamp)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp).ToLocalTime();
            Trace.WriteLine("[Static.TimestampToDate] Converted " + timestamp + " to " + date.ToString("F"));
            return date;
        }
    }
}
