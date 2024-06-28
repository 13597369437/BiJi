using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BiJi
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool crea;
            System.Threading.Mutex m = new System.Threading.Mutex(true, Application.ProductName, out crea);

            if (crea)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                //Application.Run(new 队列方式读取串口());
            }
            else
            {
                MessageBox.Show("程序以打开", "提示");
                return;
            }
        }
    }
}
