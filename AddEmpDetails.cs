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
    public partial class AddEmpDetails : Form
    {
        BaseConnection1 con1 = new BaseConnection1();
        BaseConnection1 con2 = new BaseConnection1();
        public static string userid = "";
        public AddEmpDetails()
        {
            InitializeComponent();
        }
        public void getid()
        {

            string query = "select isnull(max(Empid)+1,200) from emptb";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {
                userid = sd[0].ToString();
                textBox9.Text = userid;

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox9.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox8.Text == "" || textBox7.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("Please Enter Details.....");
            }
            else
            {
                if (textBox2.Text == textBox3.Text)
                {

                    string fpath = Application.StartupPath + "\\" + textBox1.Text;

                    if (Directory.Exists(fpath))
                    {
                        MessageBox.Show("That User exists already.");
                        return;
                    }
                    else
                    {
                        //Program.username = "chandu123";
                        DirectoryInfo di = Directory.CreateDirectory(fpath);
                        string stat = "0";
                        string s1 = "insert into emptb values(" + textBox9.Text + ",'" + Program.username + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox11.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox5.Text + "','" + comboBox2.SelectedItem.ToString() + "'," + stat + ")";
                        con1.exec(s1);
                        if (comboBox2.SelectedItem.ToString() == "PROJECT LEADER")
                        {
                            string stat1 = "1";
                            string s2 = "insert into Login values(" + textBox9.Text + ",'" + textBox1.Text + "','" + textBox2.Text + "'," + stat1 + ")";
                            con2.exec(s2);
                        }
                        else if (comboBox2.SelectedItem.ToString() == "PROGRAMMER")
                        {


                            string stat1 = "2";
                            string s2 = "insert into Login values(" + textBox9.Text + ",'" + textBox1.Text + "','" + textBox2.Text + "'," + stat1 + ")";
                            con2.exec(s2);
                        }
                        MessageBox.Show("Successfully inserted");
                        textBox1.Text = "";
                        textBox5.Text = "";
                        textBox2.Text = "";
                        textBox9.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox8.Text = "";
                        textBox7.Text = "";
                        textBox11.Text = "";


                    }
                }
                else
                {
                    MessageBox.Show("Password Not Matching");
                }
            }
        }

        private void AddEmpDetails_Load(object sender, EventArgs e)
        {
            getid();
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
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') )
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
