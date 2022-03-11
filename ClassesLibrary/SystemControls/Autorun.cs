using Microsoft.Win32;
using System.Reflection;
using System.Windows.Forms;

namespace ClassesLibrary.SystemControls
{
    /// <summary>
    /// Set on or off autorun for application.
    /// Path to application in regedit:
    /// PC\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
    /// Path to application:
    /// C:\Users\UserName\AppData\Local\Apps
    /// </summary>
    public static class Autorun
    {
        private static string name = Assembly.GetEntryAssembly().GetName().Name.ToString();
        public static bool SetAutorunValue(bool autorun)
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
