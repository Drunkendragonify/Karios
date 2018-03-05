using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karios
{
    class Startup
    {
        public static void SetStartup()
        {
            //Changes the startup registary keys to run Karios at startup.
            /*
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string pathToSecCopy = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + AppDomain.CurrentDomain.FriendlyName;
            string pathToApp = Application.ExecutablePath;
            if (rkApp.GetValue(System.AppDomain.CurrentDomain.FriendlyName) == null)
            {
                rkApp.SetValue(System.AppDomain.CurrentDomain.FriendlyName, pathToSecCopy);
            }
            */
        }
    }
}
