using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BiJi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        string[] wjm;
        int lead = 0;
        private void Form4_Load(object sender, EventArgs e)
        {
            //程序添加图片//也可在属性里添加
            pictureBox1.Image = Image.FromFile(@"C:\Users\admin\Desktop\tupian\1.png");

            //获取指定文件夹里的全部文件路径
            wjm = Directory.GetFiles(@"C:\Users\admin\Desktop\tupian");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lead--;
            if (lead < 0)
                lead = wjm.Length - 1;
            pictureBox1.Image = Image.FromFile(@wjm[lead]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lead++;
            if (lead >= wjm.Length)
                lead = 0;
            pictureBox1.Image = Image.FromFile(@wjm[lead]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
