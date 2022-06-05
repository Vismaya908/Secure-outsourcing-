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
    public partial class FileDownload1 : Form
    {
        BaseConnection1 con = new BaseConnection1();
        public static string share1, share2, pass1, pass2="";
        public static string filedir = "";
        string ppath = "";
        Bitmap s1;
        Bitmap s2;
        Bitmap result;

        public FileDownload1()
        {
            InitializeComponent();
        }

        private void splitContainer3_Panel2_Click(object sender, EventArgs e)
        {

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
        private void FileDownload1_Load(object sender, EventArgs e)
        {
           // Program.username = "anu123";
            string query = "select * from datadb where username='" + Program.username + "'";
            SqlDataReader sd = con.ret_dr(query);
            while (sd.Read())
            {
                comboBox2.Items.Add(sd[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                ppath = Application.StartupPath + "\\" + Program.username+ "\\" + comboBox2.SelectedItem.ToString() + ".txt";
                // string ppath="E:\\"+ textBox1.Text + ".txt";
                //   string ppath = Application.StartupPath + "\\image\\" + textBox1.Text + ".txt";
                richTextBox1.Text = System.IO.File.ReadAllText(ppath);
                string query = "select * from datadb where fileid='" + comboBox2.SelectedItem.ToString() + "'";
                SqlDataReader sd = con.ret_dr(query);
                if (sd.Read())
                {
                    share1 = sd[7].ToString();
                    share2 = sd[8].ToString();
                    pass1 = sd[11].ToString();
                    pass2 = sd[12].ToString();

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            String sss = "Username:  " + Program.username + " Download the file with ID: " + Program.fileid + " successfully";

            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag006^" + sss);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text1;
            text1 = System.IO.File.ReadAllText(ppath);
            text1 = Crypto.DecryptStringAES(text1, textBox2.Text);
            KeyMerging km = new KeyMerging(text1);
            km.Show();
            String sss = "Username:  " + Program.username + " Decrypt the file with ID: " + Program.fileid + " successfully";

            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag007^" + sss);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != progressBar1.Maximum)
            {
                progressBar1.Value = progressBar1.Value + 10;
            }
            else
            {
                timer1.Enabled = false;
                result = new Bitmap(s1.Width, s1.Height);
                for (int x = 0; x < result.Width - 1; x += 1)
                {
                    for (int y = 0; y < result.Height; y += 1)
                    {
                        Color c1 = s1.GetPixel(x, y);
                        Color c2 = s2.GetPixel(x, y);

                        if (c1.ToArgb() != Color.Empty.ToArgb() && c2.ToArgb() == Color.Empty.ToArgb())
                        {
                            result.SetPixel(x, y, c1);
                        }
                        else if (c1.ToArgb() == Color.Empty.ToArgb() && c2.ToArgb() != Color.Empty.ToArgb())
                        {
                            result.SetPixel(x, y, c2);
                        }
                        pictureBox3.BackColor = Color.White;
                        pictureBox3.Image = (Image)result;

                    }
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == pass1)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\shares\\" + share1);
            }
            if (textBox3.Text == pass2)
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\shares\\" + share2);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\shares\\" + share1);
            s1 = (Bitmap)Image.FromFile(Application.StartupPath + "\\shares\\" + share1);
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\shares\\" + share2);
            s2 = (Bitmap)Image.FromFile(Application.StartupPath + "\\shares\\" + share2);
            timer1.Enabled = true;
        }
    }
}
