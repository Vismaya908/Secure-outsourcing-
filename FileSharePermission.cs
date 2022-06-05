using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace encryption
{
    public partial class FileSharePermission : Form
    {
        BaseConnection1 con = new BaseConnection1();
        public static string shareid="";

        public FileSharePermission()
        {
            InitializeComponent();
        }

        private void FileSharePermission_Load(object sender, EventArgs e)
        {
          // Program.username = "anu123";
           
           string query = "select * from datadb where username='" +Program.username + "'";
            SqlDataReader sd = con.ret_dr(query);
            while (sd.Read())
            {
                comboBox2.Items.Add(sd[0].ToString());
            }
            string query1 = "select * from login";
            SqlDataReader sd1 = con.ret_dr(query1);
            while (sd1.Read())
            {
                comboBox3.Items.Add(sd1[1].ToString());
            }
            getshareid();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = Program.username +"\\"+comboBox2.SelectedItem.ToString() + ".txt";
            StreamReader rdr = new StreamReader(filename, true);
            string str = "";

            while (!rdr.EndOfStream)
            {
                str = rdr.ReadLine();
                richTextBox1.Text = richTextBox1.Text + " " + str;
            }
            
           
            string query = "select * from datadb where fileid=" + comboBox2.SelectedItem.ToString() + "";
            SqlDataReader sd = con.ret_dr(query);
            if (sd.Read())
            {
                tdate.Text = sd[5].ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString()=="Shared")
            {
                comboBox3.Enabled=true;
            }
            else
            {
                comboBox3.Enabled=false;
            }

        
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        public void getshareid()
        {

            string query = "select isnull(max(shareid)+1,200) from sharedb";
            SqlDataReader sd = con.ret_dr(query);
            if (sd.Read())
            {
                shareid = sd[0].ToString();
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Shared")
            {
                string ss = "Shared";
                string str2 = "update datadb set priority='" + ss + "',status=3 where fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str2);
                string str3 = "insert into sharedb values (" + shareid + "," + comboBox2.SelectedItem.ToString() + ",'" + comboBox3.SelectedItem.ToString() + "','"+Program.username+"')";
                con.exec(str3);
                MessageBox.Show("Permission Granted");
            }
            else if (comboBox1.SelectedItem.ToString() == "Secret")
            {
                string ss = "Secret";
                string str2 = "update datadb set priority='" + ss + "',status=1 where fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str2);
                MessageBox.Show("Permission Granted");
            }
            else 
            {
                string ss = "Public";
                string str2 = "update datadb set priority='" + ss + "',status=2 where fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str2);
                MessageBox.Show("Permission Granted");
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
