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
    public partial class PMUploadFile : Form
    {
        BaseConnection1 con1 = new BaseConnection1();
        public string fileid = "";
        public static string file1="";
        public static string file2 = "";
        public PMUploadFile()
        {
            InitializeComponent();
        }
        public void filllistbox()
        {

            string query = "select distinct projectid from workassign where pmusername='" + Program.username + "' and status=0";
            SqlDataReader sd = con1.ret_dr(query);
            while (sd.Read())
            {
              
                    listBox1.Items.Add(sd[0].ToString());
               

            }

        }
        public void fillgrid()
        {

            string query = "select projectid,projectname,empusername,designation,phone,emailid,pdate,pstatus from workassign where projectid=" + listBox1.SelectedItem.ToString() + " and status=0";
            DataSet ds = new DataSet();
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
        public void getid()
        {

            string query = "select isnull(max(fileid)+1,500) from pmupload";
            SqlDataReader sd = con1.ret_dr(query);
            if (sd.Read())
            {

               fileid = sd[0].ToString();

            }

        }
        private void PMUploadFile_Load(object sender, EventArgs e)
        {
           
            filllistbox();
            getid();
            tid.Text = fileid.ToString();
            tdate.Text = DateTime.Now.ToShortDateString();
            tname.Text = fileid.ToString()+".txt";
           
               
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillgrid();
            textBox2.Text = listBox1.SelectedItem.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = "";
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
                      //  richTextBox1.Text = richTextBox1.Text + " " + str;
                        richTextBox1.Text =  str;
                        file1 = str;
                    }
                    label5.Text = Path.GetFileName(loadedfile.Text);
                    Program.filename = Path.GetFileName(loadedfile.Text);

                    if (!File.Exists(Application.StartupPath + "\\manifest1\\" + label5.Text))
                    {
                        using (StreamWriter writer = new StreamWriter(Application.StartupPath + "\\manifest1\\" + label5.Text))
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Text = "";
                System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = ofd.Filter = "Text(*.txt)|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {


                    textBox1.Text = ofd.FileName;
                    Program.orginalfilepath1 = textBox1.Text;
                    StreamReader rdr = new StreamReader(ofd.FileName, true);
                    string str = "";
                    
                    while (!rdr.EndOfStream)
                    {
                        str = rdr.ReadLine();
                        richTextBox2.Text = str;
                        file2 = str;
                    }
                    label5.Text = Path.GetFileName(textBox1.Text);
                    Program.filename1 = Path.GetFileName(textBox1.Text);

                    if (!File.Exists(Application.StartupPath + "\\manifest1\\" + label5.Text))
                    {
                        using (StreamWriter writer = new StreamWriter(Application.StartupPath + "\\manifest1\\" + label5.Text))
                        {

                            writer.WriteLine(richTextBox2.Text);

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
            PMEncryptFile obj = new PMEncryptFile(tid.Text, textBox2.Text, tdate.Text, tname.Text, comboBox1.SelectedItem.ToString(), file1, file2);
            obj.Show();
        }
    }
}
