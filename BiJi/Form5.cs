using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BiJi
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            //MX設置站號
            com1.ActLogicalStationNumber = 2;
        }
        //MX實例化
        ActUtlTypeLib.ActUtlTypeClass com1 = new ActUtlTypeLib.ActUtlTypeClass();

        private void Form5_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.wt += wt1;
        }

        void wt1()
        {
            MessageBox.Show("OK");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            //foreach (Control con in this.Controls)
            //{
            //    if (con is TextBox)
            //    {
            //        TextBox tb = (TextBox)con;
            //        tb.Text = "123";
            //    }
            //}

            //用MX連接PLC判斷PLC是否連接成功
            //if (com1.Open() == 0)
            //{
            //    MessageBox.Show("PLC連接成功");
            //}
            //else
            //    MessageBox.Show("PLC連接失败");

            //string ss = "btnI2";
            //short st = (short)(64 + Convert.ToInt16(ss.Remove(0, 4)));
            //uint sts = 0;
            //sts = 2 << 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //foreach (Control con in this.Controls)
            //{
            //    if (con is TextBox)
            //    {
            //        TextBox tb = (TextBox)con;
            //        tb.Clear();
            //    }
            //}
            int[] data = new int[10];
            if (0 == com1.ReadDeviceBlock("D1590", 10,out data[0]))
            {
                //如果讀取成功。。。。。
                textBox1.Text = data[0].ToString();
            }
        }
    }
}
