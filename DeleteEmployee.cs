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
    public partial class DeleteEmployee : Form
    {
        BaseConnection1 con1 = new BaseConnection1();
        public DeleteEmployee()
        {
            InitializeComponent();
        }
        public void getid()
        {

            string query = "select empusername from emptb where pmusername='"+Program.username+"'";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {
                comboBox3.Items.Add(sd[0].ToString());

            }

        }
        private void DeleteEmployee_Load(object sender, EventArgs e)
        {
            //Program.username = "chandu123";
            getid();


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            string query = "select * from emptb where empusername='" + comboBox3.SelectedItem.ToString() + "'";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {
                textBox4.Text = sd[4].ToString();
                textBox5.Text = sd[9].ToString();
                textBox9.Text = sd[5].ToString();
                textBox1.Text = sd[0].ToString();
                textBox8.Text = sd[8].ToString();
                textBox2.Text = sd[10].ToString();
                textBox11.Text = sd[6].ToString();
                textBox7.Text = sd[7].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox9.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox8.Text == "" || textBox7.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("Please Enter Details.....");
            }
            else
            {

                string s1 = "update emptb set address='" + textBox5.Text + "', dob='" + textBox8.Text + "', phone='" + textBox11.Text + "', email='" + textBox7.Text + "', designation='" + textBox2.Text + "' where empusername='" + comboBox3.SelectedItem.ToString() + "'";
                con1.exec(s1);
                MessageBox.Show("Successfully Updated");
                textBox1.Text = "";
                textBox5.Text = "";
                textBox2.Text = "";
                textBox9.Text = "";
                
                textBox4.Text = "";
                textBox5.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox11.Text = "";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str3 = "delete from emptb where empusername='" + comboBox3.SelectedItem.ToString() + "'";
            con1.exec(str3);
            MessageBox.Show("Delete Successfully");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar == ' '))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '/'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
