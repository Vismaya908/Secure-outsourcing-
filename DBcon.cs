using System;
using System.Collections.Generic;
using System.Text;

using DBRemoting;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Data.SqlClient;
using System.Data;

namespace RemoteDatabase
{
    class DBcon
    {
        TcpChannel tcpChannel = new TcpChannel();

        string remoteserver = "";
        public DBcon()
        {
            remoteserver = "localhost";
        }
        public DBcon(string server)
        {
            remoteserver = server;
        }

        public bool ExecuteQuery(string qry)
        {
            IChannel cd = ChannelServices.GetChannel("tcp");
            if (cd == null)
            {
                ChannelServices.RegisterChannel(tcpChannel);
            }
            Type Dbtype = typeof(DBfunctions);
            DBfunctions remoteDBObject = (DBfunctions)Activator.GetObject(Dbtype, "tcp://"+remoteserver+":9998/RemoteDBcon");
            RemoteDBcon cc = new RemoteDBcon();

         cc.nonq=  remoteDBObject.ExecuteNonQuery(qry);
         bool fl = cc.NonQuery;
         return fl;

        }
        public SqlDataReader ExecuteReader(string qry)
        {
            IChannel cd = ChannelServices.GetChannel("tcp");
            if (cd == null)
            {
                ChannelServices.RegisterChannel(tcpChannel);
            }
            Type Dbtype = typeof(DBfunctions);
            DBfunctions remoteDBObject = (DBfunctions)Activator.GetObject(Dbtype, "tcp://" + remoteserver + ":9998/RemoteDBcon");
            RemoteDBcon cc = new RemoteDBcon();

            cc.rdr = remoteDBObject.ExecuteReader(qry);
            return cc.Reader;
        }

        public DataSet getDataset(string qry)
        {
            IChannel cd = ChannelServices.GetChannel("tcp");
            if (cd == null)
            {
                ChannelServices.RegisterChannel(tcpChannel);
            }
            Type Dbtype = typeof(DBfunctions);
            DBfunctions remoteDBObject = (DBfunctions)Activator.GetObject(Dbtype, "tcp://" + remoteserver + ":9998/RemoteDBcon");
            RemoteDBcon cc = new RemoteDBcon();

            cc.ds = remoteDBObject.FillData(qry);
            return cc.Dataset;
        }

        public bool imageuploadQuery(string qry, byte[] image)
        {
            IChannel cd = ChannelServices.GetChannel("tcp");
            if (cd == null)
            {
                ChannelServices.RegisterChannel(tcpChannel);
            }
            Type Dbtype = typeof(DBfunctions);
            DBfunctions remoteDBObject = (DBfunctions)Activator.GetObject(Dbtype, "tcp://" + remoteserver + ":9998/RemoteDBcon");
            RemoteDBcon cc = new RemoteDBcon();

            cc.nonq = remoteDBObject.ImageUploadQuery(qry,image);
            bool fl = cc.NonQuery;
            return fl;
        }
    }
}
