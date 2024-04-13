using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace City.MainWindowClasses
{
    public class RunAsAdministrator
    {
        public RunAsAdministrator()
        {
            if (!IsRunAsAdministrator())
            {
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception)
                {
                }
                Environment.Exit(0);
            }
        }
        private bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
