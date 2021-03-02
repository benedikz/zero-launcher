using System;
using System.Collections.Generic;
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
            // Ošetřit pomocí try catch

            var request = WebRequest.Create(url);

            /*
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var strContent = reader.ReadToEnd();
                return strContent.ToString();
            }
            */

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Reads index.xml in local-machine path
        /// </summary>
        /// <param name="path">Absolute path to file on client's PC</param>
        /// <returns>Unserialized contents of index.xml as string</returns>
        public string ReadIndex(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var content = reader.ReadToEnd();
                return content.ToString();
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
