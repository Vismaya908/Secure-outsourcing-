using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace encryption
{
    class BaseConnection
    {
        public const string SQLCONNECTION = "Server=127.0.0.1;uid=sa;pwd=yuva;database=securepre";
        public SqlConnection opencon()
        {
            SqlConnection con=new SqlConnection(SQLCONNECTION);
            con.Open();
            return con;
        }
    }
}
