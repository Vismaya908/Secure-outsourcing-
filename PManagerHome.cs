using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace encryption
{
    public partial class PManagerHome : Form
    {
        public PManagerHome()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddEmpDetails obj = new AddEmpDetails();
            obj.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            DeleteEmployee obj = new DeleteEmployee();
            obj.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            PMAssignWork obj = new PMAssignWork();
            obj.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            PMUploadFile obj = new PMUploadFile();
            obj.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PMDownloadFile obj = new PMDownloadFile();
            obj.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FileDirectoryView obj = new FileDirectoryView();
            obj.Show();
        }
    }
}
