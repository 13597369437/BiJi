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

namespace BiJi
{
    public partial class 队列方式读取串口 : Form
    {
        Queue<Action> Duilie = new Queue<Action>();
        bool zt1 = false;
        bool zt2 = false;
        public 队列方式读取串口()
        {
            InitializeComponent();
        }

        private void 队列方式读取串口_Load(object sender, EventArgs e)
        {
            Duilie.Enqueue(lianxu);
            Task.Run(new Action(() =>
            {
                while(true)
                {
                    try
                    {
                        //队列方式
                        //if (Duilie.Count > 0)
                        //{
                        //    //Invoke(new Action(() => listBox1.Items.Add(Duilie.Count)));
                        //    Thread.Sleep(100);
                        //    Action act = Duilie.Dequeue();
                        //    act();
                        //}
                        //else
                        //    Duilie.Enqueue(lianxu);

                        //循环方式
                        lianxu();
                        Thread.Sleep(100);
                        if (zt1)
                        {
                            test1();
                            zt1 = false;
                            Thread.Sleep(100);
                        }
                        if (zt2)
                        {
                            test2();
                            zt2 = false;
                            Thread.Sleep(100);
                        }
                    }
                    catch { }
                  
                }
            }));

        }

        //队列方式测试方法
        void lianxu()
        {
            //DateTime dt = DateTime.Now;
            //Invoke(new Action(() => listBox1.Items.Add("查询0/" + dt.ToString("HH:mm:ss"))));
           
        }
        void test1()
        {
            DateTime dt = DateTime.Now;
            Invoke(new Action(() => listBox1.Items.Add("查询1/" + dt.ToString("HH:mm:ss"))));
         
        }
        void test2()
        {
            DateTime dt = DateTime.Now;
            Invoke(new Action(() => listBox1.Items.Add("查询2/" + dt.ToString("HH:mm:ss"))));
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            zt1 = true;
            lock (Duilie)
            {
                DateTime dt = DateTime.Now;
                listBox2.Items.Add("查询1/" + dt.ToString("HH:mm:ss"));
                Duilie.Enqueue(test1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zt2 = true;
            lock (Duilie)
            {
                DateTime dt = DateTime.Now;
                listBox2.Items.Add("查询2/" + dt.ToString("HH:mm:ss"));
                Duilie.Enqueue(test2);
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            test(4);
        }

        void test(int n)
        {
            int[,] she = new int[n, n];
            int num = 1;
            for (int i = 0; i < n / 2; i++)
            {
                for (int j1 = i; j1 < n - i; j1++)
                {
                    she[i, j1] = num++;
                }
                for (int j2 = i + 1; j2 < n - i-1; j2++)
                {
                    she[j2, n - i-1] = num++;
                }
                for (int j3 = n - i-1; j3 > i; j3--)
                {
                    she[n - i-1, j3] = num++;
                }
                for (int j4 = n - i-1; j4 > i; j4--)
                {
                    she[j4, i] = num++;
                }
            }

            for(int i=0;i<n;i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Debug.Write(she[i, j]);
                    Debug.Write("\t");
                }
                Debug.Write("\n");
            }
            
        }
    }
}
