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
        // Client Token
        //private string c_Token = String.Empty;

        // Sends API request for authorization token
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

        // Sends API request for registration of a new user [true - Success | false - Failure]
        public static bool Register(string username, string password, string passcheck)
        {
            try
            {
                string content = "{\"name\":\"registerUser\",\"param\":{\"username\":\"" + username + "\",\"password\":\"" + password + "\", \"passcheck\":\"" + password + "\"}}";
                string[] response = Web.SendWebRequest("register", "POST", "", "application/json", null);
                string r_Status = response[0];

                Trace.WriteLine("<Register> " + r_Status);

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

        // Sends API request for login form validation [Token - Success | Empty Token - Failure]
        public static string Login(string username, string password)
        {
            try
            {
                string content = "{\"name\":\"generateToken\",\"param\":{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}}";
                string[] response = Web.SendWebRequest(AuthWindow.API_URL, "POST", "application/json", content, null);
                string r_Token = response[1];

                Trace.WriteLine(content);
                Trace.WriteLine(r_Token);

                return r_Token;
            }
            catch (Exception e)
            {
                Trace.WriteLine("[Login] Task Failed :: Exception [" + e + "] thrown.");
                return String.Empty;
            }
        }

        // Facilitates removal of all session data and exits user from the app
        public static void Logout()
        {
            // Clear temporary data & configuration files
        }
    }
}
