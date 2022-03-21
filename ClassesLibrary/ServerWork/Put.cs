using System.IO;
using System.Net;
using System;

namespace ClassesLibrary.ServerWork
{
    public static class Put
    {
        public static void PutData(string uri, string id, string json)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri + "/" + id);
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json";
                using (var requestStream = httpRequest.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
                using (var httpResponse = httpRequest.GetResponse())
                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string response = reader.ReadToEnd();
                }
            }
            catch (Exception) { }
        }

    }
}
