using System;
using System.Runtime.InteropServices;

namespace ClassesLibrary.SystemControls
{
    /// <summary>
    /// Control Sysyem. Work only in Windows.
    /// Commands:
    /// Lock() - lock os;
    /// halt(true, false) - soft reset;
    /// halt(true, true) - hard reset;
    /// halt(false, false) - soft shut down;
    /// halt(false, true) - hard shut down.
    /// Sleep(false, false, false) - sleep;
    /// Sleep(true, true, true) - hibernate;
    /// Sleep(false, true, true) - standy;
    /// Soft - waiting for close all apps/servises and restart/sut down pc;
    /// Hard - force close all apps/servises and restart/sut down pc;
    /// </summary>
    public static class SystemControl
    {
        [DllImport("advapi32.dll", EntryPoint = "InitiateSystemShutdownEx")]
        static extern int InitiateSystemShutdown(string lpMachineName, string lpMessage, int dwTimeout, bool bForceAppsClosed, bool bRebootAfterShutdown);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("user32.dll", EntryPoint = "LockWorkStation")]
        static extern bool LockWorkStation();

        [DllImport("Powrprof.dll", SetLastError = true)]
        static extern uint SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        private static void SetPriv()
        {
            TokPriv1Luid tkp;
            IntPtr htok = IntPtr.Zero;
            if (OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok))
            {
                tkp.Count = 1;
                tkp.Attr = SE_PRIVILEGE_ENABLED;
                tkp.Luid = 0;
                LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tkp.Luid);
                AdjustTokenPrivileges(htok, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public static int halt(bool RSh, bool Force)
        {
            SetPriv();
            return InitiateSystemShutdown(null, null, 0, Force, RSh);
        }

        public static int Lock()
        {
            if (LockWorkStation())
                return 1;
            else
                return 0;
        }

        public static uint Sleep(bool hibernate, bool forceCritical, bool disableWakeEvent)
        {
            return SetSuspendState(hibernate, forceCritical, disableWakeEvent);
        }
    }
}
