using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace City.MainWindowClasses
{
    public class CheckProgramStart
    {

        private Process _process { get; set; }
        private static Mutex InstanceCheckMutex;

        public CheckProgramStart(Process process)
        {
            _process = process;
            if (!IsProgramStart())
            {
                ResourceDictionary Russian = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.ru-RU.xaml", UriKind.Relative)) as ResourceDictionary;
                ResourceDictionary English = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.xaml", UriKind.Relative)) as ResourceDictionary;
                switch (CultureInfo.InstalledUICulture.Name)
                {
                    case "ru-RU":
                        MessageBox.Show(Russian["m_Anothercopy"].ToString());
                        break;
                    default:
                        MessageBox.Show(English["m_Anothercopy"].ToString());
                        break;
                }
                Process.GetCurrentProcess().Kill();
            }
        }

        private bool IsProgramStart()
        {
            var currentProc = _process;
            string processName = currentProc.ProcessName;
            bool isNew;
            InstanceCheckMutex = new Mutex(true, processName, out isNew);
            return isNew;
        }

    }
}
