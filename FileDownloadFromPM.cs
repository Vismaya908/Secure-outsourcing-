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
using System.Net;

namespace encryption
{
    public partial class FileDownloadFromPM : Form
    {
        BaseConnection1 con1 = new BaseConnection1();
        public string fileid = "";
        public static string file1 = "";
        public static string file2 = "";
        public static string pass="";
        public FileDownloadFromPM()
        {
            InitializeComponent();
        }

        private void splitContainer3_Panel2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FileDownloadFromPM_Load(object sender, EventArgs e)
        {
            string query = "select projectid from workassign where empusername='" + Program.username + "' and status=0";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {

                comboBox1.Items.Add(sd[0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select fileid from pmupload where projectid=" + comboBox1.SelectedItem.ToString() + " and fstatus=0";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {

                comboBox2.Items.Add(sd[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select code,password from pmupload where fileid=" + comboBox2.SelectedItem.ToString() + " and fstatus=0";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {

                richTextBox1.Text = sd[0].ToString();
                pass = sd[1].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pass == textBox2.Text)
            {
                string text1;

                text1 = Crypto.DecryptStringAES(richTextBox1.Text, textBox2.Text);

                string[] a = text1.Split(new[] { "JOIN" }, StringSplitOptions.None);
                if (Program.des == "PROJECT LEADER")
                {

                    richTextBox2.Text = a[0].ToString();
                }
                if (Program.des == "PROGRAMMER")
                {

                    richTextBox2.Text = a[1].ToString();
                }
                String sss = "Username:  " + Program.username + " Decrypt the file with ID: " + comboBox2.SelectedItem.ToString() + " successfully";

                TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
                ts.SendMessage("flag007^" + sss);
            }
            else
            {
                MessageBox.Show("invalid password");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
