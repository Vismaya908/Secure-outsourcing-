using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ProxyServer
{
    class ServerAdvertise
    {
        Thread advert = null;
        void sendPackets()
        {

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 9050);
            string hostname = Dns.GetHostName();
            byte[] data = Encoding.ASCII.GetBytes(hostname);
            while (true)
            {
                try
                {
                    sock.SendTo(data, iep);
                    Thread.Sleep(1000);
                }
                catch
                {
                }
            }

        }
        public void startAdvertise()
        {
            advert = new Thread(new ThreadStart(sendPackets));
            advert.IsBackground = true;
            advert.Start();
        }
        public void stopAdvertise()
        {
            advert.Abort();
        }
    }
}
