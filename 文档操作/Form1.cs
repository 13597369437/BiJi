using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace 文档操作
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region 文件操作
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="wjlj">要创建的文件夹名称及其子目录</param>
        /// <returns></returns>
        public bool wj_jianli(string[] wjlj)
        {
            try
            {
                string s1 = Directory.GetCurrentDirectory();
                for (int i = 0; i < wjlj.Length; i++)
                {
                    s1 += "\\" + wjlj[i];
                }
                if (!Directory.Exists(s1))
                    Directory.CreateDirectory(s1);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("創建文件夾失敗!" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 生成記事本
        /// </summary>
        /// <param name="mc">記事本名稱</param>
        /// <param name="jilu">要記錄的數據</param>
        private void scjishiben(string mc, List<string> jilu)
        {
            string[] aa = { mc, DateTime.Now.ToString("yyyy-MM") };
            if (wj_jianli(aa))
            {
                string txtName = Directory.GetCurrentDirectory() + "\\" + mc + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\" +
                                   DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                using (StreamWriter sw = new StreamWriter(txtName, true))//自动创建记事本
                {
                    sw.WriteLine(" " + "\n");
                    sw.WriteLine(" " + "\n");
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    for (int i = 0; i < jilu.Count; i++)
                    {
                        sw.WriteLine(jilu[i]);
                    }
                }
                //sw.Flush();
                //sw.Close();
            }
        }

        //文件类 File 操作
        void fileaa()
        {
            //记事本后缀.txt，文档后缀.docx
            File.Create(@"要创建文件的路径");
            File.Delete(@"要删除文件的路径");
            File.Copy(@"要复制文件的路径", @"要粘贴的路径");

            //读文件内容以字节的形式
            byte[] b = File.ReadAllBytes(@"要读文件的路径");
            //把读取的文件内容转换成字符串
            string s = Encoding.Default.GetString(b);

            //写文件内容，以字节的形式
            string ss = "要写的内容";
            byte[] bb = Encoding.Default.GetBytes(s);
            File.WriteAllBytes(@"要写进去的文件路径", b);
            /* 
             * 要注意编码格式不然会乱码
             * 编码格式有
             */

            //以行的形式读取
            string[] str = File.ReadAllLines(@"要读文件的路径");

            //以行的形式写入
            string[] str1 = new string[22];
            File.WriteAllLines(@"路径", str1);

            //以文本的形式读取,Encoding.GetEncoding("GB2312")为简体中文，Encoding.GetEncoding("GBK")为繁简体
            string str2 = File.ReadAllText(@"路径", Encoding.GetEncoding("GB2312"));

            //以文本的形式写入
            File.WriteAllText(@"路径", "文本内容");

            //以上方式写人会覆盖掉原来的文本
            //按下面方法则不会
            //File.AppendAllText();


        }

        /// <summary>
        /// 从txt文本中读出数据赋值给控件，文件放在Debug里
        /// </summary>
        /// <param name="textBox">要赋值的控件</param>
        /// <param name="path">TXT文件名（要带后缀）</param>
        private void jthinit(TextBox textBox, string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("GB2312"));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                textBox.Text = line;
            }
            sr.Close();
        }
        #endregion

        #region 数据库操作
        //2003以下版本用4.0，2007以下版本用12.0
        const string CONN = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Baobiao.mdb";
        //const string CONN = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Baobiao.mdb;Jet OLEDB:Database Password=1353060";

        void shujuku(int gnm)
        {
            switch (gnm)
            {
                case 0:
                    #region 新增数据
                    //新增数据
                    using (OleDbConnection con = new OleDbConnection(CONN))
                    {
                        con.Open();

                        //判断数据库是否连接成功
                        if (con.State == ConnectionState.Open)
                        {
                            //连接成功
                        }

                        using (OleDbCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "inser into zancun(sv1,sv2,sv3) values(@s1,@s2,@s3)";
                            //sv1为日期格式
                            OleDbParameter parameter = new OleDbParameter();
                            parameter.ParameterName = "s1"; //注意这里没有@
                            parameter.OleDbType = OleDbType.DBDate;
                            parameter.Value = DateTime.Now;
                            cmd.Parameters.Add(parameter);

                            cmd.Parameters.AddWithValue("s1", "要添加的内容");
                            cmd.Parameters.AddWithValue("s2", "要添加的内容");
                            cmd.Parameters.AddWithValue("s3", "要添加的内容");
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion
                    break;

                case 1:
                    #region 修改数据
                    //修改数据
                    using (OleDbConnection con = new OleDbConnection(CONN))
                    {
                        con.Open();
                        using (OleDbCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "update zancun set sv1=@s1,sv2=@s2,sv3=@s3 where sv4=@s4 and sv5=@s5";
                            cmd.Parameters.AddWithValue("s1", "要修改的内容");
                            cmd.Parameters.AddWithValue("s2", "要修改的内容");
                            cmd.Parameters.AddWithValue("s3", "要修改的内容");
                            cmd.Parameters.AddWithValue("s4", "要判断的内容");
                            cmd.Parameters.AddWithValue("s5", "要判断的内容");
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion
                    break;

                case 2:
                    #region 查询数据
                    //查询数据
                    using (OleDbConnection con = new OleDbConnection(CONN))
                    {
                        con.Open();
                        using (OleDbCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "select*from zancun  where sv1=@s1";
                            cmd.Parameters.AddWithValue("s1", "要查询的内容");
                            using (OleDbDataReader reat = cmd.ExecuteReader())
                            {
                                if (reat.Read())
                                {
                                    //如果存在。。。。
                                    if (reat["其它要查询的数据"].ToString() == "")
                                    {
                                        string aa = reat["其它要查询的数据"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;

                case 3:
                    #region 把该列的数据全部读出
                    //把该列的数据全部读出
                    using (OleDbConnection con = new OleDbConnection(CONN))
                    {
                        con.Open();
                        using (OleDbCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "select sv1,sv2 from zancun";
                            OleDbDataReader read = cmd.ExecuteReader();
                            listBox1.Items.Clear();
                            while (read.Read())
                            {
                                listBox1.Items.Add(read["sv1"].ToString() + "-" + read["sv2"].ToString());
                            }
                        }
                    }
                    #endregion
                    break;

                case 4:
                    #region 删除数据
                    //删除数据
                    using (OleDbConnection con = new OleDbConnection(CONN))
                    {
                        con.Open();
                        using (OleDbCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "delete from zancun  where sv4=@s4 and sv5=@s5";
                            cmd.Parameters.AddWithValue("s4", "要判断的内容");
                            cmd.Parameters.AddWithValue("s5", "要判断的内容");
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion
                    break;

                case 5:
                    #region 读取第一行或最后一行数据
                    //读取最后一行数据
                    try
                    {
                        string[] tempData = new string[30];
                        using (OleDbConnection con = new OleDbConnection(CONN))
                        {
                            con.Open();
                            using (OleDbCommand cmd = con.CreateCommand())
                            {
                                cmd.CommandText = "select top 1  *  from zancun order by ID desc ";//增加dsec是最后一行,不用就是第一行,top 1的作用是只读一行
                                using (OleDbDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        tempData[3] = reader["sv1"].ToString();
                                        tempData[4] = reader["sv2"].ToString();
                                        tempData[5] = reader["sv3"].ToString();
                                        tempData[6] = reader["sv4"].ToString();

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    #endregion
                    break;

                case 6:
                    #region 读取最后一行数据并修改
                    //读取第一行数据
                    try
                    {
                        string[] tempData = new string[30];
                        using (OleDbConnection con = new OleDbConnection(CONN))
                        {
                            con.Open();
                            using (OleDbCommand cmd = con.CreateCommand())
                            {
                                cmd.CommandText = "select top 1  *  from zancun order by ID desc";
                                using (OleDbDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        tempData[6] = reader["ID"].ToString();

                                    }
                                }
                                cmd.CommandText = "update zancun set sv1=@s1,sv2=@s2,sv3=@s3,sv4=@s4,sv5=@s5 where ID=@id";
                                cmd.Parameters.AddWithValue("s1", "要修改的内容");
                                cmd.Parameters.AddWithValue("s2", "要修改的内容");
                                cmd.Parameters.AddWithValue("s3", "要修改的内容");
                                cmd.Parameters.AddWithValue("s4", "要判断的内容");
                                cmd.Parameters.AddWithValue("s5", "要判断的内容");
                                cmd.Parameters.AddWithValue("id", tempData[6]);
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    #endregion
                    break;

                case 7:
                    #region 查询到的结果排序
                    //查询结果根据ID降序，根据sv1升序，升序时ASC可以省略
                    //cmd.CommandText = "SELECT *  FROM zancun ORDER BY ID desc, sv1 ASC";

                    #endregion
                    break;

                default: break;

            }

        }

        //使用MySQLClass1查询数据库
        void test2_2()
        {
            MySQLClass1 msql = new MySQLClass1("dome");
            string strsql = "SELECT * FROM table2 WHERE id='1'";
            if (msql.chaxun(strsql) == 1)
                Debug.WriteLine("查询成功");
            
            List<string> colmuns = new List<string>();
            List<string> datatypes = new List<string>();
            colmuns.Add("id");
            colmuns.Add("name");
            colmuns.Add("num");
            datatypes.Add("int primary key auto_increment");//设id为int类型，设为主键且自增
            datatypes.Add("text not null");//表示不允许空值
            datatypes.Add("text");
            msql.CreatedMysqlTable("table1", colmuns, datatypes);

        }

        //关于数据表的各种操作
        void test2_3()
        {
            //复制表（不复制数据）
            //string strsql = "CREATE TABLE 表名 LIKE 被复制的表名";
            string strsql = "CREATE TABLE table3 LIKE table2";

            //删除数据表
            //strsql = "DROP TABLE 表名";
            strsql = "DROP TABLE table4";
            
            //修改表名
            //strsql = "ALTER TABLE 表名 RENAME TO 新的表名";
            strsql = "ALTER TABLE table3 RENAME TO table4";

            //添加一列
            //strsql = "ALTER TABLE 表名 ADD 列名 数据类型";
            strsql = "ALTER TABLE table1 ADD num char";
            
            //修改列名称
            strsql = "ALTER TABLE 表名 CHANGE 列名 新列名 新数据类型";
            strsql = "ALTER TABLE 表名 MODIFY 列名 新数据类型";
            
            //删除列
            //strsql = "ALTER TABLE 表名 DROP 列名";
            strsql = "ALTER TABLE table1 DROP num1";
            
        }

        #endregion

        #region  配置文件（.ini）读取

        #region 方法1（直接用记事本打开编辑的无法读取，用程序写入的打开不显示）
        public Dictionary<string, string> configData;
        string fullFileName;
        public void Config(string _fileName)
        {
            configData = new Dictionary<string, string>();
            fullFileName = Application.StartupPath + @"\" + _fileName;
            //判断指定路径是否存在
            bool hasCfgFile = File.Exists(Application.StartupPath + @"\" + _fileName);
            if (hasCfgFile == false)
            {
                //如果不存在就创建
                StreamWriter writer = new StreamWriter(File.Create(Application.StartupPath + @"\" + _fileName));
                writer.Close();
            }
            StreamReader reader = new StreamReader(Application.StartupPath + @"\" + _fileName);
            string line;

            int indx = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith(";") || string.IsNullOrEmpty(line))
                {
                    configData.Add(";" + indx++, line);
                }
                else
                {
                    string[] key_value = line.Split('=');
                    if (key_value.Length >= 2)
                        configData.Add(key_value[0], key_value[1]);
                    else
                        configData.Add(";" + indx++, line);
                }
            }
            reader.Close();
        }
        //读取指定的值
        public string get(string key)
        {
            if (configData.Count <= 0)
                return null;
            else if (configData.ContainsKey(key))
                return configData[key].ToString();
            else
                return null;
        }
        //如果存在就修改，不存在就添加
        public void set(string key, string value)
        {
            if (configData.ContainsKey(key))
                configData[key] = value;
            else
                configData.Add(key, value);
        }
        public void save()
        {
            StreamWriter write = new StreamWriter(fullFileName, false, Encoding.Default);
            IDictionaryEnumerator enu = configData.GetEnumerator();
            while (enu.MoveNext())
            {
                if (enu.Key.ToString().StartsWith(";"))
                    write.WriteLine(enu.Value);
                else
                    write.WriteLine(enu.Key + "=" + enu.Value);
            }
            write.Close();
        }

        #endregion

        #region  方法2用动态连接数据库来实现(没有实现）
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">项目名称</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void IniwriteValue(string section, string Key, string Value)
        {
            WritePrivateProfileString(section, Key, Value, this.inipath);
        }

        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称</param>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }

        public bool ExistlINIFile()
        {
            return File.Exists(inipath);
        }
        #endregion

        #region 方法3以读取TXT文本的方式读取
        //public Dictionary<string, string> dicty;
        public void dakai(string _fileph, out Dictionary<string, string> dicty)
        {
            dicty = new Dictionary<string, string>();
            string filesw = Directory.GetCurrentDirectory() + "\\" + _fileph + ".txt";
            StreamWriter sw = new StreamWriter(filesw, true);//自动创建记事本
            sw.Close();
            string[] filestr = File.ReadAllLines(filesw);
            if (filestr.Length >= 1)
            {
                for (int i = 0; i < filestr.Length; i++)
                {
                    string[] dicstr = filestr[i].Split('=');
                    dicty.Add(dicstr[0], dicstr[1]);
                }
            }
        }
        public string dicget(string key, Dictionary<string, string> dicty)
        {
            if (dicty.Count <= 0)
                return null;
            else if (dicty.ContainsKey(key))
                return dicty[key].ToString();
            else
                return null;
        }


        #endregion

        #endregion
        
        #region 打开文件夹选择文件

        public void dakaiwenjian()
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Title = "请选择文件";  //标题
            ofg.InitialDirectory = @"C:\Users\admin\Desktop\tupian"; //文件夹地址
            ofg.Multiselect = true; //允许选择多个文件
            //ofg.Filter = "文件|*.wav|所有文件|*.*";
            ofg.ShowDialog();
            string[] path = ofg.FileNames; //获取选中的文件的名称
            for (int i = 0; i < path.Length; i++)
            {
                listBox1.Items.Add(Path.GetFileName(path[i]));
            }
        }

        #endregion

        #region XML文档操作

        //创建结构体方便管理xml读出的数据（也可以用类）
        struct xmldata
        {
            public int ID;
            public string Name;
            public int Num;
            public string loag;//Name的属性

        }

        //要保存xml文件在解决方案里找到创建的xml文件右键选择属性
        //复制到输出目录一栏选择：如果较新则复制
        //读取XML文件
        void xmlload()
        {
            XmlDocument xmldoc = new XmlDocument();
            //两种读取方式
            xmldoc.Load("XMLFile1.xml");
            //xmldoc.LoadXml(File.ReadAllText("XMLFile1.xml"));

            //获取分段目录，选择1是因为第一行：<?xml version="1.0" encoding="utf-8" ?>也算进分段里的
            //现在获取的是<xmlfile>这个分段名称
            XmlNode root = xmldoc.ChildNodes[1];
            //获取分段里的全部分段
            XmlNodeList xmllist = root.ChildNodes;

            List<xmldata> xmldatas = new List<xmldata>();

            //遍历获取全部内容
            foreach(XmlNode xmln in xmllist)
            {
                xmldata data = new xmldata();
                foreach (XmlNode xmln1 in xmln.ChildNodes)
                {
                    if (xmln1.Name == "id")
                        data.ID = int.Parse(xmln1.InnerText);
                    else if (xmln1.Name == "name")
                    {
                        data.Name = xmln1.InnerText;
                        data.loag = xmln1.Attributes[0].Value;//获取属性
                    }
                    else
                        data.Num = int.Parse(xmln1.InnerText);
                }
                xmldatas.Add(data);
            }

            foreach(xmldata xd in xmldatas)
            {
                listBox1.Items.Add($"{xd.ID}：{xd.Name} 属性：{xd.loag}  {xd.Num}");
            }

        }

        //第二种获取xml内容的方式
        void xmlload1()
        {
            XmlDocument xmldoc = new XmlDocument();
            //两种读取方式
            xmldoc.Load("XMLFile1.xml");
            //xmldoc.LoadXml(File.ReadAllText("XMLFile1.xml"));

            //获取分段目录，选择1是因为第一行：<?xml version="1.0" encoding="utf-8" ?>也算进分段里的
            //现在获取的是<xmlfile>这个分段名称
            XmlNode root = xmldoc.ChildNodes[1];
            //获取分段里的全部分段
            XmlNodeList xmllist = root.ChildNodes;

            //创建一个集合存储获取到的内容
            List<xmldata> xmldatas = new List<xmldata>();

            //遍历获取全部内容
            foreach (XmlNode xmln in xmllist)
            {
                xmldata data = new xmldata();

                XmlElement idele = xmln["id"];
                data.ID = int.Parse(idele.InnerText);

                XmlElement nameele = xmln["name"];
                data.Name = nameele.InnerText;
                //获取属性
                XmlAttributeCollection nameabc = nameele.Attributes;
                XmlAttribute nameab = nameabc["loag"];
                data.loag = nameab.Value;

                XmlElement numele = xmln["num"];
                data.Num = int.Parse(numele.InnerText);

                xmldatas.Add(data);
            }

            foreach (xmldata xd in xmldatas)
            {
                listBox1.Items.Add($"{xd.ID}：{xd.Name} 属性：{xd.loag}  {xd.Num}");
            }

        }

        #endregion

        #region JSON文档

        //JSON文档的创建是通过在解决方案创建文本文件来操作的
        //要保存JSON文件在解决方案里找到创建的JSON文件右键选择属性
        //复制到输出目录一栏选择：如果较新则复制
        //JSON的读取通过添加NuGet工具包：Newtonsoft.Json,来进行读取及后续的操作
        
        //json规则
        //json的对象用{}，数组用[]中间用“,”隔开
        //成员可以进行嵌套，嵌套的东西可以是对象可以是数组，如下
        //{
        //	"成员1":1
        //	"成员2":[
        //		 {"cy1":1,"cy2":"a"},
        //		 {"cy1":2,"cy2":"b"}
        //	 ]
        //}


        //创建一个结构体接收读取到的json数据
        struct jsondata
        {
            public int id;
            public string name;
            public int num;
        }
        //读取
        void test6_1()
        {
            //序列化
            jsondata[] jsondatas = JsonConvert.DeserializeObject<jsondata[]>(File.ReadAllText("jsonFile.txt"));
            foreach (jsondata xd in jsondatas)
            {
                listBox1.Items.Add($"{xd.id}：{xd.name} {xd.num}");
            }
        }

        //写入
        void test6_2()
        {
            //数组之类的也可以通过这个方式序列化
            var jsdata = new jsondata();
            jsdata.id = 4;
            jsdata.name = "第四个";
            jsdata.num = 40;
            string str = JsonConvert.SerializeObject(jsdata);
            Debug.WriteLine(str);
        }

        #endregion

        #region Excel操作(读可以，追加内容操作失败)

        //可以通过复制粘贴的Excel表格添加到解决方案里
        //使用前要安装NuGet程序包：NPOI
        //添加三个程序集

        //读取
        void text7_1()
        {
            DataTable dataTable = MyExcelClas.ExcletoDataTable("Excel操作.xls", "Sheet2", true);
        }

        //写入
        void text7_2()
        {
            List<List<string>> data = new List<List<string>>();
            List<string> values = new List<string>();
            values.Add("3");
            values.Add("c");
            values.Add("30");
            data.Add(values);
            if(MyExcelClas.ListToExcel("Excel操作.xlsx", "Sheet1", data))
                Debug.WriteLine("写入Excel成功！");
        }

        //使用EPPlus读写Excel
        //添加NuGet程序包：EPPlus
        void text7_3()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(@"E:\C#项目\C#笔记\BiJi_2.0\文档操作\Excel操作.xlsx");
            ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet2"];
            sheet.Cells.Style.Font.Name = "宋体";
            sheet.Cells.Style.Font.Size = 11F;
            sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Cells[1, 0].Value = "2";
            sheet.Cells[1, 1].Value = "C";
            sheet.Cells[1, 2].Value = "20";
            package.Save();
        }





        #endregion

        #region 使用NPOI将到出到Excel
        //使用前安装NuGet程序包：NPOI
        //dataGridView控件的RowHeadersVisible属性是关闭列标头


        //读取数据
        private void button4_Click(object sender, EventArgs e)
        {
            MySQLClass1 msql = new MySQLClass1("dome");
            string strsql = "SELECT * FROM daochu";
            DataSet daochu = msql.chaxunsql(strsql);
            DataTable dt = daochu.Tables[0];
            DataView dv = dt.DefaultView;
            dataGridView1.DataSource = dv;
        }

        //导出按钮
        private void button3_Click(object sender, EventArgs e)
        {
            SelectDataToExport(dataGridView1);
        }

        void SelectDataToExport(DataGridView Udgv)
        {
            DataTable dataTable = new DataTable();
            // 添加列定义
            foreach (DataGridViewColumn dataGridViewColumn in Udgv.Columns)
            {
                dataTable.Columns.Add(dataGridViewColumn.HeaderText, dataGridViewColumn.ValueType);
            }

            // 添加行数据
            foreach (DataGridViewRow dataGridViewRow in Udgv.Rows)
            {
                if (!dataGridViewRow.IsNewRow)
                {
                    DataRow row = dataTable.Rows.Add();
                    foreach (DataGridViewCell dataGridViewCell in dataGridViewRow.Cells)
                    {
                        row[dataGridViewCell.ColumnIndex] = dataGridViewCell.Value;
                    }
                }
            }

            int rows = Udgv.CurrentRow.Index;
            ExportDataToExcel(dataTable, "导出测试");
        }

        private void ExportDataToExcel(DataTable TableName, string FileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件标题
            saveFileDialog.Title = "导出Excel文件";
            //设置文件类型
            saveFileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls";
            //设置默认文件类型显示顺序  
            saveFileDialog.FilterIndex = 1;
            //是否自动在文件名中添加扩展名
            saveFileDialog.AddExtension = true;
            //是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //设置默认文件名
            saveFileDialog.FileName = FileName;
            //按下确定选择的按钮  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径 
                string localFilePath = saveFileDialog.FileName.ToString();

                //数据初始化
                int TotalCount;     //总行数

                TotalCount = TableName.Rows.Count;


                //NPOI
                IWorkbook workbook;
                string FileExt = Path.GetExtension(localFilePath).ToLower();
                if (FileExt == ".xlsx")
                {
                    workbook = new XSSFWorkbook();
                }
                else if (FileExt == ".xls")
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = null;
                }
                if (workbook == null)
                {
                    return;
                }
                ISheet sheet = string.IsNullOrEmpty(FileName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(FileName);




                try
                {
                    //读取标题  
                    IRow rowHeader = sheet.CreateRow(0);
                    for (int i = 0; i < TableName.Columns.Count; i++)
                    {
                        ICell cell = rowHeader.CreateCell(i);
                        cell.SetCellValue(TableName.Columns[i].ColumnName);
                    }

                    //读取数据  
                    for (int i = 0; i < TableName.Rows.Count; i++)
                    {
                        IRow rowData = sheet.CreateRow(i + 1);
                        for (int j = 0; j < TableName.Columns.Count; j++)
                        {
                            ICell cell = rowData.CreateCell(j);
                            cell.SetCellValue(TableName.Rows[i][j].ToString());
                        }

                    }


                    //转为字节数组  
                    MemoryStream stream = new MemoryStream();
                    workbook.Write(stream);
                    var buf = stream.ToArray();

                    //保存为Excel文件  
                    using (FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(buf, 0, buf.Length);
                        fs.Flush();
                        fs.Close();
                    }




                    //成功提示
                    if (MessageBox.Show("导出成功，是否立即打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(localFilePath);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

        }


        #endregion

        #region CSV文件操作

        void test9_0()
        {
            WriteCsv("1,谭阳,21");
            //List<string> datas;
            //ReadCsv("D:\\CSV\\2024-04-15-16.csv", out datas);//读取csv
            //Console.WriteLine(datas[1].Split(',')[1]);//前方数字为对应的行（1为去掉表头行的第一行），后方的1为对应的列（为第二列）
            //Console.WriteLine(datas[1].Split(',')[2]);
            //Console.WriteLine(datas[1].Split(',')[3]);
            //Console.ReadLine();
        }

        /// <summary>
        /// 写入csv
        /// </summary>
        /// <param name="result">写入内容 ----单元格内容，单元格内容-----</param>
        public static void WriteCsv(string result)
        {
            string path3 = System.IO.Directory.GetCurrentDirectory();//获取当前工作目录
            string path = "D:\\CSV\\";//保存路径
            string fileName = path + DateTime.Now.ToString("yyyy-MM-dd-HH") + ".csv";//文件名
            string Datedate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//年月日小时分钟秒
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fileName))
            {
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                string str1 = "时间" + "," + "编号" + "," + "姓名" + "," + "年龄" + "\t\n";
                sw.Write(str1);
                sw.Close();
            }
            StreamWriter swl = new StreamWriter(fileName, true, Encoding.UTF8);
            string str = Datedate + "," + result + "\t\n";
            swl.Write(str);
            swl.Close();
        }
        //读取csv
        public static void ReadCsv(string path, out List<string> data)
        {
            StreamReader sr;
            data = new List<string>();
            try
            {
                using (sr = new StreamReader(path, Encoding.GetEncoding("GB2312")))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.ToUpper().Equals("EXCEL"))
                        process.Kill();
                }
                GC.Collect();
                Thread.Sleep(10);
                Console.WriteLine(ex.StackTrace);
                using (sr = new StreamReader(path, Encoding.GetEncoding("GB2312")))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                    }
                }
            }

        }

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            //test6_1();
            //test6_2();
            MySQLClass1 msql = new MySQLClass1("dome");
            string strsql = $"INSERT INTO tab4 VALUES('1','2','3','4','5','6')";
            msql.ExecSql(strsql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            text7_3();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            test9_0();
        }
    }
}
