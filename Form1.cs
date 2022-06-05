using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string sourceFileName = @"E:\504.txt";

        string destFileLocation = @"E:\";

        int index = 0;

        long maxFileSize = 10000;

        byte[] buffer = new byte[65536];
        private void button1_Click(object sender, EventArgs e)
        {

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

        }

    }
}
