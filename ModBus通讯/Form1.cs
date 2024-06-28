
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBus通讯
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region Nmodbus4的使用
      
        /************RTU通讯*****************/

        //创建一个串口对象
        SerialPort nmbcom = new SerialPort();
        //创建主站
        IModbusMaster master;

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="iPortNo">串口号</param>
        /// <param name="iBaudRate">波特率</param>
        /// <param name="iDataBits">数据位</param>
        /// <param name="iStopBits">停止位</param>
        /// <param name="iParity">奇偶校验位(请输入N,O,E)</param>
        /// <returns></returns>
        public bool OpenMyCom(string iPortNo, int iBaudRate, int iDataBits, int iStopBits, string iParity)
        {
            try
            {
                if (nmbcom.IsOpen && nmbcom != null)
                {
                    nmbcom.Close();
                }
                //设置串口参数
                nmbcom.PortName = iPortNo;
                nmbcom.BaudRate = iBaudRate;
                nmbcom.DataBits = iDataBits;
                switch (iStopBits)
                {
                    case 0: nmbcom.StopBits = StopBits.None; break;
                    case 1: nmbcom.StopBits = StopBits.One; break;
                    case 2: nmbcom.StopBits = StopBits.Two; break;
                    default: nmbcom.StopBits = StopBits.One; break;
                }
                switch (iParity)
                {
                    case "N": nmbcom.Parity = Parity.None; break;
                    case "O": nmbcom.Parity = Parity.Odd; break;
                    case "E": nmbcom.Parity = Parity.Even; break;
                    default: nmbcom.Parity = Parity.Even; break;
                }

                //创建ModbusRTU主站实例
                master = ModbusSerialMaster.CreateRtu(nmbcom);
                nmbcom.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public bool ClosePort()
        {
            if (nmbcom.IsOpen)
            {
                nmbcom.Close();
                return true;
            }
            else
                return false;
        }

        //写入
        void test_1()
        {
            //写入单个线圈(站号，起始地址，写入值)
            master.WriteSingleCoilAsync(1, 0, false);

            //批量写入线圈(站号，起始地址，写入值)
            bool[] data_b = { true, true, true };
            master.WriteMultipleCoilsAsync(1, 0, data_b);

            //写入单个寄存器(站号，起始地址，写入值)
            master.WriteSingleRegisterAsync(1, 0, 10);

            //写入多个寄存器(站号，起始地址，写入值)
            ushort[] data_us = { 1, 2, 3, 4 };
            master.WriteMultipleRegistersAsync(1, 0, data_us);

        }

        //读取
        void test_2()
        {
            //读取输出线圈(站号，起始地址，读取长度)
            bool[] data_b = master.ReadCoilsAsync(1, 0, 5).Result;

            //读取输入线圈(站号，起始地址，读取长度)
            bool[] data_b1 = master.ReadInputsAsync(1, 0, 5).Result;

            //读取保持寄存器(站号，起始地址，读取长度)
            ushort[] data_us = master.ReadHoldingRegistersAsync(1, 0, 5).Result;

            //读取输入寄存器(站号，起始地址，读取长度)
            ushort[] data_us1 = master.ReadInputRegistersAsync(1, 0, 5).Result;
        }


        /************TCP通讯*****************/

        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 502;//端口号
        TcpClient tcpClient = new TcpClient();


        //建立连接
        void test_3()
        {

            //tcpClient.Connect(ipAddress, port);
            IAsyncResult con = tcpClient.BeginConnect(ipAddress, port, null, null);
            con.AsyncWaitHandle.WaitOne(2000);

            var nmbtcp = ModbusIpMaster.CreateIp(tcpClient);

            try
            {//读写方法是一样的
                ushort[] data_us = nmbtcp.ReadHoldingRegisters(2, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #endregion


        SerialPort mycom = new SerialPort();
        DAL_modbus mycom1 = new DAL_modbus();

        private void button1_Click(object sender, EventArgs e)
        {
            //if (OpenMyCom("COM2", 9600, 8, 1, "E"))
            //    button1.Enabled = false;


            //设置串口参数
            //mycom.PortName = "COM2";
            //mycom.BaudRate = 9600;
            //mycom.DataBits = 8;
            //mycom.StopBits = StopBits.One;
            //mycom.Parity = Parity.Even;
            //mycom.DataReceived += Mycom_DataReceived;
            //mycom.Open();
            //if(mycom.IsOpen)
            //    button1.Enabled = false;

            if (mycom1.OpenMyCom("COM2", 115200, 8, 1, "E"))
                button1.Enabled = false;
        }


        private void Mycom_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //用这种方式读取如果报文发送太快会造成接收到的报文不完整
            //两种解决方式
            //1：判断接收到的报文是否完整
            
            byte[] data = new byte[25];
            mycom.Read(data, 0, 25);
            mycom.DiscardInBuffer();

            string s = "";
            foreach (var b in data)
                s += b.ToString("X2");
            this.Invoke(new Action(() =>
            {
                listBox1.Items.Add(s);
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ClosePort();
            //mycom.Close();
            //mycom.DataReceived -= Mycom_DataReceived;

            mycom1.CloseMyCom();
            button1.Enabled = true;
            r1 = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ////读取
            //ushort[] data_us = master.ReadHoldingRegistersAsync(1, 0, 5).Result;

            //foreach (var d in data_us)
            //{
            //    listBox1.Items.Add(d.ToString());
            //}


            //Task.Run(new Action(() =>
            //{
            //    int i = 0;
            //    while (i < 50)
            //    {
            //        byte[] Fasong1 = new byte[16];
            //        Fasong1[0] = (byte)1;
            //        Fasong1[1] = (byte)3;
            //        Fasong1[2] = (byte)0;
            //        Fasong1[3] = (byte)0;
            //        Fasong1[4] = (byte)0;
            //        Fasong1[5] = (byte)Convert.ToInt16("0A", 16);
            //        Fasong1[6] = (byte)Convert.ToInt16("C5", 16);
            //        Fasong1[7] = (byte)Convert.ToInt16("CD", 16);
            //        mycom.Write(Fasong1, 0, 8);
            //        i++;
            //        Task.Delay(50).Wait();
            //        //this.Invoke(new Action(() =>
            //        //{
            //        //    listBox1.Items.Add(i);
            //        //}));
            //    }
            //}));

            test_3();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        int cs = 2;
        object _lock1 = new object();
        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Task.Run(new Action(() =>
            //    {
            //        lock (_lock1)
            //        {
            //            byte[] Fasong1 = new byte[13];
            //            Fasong1[0] = (byte)1;
            //            Fasong1[1] = (byte)16;
            //            Fasong1[2] = (byte)0;
            //            Fasong1[3] = (byte)0;
            //            Fasong1[4] = (byte)0;
            //            Fasong1[5] = (byte)2;
            //            Fasong1[6] = (byte)4;
            //            Fasong1[7] = (byte)0;
            //            Fasong1[8] = (byte)0;
            //            Fasong1[9] = (byte)0;
            //            Fasong1[10] = (byte)cs;
            //            Fasong1[11] = DAL_modbus.CRC16(Fasong1)[0];
            //            Fasong1[12] = DAL_modbus.CRC16(Fasong1)[1];
            //            mycom1.write(Fasong1);
            //            cs++;
            //            Task.Delay(250).Wait();
            //        }
            //    }));

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            byte[] Fasong1 = new byte[13];
            Fasong1[0] = (byte)1;
            Fasong1[1] = (byte)16;
            Fasong1[2] = (byte)0;
            Fasong1[3] = (byte)0;
            Fasong1[4] = (byte)0;
            Fasong1[5] = (byte)2;
            Fasong1[6] = (byte)4;
            Fasong1[7] = (byte)0;
            Fasong1[8] = (byte)0;
            Fasong1[9] = (byte)0;
            Fasong1[10] = (byte)cs;
            Fasong1[11] = DAL_modbus.CRC16(Fasong1)[0];
            Fasong1[12] = DAL_modbus.CRC16(Fasong1)[1];
            mycom1.write(Fasong1);
            cs++;

        }

        bool r1=true;
        private void button6_Click(object sender, EventArgs e)
        {
            r1 = true;
            Task.Run(new Action(() =>
            {
                while(r1)
                {
                    Task.Delay(250).Wait();
                    //Task.Run(new Action(() =>
                    //{
                    //    lock (_lock1)
                    //    {
                    //        byte[] Fasong1 = new byte[8];
                    //        Fasong1[0] = (byte)1;
                    //        Fasong1[1] = (byte)3;
                    //        Fasong1[2] = (byte)0;
                    //        Fasong1[3] = (byte)0;
                    //        Fasong1[4] = (byte)0;
                    //        Fasong1[5] = (byte)8;
                    //        Fasong1[6] = DAL_modbus.CRC16(Fasong1)[0];
                    //        Fasong1[7] = DAL_modbus.CRC16(Fasong1)[1];
                    //        mycom1.write(Fasong1);
                    //    }
                    //}));

                    byte[] Fasong1 = new byte[8];
                    Fasong1[0] = (byte)1;
                    Fasong1[1] = (byte)3;
                    Fasong1[2] = (byte)0;
                    Fasong1[3] = (byte)0;
                    Fasong1[4] = (byte)0;
                    Fasong1[5] = (byte)8;
                    Fasong1[6] = DAL_modbus.CRC16(Fasong1)[0];
                    Fasong1[7] = DAL_modbus.CRC16(Fasong1)[1];
                    mycom1.write(Fasong1);

                }
            }));
           
        }
    }
}
