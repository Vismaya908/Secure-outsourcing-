using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using RSAencrypter;
using System.Data.SqlClient;
namespace encryption
{
    public partial class Form8 : Form
    {
        RSAencrypter.RSAencrypter myRsa = new RSAencrypter.RSAencrypter();
        public SqlConnection opencon()
        {
            SqlConnection con = new SqlConnection("server=MARIAN-HP;database=pre;uid=sa;pwd=multi");
            con.Open();
            return con;
        }
        public Form8()
        {
            InitializeComponent();
        }
        private void createKey(string username)
        {
            string s = "select name from reg where username='" + username + "'";
            SqlCommand cmd = new SqlCommand(s, opencon());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string StartupPath = @"C:\magzzzz\project\" + username + "\\"; //with complete path
                if (!System.IO.Directory.Exists(StartupPath))
                {
                    System.IO.Directory.CreateDirectory(StartupPath);
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    string privateKey = rsa.ToXmlString(true);
                    File.WriteAllText(StartupPath + "\\PrivateKey.xml", privateKey);
                    string publicKey = rsa.ToXmlString(false);
                    File.WriteAllText(StartupPath + "\\PublicKey.xml", publicKey);
                    MessageBox.Show("The Key pair created successfully at:\n" + StartupPath);

                }


            }
        }
        private void pubencryption(string publickey, string plain)
        {
            try
            {
                byte[] message = Encoding.UTF8.GetBytes(plain);
                byte[] encMessage = myRsa.PublicEncryption(message);

                textBox3.Text = Convert.ToBase64String(encMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred while trying to encrypt the data,\nMessage: " + ex.Message);
            }
        }
        private void pvtencryption(string privatekey, string plain)
        {
            try
            {
                byte[] message = Encoding.UTF8.GetBytes(plain);
                byte[] encMessage = myRsa.PrivateEncryption(message);
                textBox4.Text = Convert.ToBase64String(encMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred while trying to encrypt the data,\nMessage: " + ex.Message);
            }
        }

        private void pubdecryption(string publickey, string cipher)
        {
            try
            {

                byte[] decMessage = Convert.FromBase64String(cipher);
                byte[] message = myRsa.PublicDecryption(decMessage);


                string sMsg = Encoding.UTF8.GetString(message);
                MessageBox.Show("The Decrypted Message is:\n" + sMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred while trying to decrypt the data,\nMessage: " + ex.Message);
            }
        }

        private void pvtdecryption(string publickey, string cipher)
        {
            try
            {

                byte[] decMessage = Convert.FromBase64String(cipher);
                byte[] message = myRsa.PrivateDecryption(decMessage);

                string sMsg = Encoding.UTF8.GetString(message);
                MessageBox.Show("The Decrypted Message is:\n" + sMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred while trying to decrypt the data,\nMessage: " + ex.Message);
            }
        }

        private string GetHexString(byte[] byteArray)
        {
            StringBuilder hexString = new StringBuilder(byteArray.Length * 2);
            for (int i = 0; i < byteArray.Length; i++)
                hexString.Append(string.Format("{0:X}", byteArray[i]));
            int x = hexString.Capacity;
            return hexString.ToString();
        }




        private void button3_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                if (new FileInfo(openFileDialog1.FileName).Name=="PrivateKey.xml")
                {
                    myRsa.LoadPrivateFromXml(openFileDialog1.FileName);
                    RSACryptoServiceProvider localRsa = new RSACryptoServiceProvider();
                    localRsa.FromXmlString(File.ReadAllText(openFileDialog1.FileName));
                    RSAParameters rsaParams = localRsa.ExportParameters(true);
                    string exp = GetHexString(rsaParams.Exponent);
                    string modules = GetHexString(rsaParams.Modulus);
                    string pvt = GetHexString(rsaParams.D);

                    // Clearing the RSA instance
                    localRsa.Clear();
                    pvtdecryption(pvt ,textBox3 .Text );
                }
                else
                {
                    myRsa.LoadPublicFromXml(openFileDialog1.FileName);
                    RSACryptoServiceProvider localRsa = new RSACryptoServiceProvider();
                    localRsa.FromXmlString(File.ReadAllText(openFileDialog1.FileName));
                    RSAParameters rsaParams = localRsa.ExportParameters(false );
                    string exp = GetHexString(rsaParams.Exponent);
                    string modules = GetHexString(rsaParams.Modulus);
                    //string pvt = GetHexString(rsaParams.D);

                    // Clearing the RSA instance
                    localRsa.Clear(); 

                    
                    pubdecryption(modules ,textBox4 .Text );
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myRsa.LoadPublicFromXml(openFileDialog1.FileName);
               
                RSACryptoServiceProvider localRsa = new RSACryptoServiceProvider();
                localRsa.FromXmlString(File.ReadAllText(openFileDialog1.FileName));
                RSAParameters rsaParams = localRsa.ExportParameters(false  );
                string exp = GetHexString(rsaParams.Exponent);
                string modules = GetHexString(rsaParams.Modulus);
                string pvt="";
                if(rsaParams.D!=null)
                 pvt = GetHexString(rsaParams.D);


                // Clearing the RSA instance
                localRsa.Clear();

                pubencryption(modules, textBox2.Text);
                

            }

        }

        private void Form8_Load_1(object sender, EventArgs e)
        {




        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myRsa.LoadPrivateFromXml(openFileDialog1 .FileName );
                RSACryptoServiceProvider localRsa = new RSACryptoServiceProvider();
                localRsa.FromXmlString(File.ReadAllText(openFileDialog1.FileName));
                RSAParameters rsaParams = localRsa.ExportParameters(true );
                string exp = GetHexString(rsaParams.Exponent);
                string modules = GetHexString(rsaParams.Modulus);
                string pvt = GetHexString(rsaParams.D);

                // Clearing the RSA instance
                localRsa.Clear();

                pvtencryption(pvt  ,textBox2 .Text );

            }
      

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            createKey(textBox1.Text);
        }

       
    }
}
