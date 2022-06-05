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
    public partial class PMAssignWork : Form
    {
        BaseConnection1 con1 = new BaseConnection1();
        public static string pid = "",projectid="";

        public PMAssignWork()
        {
            InitializeComponent();
        }
        public void getemp()
        {

            string query = "select empusername, designation from emptb where pmusername='" + Program.username + "' and status=0";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {
                if (sd[1].ToString() == "PROJECT LEADER".ToString())
                {
                    listBox2.Items.Add(sd[0].ToString());
                }
                else
                {
                    listBox1.Items.Add(sd[0].ToString());
                }

            }

        }
        public void fillgrid()
        {

            string query = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where pmusername='" + Program.username + "' and status=0";
            DataSet ds=new DataSet();
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
        private void PMAssignWork_Load(object sender, EventArgs e)
        {
           
            getemp();
            fillgrid();

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where empusername='" + listBox1.SelectedItem.ToString() + "' and status=0";
            DataSet ds = new DataSet();
            ds = con1.ret_ds(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query1 = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where empusername='" + listBox1.SelectedItem.ToString() + "' and status=0";
                SqlDataReader sd = con1.ret_dr(query1);
                if (sd.Read())
                {
                    textBox5.Text = sd[0].ToString();
                    textBox6.Text = sd[1].ToString();
                    textBox7.Text = sd[7].ToString();
                    textBox8.Text = sd[6].ToString();
                    textBox1.Text = sd[2].ToString(); 
                    textBox10.Text = sd[3].ToString();
                    textBox2.Text = sd[4].ToString();
                    textBox3.Text = sd[5].ToString();
                }
            }
            else
            {


                string query2 = "select * from emptb where empusername='" + listBox1.SelectedItem.ToString() + "'";
                SqlDataReader sd = con1.ret_dr(query2);
                if (sd.Read())
                {
                    textBox1.Text = sd[2].ToString();
                    textBox10.Text = sd[10].ToString();
                    textBox2.Text = sd[6].ToString();
                    textBox3.Text = sd[7].ToString();
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    button3.Enabled = true;
                    button4.Enabled = true;
                }

            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where empusername='" + listBox2.SelectedItem.ToString() + "' and status=0";
            DataSet ds = new DataSet();
            ds = con1.ret_ds(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query1 = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where empusername='" + listBox2.SelectedItem.ToString() + "' and status=0";
                SqlDataReader sd = con1.ret_dr(query1);
                if (sd.Read())
                {
                    textBox5.Text = sd[0].ToString();
                    textBox6.Text = sd[1].ToString();
                    textBox7.Text = sd[7].ToString();
                    textBox8.Text = sd[6].ToString();
                    textBox1.Text = sd[2].ToString();
                    textBox10.Text = sd[3].ToString();
                    textBox2.Text = sd[4].ToString();
                    textBox3.Text = sd[5].ToString();
                }
            }
            else
            {


                string query2 = "select * from emptb where empusername='" + listBox2.SelectedItem.ToString() + "'";
                SqlDataReader sd = con1.ret_dr(query2);
                if (sd.Read())
                {
                    textBox1.Text = sd[2].ToString();
                    textBox10.Text = sd[10].ToString();
                    textBox2.Text = sd[6].ToString();
                    textBox3.Text = sd[7].ToString();
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    button3.Enabled = true;
                    button4.Enabled = true;
                }

            }
        }
        public void getid()
        {

            string query = "select isnull(max(pid)+1,300) from workassign";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {

                pid = sd[0].ToString();

            }

        }
        public void getprojectid()
        {

            string query = "select isnull(max(projectid)+1,400) from workassign";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {
                textBox5.Text = sd[0].ToString();


            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox6.Enabled = true;
            getid();
            getprojectid();
            projectid = textBox5.Text;
            textBox8.Text = DateTime.Now.ToShortDateString();
            textBox7.Text = "Started".ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stat = "0";
            string s1 = "insert into workassign values(" + pid + "," + projectid + ",'" + textBox6.Text + "','" + Program.username + "','" + textBox1.Text + "','" + textBox10.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox8.Text + "','" + textBox7.Text + "'," + stat + ")";
            con1.exec(s1);
            MessageBox.Show("Successfully inserted");
            fillgrid();
            textBox1.Text = "";
            textBox10.Text = "";
            textBox2.Text = "";
            textBox3.Text ="";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s1 = "update workassign set pstatus='" + textBox7.Text + "', status=1 where empusername='" + textBox1.Text + "'";
            con1.exec(s1);
            MessageBox.Show("Successfully Updated");
            fillgrid();
            textBox1.Text = "";
            textBox10.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Visible = false;
            button1.Enabled = true;
            comboBox1.Visible = true;
            getid();
            string query = "select projectid from workassign where status=0";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {
                comboBox1.Items.Add(sd[0].ToString());
            }

            button4.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            projectid = comboBox1.SelectedItem.ToString();
            string query = "select projectname from workassign where projectid='"+comboBox1.SelectedItem.ToString()+"' and status=0";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {
                textBox6.Text = sd[0].ToString();
            }
            textBox8.Text = DateTime.Now.ToShortDateString();
        }
    }
}
