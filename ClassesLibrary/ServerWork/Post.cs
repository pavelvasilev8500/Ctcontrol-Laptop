using System.IO;
using System.Net;
using System;

namespace ClassesLibrary.ServerWork
{
    public static class Post
    {
        public static void PostData(string uri, string json)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Method = "POST";
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
            catch (Exception){}
        }
    }
}