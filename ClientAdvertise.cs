using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace encryption
{
    class ClientAdvertise
    {
        Thread advertcl = null;
        string sip = "",sname="localhost";
        //public event EventHandler Path_Send;
        public event EventHandler Server_Found;
        public string ServerIP
        {
            get
            {
                if(sip.IndexOf(':')!=-1)
                sip = sip.Substring(0, sip.IndexOf(':'));
                return sip;
            }
        }

        public String ServerName
        {
            get
            {
                return sname;
            }
        }

        void receivePacket()
        {


            Socket remoteHosts = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            EndPoint ep = (EndPoint)iep;
            remoteHosts.Bind(iep);
            byte[] data = new byte[1024];
            int recv = remoteHosts.ReceiveFrom(data, ref ep);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            string entry = stringData + ":" + ep.ToString();
            sname = stringData;
            sip = ep.ToString();
            if (this.Server_Found != null)
                this.Server_Found(this, new EventArgs());
            remoteHosts.Close();

        }
        public void startAdvertise()
        {
            advertcl = new Thread(new ThreadStart(receivePacket));
            advertcl.IsBackground = true;
            advertcl.Start();
        }
        public void stopAdvertise()
        {
            advertcl.Abort();
        }


    }
}
