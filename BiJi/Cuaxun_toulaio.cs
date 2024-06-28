using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using BiJi;

namespace BiJi
{
    public partial class Cuaxun_toulaio : Form
    {
        public Cuaxun_toulaio()
        {
            InitializeComponent();
        }
         const string COON = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Chaxun.mdb";
        private void button1_Click(object sender, EventArgs e)
        {
            string kaishi = dateTimePicker1.Value.AddDays(-0).ToString("yyyy-MM-dd");
            string jieshu = dateTimePicker2.Value.AddDays(+0).ToString("yyyy-MM-dd");
            try
            {
                OleDbConnection con = new OleDbConnection(COON);
                string cmd_1 = "select sv0 as 日期,sv1 as 開始時間,sv2 as 料號,sv3 as 批號,sv4 as 制程,sv5 as 速度,sv6 as 剝膜溫度,sv7 as 新液洗溫度," +
                    "sv8 as 酸洗溫度,sv9 as 烘干溫度,pl1 as 剝膜噴霧,pl2 as 剝膜膜屑帶出,pl3 as 新液洗噴霧,pl4 as 風刀1,pl5 as 風刀2,pl6 as 風刀3,pl7 as 投料人,pl8 as 是否投入成功 ";
                string cmd_2 = "from touliaojilu where sv0 between #" + kaishi + "# and #" + jieshu + "#";
                string cmdtext = cmd_1 + cmd_2;
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdtext, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
               
            }
            catch
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class1.dinyue = true;
        }

        public void xianshi()
        {
            MessageBox.Show("订阅成功");
        }
        
    }
}
