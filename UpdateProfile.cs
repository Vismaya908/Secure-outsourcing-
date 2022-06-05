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
    public partial class UpdateProfile : Form
    {
        BaseConnection1 con = new BaseConnection1();
        public UpdateProfile()
        {
            InitializeComponent();
        }

        private void UpdateProfile_Load(object sender, EventArgs e)
        {
         
           // Program.username = "anu123";
            textBox1.Text = Program.username;
            string query = "select * from reg where username='" + Program.username + "'";
            SqlDataReader sd = con.ret_dr(query);
            if (sd.Read())
            {
                textBox4.Text = sd[2].ToString();
                textBox5.Text = sd[3].ToString();
                textBox6.Text = sd[5].ToString();
                textBox2.Text = sd[4].ToString();
                textBox7.Text = sd[6].ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str2 = "update reg set name='" + textBox4.Text + "',address='" + textBox5.Text + "',gender='" + textBox2.Text + "',phone='" + textBox6.Text + "',email='" + textBox7.Text + "' where username='" + Program.username + "'";
            con.exec(str2);
            MessageBox.Show("Updated Successfully");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
