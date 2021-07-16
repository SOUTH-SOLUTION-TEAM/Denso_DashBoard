using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash_Board.CommonClasses
{
  public  class PLC_Connectivity
    {
        EasyModbus.ModbusClient Client = null;
        public delegate void DataArrivalHandler(string Message, string Client);
        public delegate void ScannerStatusChangeHandler(bool flag);
        public event DataArrivalHandler OnDataArrived;
        public event ScannerStatusChangeHandler OnScannerStatusChanged;
        bool Flag = false;
        bool Ping = false;
        public string IP; public int port; public int PLCAddress;

        bool strData = false;

        public virtual void ScannerDataArrived(string Data, string Client)
        {
            if (!ReferenceEquals(this.OnDataArrived, null))
            {
                this.OnDataArrived(Data, Client);
            }
        }
        public virtual void ScannerStatusChanged(bool Data)
        {
            if (!ReferenceEquals(this.OnScannerStatusChanged, null))
            {
                this.OnScannerStatusChanged(Data);
            }
        }
        private void Reconnect()
        {
            try
            {
                if (Client == null)
                    Client = new EasyModbus.ModbusClient();

                Client.Connect(IP, port);
                Flag = true;
                Thread.Sleep(15000);
                ScannerStatusChanged(true);
                Read();
            }
            catch (Exception ex)
            {
                Dispose();
                Reconnect();
            }

        }


        public bool Connect()
        {
            try
            {
                if (Client == null)
                    Client = new EasyModbus.ModbusClient();
                if (Client.Connected == false)
                {
                    //
                    //if(strData)
                    //{
                    Client.Connect(IP, port);
                    Flag = true;
                    ScannerStatusChanged(true);
                    Read();
                    CheckPinging();
                    // }


                }
                else
                    Flag = true;

            }
            catch (Exception ex)
            {
                Flag = false;
                //   Dispose();
                //Connect();
            }
            return Flag;
        }

        public bool CheckPinging()
        {
            try
            {
                Thread th = new Thread(new ThreadStart(delegate
                {
                    Thread.Sleep(4000);
                    while (true)
                    {
                        try
                        {
                            Ping ping = new Ping();
                            PingReply reply = ping.Send(IP);
                            strData = reply.Status == IPStatus.Success;
                            if (strData == false)
                            {
                                Ping = true;
                                ScannerStatusChanged(false);
                            }
                            if (strData == true)
                            {
                                if (Ping == true)
                                {
                                    Dispose();
                                    Reconnect();
                                    Ping = false;
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            ScannerStatusChanged(false);
                        }
                    }

                }));
                th.Start();
            }
            catch (Exception ex)
            {
                strData = false;
                //  Client.Disconnect();
                // Connect();
            }
            return strData;

        }


        public void Read()
        {
            //  string strData = "";
            StringBuilder sr = new StringBuilder();
            bool Data = false;
            try
            {
                Thread th = new Thread(new ThreadStart(delegate
                {
                    while (Flag)
                    {
                        try
                        {

                            bool[] Result = Client.ReadCoils(PLCAddress, 10);
                            for (int i = 0; i < Result.Length; i++)
                            {
                                if (Result[i] == true)
                                {

                                    // ScannerStatusChanged(true, IP);
                                    ScannerDataArrived("1", i.ToString());
                                    Thread.Sleep(600);
                                }
                            }

                            //if (Result[0] == true)
                            //{
                            //    ScannerDataArrived("1", IP);
                            //    Thread.Sleep(600);
                            //}

                        }
                        catch (Exception ex)
                        {
                            // Dispose();
                            //Connect();
                            ScannerDataArrived(ex.Message.ToString(), IP);
                            //throw ex;
                        }

                    }
                }));
                th.Start();
            }
            catch (Exception ex)
            {
                // Dispose();
                //Connect();
                throw ex;
            }
            //return strData;
        }

        public void Write(bool ErrorCode, int Address)
        {
            try
            {
                Client.WriteSingleCoil(Address, ErrorCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            try
            {
                if (Client != null)
                {
                    if (Client.Connected)
                    {
                        Client.Disconnect();
                    }
                }
                ScannerStatusChanged(false);
                Client = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
