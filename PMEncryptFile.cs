using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Net;

namespace encryption
{
    public partial class PMEncryptFile : Form
    {
        public static string fid, pid, fdate, fname, priority, file1, file2 = "";
        string filename;
        public static string fileencrypt = "";
        string ppth;
        string pwd;
        public string text = "";
        public string status = "0";
        BaseConnection1 con = new BaseConnection1();
        string sourceFileName = "";

        string destFileLocation = "";

        int index = 0;

        long maxFileSize = 10000;

        byte[] buffer = new byte[65536];
        public PMEncryptFile()
        {
            InitializeComponent();
        }
        public PMEncryptFile(string fid1,string pid1,string fdate1,string fname1, string priority1,string file11, string file21)
        {
            InitializeComponent();
            fid = fid1;
            pid = pid1;
            fdate = fdate1;
            fname = fname1;
            priority = priority1;
            file1 = file11;
            file2 = file21;

        }
        private void PMEncryptFile_Load(object sender, EventArgs e)
        {
            string file = file1 + file2;
            fileencrypt = file1 + "JOIN" + file2;
            richTextBox2.Text = file;


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            pwd = textBox1.Text;
            text = Crypto.EncryptStringAES(fileencrypt, textBox1.Text);
            richTextBox1.Text = text;
            filename = fid + ".txt";
            ppth = Application.StartupPath + "\\" + Program.username + "\\" + filename;
            StreamWriter sw1 = new StreamWriter(ppth);
            sw1.WriteLine(text);
            sw1.Close();
            String sss = "Username:  " + Program.username + " First Encryption the file with ID: " +fid + " successfully";

            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag005^" + sss);
        }

        private void button3_Click(object sender, EventArgs e)
        {
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

            if (priority == "Secret".ToString())
            {
                 status = "1";
            }
            if (priority == "Public".ToString())
            {
               status = "2";
            }

          
            string status2 = "0";

            string str2 = "insert into pmupload values (" + fid + "," + pid + ",'" + Program.username + "','" + fdate + "','" + fname + "','" + priority + "','" + text + "','" + pwd + "'," + status + "," + status2 + ")";
            con.exec(str2);
            String sss = "Username:  " + Program.username + " Upload the file with ID: " + fid + " successfully";
            String sss1 = "Save File to Proxy";
            TsSender ts = new TsSender(IPAddress.Parse(Program.ServerIP), 9000);
            ts.SendMessage("flag004^" + sss + "^" + text + "^" + sss1 + "^" + fid);
            MessageBox.Show("File successfully uploaded");
            this.Close();
        }
    }
}
