using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace encryption.DataLayer
{
    class videoconnection:BaseConnection
    {
        private const string Filetb_Insert = "Filetb_Insert";
        private const string Video_Select = "Video_Select";
        private const string Video_Select4mtag = "Video_Select4mtag";
        private const string Videotb_Delete = "Videotb_Delete";
        private const string Videotb_Update = "Videotb_Update";
        private const string Videotb_Update_All = "Videotb_Update_All";

        //fn to edit video details........
        public bool editvideo(string name, string tag, string size, string location,int vid)
        {
           
            using (SqlConnection con = new SqlConnection(SQLCONNECTION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Videotb_Update, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@videoID", vid);
                cmd.Parameters.AddWithValue("@name",name);
                cmd.Parameters.AddWithValue("@tag",tag);
                cmd.Parameters.AddWithValue("@size",size);
                cmd.Parameters.AddWithValue("@location",location);
                cmd.ExecuteNonQuery();
                return true;

            }

        }
        //update
        public bool updateallvideo(string ip, int status)
        {

            using (SqlConnection con = new SqlConnection(SQLCONNECTION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Videotb_Update_All, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ip", ip);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();
                return true;

            }

        }
        //fn to delete a video given videoid
        public bool deletevideo(int videoid)
        {
            using (SqlConnection con = new SqlConnection(SQLCONNECTION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Videotb_Delete, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@videoID ",videoid);
                cmd.ExecuteNonQuery();
                return true;

            }

        }
        
        //fn to get video details given tag name
        public DataSet getvideodetails4mtag(string tag)
        {
            using (SqlConnection con = new SqlConnection(SQLCONNECTION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Video_Select4mtag, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tag ",tag);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }

        }

        //fn to get video tag names

        public DataSet getvideodetails()
        {
            using (SqlConnection con = new SqlConnection(SQLCONNECTION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Video_Select, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }

        }
        //fn to insert video details........
        public bool insertvideo(string name, string size, string tag, string location, int status)
        {
            using (SqlConnection con = new SqlConnection(SQLCONNECTION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Filetb_Insert, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name ", name);
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@tag", tag);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();
                return true;

            }

        }
    }
}
