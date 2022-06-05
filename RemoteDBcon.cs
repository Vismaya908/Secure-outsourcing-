using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Data.SqlClient;
using System.Data;
using Crypter;

namespace DBRemoting
{
    public class RemoteDBcon : MarshalByRefObject, DBfunctions
    {
        bool nonq = false;
        TcpChannel tcpChannel = null;
        public SqlDataReader rdr;
        public DataSet ds = new DataSet();
        public static string LocalDB = null;

        

        public bool NonQuery
        {
            get { return nonq; }
        }
        public SqlDataReader Reader
        {
            get { return rdr; }
        }
        public DataSet Dataset
        {
            get { return ds; }
        }
        public RemoteDBcon()
        {
        }
        public RemoteDBcon(string Database)
        {

            tcpChannel = new TcpChannel(9998);
            LocalDB = Database;
            Console.WriteLine("Database Server started...");
            //Channel Creation
            ChannelServices.RegisterChannel(tcpChannel);

            //Remote Config
            RemotingConfiguration.RegisterWellKnownServiceType(Type.GetType("DBRemoting.RemoteDBcon"), "RemoteDBcon", WellKnownObjectMode.Singleton);

        }


        private SqlConnection opencon()
        {
            SqlConnection con = new SqlConnection("server=.;database=" + LocalDB + ";uid=sa;pwd=yuva");
            con.Open();
            return con;
        }
        public bool ExecuteNonQuery(string qry)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand(qry, opencon());
                if (cmd.ExecuteNonQuery() >= 0)
                {
                    nonq = true;
                if(qry.StartsWith("insert into reg values"))
                {

                    ProxyServer.Program.UserCount++;
                    crypter RSAEN = new crypter();
                    RSAEN.generate_key_pair(true,@"C:\Windows\Temp");
                    ProxyServer.User userobj = new ProxyServer.User();

                    qry = qry.Replace("insert into reg values(", " ");
                    userobj.addUserKey("C:\\Windows\\Temp", qry.Split(',')[8]);

                }
                    return true;
                }
                else
                {
                    nonq = false;
                    return false;
                }
            }
            catch
            {
                nonq = false;
                return false;
            }
        }
        public SqlDataReader ExecuteReader(string qry)
        {
            rdr = new SqlCommand(qry, opencon()).ExecuteReader();
            return rdr;
        }

        public DataSet FillData(string qry)
        {
            int d = new SqlDataAdapter(qry, opencon()).Fill(ds); ;
            return ds;
        }

        public bool ImageUploadQuery(string qry, byte[] imagebyte)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, opencon());
                cmd.Parameters.AddWithValue("@image", (object)imagebyte);
                if (cmd.ExecuteNonQuery() >= 0)
                {
                    nonq = true;
                    return true;
                }
                else
                {
                    nonq = false;
                    return false;
                }
            }
            catch
            {
                nonq = false;
                return false;
            }


        }
    }

    public interface DBfunctions
    {
        bool ExecuteNonQuery(string qry);
        SqlDataReader ExecuteReader(string qry);
        DataSet FillData(string qry);
        bool ImageUploadQuery(string qry, byte[] imagebyte);

    }

    
}
