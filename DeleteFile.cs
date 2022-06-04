using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace encryption
{
    public partial class DeleteFile : Form
    {
        BaseConnection1 con = new BaseConnection1();
        BaseConnection1 con1 = new BaseConnection1();
        public DeleteFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteFile_Load(object sender, EventArgs e)
        {
            string query = "select fileid from datadb where username='" + Program.username + "'";
            SqlDataReader sd = con.ret_dr(query);
            while (sd.Read())
            {
                comboBox2.Items.Add(sd[0].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string query = "select shareduser from sharedb where fileid=" + comboBox2.SelectedItem.ToString() + "";
            SqlDataReader sd = con.ret_dr(query);
            while (sd.Read())
            {
                listBox1.Items.Add(sd[0].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str2 = "delete from datadb where fileid=" + comboBox2.SelectedItem.ToString() + "";
            con.exec(str2);
            string str3 = "delete from sharedb where fileid=" + comboBox2.SelectedItem.ToString() + "";
            con1.exec(str3);
            MessageBox.Show("Delete Successfully");
        }
    }
}
