using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace 订阅_发布_委托
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //订阅按钮
        private void button2_Click(object sender, EventArgs e)
        {
            EatAction = null;
            dingyue();
        }

        //发布按钮
        private void button1_Click(object sender, EventArgs e)
        {
            fabu();
        }

        //委托按钮
        private void button3_Click(object sender, EventArgs e)
        {
            test1();
        }

        //调用委托按钮
        private void button4_Click(object sender, EventArgs e)
        {
            test2();
            test3();
        }

        /*****************委托****************/
        //第一步定义委托：delegate开头+返回类型+委托名+参数列表
        public delegate void weituo(string ph);

        //第二步声明委托
        weituo wt1;

        //第三步委托初始化：声明的委托 = new 委托名
        //要调用的方法（该方法的参数列表和类型必须和委托的一样）
        void test1()
        {
            wt1 = new weituo(weituoren);
        }
        //要委托的方法
        public void weituoren(string str) 
        {
            textBox2.Text = str;
        }

        //第四步委托的调用：先判断委托是否为null防止抛异常，声明的委托+参数列表
        void test2()
        {
            //if (wt1 != null)
            //{
            //    wt1("方式一");
            //}

            //跟 if (wt1 != null)等效
            wt1?.Invoke("方式二");
        }

        

        /********************c#内置的委托*****************/
        void nzwt()
        {
            //1、无返回值委托Action
            //无参数时
            Action action = test1;
            action();
            //有参数时
            //Action<string> action_s = weituoren;
            Action<string> action_s = s => weituoren(s);
            action_s("有参数时");

            //2、有返回值委托Func
            //无参数时<TResult>里为返回值类型
            //Func<string> s_func = sfunc;
            Func<string> s_func = delegate () { return "匿名方法"; };
            string str = s_func();
            //有参数时<T,TResult>里为返回值类型(多参数时，最后一个是返回值类型)
            //Func<string, string> s_func_s = sfunc_s;
            Func<string, string> s_func_s = delegate (string s) { return s; };//匿名方法
            str = s_func_s("有参数时");

            //3、返回值为bool的委托Predicate
            //有且只有一个参数
            Predicate<int> predicate = bpredicate;
            bool b = predicate(10);

            //4、返回值为int的委托Comparison
            //有且只有一个参数
            //此委托由 Array 类的 Sort<T>(T[], Comparison<T>) 方法重载和 List<T> 类的 Sort(Comparison<T>) 方法重载使用，
            //用于对数组或列表中的元素进行排序。
        }
        string sfunc()
        {
            string s = "无参数时Func委托";
            return s;
        }
        string sfunc_s(string s)
        {
            return s;
        }
        bool bpredicate(int i)
        {
            return i > 0 ? true : false;
        }

        /*****************订阅、发布事件***************/
        //事件就是特殊的委托，不能在类外被调用且只能通过+=和-=赋值，除了null
        // 第一步定义事件：event开头+委托（Action是系统默认的无返回值委托）+事件名
        public event Action EatAction;

        // 第二步订阅事件（一般放在窗口的Load事件里或者只会执行一次的方法里不然会重复订阅导致订阅的方法重复执行）
        public void dingyue()
        {
            //同一个事件可以有多个订阅者,订阅不能通过=添加
            //跨窗体订阅事件，如果订阅和发布不在同一窗体定义的事件要是静态的
            //在form2发布消息，执行form1上的方法，参考小键盘调用
            //以下注释会报错
            //EatAction = () => textBox1.Text = "收到消息了";
            EatAction += () => textBox1.Text = "收到消息了";
            EatAction += Form2.xianshi; //跨窗体订阅事件
        }

        // 第三步发布事件
        void fabu()
        {
            //事件不能在类外被调用
            EatAction();
        }


        /***************委托作为参数进行使用**************/
        public static void Sort<T>(T[] array,Func<T,T,bool>sort)
        {
            bool isjiaohuan;
            do
            {
                isjiaohuan = false;
                for(int i = 0;i<array.Length-1;i++)
                {
                    if (sort(array[i], array[i+1]))
                    {
                        T temp;
                        temp = array[i];
                        array[i] = array[i+1];
                        array[i+1] = temp;
                        isjiaohuan = true;
                    }
                }
            }
            while (isjiaohuan);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            YuanGong[] yuanGongs = {
            new YuanGong("1",31),
            new YuanGong("2",33),
            new YuanGong("3",45),
            new YuanGong("4",26),
            new YuanGong("5",64)
            };

            Sort<YuanGong>(yuanGongs, YuanGong.sort);

            foreach (var item in yuanGongs)
            {
                Debug.WriteLine(item.Name + ": " + item.Num.ToString());
            }


        }


        /******************委托可以异步调用*****************/
        delegate void Weituo2();
        void test3()
        {
            Weituo2 wt2 = () =>
            {
                Thread.Sleep(2100);
                MessageBox.Show("异步调用委托！");
            };

            wt2.BeginInvoke(null, null);
        }
    }
}
