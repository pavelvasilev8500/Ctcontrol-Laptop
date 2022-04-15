using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WarningDialog.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            ThreadController();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                if (SystemInformation.PowerStatus.BatteryChargeStatus == (BatteryChargeStatus.Low | BatteryChargeStatus.Charging))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });
                }
                Thread.Sleep(1000);
            }
            while (true);
        }

    }
}
