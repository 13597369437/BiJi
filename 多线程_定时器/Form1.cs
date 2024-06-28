using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 多线程_定时器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region 多线程、定时器

        /*
         1、调用命名空间 using System.Threading;
         2、使 CheckForIllegalCrossThreadCalls = false;让线程可以调用控件
         3、编写线程
         4、启动线程
         注意：当线程中有死循环时，程序关闭时要退出死循环，不然程序没有真正结束
        */

        //创建线程  
        Thread th = null;
        void load()
        {
            //创建线程的实例和启动线程new ThreadStart(xiancheng1)
            th = new Thread(xiancheng1);
            th.IsBackground = true;//把线程设置为后台线程，后台线程程序结束会自动销毁
            //启动线程(要使有循环的线程结束，让循环体的条件结束即可，下次再启动线程就可以了)
            th.Start();
            ////挂起线程
            //th.Suspend();
            ////继续线程
            //th.Resume();
            ////终止线程
            //th.Abort();

            ////框架要4.5以上才有这个方法
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        this.Invoke(new Action(() =>
            //        {

            //        }));
            //    }
            //});
            ////这样就创建好线程并且启动了
            //Task.Run(new Action(xiancheng1));
        }

        bool xianchenEnd = true;
        void xiancheng1()
        {
            int i = 0;
            while (xianchenEnd)
            {
                //以下是该线程要运行的方法
                //若要线程操作空间可以用以下方式
                this.Invoke(new Action(() =>
                {
                    listBox1.Items.Add(i.ToString()+"线程ID:"+Thread.CurrentThread.ManagedThreadId);
                }));
                i++;
                //使线程修眠
                Thread.Sleep(1000);
                
            }
        }

        //线程可以传输一个参数进来，有多个参数需要传递的时候可以定义一个结构体
        struct Data
        {
            public int id;
            public string name;

            public Data(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
        void xiancheng2(object o)
        {
            //结构体是值类型所以不能用as来转换
            Data d = (Data)o;
            //可以为null的类型可以用as来转换
            //string s = o as string;

            string name = d.name;
            int id = d.id;
        }
        void test4()
        {
            Thread th1 = new Thread(xiancheng2);
            Data d = new Data(1,"A");
            //另一种初始化方式
            Data d1 = new Data() { id = 2, name = "B" };
            th1.Start(d);
        }

        #region 后台线程
        //创建后台线程
        //using System.ComponentModel; 创建后台线程要添加这个
        BackgroundWorker bkw = new BackgroundWorker();
        void load_ht()
        {
            //允许运行过程中把参数传到界面
            bkw.WorkerReportsProgress = true;
            //允许取消正在进行的运算
            bkw.WorkerSupportsCancellation = true;
            //后台线程要运行的内容
            bkw.DoWork += new DoWorkEventHandler(bkw_DoWork);
            //运行过程中把参数传到界面
            bkw.ProgressChanged += new ProgressChangedEventHandler(bkw_ProgressChanged);
            //当后台运行完或者取消后发生
            bkw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkw_RunWorkerCompleted);

        }

        void kaishi()
        {
            //开始执行先判断有没有在执行不然重复运行会报错
            if (bkw.IsBusy == false)
            {
                //可以赋初值传参数进DoWork
                bkw.RunWorkerAsync(50);
                //bkw.RunWorkerAsync();
            }
        }

        void bkw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //重新开始
            //bkw.RunWorkerAsync();
            MessageBox.Show("jieshu");
            string str = "后台操作结束（可能是完成了或者用户取消了或发生了异常）";
        }

        //运行过程中把参数传到界面
        void bkw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = @"已完成：" + e.ProgressPercentage.ToString() + @"%";
            listBox1.Items.Add(bgk);
        }

        //按Tab键得到此方法内写要在后台运行的内容(不要在此方法里对UI界面进行操作)
        //可以在此方法外面创建变量，在方法里对这些变量进行操作然后通过ProgressChanged
        //对UI界面进行操作
        string bgk = "";
        void bkw_DoWork(object sender, DoWorkEventArgs e)
        {
            //获取传递进来的参数根据参数类型进行转换
            int baa = 0;
            if (e.Argument != null)
            {
                baa = (int)e.Argument;
            }
            //如果BackgroundWorker bkw = new BackgroundWorker();是写在方法里这里不能调用bkw可用var来创建局部变量
            //var bgw = sender as BackgroundWorker;
            for (int i = 0; i <= baa; i++)
            {
                //取消背景作业
                if (bkw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    bgk = i.ToString();
                    //bkw.ReportProgress(baa);
                    bkw.ReportProgress(i * 100 / baa);////运行过程中把参数传到界面,不要这个ProgressChanged不会执行
                    Thread.Sleep(300);
                }
            }


        }

        //取消后台线程（可中途取消）
        void quxiao()
        {
            bkw.CancelAsync();
        }
        #endregion

        //在多线程中操作控件
        void xianchen3()
        {
            //在线程里访问ui都需要this.Invoke
            //(EventHandler)delegate 匿名委托
            this.Invoke((EventHandler)delegate { button1.Enabled = false; });

            //C#3.0以后可以用Lambda表达式代码更简洁
            this.Invoke(new Action(() =>
            {
                button1.Enabled = false;
                button1.Text = "用Lambda表达式";
            }));

            //记录一个时间戳，以演示倒计时效果
            int tick = Environment.TickCount;

            while (Environment.TickCount - tick < 1000)
            {
                this.Invoke(new Action(() => button1.Text = (1000 - Environment.TickCount + tick).ToString()));
                Thread.Sleep(100);
            }
            
       
        }


        #region 异步操作

        void test_3()
        {
            //new Thread(() =>
            //{
            //    Debug.Write("2到3000000的素数总和：");
            //    int a = Enumerable.Range(2, 3000000).Count(n =>
            //         Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
            //    Debug.Write(a.ToString()+"\n");
            //}).Start();

            //获取返回值的话当线程所需时间久的话主线程也会卡住
            //Task<int> ptack = Task.Run(() =>
            //  Enumerable.Range(2, 3000000).Count(n =>
            //   Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            //Debug.Write("2到3000000的素数总和：");
            //Debug.Write(ptack.Result);

            //延续，此方法可以解决上面的问题
            Task<int> ptack = Task.Run(() =>
              Enumerable.Range(2, 3000000).Count(n =>
               Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            var awa = ptack.GetAwaiter();
            awa.OnCompleted(() =>
            {
                int a = awa.GetResult();
                Debug.Write(a);
            });

            //var tcs = new TaskCompletionSource<int>();
            //new Thread(() => { Thread.Sleep(5000); tcs.SetResult(42); })
            //{ IsBackground = true }.Start();
            //Task<int> task = tcs.Task;
            //Debug.Write(task.Result);

            //以下两个方法效果一样，都是等待5秒后输出42，跟Thread.Sleep(5000)效果一样但是他不会阻塞主线程
            //Task.Delay(5000).GetAwaiter().OnCompleted(() => Debug.Write(42));
            //Task.Delay(5000).ContinueWith(a => Debug.Write(42));


        }

        async void test_3_1()
        {
            //int result = await GetPrimesCountAsync(2, 1000000);
            //Debug.Write(result);


            //执行异步操作如果特别耗时，最好把触发该异步方式的条件禁用
            //防止连续执行
            button5.Enabled = false;
            for (int i = 1; i < 5; i++)
            {
                listBox1.Items.Add(await GetPrimesCountAsync(i * 1000000, 1000000));
            }
            button5.Enabled = true;

            //可以用这种方式使某个方法延迟一定时间执行
            //await Task.Delay(5000);
            //Debug.Write("result");
        }

        //await修饰的函数返回值要是Task<>或者Task有类型的，
        Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
              ParallelEnumerable.Range(start, count).Count(n =>
               Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        //要编写异步函数，可将返回类型由Void改为Task，这样方法本身就可以进行异步调用
        //此时不需要显式返回一个Task，编译器会负责生成，如果有返回参数，直接返回参数即可
        async Task PrintAns()
        {
            await Task.Delay(5000);
            Debug.Write("PrintAns");
            
        }
        async Task Go()
        {
            await PrintAns();
            Debug.Write("Go");
        }
        async Task<int> GetAns()
        {
            await Task.Delay(5000);
            int ans = 21;
            return ans;
        }

        //以下可在c#8.0 14.6章中查询
        //取消异步操作
        //要获得一个取消令牌，首先要实例化一个CancellationTokenSource
        //它有一个Token属性，可以返回一个CancellationToken
        // cancelSource.Cancel();取消异步操作
        //注意取消时会发生异常
        async void test_3_2()
        {
            try
            {
                var cancelSource = new CancellationTokenSource();
                for (int i = 0; i < 10; i++)
                {
                    if (i == 5)
                        cancelSource.Cancel();
                    listBox1.Items.Add(i);
                    await Task.Delay(1000, cancelSource.Token);
                }
            }
            catch
            { }
        }

        //实例化CancellationTokenSource时就可以指定一个时间间隔，达到一定时间后
        //启动取消操作的目的，不论是同步还是异步，都可以用这种方式来实现超时操作
        async void test_3_3()
        {
            var cancelSource = new CancellationTokenSource(5000);
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    listBox1.Items.Add(i);
                    await Task.Delay(1000, cancelSource.Token);
                }
            }
            catch (OperationCanceledException ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }



        #endregion

        //定时器
        void test_3_4()
        {
            try
            {
                new WebClient().DownloadFile("http://www.linqpad.net", "lp.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region 任务

        //创建任务并启动
        void test5()
        {

            Task t1 = new Task(test5_1);

            //连续任务
            Task t2 = t1.ContinueWith(test5_2);

            Task t3 = t2.ContinueWith(test5_3);

            //连续任务的话启动最开始的任务即可
            t1.Start();

            //等待t1完成，如果t1没有启动则会卡在这里
            //t1.Wait();
        }
        void test5_1()
        {
            //Invoke中不可进行延时操作不然会阻塞主线程
            this.Invoke(new Action(() =>
            {
                listBox1.Items.Add("任务1被执行");
                textBox1.Text = "任务1被执行";
                //Thread.Sleep(2000);
            }));
            //Thread.Sleep(2000);
            //在任务中延时可以用以下方式
            Task.Delay(2000).Wait();

        }
        void test5_2(Task t)
        {
            this.Invoke(new Action(() =>
            {
                listBox1.Items.Add("任务1执行完成");
                textBox1.Text = "任务1执行完成";
            }));
        }

        //任务的四种启动方式
        void test5_3(Task t)
        {
            Action<object> action = (object obj) =>
            {
                Debug.WriteLine("Task={0}, obj={1}, Thread={2}",
                Task.CurrentId, obj,
                Thread.CurrentThread.ManagedThreadId);
            };

            // 第一种：Start启动
            Task t1 = new Task(action, "alpha");
            t1.Start();
            // 等待t1完成
            t1.Wait();

            // 第二种：Factory.StartNew启动
            Task t2 = Task.Factory.StartNew(action, "beta");
            t2.Wait();


            // 第三种：Task.Run启动
            String taskData = "delta";
            Task t3 = Task.Run(() =>
            {
                Debug.WriteLine("Task={0}, obj={1}, Thread={2}",
                                  Task.CurrentId, taskData,
                                   Thread.CurrentThread.ManagedThreadId);
            });
            t3.Wait();



            // 第四种：RunSynchronously同步运行
            Task t4 = new Task(action, "gamma");
            // 同步运行，尽管任务是同步运行的，但这是一个很好的做法
            t4.RunSynchronously();
            t4.Wait();

        }

        //创建可以返回值的任务
        void test5_4()
        {
            //获取返回值的话当线程所需时间久的话主线程也会卡住
            var t = Task<string>.Run(() =>
              {
                  Thread.Sleep(2000);
                  return "任务的返回值";
              });
            //Debug.WriteLine(t.Result);

            //使用延续可以解决上述问题
            var awa = t.GetAwaiter();
            awa.OnCompleted(() =>
            {
                Debug.WriteLine(awa.GetResult());
            });
           
        }

        //任务中的异常处理
        void test5_5()
        {
            Task t = Task.Run(() =>
              {
                  try
                  {
                      //在线程中直接操作UI界面会引发异常
                      label1.Text = "error";
                  }
                  catch(Exception e)
                  {
                      Debug.WriteLine(e.Message);
                  }
              });

            try
            {
                t.Wait();
            }
            catch (AggregateException ae)
            {
                //foreach(var ex in ae.InnerExceptions)
                //{
                //    if (ex is CustomException)
                //    {

                //    }
                //    else
                //        throw ex;
                //}

                Debug.WriteLine(ae.Message);
            }


        }


        #endregion


        #endregion

        //开始线程按钮
        private void button2_Click(object sender, EventArgs e)
        {
            xianchenEnd = true;
            if (th == null)
                load();
        }

        //结束线程按钮
        private void button3_Click(object sender, EventArgs e)
        {
            xianchenEnd = false;

            th = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //test5();
            //test5_3();
            //test5_4();
            test5_5();
        }
    }
}
