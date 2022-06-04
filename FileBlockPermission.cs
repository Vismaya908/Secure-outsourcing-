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
    public partial class FileBlockPermission : Form
    {
        BaseConnection1 con = new BaseConnection1();
        public static string shareid = "";
        public FileBlockPermission()
        {
            InitializeComponent();
        }

        private void FileBlockPermission_Load(object sender, EventArgs e)
        {
          //  Program.username = "anu123";
            string ss = "Shared";
            string query = "select * from datadb where username='" + Program.username + "' and priority='"+ss+"'";
            SqlDataReader sd = con.ret_dr(query);
            while (sd.Read())
            {
                comboBox2.Items.Add(sd[0].ToString());
            }
           
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // string filename = comboBox2.SelectedItem.ToString() + ".png";
            string filename = Program.username + "\\" + comboBox2.SelectedItem.ToString() + ".txt";
            StreamReader rdr = new StreamReader(filename, true);
            string str = "";

            while (!rdr.EndOfStream)
            {
                str = rdr.ReadLine();
                richTextBox1.Text = richTextBox1.Text + " " + str;
            }
            
            
            string query = "select shareduser from sharedb where username='" + Program.username + "' and fileid="+comboBox2.SelectedItem.ToString()+"";
            SqlDataReader sd = con.ret_dr(query);
            while (sd.Read())
            {
               textBox1.Text=sd[0].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Public")
            {
                string ss = "Public";
                string str2 = "update datadb set priority='" + ss + "',status=2 where fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str2);
                string str3 = "delete from sharedb where username='" + Program.username + "' and fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str3);
                MessageBox.Show("Permission Successfully Deny");
            }
            else if (comboBox1.SelectedItem.ToString() == "Secret")
            {
                string ss = "Secret";
                string str2 = "update datadb set priority='" + ss + "',status=1 where fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str2);
                string str3 = "delete from sharedb where username='" + Program.username + "' and fileid=" + comboBox2.SelectedItem.ToString() + "";
                con.exec(str3);
                MessageBox.Show("Permission Successfully Deny");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
