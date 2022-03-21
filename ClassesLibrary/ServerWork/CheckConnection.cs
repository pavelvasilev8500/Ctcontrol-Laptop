using System;
using System.Runtime.InteropServices;

namespace ClassesLibrary.ServerWork
{
    public static class CheckConnection
    {
        [DllImport("wininet.dll", SetLastError = true)]
        extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        [Flags]
        enum ConnectionStates
        {
            Modem = 0x1,
            LAN = 0x2,
            Proxy = 0x4,
            RasInstalled = 0x10,
            Offline = 0x20,
            Configured = 0x40,
        }

        public static bool ConnectionStatus()
        {
            int flags;
            bool isConnected = InternetGetConnectedState(out flags, 0);
            return isConnected;
        }
    }
}
