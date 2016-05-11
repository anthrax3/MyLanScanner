using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LocalLister
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                   Application.Run(new FormMain());
                }
                catch (Exception x)
                { MessageBox.Show(x.Message,"error"); }
               
            }
            catch { }          


        }



        public static string ConnectorName = "localister";
        public static string SenderUrl = "https://raw.githubusercontent.com/drjuzto/ints/master/sender";

    }
}
