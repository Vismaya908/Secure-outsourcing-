using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProxyServer
{
    static class Program
    {
        public static int UserCount = 0;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
