#define CESHI
#define DEBUG
using BiJi.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BiJi
{
    public partial class Form1 : Form
    {

        #region  测试用
        public Form1()
        {
            InitializeComponent();

            button1.Click += new EventHandler(button2_Click);
            //使线程可以调用控件
            CheckForIllegalCrossThreadCalls = false;
            //MX設置站號
            com1.ActLogicalStationNumber = 1;
            test20();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“baobiaoDataSet1.zancun”中。您可以根据需要移动或删除它。
            this.zancunTableAdapter1.Fill(this.baobiaoDataSet1.zancun);
            // TODO: 这行代码将数据加载到表“baobiaoDataSet.zancun”中。您可以根据需要移动或删除它。
            this.zancunTableAdapter.Fill(this.baobiaoDataSet.zancun);
            cobx();
            //让窗口显示在前面,false则显示在后面
            //this.TopMost = true;

          


            
            //用MX連接PLC判斷PLC是否連接成功
            if (com1.Open() == 0)
            {
                MessageBox.Show("PLC連接成功");
            }


            //chuanti();
            duqu();
            
            
            
            qipaokongjian();
        }

        public string Remov0(string str) //去除0
        {
            int a = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '0')
                    a++;
                else
                    break;
            }

            string sr = str.Substring(a);
            if (sr.StartsWith("."))
                sr = "0" + sr;
            if (str == "0" || str == "00" || str == "000" || str == "0000")
                sr = "0";
            return sr;
        }

        int itest_3 = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //test_23();
            //test_3_4();
            //test8_2();
            //txt1.Text = Convert.ToString(530, 16);
            //qipaokongjian();
            //string[] data = new string[10];
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data[i] = "";
            //}
            //string[] sr = txt1.Text.Split('/');
            //for (int i = 0; i < sr.Length; i++)
            //{
            //    data[i] = sr[i];
            //}
            //daojishi();
            //invoke();
            //ceshilistView();
            //textBox3.Text = "000M10622039328602".Substring(3, 10);
            //Eat(); // 第三步发布事件
            //cunchu();
            //dakaiwenjian();
            //kaishi();
            //mxpl_read();
            //DateTime sheding = DateTime.Now.AddDays(1).Date;
            //DateTime now = DateTime.Now.AddHours(-8);
            //int sec = (int)sheding.Subtract(now).TotalSeconds;

            zifuchuan1();
        }



        Cuaxun_toulaio ctl = new Cuaxun_toulaio();
        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ctl.Show();
        }
        
        
        private void button2_Click(object sender, EventArgs e)
        {
            txt2.Text = "2";
            //if (dalms.OpenMyComm("COM8", 9600, 8, 1, "E"))
            //{
            //    MessageBox.Show("打开");
            //    dalms.Read_03(1, 8450, 3);
            //}
  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (dalms.ClosePort())
            //{
            //    MessageBox.Show("关闭");
            //}
            //wt1 = new weituo(fabu);
            //wt1("13");
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            plfuzhi();
            //dvr(0);
            //chazhaolistview();
            //guanjianzi();
            //Dictionary<string, string> dicty;
            //dakai("ceshi",out dicty);
            //if (dicty.ContainsKey("id"))
            //    textBox1.Text = dicty["id"].ToString();
            //set("ID", "10.182.36.24");
            //textBox1.Text = get("qq");
            //textBox2.Text = get("id");
            //inipath = "peizhi";
            //IniwriteValue("dizhi", "id4", "188.22.10.3");
            //textBox1.Text = IniReadValue("dizhi", "id4");
            //int lac;
            //labjj(ref lab,out lac);
            //textBox1.Text = lab.ToString();
            //if (dalms.Wrute_06(1,0x20,0x01,0x0B,0xB8))
            //{

            //}
            //if (dalms.Wrute_06(1, Convert.ToInt16(textBox2.Text, 16), Convert.ToInt16(textBox3.Text)))
            //{

            //}
            //quxiao();
            //lab++;
            //label2.Text = lab.ToString();
            //int  a = 65516;
            //int b = ~a;
            //piliancaozuokj();
            //diguikj(this);
            //shujuku(6);
        }
        private void label2_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("是TextChanged");
        }

        private void label2_TextAlignChanged(object sender, EventArgs e)
        {
            MessageBox.Show("是TextAlignChanged");
        }
        #endregion

        #region 关键字

        void guanjianzi()
        {

            //   continue 跳过本次循环
            for (int i = 0; i < 10; i++)
            {
                if (i == 3 || i == 5 || i == 7)
                    continue;
                textBox2.Text += i + ",";
                //当i=3,5,7时跳过
            }

            //const 常量，如下aa将不能改变
            const string aa = "123";
            
            //try-finally
            try
            {
               // Math : 提供三角函数，对数函数，及其它的一些数学方法
            }
            catch
            {
                //发生异常时执行的操作
            }
            finally
            {
                //不管有没有异常都会执行的操作
            }
         
        }


        #endregion

        #region 用MX連接PLC讀、寫參數
        //MX连接不是CPU直接连接,通过模块连接的话要在模块把通讯协议设置成(MELSOFT连接)
        //MX實例化
        //如果这里报错，解决办法（在VS中找到引用控件所在的项目--〉属性--〉生成--〉常规---〉目标平台---〉选择X86即可解决。）
        //且.NET要选择3.5的
        //第二种解决方式：查看引用的ActUtlTypeLib的属性，将嵌入互操作类型改为False
        ActUtlType64Lib.ActUtlType64Class com1 = new ActUtlType64Lib.ActUtlType64Class();

        //用MX連接讀數
        void mxpl_read()
        {
            /*批量讀取Y或者M等位寄存器需要從0開始按兩個字節取數
              例如Y20,M16等，其它的會讀不出來，Y寄存器是按八進制計數的
              位寄存器沒有這些限制*/

            ////MX设置站号
            //com1.ActLogicalStationNumber = 1;

            ////判断是否连接成功
            //if (com1.Open() == 0)
            //{
            //    MessageBox.Show("PLC连接成功");
            //}
            //批量操作寄存器
            int[] data = new int[10];
            if (0==com1.ReadDeviceBlock("Y0", 10, out data[0]))
            {
                //如果讀取成功。。。。。
            }
            if (0==com1.ReadDeviceBlock("M16", 10, out data[0]))
            {
                //如果讀取成功。。。。。
            }
            if (0==com1.ReadDeviceBlock("D10", 10, out data[0]))
            {
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("失败");
                com1.Close();
                com1.Open();
            }
            //对位元件操作
            if(0==com1.ReadDeviceRandom("m1",1,out data[0]))
            {

            }

        }

        //  //用MX連接寫數據
        void mx_write()
        {
            //置位位寄存器
            com1.SetDevice2("M10", 1);

            //復位位寄存器
            com1.SetDevice2("M10", 0);

            //批量寫數據進入PLC
            int[] data = new int[10];
            com1.WriteDeviceBlock("D100", 10, ref data[0]);
        }
        #endregion

        #region 窗体控制
        
        #region 窗体关闭按钮取消，不满足条件不能关闭，程序只能开一个

        [DllImport("User32.DLL")]
        public static extern int GetSystemMenu(int hwnd, int bRevert);
        [DllImport("User32.DLL")]
        public static extern int RemoveMenu(int hMenu, int nPosition, int wFlags);
        const int MF_REMOVE = 0X1000;
        const int SC_Close = 0XF060;//关闭
        const int SC_RESTORE = 0xF120;//还原
        const int SC_MOVE = 0xF010;//移动
        const int SC_SIZE = 0xF000;//大小
        const int SC_MINIMIZE = 0xF020;//最小化
        const int SC_MAXIMIZE = 0xF030;//最大化

        //窗體按鈕控制
        public void Quxiao()
        {
            int hmen = GetSystemMenu(this.Handle.ToInt32(), 0);
            RemoveMenu(hmen, SC_Close, MF_REMOVE);
        }

        //退出程序
        void tuichu()
        {
            if (DialogResult.Yes == MessageBox.Show("是否退出程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                Environment.Exit(0);
        }

        //不满足条件不能退出
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("不满足条件不能退出","提示", MessageBoxButtons.YesNo, MessageBoxIcon.None)) 
                e.Cancel = true; //重点这里用return不能取消关闭，必须用这个取消事件
        }


        //程序只能开一个在启动文件Program.cs里写

        #endregion

        #region 自动控制窗体大小

        AutoSizeFormClass asc = new AutoSizeFormClass();

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                asc.controlAutoSize(this);
            }
            catch { }
        }

        void chuanti() //放在Load事件里
        {
            //窗体开始就最大化
            this.WindowState = FormWindowState.Maximized;
            try
            {
                asc.controllInitializeSize(this);
            }
            catch { }
        }

        #endregion

        #region 窗体只打开一个，且显示在最上层

        ////串口设置按钮
        //ComForm comform = null;
        //private void toolStripButton3_Click(object sender, EventArgs e)
        //{
        //    if (comform == null)
        //    {
        //        comform = new ComForm();
        //        comform.Show();
        //        comform.FormClosed += Comform_FormClosed;
        //        comform.BringToFront();//窗体显示在最上层
        //    }
        //    else
        //    {
        //        comform.WindowState = FormWindowState.Normal;//窗体正常显示,不然窗体最小化时，下一行代码失效
        //        comform.BringToFront();
        //    }
        //}
        //private void Comform_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    comform = null;
        //}



        #endregion

        #region 判断窗体是否打开

        //using System.Runtime.InteropServices;
        //[DllImport("user32.dll", EntryPoint = "FindWindow")]
        //private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        //IntPtr formshow = (IntPtr)FindWindow(null, "窗体标题");//窗体标题为窗体的Text属性
        //    if (formshow == IntPtr.Zero)//窗体没打开
        //        form.Show();

        #endregion

        #region MID窗体设置
        //Form3为父窗体
        //第一步确认父窗体，在窗体视窗样式属性里把 IsMdicontainer 改为 true
        //第二步创建子窗体设置父窗体

        #endregion

        #endregion
            
        #region 枚举，结构
        //声明枚举类型
        public enum stg
        {
            a=10 ,
            b=20 ,
            c =30
        }
        void ens()
        {
            textBox1.Text =((int) stg.b).ToString();
        }

        //结构
        public struct jirgou
        {
            public int a;
            public string b;
            public int c;
            public bool d;
        }
        void jg()
        {
            jirgou j1;
            j1.a = 1;
            j1.b = "jg1";
            j1.c = 2;
            j1.d = false;
        }
        #endregion

        #region 字符串操作

        //string 操作（操作结果不会改变字符串除非重新赋值）
        void zifuchuan()
        {
            string seg = "Len And 123";
            //大写输出
            textBox1.Text = seg.ToUpper();
            
            //小写输出
            textBox1.Text = seg.ToLower();

            //截取字符串
            textBox1.Text = seg.Substring(0, 2);

            //替换字符串
            string aa = seg.Replace("And", "***");

            //判断字符串是否以某个字符串开头(后面的参数为忽略大小写)
            bool startwith = seg.StartsWith("len", StringComparison.CurrentCultureIgnoreCase);

            //判断字符串是否以某个字符串结尾(后面的参数为忽略大小写)
            bool endwith = seg.EndsWith("123", StringComparison.CurrentCultureIgnoreCase);

            //判断字符串是否包含某字符
            if (seg.Contains("123"))
            {
                //包含
            }
            
            //去除重复字符串
            seg = "helloworld";
            seg = new string(seg.Distinct().ToArray());//seg="helowrd"
            
            //把一个数组用指定分割符组合成一个字符串
            string[] ab = { "afv", "ave", "214", "f2a" };
            string ab1 = textBox1.Text = string.Join("|", ab);

            //分割字符串（单个分割符可以直接写进去）
            char[] c = { '|' };//可以是多个分隔符{ '|'  , ',' , ' '}，还可以是个字符串数组

            string[] cs1 = ab1.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            
            //删除指定字符串
            seg = seg.Remove(0, 3);//从第0个开始删除3个

            //把十六进制字符转为数字
            int ox = Convert.ToInt32("FE", 16);
            
            //把十六进制转为二进制
            string b2 = Convert.ToString(ox, 2).PadLeft(8, '0'); //PadLeft(8, '0')是转换不够8位在前面用0补齐

            //把十进制转为十六进制，大写X转换出来的为大写，小写x转换出来的为小写，后面加的数字表示转为几位十六进制数
            int d20 = 228;
            b2 = d20.ToString("X");
            b2= d20.ToString("X2");

            //把二进制字符转为int型
            int d2 = Convert.ToInt32(b2, 2);

            //把一个字符串的每一个字符拆分成一个数组
            char[] ca = seg.ToCharArray();

            //搜索某字符在字符串中的位置
            int na = seg.IndexOf("n", 0);

            //把字符串转换成枚举类型,用于像串口选择串口号，奇偶校验选择
            stg s1 = (stg)Enum.Parse(typeof(stg), "a");

            //把字符串转成任意类型例如
            seg = "123";
            int i1 = int.Parse(seg);

            //把ASCII码转为对应字符
            //在 string str= (char)byte
            //把字符转为对应的ASCII
            //int ascii=(int)'a';

            //将字符串顺序反转
            //1、
            seg = new string(seg.ToCharArray().Reverse().ToArray());
            //2、先将数组反转
            Array.Reverse(c);
            string str = new string(c);

        }

        //StringBuilder 操作（操作结果会改变字符串）
        void zifuchuan1()
        {
            StringBuilder sb = new StringBuilder("www.com");

            //追加
            sb.Append("123");//sb=="www.com123"

            //插入
            sb.Insert(3, "  ");//sb=="www  .com123"

            //移除
            sb.Remove(3, 2);//sb=="www.com123"

            //替换
            sb.Replace(".", "-");//sb=="www-com123"

            string s = sb.ToString();

            MessageBox.Show(s);
        }
        #endregion

        #region 正则表达式

        void test_zcbds()
        {
            //判断一个字符串是否符合一个正则表达式，结果返回一个布尔变量
            //第二个参数为正则表达式，""表示没有规则所有结果都返回true
            Regex.IsMatch("1232", "");

            //利用正则表达式替换字符
            //下例是将字符串中的数字替换成*
            Regex.Replace("12122daedw242", @"[0-9]", "*");

            //判断是否全是数字
            bool isnum = Regex.IsMatch("12131", @"^\d+$");
            isnum = Regex.IsMatch("12131", "^[0-9]+$");


        }

        #endregion

        #region 控件操作
        //可以通过+=给控件的事件注册多个事件

        #region 带查询功能的comboBox控件

        //一般放在窗体的Load事件里
        void cobx()
        {
            //清空控件
            comboBox1.Items.Clear();

            //添加内容
            comboBox1.Items.Add("asf");
            comboBox1.Items.Add("grf");
            comboBox1.Items.Add("123");
            comboBox1.Items.Add("125");
            comboBox1.Items.Add("1456");
            comboBox1.Items.Add("12653");

            // 实现查询功能
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            //让控件开始时显示的内容，以下两种方法都可以
            comboBox1.SelectedIndex = 0;
            comboBox1.Text = comboBox1.Items[0].ToString();
        }

        #endregion

        #region 单选按钮控件、勾选框控件
        void gouxuan()
        {
            if (radioButton1.Checked == true)
            {
                //单选按钮被选上
            }
            if (checkBox1.Checked == true)
            {
                //勾选框被选上
            }
        }

        #endregion

        #region  dataGridView控件

        void dvr(int gnm)
        {

            dgv1.RowHeadersVisible = false;  //行头隐藏
            dgv1.Rows[0].Cells[0].Value = "数据"; //添加数据


            //属性
            //SelectionMode=FullRowSelect  表示选中当前行
            //dataGridView控件的RowHeadersVisible属性是关闭列标头


            //删除指定行
            //int i = 0;
            //DataGridViewRow row = dgv1.Rows[i];
            //dgv1.Rows.Remove(row);

            //按照日期降序排序
            //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);

            //按照多列进行排序，已收藏在CSDN
            //DataTable dt = Mysqlclass.chaxunsql(sql).Tables[0];//这一行表示dt的数据来源
            //DataView dataview1 = dt.DefaultView;
            //dataview1.Sort = "[riqi] ASC, [time] DESC";//中扩号里表示数据库的栏位名称，ASC表示升序，DESC表示降序
            //dataGridView1.DataSource = dataview1;//排序完赋值给表格

            //使滚动条滚动到指定行
            dataGridView1.FirstDisplayedScrollingRowIndex = 9;

            //获取选中的行
            //var row = dataGridView1.Rows[dataGridView1.SelectedIndex];


            #region 合并单元格
            //需要(行、列)合并的所有列标题名
            List<String> colsHeaderText_V = new List<String>();
            List<String> colsHeaderText_H = new List<String>();

            ////纵向合并
            //if (e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].HeaderText == "套数" && e.RowIndex >= 0)
            //{
            //    using (
            //        Brush gridBrush = new SolidBrush(dgv.GridColor),
            //        backColorBrush = new SolidBrush(e.CellStyle.BackColor))
            //    {
            //        using (Pen gridLinePen = new Pen(gridBrush))
            //        {
            //            // 擦除原单元格背景
            //            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            //            /****** 绘制单元格相互间隔的区分线条，datagridview自己会处理左侧和上边缘的线条，因此只需绘制下边框和和右边框
            //             DataGridView控件绘制单元格时，不绘制左边框和上边框，共用左单元格的右边框，上一单元格的下边框*****/

            //            //不是最后一行且单元格的值不为null
            //            if (e.RowIndex < dgv.RowCount - 1 && dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value != null)
            //            {
            //                //若与下一单元格值不同
            //                if (e.Value.ToString() != dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString())
            //                {
            //                    //下边缘的线
            //                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
            //                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
            //                    //绘制值
            //                    if (e.Value != null)
            //                    {
            //                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
            //                            Brushes.Crimson, e.CellBounds.X + 2,
            //                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
            //                    }
            //                }
            //                //若与下一单元格值相同 
            //                else
            //                {
            //                    //背景颜色
            //                    //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
            //                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
            //                    dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
            //                    //只读（以免双击单元格时显示值）
            //                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            //                    dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = true;
            //                }
            //            }
            //            //最后一行或单元格的值为null
            //            else
            //            {
            //                //下边缘的线
            //                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
            //                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

            //                //绘制值
            //                if (e.Value != null)
            //                {
            //                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
            //                        Brushes.Crimson, e.CellBounds.X + 2,
            //                        e.CellBounds.Y + 2, StringFormat.GenericDefault);
            //                }
            //            }

            //            ////左侧的线（）
            //            //e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
            //            //    e.CellBounds.Top, e.CellBounds.Left,
            //            //    e.CellBounds.Bottom - 1);

            //            //右侧的线
            //            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
            //                e.CellBounds.Top, e.CellBounds.Right - 1,
            //                e.CellBounds.Bottom - 1);

            //            //设置处理事件完成（关键点），只有设置为ture,才能显示出想要的结果。
            //            e.Handled = true;
            //        }
            //    }
            //}

            //foreach (string fieldHeaderText in colsHeaderText_V)
            //{
            //    //横向合并
            //    if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].HeaderText == fieldHeaderText && e.RowIndex >= 0)
            //    {
            //        using (
            //            Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
            //            backColorBrush = new SolidBrush(e.CellStyle.BackColor))
            //        {
            //            using (Pen gridLinePen = new Pen(gridBrush))
            //            {
            //                // 擦除原单元格背景
            //                e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            //                /****** 绘制单元格相互间隔的区分线条，datagridview自己会处理左侧和上边缘的线条，因此只需绘制下边框和和右边框
            //                 DataGridView控件绘制单元格时，不绘制左边框和上边框，共用左单元格的右边框，上一单元格的下边框*****/

            //                //不是最后一列且单元格的值不为null
            //                if (e.ColumnIndex < this.dataGridView1.ColumnCount - 1 && this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value != null)
            //                {
            //                    if (e.Value.ToString() != this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString())
            //                    {
            //                        //右侧的线
            //                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top,
            //                            e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
            //                        //绘制值
            //                        if (e.Value != null)
            //                        {
            //                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
            //                                Brushes.Crimson, e.CellBounds.X + 2,
            //                                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            //                        }
            //                    }
            //                    //若与下一单元格值相同 
            //                    else
            //                    {
            //                        //背景颜色
            //                        //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
            //                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightPink;
            //                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.BackColor = Color.LightPink;
            //                        //只读（以免双击单元格时显示值）
            //                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            //                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].ReadOnly = true;
            //                    }
            //                }
            //                else
            //                {
            //                    //右侧的线
            //                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top,
            //                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

            //                    //绘制值
            //                    if (e.Value != null)
            //                    {
            //                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
            //                            Brushes.Crimson, e.CellBounds.X + 2,
            //                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
            //                    }
            //                }
            //                //下边缘的线
            //                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
            //                                            e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
            //                e.Handled = true;
            //            }
            //        }

            //    }
            //}



            #endregion

            switch (gnm)
            {
                #region 删除选中的行
                case 0:
                    foreach (DataGridViewRow r in dgv1.SelectedRows)
                    {
                        if (!r.IsNewRow)
                            dgv1.Rows.Remove(r);
                    }
                    break;
                #endregion

                #region 获取选中的行和列
                case 1:
                    foreach (DataGridViewRow r in dgv1.SelectedRows)
                    {

                    }
                    break;
                    #endregion
            }
        }
        //点击事件
        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;
            string srt = "";
            srt = dgv1.CurrentCell.Value.ToString(); //取得当前单元格内容
            txt1.Text = srt;
            index = dgv1.CurrentCell.ColumnIndex; //取得当前单元格的列索引
            index = dgv1.CurrentCell.RowIndex; //取得当前单元格的行索引
            DataGridViewRow dgv = dgv1.CurrentRow; //取得当前行
            index = dgv1.CurrentCellAddress.X; //取得当前单元格的列索引
            index = dgv1.CurrentCellAddress.Y;//取得当前单元格的行索引
            index = dgv1.SelectedRows[0].Index; //取得当前行的行号


        }
        #endregion

        #region 控件保留关闭程序之前的值
        //第一步在资料属性下Applicationsetti  ->PropertyBind  ->Text  ->新增

        //第二步
        private Settings sets = new Settings();

        //一般放在Load事件里
        public void duqu()
        {
            textBox1.Text = sets.txt1;
        }

        //一般放在关闭事件FormClosing里
        public void cunchu()
        {
            sets.txt1 = textBox1.Text;
            sets.Save();
        }

        #endregion

        #region 显示影像控件 PictureBox

        //在 Form4

        #endregion

        #region 焦点切换到该控件及只能输入某些字符

        void jiaodian()
        {
            textBox1.Focus();

            SendKeys.Send("{TAB}");
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0~9数字
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region 对控件进行批量操作

        //批量读取操作控件
        void piliancaozuokj()
        {
            //这里要注意如果控件里面还有控件直接用this是扫描不到里面的控件的要进行递归
            //如果只需要对控件里面的控件进行批量操作把this换成该控件名即可
            foreach (Control con in this.Controls)
            {
                if (con is TextBox) //先对控件进行判断课避免资源浪费
                {
                    var tb = con as TextBox;

                    //这一步通过判断读取到的控件的名称（Name）开头是否包含某些字符可以选择要操作的控件
                    //对不需要操作的同类型的控件重命名即可
                    if (tb != null && tb.Name.StartsWith("xz"))
                    {
                        tb.Text = "12.34";
                        //tb.Clear();
                    }
                }
                foreach (Control cot in con.Controls)//进行递归
                {
                    if (cot is TextBox)
                    {
                        var tb = cot as TextBox;
                        if (tb != null && tb.Name.StartsWith("xz"))
                        {
                            tb.Text = "12.34";
                        }
                    }
                }
            }
        }

        //对所有同一类型的控件进行操作包括控件里的控件
        public void diguikj(Control con)
        {
            foreach (Control C in con.Controls)
            {
                if (C is TextBox)
                {
                    var tb = C as TextBox;
                    if (tb != null)
                        tb.Text = "0.123";
                }

                //递归
                diguikj(C);
            }
        }

        //批量创建控件
        void chuangjiankj()
        {
            Button[] btnin = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btnin[i] = new Button();
                btnin[i].Name = "btnI" + i;
                btnin[i].Size = new Size(26, 26);
                btnin[i].Location = new Point(20 + i * 30, 114);
                btnin[i].Enabled = false;
                btnin[i].BackColor = Color.Transparent;
                this.Controls.Add(btnin[i]);
            }
        }

        //把数组的值按顺序赋值给控件
        void plfuzhi()
        {
            string[] msg = { "1", "2", "3", "4" };
            for (int i=0;i<4;i++)
            {
                //方法1
                TextBox txt = (TextBox)groupBox2.Controls.Find("txt" + (i + 1).ToString(), true)[0];
                txt.Text = msg[i];
                //方法2
                groupBox2.Controls["txt" + (i + 1).ToString()].Text= msg[i];
                //如果控件要操作的属性不是TEXT，属于该控件特有的属性要先进行转换
                var txt1 = (TextBox)groupBox2.Controls[$"txt{i + 1}"];
                txt1.Text = msg[i];
            }

        }
        #endregion

        #region 多个相同控件共用同一个事件
        //在控件的属性相应的事件里添加上该事件即可
        private void button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            //此时btn就相当于触发该事件的控件
            //可以用控件的Name或Text属性来区别该控件

            //还可以使用控件的Tag属性（标签）
            btn.Tag.ToString();

        }
        #endregion

        #region ListView 表格控件
        //属性 View 切换为 Details（列表模式）
        //属性GridLines ：是否显示网格线
        //属性HeaderStyle：1、Clickable：单击列表头时可执行排序等操作;2、NonClickable：不响应鼠标单击
        void ceshilistView()
        {
            listView1.BeginUpdate();//开始更新，可有效避免控件闪烁，提高速度
            listView1.Items.Clear();
            /**********添加数据****************/
            for (int i = 0; i < 10; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Clear();
                lvi.ImageIndex = i;
                lvi.Text = "第" + i.ToString() + "行";
                lvi.SubItems.Add(i.ToString() + "月");
                lvi.SubItems.Add(i.ToString() + "日");
                lvi.SubItems.Add(i.ToString() + "时");
                listView1.Items.Add(lvi);
            }
            listView1.EndUpdate();//结束更新
        }

        //遍历显示listView数据项
        void chazhaolistview()
        {
            foreach(ListViewItem item in listView1.Items)
            {
                for(int i=0;i<item.SubItems.Count;i++)
                {
                    listBox1.Items.Add(item.SubItems[i].Text);
                }
            }
        }


        //把表格选中的内容在指定控件显示出来
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            //获取焦点所在的项的索引
            int ixdin = listView1.Items.IndexOf(listView1.FocusedItem);

            string[] neirong = new string[listView1.Items[ixdin].SubItems.Count];

            for (int i = 0; i < listView1.Items[ixdin].SubItems.Count; i++)
            {
                neirong[i] = listView1.Items[ixdin].SubItems[i].Text;
            }
            foreach (Control btn in groupBox2.Controls)
            {
                var btn1 = btn as TextBox;
                if (btn1 != null)
                {
                    btn1.Text = neirong[Convert.ToInt16(btn1.Name.Substring(3))-1];
                }
            }
        }
        #endregion

        #region 警告提示控件 errorProvider  气泡控件 toolTip
        void tishikongjian()
        {
            if (txt1.Text.Trim() != "9")
            {
                //显示警告图标
                errorProvider1.SetError(txt1, "警告,不等于9");
            }
            else
            {
                //取消警告图标
                errorProvider1.SetError(txt1, "");

            }
        }
        
        void qipaokongjian()
        {
            //设置要提示非内容和要提示的控件,显示时间
            toolTip1.Show("  要提示的消息", txt1, 5000);

            //气泡样式
            toolTip1.IsBalloon = true;
        }
        #endregion

        #region 自制右键菜单 contextMenuStrip

        void menusp()
        {
            //编辑该菜单要点击下面添加进去的才会显示出来
            //编辑好的菜单在要添加菜单的控件contextMenuStrip属性里添加进去
        }

        #endregion

        #endregion


        #region 时间操作

        //测试程序运行时间
        void ceshi()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start(); //开始

            //中间运行的程序

            sw.Stop(); //结束

            string time = sw.Elapsed.ToString();//所用的时间
        }

        //其它关于时间的操作
        DateTime time = new DateTime();
        bool atime = false;
        void Time()
        {
            //获取当前时间
            string nowtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            //把一个字符串转换为时间格式
            string ts1 = "8:00";
            DateTime dt1 = Convert.ToDateTime(ts1);

            //时间相减
            string ts2 = "9:00";
            DateTime dt2 = Convert.ToDateTime(ts2);
            DateTime.Compare(dt2, dt1);

            //计算时间差
            string ts3 = "2021/05/10 16:30:03";
            DateTime dt3 = Convert.ToDateTime(ts3);
            DateTime dt4 = DateTime.Now;
            TimeSpan tim = dt4.Subtract(dt3);
            string s1 = tim.Days.ToString();  //取整天数
            string s2 = tim.Hours.ToString(); //取整时数
            string s3 = tim.Minutes.ToString();//取整分钟数
            string s4 = tim.Seconds.ToString();//取整秒数

            //计算当前时间减去指定天数后的日期(下面为减去100天)
            dt4 = dt4.Subtract(new TimeSpan(100, 0, 0, 0));

            //判断时间差
            if (atime == true)
            {
                //记录当前时间
                time = DateTime.Now;
            }
            if (DateTime.Now > time.AddMinutes(5))
            {
                //当前时间大于记录时间5分钟
                //AddMinutes 表示把之前记录的时间加上括号里的时间
            }

        }

        //倒计时
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            TimeSpan ts = dt1.Subtract(DateTime.Now);
            txt2.Text = ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');
            if(txt2.Text=="00:00")
            {
                timer2.Stop();
            }
        }
        DateTime dt1;
        void daojishi()
        {
            if(txt1.Text=="")
            {
                txt1.Text = "5";
            }
            timer2.Start();
            dt1 = DateTime.Now.AddMinutes(Convert.ToInt16(txt1.Text));
        }

        #endregion

        #region 重启，其他情况返回

        //重启程序，放在重启条件里即可
        private void chongqi()
        {
            Process.Start(Application.StartupPath + "\\重启的程序名.exe");
            Process.GetCurrentProcess().Kill();
        }

        //switch其他情况返回
        void qitifanui()
        {
            int i=0;
            string ss = "";
            switch (i)
            {
                case 0: ss = "情况1"; break;
                case 1: ss = "情况2"; break;
                case 2: ss = "情况3"; break;
                default: ss = "其它情况"; break;
            }
        }


        #endregion

        #region 控件回车触发，只能扫码枪输入

        //通过控件松开的时间来判断是否为扫码枪输入
        private DateTime _dt = DateTime.Now;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            _dt = DateTime.Now;
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            var tb = sender as TextBox;
            if (e.KeyCode != Keys.Back)
            {
                DateTime tempDt = DateTime.Now;
                TimeSpan ts_1 = tempDt.Subtract(_dt);
                if (ts_1.Milliseconds > 120)
                {
                    MessageBox.Show("不可手動輸入\n請使用掃碼槍輸入");
                    tb.Clear();
                }
                _dt = tempDt;
            }
        }


        //通过规定时间内是否有回车来判断是否为扫码枪输入（扫码枪输入完成回自动回车）
        private void xztextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Stop();
                //要执行的内容
            }
            else
                timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            xztextBox3.Clear();
            MessageBox.Show("不可手動輸入\n請使用掃碼槍輸入1");
        }

        #endregion

        #region 小键盘调用

        #region 调用内置的小键盘（只能在调用的控件使用）
        //加入CSkin.dll
        FormNumberKeys xiaokey = null;
        void keyboard(string sr)
        {
            if (xiaokey == null)
            {
                xiaokey = new FormNumberKeys(sr);
                xiaokey.FormClosing += Xiaokey_FormClosing;
                xiaokey.eventHandle += Xiaokey_eventHandle;
                xiaokey.BringToFront();
                xiaokey.Show();
            }
            else
            {
                xiaokey.WindowState = FormWindowState.Normal;
                xiaokey.BringToFront();
            }
        }

        TextBox tb1 = null;
        private void Xiaokey_eventHandle(string sr)
        {
            tb1.Text = sr;
        }

        private void Xiaokey_FormClosing(object sender, FormClosingEventArgs e)
        {
            xiaokey = null;
        }

        //在控件Click事件中加入下面的事件
        private void text_Click(object sender, EventArgs e)
        {
            //TextBox tb = (TextBox)sender;
            var tb = sender as TextBox;
            tb1 = tb;
            keyboard(tb.Text);
            
        }
        #endregion

        #region 调用外部写好的键盘程序（通用）
        void jianpan()
        {
            //放在Debug里可直接写程序名即可
            Process.Start(@"KEYS.exe");
        }

        #endregion

        #endregion

        #region 方法返回多个参数
        //ref 变量要经过初始化，在方法里赋值，返回的是最后一次赋值
        //ref 的用处是一个变量需要经过其它类的某个方法转换时，最后返回
        //out 变量不需要初始化，在方法里进行初始化，赋值，返回的是最后一次赋值
        int lab = 0;
        void labjj(ref int t, out int lac)
        {
            t += 10;
            lac = t;
        }
        #endregion

        #region 集合、数组操作
        //集合
        public void text18_1()
        {
            List<int> l_int = new List<int>();
            //单个数据添加
            l_int.Add(1);
            //添加多个数据
            int[] nums = { 0, 1, 3 };
            l_int.AddRange(nums);
            //集合的元素数量
            int num = l_int.Count;
            //把集合转为数组
            int[] shuzu = l_int.ToArray();

            //集合或数组可以直接获取最大值、最小值、平均值
            int max = shuzu.Max();
            int min = shuzu.Min();
            double average = shuzu.Average();

        }

        //数组，多维数组
        public void test8_2()
        {
            int[,] shuzu = new int[6, 4];

            MessageBox.Show("行数：" + shuzu.GetLength(0) + "    列数：" + shuzu.GetLength(1));

            //按要求生成整数序列的数组
            Enumerable.Range(0, 23).Select(x => (object)x).ToArray();
        }
        #endregion

        #region 内存操作

        public void test19_1()
        {
            //开辟一块内存
            MemoryStream ms = new MemoryStream();

            int count = 20;
            byte[] buffer = new byte[1024];

            //把数据写入内存
            ms.Write(buffer, 0, count);

            byte[] buffer1 = ms.ToArray();
        }

        #endregion

        #region 属性
        //属性创建的快捷方式propfull+属性类型+按两次Tab键
        //如果需要有判断的，就用propfull
        //直接赋值使用的，就用prop
        private int biaotisize;
        [Browsable(true)] //设置是否在属性窗口显示该属性
        [Category("我的控件属性")]//设置该属性的分组名称
        [Description("设置按钮图片")]//设置该属性的说明
        public int Biaotisize
        {
            get { return biaotisize; }
            set {biaotisize = value;
                this.Invalidate();//调用重绘事件使更改该属性回车后控件能重绘
            }
        }

        #endregion

        #region Linq查询

       public void test20()
        {
            //建议在存储查询结果时，将返回的集合对象分配给一个新变量
            //查询数组中<5的值
            int[] nums = { 1, 3, 5, 7, -1, -2 };
            var data = from num in nums
                       where num < 5
                       select num;
            int[] a = data.ToArray();
            //以上可以简化为
            int[] a1 = nums.Where(num => num < 5).ToArray();

            /*从集合中找出所有包含字母“a”的字符串，
            并将其按照长度进行排序，然后将结果转换为大写的形式
            最后再反转序列的元素。
            变量n在每一个Lambda表达式中都是私有的*/
            string[] names = { "sfqa", "eds", "zdasda" };
            var a2 = names
                .Where(n => n.Contains("a"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToUpper()).Reverse();

            

            //按键值分组查询
            //let 用于存储中间值
            //group ... by 根据什么键值分组，into 追加语句
            //orderby 排序（默认升序）加上descending为降序，多个条件的时候用","把条件分割即可
            //下例为将数组中的元素除10+1的结果为键值进行降序分组
            var data1 = from num in nums
                        let l1 = num / 10                       
                        group num by l1 + 1 into g      
                        orderby g.Key descending
                        select g;

            //组合元素Union和Concat，Concat组合两个集合的元素，Union组合两个集合的元素并删掉重复的
            IEnumerable<int> concat = nums.Concat(nums);
            names.Concat(nums.Select(n=>n.ToString()));

            //Intersect返回两个序列中的共同元素，
            //而Except则返回存在于第一个序列却不存在于第二个序列中的元素：
        }

        //对比两个数据源的操作
        public void test20_1()
        {
            //先创建两个数据源
            List<student> l_xs = new List<student>();
            l_xs.Add(new student("A", 1));
            l_xs.Add(new student("B", 1));
            l_xs.Add(new student("C", 2));

            List<Techen> l_ls = new List<Techen>();
            l_ls.Add(new Techen("老师1", 1));
            l_ls.Add(new Techen("老师2", 2));
            l_ls.Add(new Techen("老师3", 3));

            //下例为内部连接（from中有的，join中没有，则结果不会出现）
            //用join来连接 equals表示等号
            //生成一个新的表格
            var l_ls_xs = from l1 in l_ls
                          join l2 in l_xs on l1._ID equals l2.ID
                          select new { laoshi = l1._Name, xuesheng = l2.Name };

            //下例为左外部连接（from中有的，join中没有，用DefaultIfEmpty添加要的数据）
            //下例为如果老师编号没有对应的学生则创建一个空的学生
            var l_ls_xs1 = from l1 in l_ls
                           join l2 in l_xs on l1._ID equals l2.ID into l3
                           select l3.DefaultIfEmpty(new student("NULL", l1._ID));

        }
        private class student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public student(string name,int id)
            {
                ID = id;
                Name = name;
            }
        }
        private class Techen
        {
            public int _ID { get; set; }
            public string _Name { get; set; }
            public Techen(string name, int id)
            {
                _ID = id;
                _Name = name;
            }
        }

        #endregion

        #region Lambda表达式
        //注意事项
        //Lambda可以使用外部变量但是使用的外部变量使用的是委托调用时最新的数据
        public void text_21()
        {
            int see = 3;
            Func<int, int> act_int = n => see * n;
            see = 10;
            act_int(10);//结果为100；

            //如果在循环里要新定义一个变量=i，如果直接在Lambda里使用i则输出时只使用最后的i值
            Action[] acts = new Action[3];
            string str = "";
            for(int i=0;i<3;i++)
                acts[i] = () => str += i.ToString();
            foreach (Action act in acts)
                act();//str=333

            for (int i = 0; i < 3; i++)
            {
                int a = i;
                acts[i] = () => str += a.ToString();
            }
            foreach (Action act in acts)
                act();//str=012

        }


        #endregion

        #region 调试、Windows事件日志、测量运行时间
        //在没有#define CESHI之前test_22()调用了也不会执行
        //所有Debug类中的方法都标记为[Conditional("DEBUG")]。
        [Conditional("CESHI")]
        void test_22()
        {
            //Debug.Write会在输出窗口输出
            Debug.Write("test_22\n");
            Debug.WriteLine("隔行输出");
            Debug.WriteLineIf(true, "第一个参数为true时隔行输出");
            //Debug.Fail会弹出一个提示窗口，指出当前错误发生的行号和其他的一些错误信息，
            //并且提供三个按钮：中止、重试、忽略
            Debug.Fail("发生错误");
            //Assert方法在其bool参数为false时调用Fail方法，称为断言（assertion）
            Debug.Assert(false, "第一个参数为false时调用Fail方法");
            //应用程序结束最好刷新监视器
            Debug.Close();
            //如果是文件或者流的话推荐使用(每次写入结束都会强制刷新监视器)
            Debug.AutoFlush = true;
        }

        void test_22_1()
        {
            //1、写入事件日志
            const string sourceName = "事件源名称";
            if (!EventLog.SourceExists(sourceName))
                EventLog.CreateEventSource(sourceName, "源的项写入的日志名");//利用该名称查找日志
            EventLog.WriteEntry(sourceName, "要写入日志的消息", EventLogEntryType.Information);

            //2、读取事件日志
            EventLog log = new EventLog("源的项写入的日志名");
            EventLogEntry lsat = log.Entries[log.Entries.Count - 1];
            Debug.WriteLine("Index：" + lsat.Index);
            Debug.WriteLine("source：" + lsat.Source);
            Debug.WriteLine("type：" + lsat.EntryType);
            Debug.WriteLine("time：" + lsat.TimeWritten);
            Debug.WriteLine("Message：" + lsat.Message);
            Debug.Close();
            log.Close();

            //3、监视事件日志
            //EntryWritten事件可以在条目写入Windows事件日志时获得通知。
            //这种机制适用于本机事件日志，任何应用程序记录日志均会触发该事件。
            using (var log1 = new EventLog("日志名"))
            {
                log1.EnableRaisingEvents = true;//启用事件
                log1.EntryWritten += Log1_EntryWritten;
            }

        }
        
        private void Log1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            EventLogEntry entry = e.Entry;
            Debug.WriteLine(entry.Message);
            Debug.Close();
        }

        //Stopwatch类提供了一种方便的机制来测量运行时间
        void test_22_2()
        {
            //Stopwatch类的精度能少于1微秒，用DateTine.Now精度在15毫秒左右
            Stopwatch sw = new Stopwatch();

            sw.Start(); //开始

            //中间运行的程序

            sw.Stop(); //结束

            Debug.WriteLine(sw.Elapsed.ToString());//所用的时间
            Debug.Close();
        }

        #endregion

        #region 加密

        void test_23()
        {
            //散列算法加密，相同的字节数组散列运算后的散列值一样，结果不可逆
            //可以在数据库中存运算后的散列值，这样数据库泄露也能保证密码安全
            string mm = "1276145143yghrtxgh+8*/456";//密码
            byte[] bmm = Encoding.Default.GetBytes(mm);//把字符串转为byte[]

            //安全性SHA1(20)->SHA256(32)->SHA384(48)->SHA512(64)
            //密码推荐使用SHA256
            byte[] jiami = SHA256.Create().ComputeHash(bmm);//获取散列值
            string s = Convert.ToBase64String(jiami);//把散列值转为字符串
            byte[] jiama1 = Convert.FromBase64String(s);//把字符串转为散列值
       
        }

        #endregion

        private void textBox1_Click(object sender, EventArgs e)
        {
            Process.Start(@"KEYS.exe");
            KeyValuePair<int, string> kp = new KeyValuePair<int, string>(1, "s");
            List<int> a = new List<int>();
          
            
        }

      

    }
}
