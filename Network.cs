using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ProxyServer
{
    class Network
    {

        public static string getHostIP()
        {
            IPAddress[] ipa = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ipa)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();
            }

            return "127.0.0.1";
        }
    }
}
