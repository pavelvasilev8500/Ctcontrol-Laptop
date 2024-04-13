using ClassesLibrary.DataModels;
using Newtonsoft.Json;

namespace ClassesLibrary.Client
{
    public static class CreateJson
    {
        public static string Create(string batary)
        {
            var cliendata = new ClientDataModel
            {
                Worktime = $"{SystemInfo.SystemInfo.GetPcWorkTimeDay()}:" +
                           $"{SystemInfo.SystemInfo.GetPcWorkTimeHour()}:" +
                           $"{SystemInfo.SystemInfo.GetPcWorkTimeMinut()}:" +
                           $"{SystemInfo.SystemInfo.GetPcWorkTimeSecond()}:",
                Batary = batary,
                CpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item1,
                GpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item2
            };
            string json = JsonConvert.SerializeObject(cliendata);
            return json;
        }
    }
}
