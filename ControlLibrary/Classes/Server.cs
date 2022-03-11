using System;
using System.IO;
using System.Net;

namespace ControlLibrary.Classes
{
    public class Server
    {

        DataPC dataPC = new DataPC();

        public void Post(string json, string ip)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(ip);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Timeout = 100000;
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
            catch (Exception)
            {
               // MessageBox.Show($"{e}");
            }

        }

        public void Put(string json, string ip)
        {
            string fullip = ip + "update/" + $"{Properties.Settings.Default.PCID}";
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(fullip);
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json";
                httpRequest.Timeout = 100000;
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
            catch (Exception)
            {
               // MessageBox.Show($"{e}");
            }

        }
    }
}
