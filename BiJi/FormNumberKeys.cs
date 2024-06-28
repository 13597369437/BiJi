using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace BiJi
{
    public partial class FormNumberKeys : Skin_VS
    {
        public delegate void deMoveData(string sr);
        public event deMoveData eventHandle;
       
        public FormNumberKeys(string sr)
        {
            InitializeComponent();
            txtInPut.Clear();
            txtInPut.Text = sr;
            txtInPut.Focus();
        }
        
        //输入
        private void button1_Click(object sender, EventArgs e)
        {
            var but = sender as Button;
            txtInPut.AppendText(but.Text);
        }

        //清除
        private void button12_Click(object sender, EventArgs e)
        {
            txtInPut.Clear();
        }

        //删除
        private void button13_Click(object sender, EventArgs e)
        {
            if (txtInPut.Text.Trim().Length > 0)
            {
                string sr = txtInPut.Text.Trim();
                txtInPut.Text = sr.Remove(sr.Length - 1, 1);
            }
        }

        //确定
        private void button14_Click(object sender, EventArgs e)
        {
            eventHandle(txtInPut.Text.Trim());
            this.Close();
        }


        private void txtInPut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                eventHandle(txtInPut.Text.Trim());
                this.Close();
            }
        }

        //大小写切换
        bool qf = false;
        private void button2_Click(object sender, EventArgs e)
        {
            qf = !qf;
            if (qf == true)
            {
                button2.Text = "大写";
                foreach (Control con in this.Controls)
                {
                    if (con is Button)
                    {
                        var btn = con as Button;
                        if (btn != null && btn.Name.StartsWith("zm"))
                        {
                            btn.Text = btn.Text.ToLower();
                        }
                    }
                }
            }
            else
            {
                button2.Text = "小写";
                foreach (Control con in this.Controls)
                {
                    if (con is Button)
                    {
                        var btn = con as Button;
                        if (btn != null && btn.Name.StartsWith("zm"))
                        {
                            btn.Text = btn.Text.ToUpper();
                        }
                    }
                }
            }
        }
    }
}
