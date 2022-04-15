using ClassesLibrary.Classes;
using Prism.Events;
using Prism.Mvvm;
using ResourcesLibrary.Resources.Languages.Classes;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace WarningDialog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _ea;

        public MainWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<SendLanguageEvent>().Subscribe(MessageReceived);
            ThreadController();
        }

        private void MessageReceived(CultureInfo language)
        {
            Languages.Language = language;
        }

        private void ThreadController()
        {
            Thread batteryCheck = new Thread(() =>
            {
                BatteryCheck();
            });
            batteryCheck.Name = "BatteryCheckThread";
            batteryCheck.Start();
        }

        private void BatteryCheck()
        {
            do
            {
                if(SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging)
                {
                    Environment.Exit(0);
                }
            }
            while (true);
        }
    }
}
