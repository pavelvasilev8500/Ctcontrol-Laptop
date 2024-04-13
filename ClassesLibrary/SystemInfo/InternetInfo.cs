using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibrary.SystemInfo
{
    public static class InternetInfo
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool ConnectionStatatus()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
    }
}
