using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace encryption
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

      public  static string Servername = "localhost",ServerIP="127.0.0.1";
      public static string userName = "Unknown";

      public static string orginalfilepath = "";
      public static string orginalfilepath1 = "";
      public static string pid = "";
      public static string fileid = "";
      public static string username = "";
      public static string filename = "";
      public static string filename1 = "";
      public static string fdate = "";
      public static string fpriority = "";
      public static string fileastatus = "";
      public static string des = "";

      


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LOGIN());
        }
    }
}
