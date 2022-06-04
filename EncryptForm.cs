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

using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Net;

namespace encryption
{
    public partial class EncryptForm : Form
    {
        string filename;
        string ppth;
        string pwd;
        private Bitmap bmp = null;
        private string extractedText = string.Empty;

        BaseConnection1 con = new BaseConnection1();
        private Size IMAGE_SIZE = new Size(437, 106);
        private const int GENERATE_IMAGE_COUNT = 2;
        private Bitmap[] m_EncryptedImages;
        public string imageString = "";
        public string text = "";
        public EncryptForm(Image img)
        {
            InitializeComponent();
          //  pictureBox1.Image = img;
        }
        public EncryptForm(string path)
        {
            InitializeComponent();
            StreamReader rdr = new StreamReader(path, true);
            string str = "";

            while (!rdr.EndOfStream)
            {
                str = rdr.ReadLine();
                richTextBox2.Text = richTextBox1.Text + " " + str;
            }
            
        }
        string sourceFileName = "";

        string destFileLocation ="";

        int index = 0;

        long maxFileSize = 10000;

        byte[] buffer = new byte[65536];

        private void button1_Click(object sender, EventArgs e)
        {

            pwd = textBox1.Text;
            text = Crypto.EncryptStringAES(richTextBox2.Text, textBox1.Text);
            richTextBox1.Text = text;
            filename = Program.fileid + ".txt";
            ppth = Application.StartupPath + "\\"+Program.username+"\\" + filename;
            StreamWriter sw1 = new StreamWriter(ppth);
            sw1.WriteLine(text);
            sw1.Close();
            String sss = "Username:  " + Program.username + " First Encryption the file with ID: " + Program.fileid + " successfully";
           
            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag005^" + sss);

        }
        private Bitmap[] GenerateImage(string inputText)
        {
            Bitmap finalImage = new Bitmap(IMAGE_SIZE.Width, IMAGE_SIZE.Height);
            Bitmap tempImage = new Bitmap(IMAGE_SIZE.Width / 2, IMAGE_SIZE.Height);
            Bitmap[] image = new Bitmap[GENERATE_IMAGE_COUNT];

            Random rand = new Random();
            SolidBrush brush = new SolidBrush(Color.Black);
            Point mid = new Point(IMAGE_SIZE.Width / 2, IMAGE_SIZE.Height / 2);

            Graphics g = Graphics.FromImage(finalImage);
            Graphics gtemp = Graphics.FromImage(tempImage);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font font = new Font("Times New Roman", 48);
            Color fontColor;

            g.DrawString(inputText, font, brush, mid, sf);
            gtemp.DrawImage(finalImage, 0, 0, tempImage.Width, tempImage.Height);


            for (int i = 0; i < image.Length; i++)
            {
                image[i] = new Bitmap(IMAGE_SIZE.Width, IMAGE_SIZE.Height);
            }


            int index = -1;
            int width = tempImage.Width;
            int height = tempImage.Height;
            for (int x = 0; x < width; x += 1)
            {
                for (int y = 0; y < height; y += 1)
                {
                    fontColor = tempImage.GetPixel(x, y);
                    index = rand.Next(image.Length);
                    if (fontColor.Name == Color.Empty.Name)
                    {
                        for (int i = 0; i < image.Length; i++)
                        {
                            if (index == 0)
                            {
                                image[i].SetPixel(x * 2, y, Color.Black);
                                image[i].SetPixel(x * 2 + 1, y, Color.Empty);
                            }
                            else
                            {
                                image[i].SetPixel(x * 2, y, Color.Empty);
                                image[i].SetPixel(x * 2 + 1, y, Color.Black);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < image.Length; i++)
                        {
                            if ((index + i) % image.Length == 0)
                            {
                                image[i].SetPixel(x * 2, y, Color.Black);
                                image[i].SetPixel(x * 2 + 1, y, Color.Empty);
                            }
                            else
                            {
                                image[i].SetPixel(x * 2, y, Color.Empty);
                                image[i].SetPixel(x * 2 + 1, y, Color.Black);
                            }
                        }
                    }
                }
            }
            int id = 0;
            if (!Directory.Exists(Application.StartupPath + "\\shares"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\shares");
            }
            else
            {
                string[] filelist = Directory.GetFiles(Application.StartupPath + "\\shares", "*.png");
                int i = filelist.Length;
                id = i;
            }
            id++;
            string pic1name = "share" + id + ".png";
            tshare1.Text = pic1name;
            id++;
            string pic2name = "share" + id + ".png";
            tshare2.Text = pic2name;

            image[0].Save(Application.StartupPath + "\\shares\\" + pic1name, System.Drawing.Imaging.ImageFormat.Png);
            image[1].Save(Application.StartupPath + "\\shares\\" + pic2name, System.Drawing.Imaging.ImageFormat.Png);
            brush.Dispose();
            tempImage.Dispose();
            finalImage.Dispose();

            return image;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = (Bitmap)pictureBox1.Image;
            //string filename = Program.fileid+ ".png";
            //bmp.Save(Application.StartupPath + "\\image\\" + filename, ImageFormat.Png);
            //Image im = pictureBox1.Image;
            //Bitmap sBit = (Bitmap)pictureBox1.Image;
            //imageString = ImageToBase64(im, ImageFormat.Jpeg);//ConvertImage(sBit);


            //if (label2.Text != "")
            //{
            //    panelCanvas.BackColor = Color.White;
            //    if (m_EncryptedImages != null)
            //    {
            //        for (int i = m_EncryptedImages.Length - 1; i > 0; i--)
            //        {
            //            m_EncryptedImages[i].Dispose();
            //        }
            //        Array.Clear(m_EncryptedImages, 0, m_EncryptedImages.Length);
            //    }

            //    m_EncryptedImages = GenerateImage(imageString);

            //    panelCanvas.Invalidate();
            //}

           
            
        }
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          //  Program.username = "anu";
            sourceFileName = ppth;
            destFileLocation = Application.StartupPath + "\\block\\";

            using (Stream source = File.OpenRead(sourceFileName))
            {

                while (source.Position < source.Length)
                {

                    index++;



                    // Create a new sub File, and read into t

                    string newFileName = Path.Combine(destFileLocation, Path.GetFileNameWithoutExtension(sourceFileName));

                    newFileName += index.ToString() + Path.GetExtension(sourceFileName);

                    using (Stream destination = File.OpenWrite(newFileName))
                    {

                        while (destination.Position < maxFileSize)
                        {

                            // Work out how many bytes to read

                            int bytes = source.Read(buffer, 0, (int)Math.Min(maxFileSize, buffer.Length));

                            destination.Write(buffer, 0, bytes);



                            // Are we at the end of the file?

                            if (bytes < Math.Min(maxFileSize, buffer.Length))
                            {

                                break;

                            }

                        }

                    }

                }

            }



            string status = "0";
            string status2 = "0";
            //string share1 = "0";
            //string share2 = "0";
            string str2 = "insert into datadb values (" + Program.fileid + ",'" + Program.username + "','" + filename + "','" + Program.fdate + "','" + text + "','" + Program.fpriority + "','" + pwd + "','" + tshare1.Text + "','" + tshare2.Text + "'," + Program.fileastatus + "," + status2 + ",'"+textBox2.Text+"','"+textBox3.Text+"')";
            con.exec(str2);
            String sss = "Username:  "+ Program.username + " Upload the file with ID: " + Program.fileid + " successfully";
            String sss1 = "Save File to Proxy";
            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag004^" + sss+"^"+text+"^"+sss1+"^"+Program.fileid);
            MessageBox.Show("File successfully uploaded");
            this.Close();
        }
        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (m_EncryptedImages != null)
            {
                Graphics g = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, 0, 0);
                for (int i = 0; i < m_EncryptedImages.Length; i++)
                {
                    rect.Size = m_EncryptedImages[i].Size;
                    g.DrawImage(m_EncryptedImages[i], rect);
                    rect.Y += m_EncryptedImages[i].Height + 5;
                }

                g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), rect.Location, new Point(rect.Width, rect.Y));
                rect.Y += 5;
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                panelCanvas.BackColor = Color.White;
                if (m_EncryptedImages != null)
                {
                    for (int i = m_EncryptedImages.Length - 1; i > 0; i--)
                    {
                        m_EncryptedImages[i].Dispose();
                    }
                    Array.Clear(m_EncryptedImages, 0, m_EncryptedImages.Length);
                }

                m_EncryptedImages = GenerateImage(textBox1.Text);

                panelCanvas.Invalidate();
            }
        }

        private void panelCanvas_Paint_1(object sender, PaintEventArgs e)
        {
            if (m_EncryptedImages != null)
            {
                Graphics g = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, 0, 0);
                for (int i = 0; i < m_EncryptedImages.Length; i++)
                {
                    rect.Size = m_EncryptedImages[i].Size;
                    g.DrawImage(m_EncryptedImages[i], rect);
                    rect.Y += m_EncryptedImages[i].Height + 5;
                }

                g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), rect.Location, new Point(rect.Width, rect.Y));
                rect.Y += 5;
            }
        }
    }
}
