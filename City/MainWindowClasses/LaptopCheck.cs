using System.Windows.Forms;

namespace City.MainWindowClasses
{
    public static class LaptopCheck
    {
        private static bool IsLaptop { get; set; }
        public static bool IsPcLaptop()
        {
            if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery || SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Unknown)
            {
                IsLaptop = false;
            }
            else
            {
                IsLaptop = true;
            }
            return IsLaptop;
        }
    }
}
