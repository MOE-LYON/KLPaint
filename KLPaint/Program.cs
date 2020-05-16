using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KLPaint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var mutex = new Mutex(true, @"Global\KLPaint", out bool firstInstance);
            if (firstInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("已经有本程序的实例允许，不能再创建实例了");
            }
            
        }
    }
}
