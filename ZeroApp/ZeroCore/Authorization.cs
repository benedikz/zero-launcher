using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;

namespace ZeroApp
{
    class Authorization
    {
        /// <summary>
        /// Sends API request for authorization token
        /// </summary>
        /// <param name="api"></param>
        /// <param name="api_name"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        private static string GetToken(string api, string api_name, string[] credentials)
        {
            try
            {
                string username = credentials[0];
                string password = credentials[1];
                string content = "{\"name\":\"" + api_name + "\",\"param\":{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}}";

                string[] response = Web.SendWebRequest(api, "POST", content, "application/json", null);
                string r_Token = response[1];

                return r_Token;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Sends API request for registration of a new user 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="passcheck"></param>
        /// <returns>[true - Success | false - Failure]</returns>
        public static bool Register(string username, string password, string passcheck)
        {
            try
            {
                string content = "{\"name\":\"registerUser\",\"param\":{\"username\":\"" + username + "\",\"password\":\"" + password + "\", \"passcheck\":\"" + password + "\"}}";
                string[] response = Web.SendWebRequest("register", "POST", "", "application/json", null);
                string r_Status = response[0];

                if ( r_Status == "200")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sends API request for login form validation 
        /// </summary>
        /// <param name="username">Username parameter</param>
        /// <param name="password">Password parameter</param>
        /// <returns>[Token - Success | Empty Token - Failure]</returns>
        public static string Login(string username, string password)
        {
            try
            {
                string content = "{\"name\":\"generateToken\",\"param\":{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}}";
                string[] response = Web.SendWebRequest(AuthWindow.API_URL, "POST", "application/json", content, null);
                string r_Token = response[1];

                return r_Token;
            }
            catch (Exception e)
            {
                Trace.WriteLine("[Login] Task Failed :: Exception [" + e + "] thrown.");
                return String.Empty;
            }
        }
    }
}
