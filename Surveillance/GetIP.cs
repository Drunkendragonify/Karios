using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Karios.Surveillance
{
    class GetIP
    {
        public static IPAddress GetIpAddress(string hostName)
        {
            //Currently Broken
            var ping = new Ping();
            var replay = ping.Send(hostName);
            //Pings googles servers and gets hostname
            return replay != null && replay.Status == IPStatus.Success ? replay.Address : null;
        }
    }
}
