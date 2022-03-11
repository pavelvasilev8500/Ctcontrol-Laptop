using Microsoft.Win32;
using System.Reflection;
using System.Windows.Forms;

namespace ControlLibrary.Classes
{
    public class Autorun
    {
        private string name = Assembly.GetEntryAssembly().GetName().Name.ToString();

        public bool SetAutorunValue(bool autorun)
        {
            string ExePath = Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
//Компьютер\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
//C:\Users\City\AppData\Local\Apps
