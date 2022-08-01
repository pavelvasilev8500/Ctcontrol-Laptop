using ClassesLibrary.DataModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Windows.Forms;

namespace ClassesLibrary.Client
{
    public static class CreateJson
    {
        private static string PcType { get; set; }
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
            cliendata = new ClientDataModel
            {
                _id = id,
                DateNumber = SystemInfo.SystemInfo.GetDateNumber(),
                DateMonth = SystemInfo.SystemInfo.GetDateMonth(),
                DateYear = SystemInfo.SystemInfo.GetDateYear(),
                Time = string.Format(SystemInfo.SystemInfo.GetStandartTime() + ":" + SystemInfo.SystemInfo.GetSecond()),
                Day = SystemInfo.SystemInfo.GetDay(),
                WorktimeDay = SystemInfo.SystemInfo.GetPcWorkTimeDay(),
                WorktimeHour = SystemInfo.SystemInfo.GetPcWorkTimeHour(),
                WorktimeMinut = SystemInfo.SystemInfo.GetPcWorkTimeMinut(),
                WorktimeSecond = SystemInfo.SystemInfo.GetPcWorkTimeSecond(),
                Batary = SystemInfo.SystemInfo.GetNotebookBatary(),
                CpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item1,
                GpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item2
            };
            string json = JsonConvert.SerializeObject(cliendata);
            return json;
        }
        public static string CreateDataJson(DesktopClientDataModel desktopcliendata, string id)
        {
            desktopcliendata = new DesktopClientDataModel
            {
                _id = id,
                DateNumber = SystemInfo.SystemInfo.GetDateNumber(),
                DateMonth = SystemInfo.SystemInfo.GetDateMonth(),
                DateYear = SystemInfo.SystemInfo.GetDateYear(),
                Time = string.Format(SystemInfo.SystemInfo.GetStandartTime() + ":" + SystemInfo.SystemInfo.GetSecond()),
                Day = SystemInfo.SystemInfo.GetDay(),
                WorktimeDay = SystemInfo.SystemInfo.GetPcWorkTimeDay(),
                WorktimeHour = SystemInfo.SystemInfo.GetPcWorkTimeHour(),
                WorktimeMinut = SystemInfo.SystemInfo.GetPcWorkTimeMinut(),
                WorktimeSecond = SystemInfo.SystemInfo.GetPcWorkTimeSecond(),
                CpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item1,
                GpuTemperature = SystemInfo.SystemInfo.GetTemperature().Item2
            };
            string json = JsonConvert.SerializeObject(desktopcliendata);
            return json;
        }
        public static string CreateDataJson(StatusDataModel statusdata, string id, bool status)
        {
            if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery || SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Unknown)
            {
                PcType = "PC";
            }
            else
            {
                PcType = "Laptop";
            }
            statusdata = new StatusDataModel
            {
                _id = id,
                Status = status,
                PCType = PcType
            };
            string json = JsonConvert.SerializeObject(statusdata);
            return json;
        }
    }
}
