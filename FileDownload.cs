using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Data.SqlClient;
namespace encryption
{
    public partial class FileDownload : Form
    {
        public static string filedir = "";
        BaseConnection1 con = new BaseConnection1();
        public FileDownload()
        {
            InitializeComponent();
        }
        public FileDownload(string id,string pass)
        {
            InitializeComponent();
            textBox1.Text = id;
            textBox2.Text = pass;
           
        }
        string ppath = "";
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               // Program.username = "anu123";
                //string filedir = "";
                string query1 = "select username from datadb where fileid=" + textBox1.Text + "";
                SqlDataReader sd1 = con.ret_dr(query1);
                while (sd1.Read())
                {
                    filedir = sd1[0].ToString();
                }
                ppath = Application.StartupPath + "\\" + filedir + "\\" + textBox1.Text + ".txt";
               // string ppath="E:\\"+ textBox1.Text + ".txt";
             //   string ppath = Application.StartupPath + "\\image\\" + textBox1.Text + ".txt";
                richTextBox1.Text = System.IO.File.ReadAllText(ppath);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;

        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            string text1;
            text1 = System.IO.File.ReadAllText(ppath);
            text1 = Crypto.DecryptStringAES(text1, textBox2.Text);
            KeyMerging km = new KeyMerging(text1);
            km.Show();
       
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
