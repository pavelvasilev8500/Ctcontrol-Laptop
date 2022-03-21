using ClassesLibrary.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System;
using System.Threading;

namespace ClassesLibrary.ServerWork
{
    public static class Get
    {
        private static bool IsExist { get; set; }
        private static bool Shutdown { get; set; }
        private static bool Reset { get; set; }
        private static bool Sleep { get; set; }
        public static bool GetId(string uri, string id)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        JArray array = JArray.Parse(reader.ReadToEnd());
                        if (array.Count == 0)
                        {
                            IsExist = false;
                        }
                        else
                        {
                            foreach(var elemet in array)
                            {
                                if (id.Equals(elemet["_id"].ToString()))
                                {
                                    IsExist = true;
                                    break;
                                }
                                else
                                {
                                    IsExist = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception){}
            return IsExist;
        }

        public static (bool, bool, bool) GetSystemData(string uri, string id)
        {
            try
            {
                WebRequest request = WebRequest.Create(uri + "/" + id);
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        using (JsonReader jsonreader = new JsonTextReader(reader))
                        {
                            var serializer = new JsonSerializer();
                            var data = serializer.Deserialize<SystemDataModel>(jsonreader);
                            Shutdown = data.Shutdown;
                            Reset = data.Reset;
                            Sleep = data.Sleep;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Shutdown = false;
                Reset = false;
                Sleep = false;
            }
            return (Shutdown, Reset, Sleep);
        }
    }
}
