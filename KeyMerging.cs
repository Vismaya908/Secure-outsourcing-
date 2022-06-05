using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace encryption
{
    public partial class KeyMerging : Form
    {
         Bitmap s1;
        Bitmap s2;
        Bitmap result;
        string imgdata = "";

        public KeyMerging()
        {
            InitializeComponent();
           
        }
        public KeyMerging(string imgdata1)
        {
           
            InitializeComponent();
            richTextBox1.Text = imgdata1;
         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            //Decryption obj = new Decryption();
            //ActiveForm.Hide();
            //obj.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
