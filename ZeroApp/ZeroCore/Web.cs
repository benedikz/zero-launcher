using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Cache;

namespace ZeroApp
{
    class Web
    {
        public static bool CheckInternetConnection()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckAPIConnection(string url)
        {
            try
            {
                Uri servUri = new Uri(url);
                IPAddress ip = Dns.GetHostAddresses(servUri.Host)[0];

                System.Net.NetworkInformation.Ping ping = new Ping();
                PingReply result = ping.Send(ip, 22800);

                if (result.Status == IPStatus.Success)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string[] SendWebRequest(string url, string method, string contentType, string content, string token)
        {
            try
            {
                // string[] result = { string status, string response };
                string[] result = { String.Empty, String.Empty };

                // Create Web Request 
                WebRequest request = WebRequest.Create(url);
                request.Method = method;
                
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                request.ContentType = contentType;

                if( !String.IsNullOrEmpty(token) )
                    request.Headers["Authorization"] = "Bearer " + token;

                request.ContentLength = (long)byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                

                result[0] = ((HttpWebResponse)response).StatusDescription.ToString();
                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                result[1] = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return result;
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DownloadFile] Task Failed :: Exception [" + e.ToString() + "] thrown.\nReturning empty result.");
                string[] result = { String.Empty, String.Empty };
                return result;
            }
        }

        public static void DownloadFile(string from, string to)
        {
            try
            {
                HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                HttpWebRequest.DefaultCachePolicy = policy;

                HttpWebRequest httpRequest = (HttpWebRequest)
                WebRequest.Create(from);
                httpRequest.Method = WebRequestMethods.Http.Get;

                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                httpRequest.CachePolicy = noCachePolicy;
                httpRequest.Headers.Add("Cache-Control", "no-cache");

                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream httpResponseStream = httpResponse.GetResponseStream();

                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;

                // Read from response and write to file
                FileStream fileStream = File.Create(to);
                while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }

                fileStream.Close();
                httpResponseStream.Close();
                httpResponse.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DownloadFile] Task Failed :: Exception [" + e.ToString() + "] thrown.");
            }
        }
    }
}
