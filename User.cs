using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace ProxyServer
{
    class User
    {
       
       
        SqlConnection con = new SqlConnection();

        public User()
        {
            con = new SqlConnection("server=localhost;database=securepre;uid=sa;pwd=yuva");
            con.Open();
        }

        public string getUserCount()
        {
            SqlCommand cmd = new SqlCommand("select count(username) from reg",con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                
                return rdr[0].ToString();
            }
            else
                return "0";

            rdr.Close();

        }

        public List<string> getUser(string username)
        {
            List<string> lst = new List<string>();
            SqlCommand cmd = new SqlCommand("select * from reg where username='"+username+"'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {

                for (int i = 0; i < rdr.FieldCount; i++)
                    lst.Add(rdr[i].ToString());
            }
           

            rdr.Close();

            return lst;
        }

        public void addUserKey(string path,string username)
        {
            string pubxml = File.ReadAllText(path + "\\PublicKey.xml");
            string prvxml = File.ReadAllText(path + "\\PrivateKey.xml");

            string qry = "insert into proxy values("+username+",'"+prvxml+"','"+pubxml+"','0.0.0.0')";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
        }


        public void userIPupdate(string username, string IPaddress)
        {
            string qry = "update proxy set ipaddress='"+IPaddress+"' where username='"+username+"'";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
        }
    }
}
