using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;



//public delegate void ReceiveDataEventHandler(object sender, ReceiveDataEventArg e);

class DAL_modbus
{
    //在需要不断读取，中间有写入的情况下，利用线程锁，写入的时候用异步线程加上锁的方式，读的线程也加锁
    object _lock = new object();
    //添加串口對象
    private SerialPort MyCom;
    //定義接收字節陣列
    byte[] bData = new byte[1024];
    byte mRecv;
    int mrecvCount = 0;
    int mBitlen;

    public DAL_modbus()
    {
        MyCom = new SerialPort();
    }


    /// <summary>
    /// 返回计算机所有的串口号
    /// </summary>
    /// <returns></returns>
    public static string[] GetPortArray()
    {
        return SerialPort.GetPortNames();
    }


    #region  ModBus RTU
   
  
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

            if (MyCom.IsOpen && MyCom != null)
            {
                MyCom.Close();
            }
            
            MyCom.PortName = iPortNo;
            MyCom.BaudRate = iBaudRate;
            MyCom.DataBits = iDataBits;
            switch (iStopBits)
            {
                case 0: MyCom.StopBits = StopBits.None; break;
                case 1: MyCom.StopBits = StopBits.One; break;
                case 2: MyCom.StopBits = StopBits.Two; break;
                default: MyCom.StopBits = StopBits.One; break;
            }
            switch (iParity)
            {
                case "N": MyCom.Parity = Parity.None; break;
                case "O": MyCom.Parity = Parity.Odd; break;
                case "E": MyCom.Parity = Parity.Even; break;
                default: MyCom.Parity = Parity.Even; break;
            }
            //MyCom.ReceivedBytesThreshold = 1;
            //MyCom.DataReceived += new SerialDataReceivedEventHandler(MyCom_DataReceived);
            MyCom.Open();
            return true;
        }
        catch (Exception ex)
        {
            string qq = ex.Message;
            return false;
        }
    }


    /// <summary>
    /// 关闭串口
    /// </summary>
    /// <returns></returns>
    public bool CloseMyCom()
    {
        if (MyCom.IsOpen)
        {
            MyCom.Close();
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// 通过加锁的方式保证串口读写不冲突
    /// </summary>
    /// <param name="data">报文</param>
    public void write(byte[] data)
    {
        Task.Run(new Action(() =>
        {
            lock (_lock)
            {
                try
                {
                    MyCom.Write(data, 0, data.Length);
                    while(MyCom.BytesToRead < 6)
                    {

                    }

                }
                catch
                {

                }
            }
        }));
        //MyCom.Write(data, 0, data.Length);
    }


   

    void MyCom_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        //while (MyCom.BytesToRead > 0)
        //{
        //    mRecv = (byte)MyCom.ReadByte();
        //    bData[mrecvCount] = mRecv;
        //    mrecvCount++;
        //    if (mrecvCount >= 1024)
        //    {
        //        mrecvCount = 0;
        //        MyCom.DiscardInBuffer();
        //        return;
        //    }
        //}
 
    }
    
    
    //CRC校验
    public static byte[] CRC16(byte[] data)
    {
        int len = data.Length - 2;
        if (len > 0)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < len; i++)
            {
                crc = (ushort)(crc ^ (data[i]));
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                }
            }
            byte Hi = (byte)((crc & 0xFF00) >> 8);
            byte Lo = (byte)(crc & 0x00FF);

            return new byte[] { Lo, Hi };
        }
        return new byte[] { 0, 0 };
    }

    //返回的报文
    private byte[] Fanhuibw(int zjs)
    {
        if (bData.Length > 5)
        {
            byte[] bw = new byte[zjs * 2];
            for (int i = 0; i < zjs * 2; i++)
            {
                bw[i] = bData[i + 3];
            }
            return bw;
        }
        else
            return null;
    }

    #endregion

    #region ModBus TCP
    public Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    {
        SendTimeout = 300,
        ReceiveTimeout = 1000
    };

    //打开Socket通讯
    public bool Reconnect(string ip, int Port)
    {
        try
        {
            IPAddress.Parse(ip);
        }
        catch
        {
            return false;
        }
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = 300, ReceiveTimeout = 1000 };
        socket.Connect(IPAddress.Parse(ip), Port);
        return socket.Connected;
    }

    //关闭Socket通讯
    public void DisConnect()
    {
        socket.Disconnect(true);
    }

    #endregion


    
}

