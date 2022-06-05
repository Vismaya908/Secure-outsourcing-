using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using RemoteDatabase;
using System.IO;

namespace encryption
{
    public partial class reg : Form
    {
        DBcon con = new DBcon(Program.ServerIP);
        BaseConnection1 con1 = new BaseConnection1();
        public static string userid = "";
        RSAEncryption RSA = new RSAEncryption();
      
        bool regValidations()
        {
            
            if (textBox1.Text == "" )
            {
                MessageBox.Show("Username Cannot be null");
                return false;
            }
            if (textBox1.Text.Length < 6)
            {
                MessageBox.Show("Username is too short");
                return false;
            }
            

            return true;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return cp;
            }
        }
        public reg()
        {
            InitializeComponent();
        }

        private void password_Click(object sender, EventArgs e)
        {

        }
        public void getfileid()
        {

            string query = "select isnull(max(userid)+1,100) from reg";
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
                string gen = "";
                if (regValidations() == true)
                {
                    if (radioButton1.Checked == true)
                    {
                        gen = radioButton1.Text;
                    }
                    else
                    {
                        gen = radioButton2.Text;
                    }


                    string fpath = Application.StartupPath + "\\" + textBox1.Text;

                    if (Directory.Exists(fpath))
                    {
                        MessageBox.Show("That User exists already.");
                        return;
                    }
                    else
                    {
                        if (textBox2.Text == textBox3.Text)
                        {
                            DirectoryInfo di = Directory.CreateDirectory(fpath);
                            string stat = "0";
                            string s1 = "insert into Login values(" + textBox9.Text + ",'" + textBox1.Text + "','" + textBox2.Text + "'," + stat + ")";
                            con1.exec(s1);


                            string s = "insert into reg values(" + textBox9.Text + ",'" + textBox4.Text + "','" + gen + "','" + textBox7.Text + "','" + textBox11.Text + "','" + textBox5.Text + "','" + textBox10.Text + "','" + textBox8.Text + "','" + textBox1.Text + "','" + textBox2.Text + "'," + stat + ")";



                            if (con.ExecuteQuery(s))
                            {

                                MessageBox.Show("Inserted successfully");
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
                                textBox1.Focus();

                            }
                        }
                        else
                        {
                            MessageBox.Show("password not matching");
                        }
                    }
                }
            }

        }

        private void reg_Load(object sender, EventArgs e)
        {
            getfileid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
         
            textBox7.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Incorrect Password");
                textBox3.Text = "";
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
   
            if (Regex.IsMatch(textBox4.Text,@"[0-9]"))
            {
                MessageBox .Show (" Sorry wrong format");
                textBox4 .Text ="";

            }
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            Regex regxobj = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            if (!regxobj.IsMatch(textBox7.Text))
            {
                MessageBox.Show(" Phone number should have at least 10 nos");
                textBox4.Text = "";

            }
        }

        private void textBox7_Validated(object sender, EventArgs e)
        {
            Regex regex=new Regex (@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
            if (!regex.IsMatch(textBox7.Text))
            {
                MessageBox.Show(" Invalid Email address");
                textBox4.Text = "";
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
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
    }
}
