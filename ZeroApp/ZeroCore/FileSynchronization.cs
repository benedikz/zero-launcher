using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZeroApp
{
    /// <summary>
    /// Class FileSynchronization
    /// </summary>
    class FileSynchronization
    {
        /// <summary>
        /// Downloads an index.xml from URL
        /// </summary>
        /// <param name="url">Source URL</param>
        /// <returns></returns>
        public string DownloadIndex(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Timeout = 4680000;
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = (Stream)response.GetResponseStream())
                using (var sr = new StreamReader(stream))
                {
                    var content = sr.ReadToEnd();
                    return content.ToString();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DownloadIndex] Task Failed :: Exception [" + e + "] thrown");
                return String.Empty;
            }
        }

        /// <summary>
        /// Reads index.xml in local-machine path
        /// </summary>
        /// <param name="path">Absolute path to file on client's PC</param>
        /// <returns>Unserialized contents of index.xml as string</returns>
        public string ReadIndex(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var content = reader.ReadToEnd();
                    return content.ToString();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[ReadIndex] Task Failed :: Exception [" + e + "] thrown");
                return String.Empty;
            }
        }

        /// <summary>
        /// Compares local and remote indexes of one modpack
        /// </summary>
        /// <param name="pack_name"></param>
        /// <param name="remote_index"></param>
        /// <param name="local_index"></param>
        /// <returns></returns>
        public int CompareIndexes(string pack_name, string remote_index, string local_index)
        {
            int diff = String.Compare(remote_index, local_index);
            return diff;
        }
    }
}
