using ClassesLibrary.SystemInfo;
using System.Threading;
using System.Windows.Forms;
using WarningDialog.Views;

namespace WarningDialog.Classes
{
    public class BatteryCheck
    {
        private bool Notification { get; set; }
        public BatteryCheck()
        {
            Thread batteryCheck = new Thread(() =>
            {
                do
                {
                    switch (SystemInformation.PowerStatus.BatteryChargeStatus)
                    {
                        case BatteryChargeStatus.Low:
                            if (Notification == false)
                            {
                                if (SystemInfo.GetNotebookBataryFloat() <= 10)
                                {
                                    var mw = new MainWindow();
                                    mw.ShowDialog();
                                    Notification = true;
                                }
                            }
                            break;
                        default:
                            Notification = false;
                            break;
                    }
                    Thread.Sleep(1000);
                }
                while (true);
            });
            batteryCheck.Name = "BatteryCheckThread";
            batteryCheck.SetApartmentState(ApartmentState.STA);
            if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery || SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.Unknown)
                batteryCheck.Start();
        }
    }
}
