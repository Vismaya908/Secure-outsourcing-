using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Net;

namespace encryption
{
    public partial class uploadImage : Form
    {
        public static string fid = "";
        public static string imgpath = "";
        BaseConnection1 con = new BaseConnection1();
        public uploadImage()
        {
            InitializeComponent();
            getfileid();
        }
        public uploadImage(string impath)
        {
            InitializeComponent();
            imgpath = impath;
            getfileid();
        }
        public void getfileid()
        {

            string query = "select isnull(max(fileid)+1,700) from datadb";
            SqlDataReader sd = con.ret_dr(query);
            if (sd.Read())
            {
                fid = sd[0].ToString();
                tid.Text = fid;
                tname.Text = fid;
            }
            tdate.Text = System.DateTime.Now.ToShortDateString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = ofd.Filter = "Text(*.txt)|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                   
                  
                    loadedfile.Text = ofd.FileName;
                    Program.orginalfilepath = loadedfile.Text;
                    StreamReader rdr = new StreamReader(ofd.FileName, true);
                    string str = "";

                    while (!rdr.EndOfStream)
                    {
                        str = rdr.ReadLine();
                        richTextBox1.Text = richTextBox1.Text + " " + str;
                    }
                    label9.Text = Path.GetFileName(loadedfile.Text);
                    Program.filename = Path.GetFileName(loadedfile.Text);
                  
                    if (!File.Exists(Application.StartupPath + "\\manifest1\\" + label9.Text))
                    {
                        using (StreamWriter writer = new StreamWriter(Application.StartupPath + "\\manifest1\\" + label9.Text))
                        {

                            writer.WriteLine(richTextBox1.Text);

                        }
                       
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("error....");
            }
        }

   
        private void button2_Click(object sender, EventArgs e)
        {
           // Program.username = "anu123";
            Program.fileid = tid.Text;
            Program.filename = tid.Text;
            Program.fpriority = comboBox1.SelectedItem.ToString();
            Program.fdate = tdate.Text;
            tname.Text = Program.filename;
            if (comboBox1.SelectedItem.ToString() == "Secret".ToString()) 
            {
                Program.fileastatus = "1";
            }
            if (comboBox1.SelectedItem.ToString() == "Public".ToString())
            {
                Program.fileastatus = "2";
            }
            
            
            EncryptForm obj = new EncryptForm(Application.StartupPath + "\\manifest1\\" + label9.Text);
            ActiveForm.Hide();
            obj.Show();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ttime.Text = System.DateTime.Now.ToLongTimeString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddMedicalImage_Load(object sender, EventArgs e)
        {
            //orginalpic.ImageLocation =imgpath;
            //orginalpic.BackgroundImageLayout = ImageLayout.Stretch;

            //loadedfile.Text = imgpath;
            //Program.orginalfilepath = loadedfile.Text;
        }

    }
}
