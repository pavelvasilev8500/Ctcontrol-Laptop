using ClassesLibrary.DataModels;
using Newtonsoft.Json;
using System.Globalization;

namespace ClassesLibrary.Client
{
    public static class CreateJson
    {
        private static string Worktime { get; set; }
        public static string CreateDataJson(SystemDataModel systemdata, string id)
        {
            systemdata = new SystemDataModel
            {
                _id = id,
                Shutdown = false,
                Reset = false,
                Sleep = false
            };
            string json = JsonConvert.SerializeObject(systemdata);
            return json;
        }
        public static string CreateDataJson(ClientDataModel cliendata, string id)
        {
            switch (CultureInfo.InstalledUICulture.Name)
            {
                case "ru-RU":
                    Worktime = string.Format(SystemInfo.SystemInfo.GetPcWorkTimeDay() + " Д " + SystemInfo.SystemInfo.GetPcWorkTimeHour() + " Ч " + SystemInfo.SystemInfo.GetPcWorkTimeMinut() + " М " + SystemInfo.SystemInfo.GetPcWorkTimeSecond() + " С");
                    break; 
                default:
                    Worktime = string.Format(SystemInfo.SystemInfo.GetPcWorkTimeDay() + " D " + SystemInfo.SystemInfo.GetPcWorkTimeHour() + " H " + SystemInfo.SystemInfo.GetPcWorkTimeMinut() + " М " + SystemInfo.SystemInfo.GetPcWorkTimeSecond() + " S");
                    break;
            }
            cliendata = new ClientDataModel
            {
                _id = id,
                Date = SystemInfo.SystemInfo.GetDate(),
                Time = string.Format(SystemInfo.SystemInfo.GetStandartTime() + ":" + SystemInfo.SystemInfo.GetSecond()),
                Day = SystemInfo.SystemInfo.GetDay(),
                Worktime = Worktime,
                Batary = SystemInfo.SystemInfo.GetNotebookBatary(),
                CpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item1,
                GpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item2
        };
            string json = JsonConvert.SerializeObject(cliendata);
            return json;
        }
        public static string CreateDataJson(StatusDataModel statusdata, string id, bool status)
        {
            statusdata = new StatusDataModel
            {
                _id = id,
                Status = status
            };
            string json = JsonConvert.SerializeObject(statusdata);
            return json;
        }
    }
}
