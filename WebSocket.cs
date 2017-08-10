using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;

namespace WebNotifier
{
    class WebSocket
    {
        private int trys = 0;
        public string Request(string Url)
        {
            WebRequest webRequest;
            WebResponse webResponse;
            try
            {
                Uri uri = new Uri(Url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //we aren't a browser we thrust peoples input.
                ServicePointManager.ServerCertificateValidationCallback =
      (sender, certificate, chain, sslPolicyErrors) => { return true; };
                webRequest = WebRequest.Create(uri);
                // TODO: I think we need more control options in the future
                webRequest.Method = "POST";
                // we wait 10 sec
                webRequest.Timeout = 10000;


                webResponse = webRequest.GetResponse();

            }
            catch (Exception e)
            {
                
                if (trys < 5)
                {
                    trys++;
                    return Request(Url);
                }
                else
                {
                    return null;
                }
            }
            Task<string> taskA = Task.Run(() => ReadFrom(webResponse.GetResponseStream()));
            string answer = taskA.Result;
            webResponse.Close();
            return answer;
        }
        public string Request(string Url, int timeout)
        {
            WebRequest webRequest;
            WebResponse webResponse;
            try
            {
                Uri uri = new Uri(Url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //we aren't a browser we thrust peoples input.
                ServicePointManager.ServerCertificateValidationCallback =
      (sender, certificate, chain, sslPolicyErrors) => { return true; };
                webRequest = WebRequest.Create(uri);
                // TODO: I think we need more control options in the future
                webRequest.Method = "POST";
                // we wait 10 sec
                webRequest.Timeout = timeout;
                webResponse = webRequest.GetResponse();
            }
            catch (Exception e)
            {
                return null;
            }
            Task<string> taskA = Task.Run(() => ReadFrom(webResponse.GetResponseStream()));
            string answer = taskA.Result;
            webResponse.Close();
            return answer;
        }
        public async Task<string> ReadFrom(Stream dataStream)
        {
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = await reader.ReadToEndAsync();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            return responseFromServer;
        }
    }
    class XMLWorker
    {
        public string ExtractBody(string alpha)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(alpha);
            XmlNode node = doc.DocumentElement.SelectSingleNode("body");
            string re = node.InnerText;
            return string.Copy(re);
        }
        public string ExtractTitle(string alpha)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(alpha);
            XmlNode node = doc.DocumentElement.SelectSingleNode("title");
            string re = node.InnerText;
            return string.Copy(re);
        }
    }
}