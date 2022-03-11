using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;
using ModuleMain.Classes;
using OpenHardwareMonitor.Hardware;

namespace ClassesLibrary.SystemInfo
{
    public static class SystemInfo
    {
        private static string CPUTemperature { get; set; }
        private static string GPUTemperature { get; set; }
        private static char[] day;
        private static Computer computer = new Computer()
        {
            CPUEnabled = true,
            GPUEnabled = true,
        };
        public static string GetDate()
        {
            DateTime now = DateTime.Now;
            switch (CultureInfo.CurrentUICulture.Name)
            {
                case "ru-RU":
                    return string.Format(now.ToString("dd ") + $"{now.ToString("Y", CultureInfo.GetCultureInfo("ru-ru"))}");
                default:
                    return string.Format(now.ToString("dd ") + $"{now.ToString("Y", CultureInfo.GetCultureInfo("en-US"))}");
            }
        }
        public static string GetStandartTime()
        {
            return DateTime.Now.ToString("HH:mm");
        }
        public static string GetSecond()
        {
            return DateTime.Now.ToString("ss");
        }
        public static string GetDay()
        {
            switch (CultureInfo.CurrentUICulture.Name)
            {
                case "ru-RU":
                    day = DateTime.Now.ToString("dddd", CultureInfo.GetCultureInfo("ru-RU")).ToCharArray();
                    break;
                default:
                    day = DateTime.Now.ToString("dddd", CultureInfo.GetCultureInfo("en-US")).ToCharArray();
                    break;
            }
            day[0] = char.ToUpper(day[0]);
            string Day = new string(day);
            return Day;
        }
        public static string GetPcWorkTimeDay()
        {
            int systemUptime = Environment.TickCount;
            var ts = TimeSpan.FromMilliseconds(systemUptime);
            return string.Format($"{ts.Days}");
        }
        public static string GetPcWorkTimeHour()
        {
            int systemUptime = Environment.TickCount;
            var ts = TimeSpan.FromMilliseconds(systemUptime);
            return string.Format($"{ts.Hours}");
        }
        public static string GetPcWorkTimeMinut()
        {
            int systemUptime = Environment.TickCount;
            var ts = TimeSpan.FromMilliseconds(systemUptime);
            return string.Format($"{ts.Minutes}");
        }
        public static string GetPcWorkTimeSecond()
        {
            int systemUptime = Environment.TickCount;
            var ts = TimeSpan.FromMilliseconds(systemUptime);
            return string.Format($"{ts.Seconds}");
        }
        public static string GetNotebookBatary()
        {
            return (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + "%";
        }
        /// <summary>
        /// Classes GetCPU & GPU Temperature with V-end using Class Visitor for access to hardware.
        /// </summary>
        /// <returns>
        /// String value
        /// </returns>
        public static string GetCPUTemperatureV()
        {
            var visitor = new Visitor();
            Computer computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.Accept(visitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            CPUTemperature = computer.Hardware[i].Sensors[j].Value.ToString();
                    }
                }
            }
            computer.Close();
            CPUTemperature = CPUTemperature.Substring(0, 2);
            CPUTemperature = CPUTemperature + " ℃";
            return CPUTemperature;
        }
        public static string GetGPUTemeratureV()
        {
            var visitor = new Visitor();
            Computer computer = new Computer();
            computer.Open();
            computer.GPUEnabled = true;
            computer.Accept(visitor);
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.GpuNvidia)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            GPUTemperature = computer.Hardware[i].Sensors[j].Value.ToString();
                    }
                }
            }
            computer.Close();
            GPUTemperature = GPUTemperature + " ℃";
            return GPUTemperature;
        }
        public static string GetCPUTemperature()
        {
            computer.Open();
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if(sensor.SensorType == SensorType.Temperature)
                        {
                            CPUTemperature = sensor.Value.ToString();
                        }
                    }
                }
            }
            CPUTemperature = CPUTemperature.Substring(0, 2);
            CPUTemperature = CPUTemperature + " ℃";
            return CPUTemperature;  
        }
        public static string GetGPUTemerature()
        {
            computer.Open();
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.GpuNvidia)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            GPUTemperature = sensor.Value.ToString() + " ℃ " + $"({hardware.HardwareType.ToString()})";
                        }
                    }
                }
                else if (hardware.HardwareType == HardwareType.GpuAti)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            GPUTemperature = sensor.Value.ToString() + " ℃ " + $"({hardware.HardwareType.ToString()})";
                        }
                    }
                }
            }
            return GPUTemperature;
        }
    }
}
