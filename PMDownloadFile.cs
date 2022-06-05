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
    public partial class PMDownloadFile : Form
    {
        BaseConnection1 con1 = new BaseConnection1();
        public string fileid = "";
        public static string encrypt = "";
        public static string file1 = "";
        public static string file2 = "";
         public  string text1="";
        public PMDownloadFile()
        {
            InitializeComponent();
        }
        public void filllistbox()
        {

            string query = "select distinct projectid from workassign where pmusername='" + Program.username + "' and status=0";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {

                listBox1.Items.Add(sd[0].ToString());


            }

        }
        public void fillgrid()
        {

            string query = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where projectid=" + listBox1.SelectedItem.ToString() + " and status=0";
            DataSet ds = new DataSet();
            ds = con1.ret_ds(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("Work Not Assigned");
            }



        }
        public void fillcombo()
        {

            string query = "select * from pmupload where projectid=" + listBox1.SelectedItem.ToString() + " and fstatus=0";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {

                comboBox1.Items.Add(sd[0].ToString());

            }



        }
        private void PMDownloadFile_Load(object sender, EventArgs e)
        {
           
            filllistbox();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillgrid();
            fillcombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from pmupload where fileid=" + comboBox1.SelectedItem.ToString() + " and fstatus=0";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {
                tdate.Text = sd[3].ToString();
                tname.Text=sd[4].ToString();
                tid.Text = sd[5].ToString();
                textBox2.Text = sd[1].ToString();
                encrypt= sd[6].ToString();
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = encrypt;
             String sss = "Username:  " + Program.username + " Download the file with ID: " + comboBox1.SelectedItem.ToString() + " successfully";

            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag006^" + sss);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                text1 = Crypto.DecryptStringAES(richTextBox1.Text, textBox1.Text);
                string[] a = text1.Split(new[] { "JOIN" }, StringSplitOptions.None);


                richTextBox2.Text = a[0].ToString();
                richTextBox3.Text = a[1].ToString();
                String sss = "Username:  " + Program.username + " Decrypt the file with ID: " + comboBox1.SelectedItem.ToString() + " successfully";

                TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
                ts.SendMessage("flag007^" + sss);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong Password");
            }
        }
    }
}
