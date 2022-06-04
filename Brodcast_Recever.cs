using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace encryption
{
    class Brodcast_Recever
    {
      int listenPort = 0;
        bool done = false;
        UdpClient listener = null;
        IPEndPoint groupEP = null;
        string received_data;
        byte[] receive_byte_array;
        Thread thr = null;
        public event EventHandler<UdpMessageReceivedEventArgs> MessageReceived;
        public Brodcast_Recever(int R_port)
        {
            listenPort = R_port;
            listener = new UdpClient(listenPort);
            groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        }
        public void receive()
        {
            thr = new Thread(new ThreadStart(start));
            thr.IsBackground = true;
            thr.Start();
        }

        public void start()
        {
            try
            {
                while (!done)
                {
                    //"Waiting for broadcast");
                    receive_byte_array = listener.Receive(ref groupEP);
                    //Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    //Console.WriteLine("data follows \n{0}\n\n", received_data);
                    sendMessage(received_data+":"+groupEP.Address.ToString());
                }
            }
            catch (Exception e)
            {
               // Console.WriteLine(e.ToString());
                listener.Close();
                thr.Abort();
            }
            
           
        }

        public class UdpMessageReceivedEventArgs : EventArgs
        {
            private string m_message;

            public UdpMessageReceivedEventArgs(string message)
            {
                m_message = message;
            }

            public string Message
            {
                get
                {
                    return m_message;
                }
            }
        }

        private void sendMessage(string message)
        {
            // Copy to a temporary variable to be thread-safe.
            EventHandler<UdpMessageReceivedEventArgs> messageReceived = MessageReceived;
            if (messageReceived != null)
                messageReceived(this, new UdpMessageReceivedEventArgs(message));
        }
        ~Brodcast_Recever()
        {
            thr.Abort();

        }

    }
}
