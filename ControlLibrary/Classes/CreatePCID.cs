using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLibrary.Classes
{
    public class CreatePCID
    {
        public void CreateID()
        {
            if(Properties.Settings.Default.PCID.Equals("0"))
            {
                Properties.Settings.Default.PCID = Guid.NewGuid().ToString();
                Properties.Settings.Default.Save();
            }
        }
    }
}
