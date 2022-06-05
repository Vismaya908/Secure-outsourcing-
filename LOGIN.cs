using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RemoteDatabase;
using System.Threading;
using System.Net;

namespace encryption
{
    public partial class LOGIN : Form
    {
        int xx = 0, ww = 0;
        DBcon Rcon = new DBcon();
        ClientAdvertise client = new ClientAdvertise();
        
        public LOGIN()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //xx = panel1.Location.X;
            //ww = panel1.Size.Width;
            client.Server_Found += new EventHandler(client_Server_Found);
            client.startAdvertise();
            
        }

        void client_Server_Found(object sender, EventArgs e)
        {
            label4.Text = "Server Connected";
            Thread.Sleep(1000);
            Rcon = new DBcon(client.ServerIP);
            Program.ServerIP = client.ServerIP;
            Program.Servername = client.ServerName;
            client.stopAdvertise();
           
           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    xx--;
            //}
            //if (xx > 300)
            //{
            //    ww += 20;
            //    panel1.Location = new Point(xx, panel1.Location.Y);
            //    panel1.Size = new Size(ww, panel1.Size.Height);
            //}


        }
        private void panel1_MouseHover(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please Enter Details.....");
            }
            else
            {
                string user = textBox1.Text;
                string pwd = textBox2.Text;
                string str = "select username,password,status from Login where username='" + textBox1.Text + "' and password='" + textBox2.Text + "' ";

                SqlDataReader dr;
                dr = Rcon.ExecuteReader(str);

                if (dr.Read())
                {
                    if (dr[2].ToString() == "0".ToString())
                    {
                        Program.userName = textBox1.Text;
                        Program.username = textBox1.Text;
                        string des = "Project Manager";
                        TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
                        ts.SendMessage("flag001^" + textBox1.Text + "^" + des);
                        //   MDIParent1 mdi = new MDIParent1();
                        PMManageHome1 mdi = new PMManageHome1();
                        mdi.Show();
                        this.Hide();

                        Rcon.ExecuteQuery("update Proxy set ipaddress='" + Network.getHostIP() + "' where username='" + textBox1.Text + "'");

                    }
                    else if (dr[2].ToString() == "1".ToString())
                    {
                        Program.userName = textBox1.Text;
                        Program.username = textBox1.Text;
                        string des = "PROJECT LEADER";
                        Program.des = des;
                        TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
                        ts.SendMessage("flag008^" + textBox1.Text + "^" + des);
                        //   MDIParent1 mdi = new MDIParent1();
                        Home1 mdi = new Home1();
                        mdi.Show();
                        this.Hide();

                        Rcon.ExecuteQuery("update Proxy set ipaddress='" + Network.getHostIP() + "' where username='" + textBox1.Text + "'");

                    }
                    else if (dr[2].ToString() == "2".ToString())
                    {
                        Program.userName = textBox1.Text;
                        Program.username = textBox1.Text;
                        string des = "PROGRAMMER";
                        Program.des = des;
                        TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
                        ts.SendMessage("flag008^" + textBox1.Text + "^" + des);
                        //   MDIParent1 mdi = new MDIParent1();
                        Home1 mdi = new Home1();
                        mdi.Show();
                        this.Hide();

                        Rcon.ExecuteQuery("update Proxy set ipaddress='" + Network.getHostIP() + "' where username='" + textBox1.Text + "'");

                    }

                }
                else
                {
                    MessageBox.Show("invalid userid or password");
                }
                textBox1.Text = "";
                textBox2.Text = "";
                dr.Close();
            }
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reg regfrm = new reg();
            regfrm.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

       
    }
}
