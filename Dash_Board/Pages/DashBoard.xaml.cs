using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Dash_Board.Pages
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        public DashBoard()
        {
            InitializeComponent();
        }
       // [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]

        //private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        Business_Layer.Masters obj_Mast = new Business_Layer.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        //BitmapImage ImgSad = null;
        //BitmapImage ImgHalfHappy = null;
        //BitmapImage ImgHappy = null;
        //BitmapImage ImgFullHappy = null;
        //decimal HourQty = 0;
        OR_Monitoring_System objOrm = new OR_Monitoring_System();
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private static readonly int DG_ONE = 0x61;
        private static readonly int DG_TWO = 0x32;
        private static readonly int DG_Three = 0x33;//This is the print-screen key.
        private static readonly int DG_Minus = 0xBD;
        private static readonly int DG_Plus = 0xBD;
        private static readonly int DG_Star = 0xBD;
        bool Flag = false;
        Thread th = null;
        Thread th1 = null;
        #endregion
        private void Start()
        {
            try
            {
                th = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        Thread.Sleep(1000);
                        if (Dash_Board.CommonClasses.CommonVariable.ModelName != "")
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                try
                                {
                                    if (CommonClasses.CommonVariable.MachineName != "")
                                        txtHeader.Text = CommonClasses.CommonVariable.MachineName;
                                    else
                                        txtHeader.Text = "DASH BOARD";

                                    if (Ggrid3.IsVisible == false)
                                    {
                                        if (CommonClasses.CommonVariable.PblCOunt == "1")
                                        {
                                            Ggrid4.Visibility = Visibility.Visible;
                                            //CommonClasses.CommonVariable.PblCOunt = "0";
                                        }
                                        else
                                            Ggrid4.Visibility = Visibility.Hidden;
                                    }
                                    else
                                    {
                                        CommonClasses.CommonVariable.PblCOunt = "0";
                                        Ggrid4.Visibility = Visibility.Hidden;
                                    }
                                    string Time = CommonClasses.CommonVariable.Current_Time;

                                    if (Time == "06:30" || Time == "15:00" || Time == "23:30")
                                    {
                                        Process.Start("shutdown", "/r /t 0");
                                    }


                                    //GC.Collect();
                                    //GC.WaitForPendingFinalizers();

                                    //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                                    //{
                                    //    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                                    //}

                                    //    Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 10 });

                                    Transaction("DashBoard");
                                    Transaction("MachineONandOFF");
                                    Transaction("ManPower");
                                    //                     Transaction("GetShiftDetails", "ORM DETAILS");
                                    //Transaction("OrMonitoring", "ORM DETAILS");

                                    
                                }
                                catch (Exception ex)
                                {
                                    CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                                }
                                finally
                                {
                                    //GC.Collect();
                                    //GC.WaitForPendingFinalizers();
                                    //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                                    //{
                                    //    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                                    //}
                                    //Thread.Sleep(1000);
                                }
                            });
                        }
                    }
                }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "DashBoard")
            {
                Dash_Board.Entity_Layer.Transaction.Type = Type;
                Dash_Board.Entity_Layer.Transaction.Time = CommonClasses.CommonVariable.Time;
                Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                Dash_Board.Entity_Layer.Transaction.NoofItem = Dash_Board.CommonClasses.CommonVariable.NoofItems;
                Dash_Board.Entity_Layer.Transaction.Puls = Dash_Board.CommonClasses.CommonVariable.Puls;
                Dash_Board.Entity_Layer.Transaction.Cycletime = CommonClasses.CommonVariable.CycleTime;
                DataSet dt = obj_Tran.BL_DashBoard();

                CommonClasses.CommonVariable.dtORMDeatile = new DataSet();
                CommonClasses.CommonVariable.dtDekidaka = new DataTable();
                CommonClasses.CommonVariable.dtCycleTimeFlectuation = new DataSet();
                CommonClasses.CommonVariable.dtAndon = new DataTable();
                CommonClasses.CommonVariable.dtKanbanProgress = new DataSet();
                CommonClasses.CommonVariable.dtMachineONOFF = new DataTable();
                CommonClasses.CommonVariable.dtProblemCode = new DataTable();
                CommonClasses.CommonVariable.dtStockVisual = new DataTable();
                CommonClasses.CommonVariable.dtShiftDetails = new DataTable();
                CommonClasses.CommonVariable.dtManPower = new DataTable();

                CommonClasses.CommonVariable.dtShiftDetails = dt.Tables[0];

                DataTable dt1 = dt.Tables[1];
                CommonClasses.CommonVariable.dtORMDeatile.Tables.Add(dt.Tables[1].Copy());
                CommonClasses.CommonVariable.dtORMDeatile.Tables.Add(dt.Tables[2].Copy());
                CommonClasses.CommonVariable.dtDekidaka = dt.Tables[3];

                CommonClasses.CommonVariable.dtAndon = dt.Tables[4];

                CommonClasses.CommonVariable.dtCycleTimeFlectuation.Tables.Add(dt.Tables[5].Copy());
                CommonClasses.CommonVariable.dtCycleTimeFlectuation.Tables.Add(dt.Tables[6].Copy());
                CommonClasses.CommonVariable.dtCycleTimeFlectuation.Tables.Add(dt.Tables[7].Copy());

                CommonClasses.CommonVariable.dtProblemCode = dt.Tables[8];
                CommonClasses.CommonVariable.dtMachineONOFF = dt.Tables[9];
                CommonClasses.CommonVariable.dtManPower = dt.Tables[10];
                CommonClasses.CommonVariable.dt5Mand1E = dt.Tables[11];
                CommonClasses.CommonVariable.dtStockVisual = dt.Tables[12];
                CommonClasses.CommonVariable.dtKanbanProgress.Tables.Add(dt.Tables[13].Copy());
                CommonClasses.CommonVariable.dtKanbanProgress.Tables.Add(dt.Tables[14].Copy());
                CommonClasses.CommonVariable.dtDekidakaTime = dt.Tables[15];
            }
            if (Type == "MachineONandOFF")
            {
                bool Flag = false;
                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.Time = CommonClasses.CommonVariable.Time;
                //Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                //  DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                DataTable dt = CommonClasses.CommonVariable.dtMachineONOFF;
                if (dt.Rows.Count > 0)
                {
                    if (TimeSpan.Parse(dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[0].ToString()) <= TimeSpan.Parse(dt.Rows[0]["CurrentTime"].ToString())
                                              && TimeSpan.Parse(dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[1].ToString()) > TimeSpan.Parse(dt.Rows[0]["CurrentTime"].ToString()))
                    {
                        Flag = true;

                    }
                }
                if (Flag == true)
                {
                    CommonClasses.CommonVariable.Current_Time = dt.Rows[0]["CurrentTime"].ToString().ToUpper();
                    CommonClasses.CommonVariable.MachinePlane = dt.Rows[0]["Status"].ToString().ToUpper();
                    CommonClasses.CommonVariable.Seconds = Convert.ToInt32(dt.Rows[0]["TimeDiff"].ToString());


                    // CommonClasses.CommonVariable.MachineStatus = "LINE UNDER" + dt.Rows[0]["Status"].ToString().ToUpper() + " FROM " + dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[0] + " TO " + dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[1];
                    CommonClasses.CommonVariable.MachineStatus = dt.Rows[0]["Status"].ToString().ToUpper() + " TILL " + dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[1];

                    Ggrid2.Visibility = Visibility.Visible;
                    Ggrid1.Visibility = Visibility.Hidden;
                    Ggrid4.Visibility = Visibility.Hidden;

                }
                else
                {
                    Dash_Board.Entity_Layer.Transaction.Type = "MachineONandOFFUpdate";
                    DataTable dt1 = obj_Tran.BL_DashBoard().Tables[0];
                    Ggrid2.Visibility = Visibility.Hidden;
                    Ggrid1.Visibility = Visibility.Visible;
                }
            }
            if (Type == "ManPower")
            {
                bool Flag = false;
                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                //Dash_Board.Entity_Layer.Transaction.Manpower = "";

                //DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                DataTable dt = CommonClasses.CommonVariable.dtManPower;
                if (dt.Rows.Count > 0)
                {
                    Ggrid3.Visibility = Visibility.Hidden;
                    // CommonClasses.CommonVariable.PblCOunt = "1";
                }
                else
                {
                    Ggrid3.Visibility = Visibility.Visible;
                    // CommonClasses.CommonVariable.PblCOunt = "0";
                }
            }
            if (Type == "GetModuleName")
            {
                Dash_Board.Entity_Layer.Masters.Type = "GetModuleName";
                Dash_Board.Entity_Layer.Masters.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                Dash_Board.Entity_Layer.Masters.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Dash_Board.Entity_Layer.Transaction.Type = Type;
                    CommonClasses.CommonVariable.CycleTime = dt.Rows[0]["CycleTime"].ToString();
                    CommonClasses.CommonVariable.ModelName = dt.Rows[0]["ModelName"].ToString();
                    CommonClasses.CommonVariable.Puls = dt.Rows[0]["Puls"].ToString();
                    CommonClasses.CommonVariable.NoofItems = dt.Rows[0]["NoofItems"].ToString();
                    CommonClasses.CommonVariable.ChangeOver = dt.Rows[0]["ChangeOver"].ToString();

                    if (CommonClasses.CommonVariable.DashBoard == null)
                    {
                        this.Close();
                        CommonClasses.CommonVariable.ModuleChange = "DashBoard";
                        CommonClasses.CommonVariable.DashBoard = new Pages.DashBoard();
                        CommonClasses.CommonVariable.MachineSelection.Close();
                        CommonClasses.CommonVariable.MachineSelection = null;
                        CommonClasses.CommonVariable.DashBoard.ShowDialog();
                    }
                }
            }
        }
        private void ShowDateTime()
        {


            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 50);
            dispatcherTimer.Start();

            dispatcherTimer1.Tick += new EventHandler(dispatcherTimer1_Tick);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer1.Start();
            //Start1();

        }
        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", Dash_Board.CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
            }

        }
        private void Start1()
        {
            try
            {
                th1 = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        Thread.Sleep(1);
                        this.Dispatcher.Invoke(() =>
                    {

                        short keyState1, keyState2, keyState3, keyState4;
                        bool DGOne = false, DGTwo = false, DGThree = false, DGMinus = false;
                        keyState1 = GetAsyncKeyState(DG_ONE);
                        keyState2 = GetAsyncKeyState(DG_TWO);
                        keyState3 = GetAsyncKeyState(DG_Three);
                        keyState4 = GetAsyncKeyState(DG_Minus);
                        //Check if the MSB is set. If so, then the key is pressed.
                        DGOne = ((keyState1 >> 97) & 0x0001) == 0x0001;
                        DGTwo = ((keyState2 >> 50) & 0x0001) == 0x0001;
                        DGThree = ((keyState3 >> 51) & 0x0001) == 0x0001;
                        DGMinus = ((keyState4 >> 189) & 0x0001) == 0x0001;
                        if (DGMinus)
                        {
                            if (CommonClasses.CommonVariable.ModuleChange != "MenuSelection")
                            {
                                if (CommonClasses.CommonVariable.MenuSelection == null)
                                {
                                    CommonClasses.CommonVariable.ModuleChange = "MenuSelection";
                                    CommonClasses.CommonVariable.MenuSelection = new Pages.MenuSelection();
                                    CommonClasses.CommonVariable.DashBoard.Close();
                                    CommonClasses.CommonVariable.DashBoard = null;
                                    CommonClasses.CommonVariable.MenuSelection.ShowDialog();
                                }
                            }
                        }
                        if (System.Windows.Input.Keyboard.IsKeyDown(Key.Delete))
                        {
                            if (Flag == false)
                            {
                                Dash_Board.Entity_Layer.Transaction.Type = "Cancel Off";
                                Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                                Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                                Dash_Board.Entity_Layer.Transaction.Shiftname = Dash_Board.CommonClasses.CommonVariable.ShiftName;
                                string Result = obj_Tran.BL_MachineOnandOffTransaction();
                                Flag = true;
                            }
                            else
                                Flag = false;
                        }
                        if (System.Windows.Input.Keyboard.IsKeyDown(Key.Multiply))
                        {
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                        }
                    });

                    }
                }));

                th1.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CommonClasses.CommonVariable.dtMachineSelection.Rows.Count > 1)
                {
                    for (int i = 0; i < CommonClasses.CommonVariable.dtMachineSelection.Rows.Count; i++)
                    {
                        if (CommonClasses.CommonVariable.MachineName != CommonClasses.CommonVariable.dtMachineSelection.Rows[i]["machineName"].ToString())
                        {
                            Dash_Board.CommonClasses.CommonVariable.MachineGroup = CommonClasses.CommonVariable.dtMachineSelection.Rows[i]["MachineGroup"].ToString();
                            Dash_Board.CommonClasses.CommonVariable.MachineName = CommonClasses.CommonVariable.dtMachineSelection.Rows[i]["machineName"].ToString();
                            Transaction("GetModuleName");

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", Dash_Board.CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
            }

        }
        private void dispatcherTimer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (frm5M.IsVisible == true)
                {
                    frm5M.Visibility = Visibility.Hidden;
                    frmStock.Visibility = Visibility.Visible;
                }
                else if (frm5M.IsVisible == false)
                {
                    frm5M.Visibility = Visibility.Visible;
                    frmStock.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", Dash_Board.CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                Start();
                // Start1();
                Version Version = Assembly.GetExecutingAssembly().GetName().Version;
                txtVersion.Text = "Version " + Version.Major + "." + Version.Minor + "." + Version.Build + "." + Version.Revision;
                KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
                //Dash_Board.Pages.AndonSMSandCallForSmallManitor obj_AndonSMSandCallForSmallManitor = new Pages.AndonSMSandCallForSmallManitor();

                //obj_AndonSMSandCallForSmallManitor.Show();

                //ImgSad = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                //ImgHalfHappy = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
                //ImgHappy = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));

                //ImgFullHappy = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
                //objOrm = new OR_Monitoring_System();

                //objOrm.txtModelName.Text = "00000000000";

                //string filePath = @"D:\Projects\Denso_ORM\Auto_Start_Application\\DENSO_ORM.DENSO_ORM.apk";
                //File.Copy(@"\\192.168.43.6\FTP\DENSO_ORM.DENSO_ORM.apk", filePath);
                //WebClient webClient = new WebClient();
                //   webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Completed);
                // string pathToNewFile = Path.Combine("", Path.GetFileName(@"\\192.168.43.6\Debug\Dash_Board.exe"));
                //webClient.DownloadFileAsync(new Uri(@"\\192.168.43.6\Debug\DENSO_ORM.DENSO_ORM.apk"), filePath);

                //using (var ftpStream = webClient.OpenRead(@"\\192.168.43.6\Debug\DENSO_ORM.DENSO_ORM.apk"));

                //{
                //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //       // ftpStream.CopyTo(fileStream);
                //    }
                //}
                // WebClient MyWebClient = new WebClient();
                //byte[] BytesFile = MyWebClient.DownloadData(@"\\192.168.43.6\Debug\DENSO_ORM.DENSO_ORM.apk");

                //MemoryStream iStream = new MemoryStream(BytesFile);
                //FileStream str = new FileStream(filePath, FileMode.Append);
                //iStream.CopyTo(str);
                //WebClient wc = new WebClient();
                //using (MemoryStream stream = new MemoryStream(wc.DownloadData(@"\\192.168.43.6\Debug\DENSO_ORM.DENSO_ORM.apk")))
                //{
                //    FileStream str = new FileStream(filePath, FileMode.Append);
                //    stream.CopyToAsync(str);
                //}
                // ResponseTask.Wait(WebCommsTimeout);
                // return iStream;

            }
            catch (Exception ex)
            {
                //    obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", Dash_Board.CommonClasses.CommonVariable.UserID);
                //  CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
            }
        }
        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            try
            {
                KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
                if (eventArgs.KeyData.ToString() == "Multiply" || eventArgs.KeyData.ToString() == "OemMultiply")
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                if (eventArgs.KeyData.ToString() == "Subtract" || eventArgs.KeyData.ToString() == "OemMinus")
                {
                    if (CommonClasses.CommonVariable.ModuleChange != "MenuSelection")
                    {
                        if (CommonClasses.CommonVariable.MenuSelection == null)
                        {
                            CommonClasses.CommonVariable.ModuleChange = "MenuSelection";
                            CommonClasses.CommonVariable.MenuSelection = new Pages.MenuSelection();
                            CommonClasses.CommonVariable.DashBoard.Close();
                            CommonClasses.CommonVariable.DashBoard = null;
                            CommonClasses.CommonVariable.MenuSelection.ShowDialog();
                        }
                    }
                }
                if (eventArgs.KeyData.ToString() == "Delete")
                {
                    if (Flag == false)
                    {
                        Dash_Board.Entity_Layer.Transaction.Type = "Cancel Off";
                        Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                        Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                        Dash_Board.Entity_Layer.Transaction.Shiftname = Dash_Board.CommonClasses.CommonVariable.ShiftName;
                        string Result = obj_Tran.BL_MachineOnandOffTransaction();
                        Flag = true;
                    }
                    else
                        Flag = false;
                }
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);
            if (th != null)
                th.Suspend();
            //th1.Suspend();
            dispatcherTimer.Stop();
            dispatcherTimer1.Stop();
            dispatcherTimer2.Stop();
            this.Close();

            // th.Suspend();
        }
      
        //private void Transaction(string Type, string Operation)
        //{
        //    if (Operation == "ORM DETAILS")
        //    {
        //        if (Type == "GetShiftDetails")
        //        {
        //            //Dash_Board.Entity_Layer.Transaction.Type = Type;
        //            //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
        //            //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
        //            //DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
        //            DataTable dt = CommonClasses.CommonVariable.dtShiftDetails;
        //            CommonClasses.CommonVariable.ShiftName = dt.Rows[0]["ShiftName"].ToString();
        //            CommonClasses.CommonVariable.Current_Time = dt.Rows[0]["CurrentTime"].ToString();
        //            CommonClasses.CommonVariable.Time = dt.Rows[0]["Time"].ToString().ToString();
        //            CommonClasses.CommonVariable.Break = dt.Rows[0]["Break"].ToString().ToString();
        //            objOrm.txtAShift.Text = "Cumulative for Shift " + dt.Rows[0]["TimeWorking"].ToString().Split(',')[0].ToString() + " - " + dt.Rows[0]["CurrentTime"].ToString();
        //            objOrm.txtTime.Text = dt.Rows[0]["CurrentTime"].ToString();
        //            objOrm.txtATimeInterval.Text = "Current Hour " + CommonClasses.CommonVariable.Time + " - " + dt.Rows[0]["CurrentTime"].ToString();
        //            if (CommonClasses.CommonVariable.Break == "Break")
        //            {
        //                objOrm.txtTime.Background = (Brush)new BrushConverter().ConvertFrom("#FF0E67E8");
        //            }
        //            else
        //            {
        //                objOrm.txtTime.Background = (Brush)new BrushConverter().ConvertFrom("#FF20F30B");
        //            }
        //        }
        //        if (Type == "OrMonitoring")
        //        {

        //            //Dash_Board.Entity_Layer.Transaction.Type = Type;
        //            //Dash_Board.Entity_Layer.Transaction.Time = CommonClasses.CommonVariable.Time;
        //            //Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
        //            //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
        //            //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
        //            //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
        //            //Dash_Board.Entity_Layer.Transaction.NoofItem = Dash_Board.CommonClasses.CommonVariable.NoofItems;
        //            //Dash_Board.Entity_Layer.Transaction.Puls = Dash_Board.CommonClasses.CommonVariable.Puls;
        //            //Dash_Board.Entity_Layer.Transaction.Cycletime = CommonClasses.CommonVariable.CycleTime;

        //            //DataSet dt = obj_Tran.BL_DashBoard();
        //            DataSet dt = CommonClasses.CommonVariable.dtORMDeatile;
        //            objOrm.imgSmily1.Source = null;
        //            if (dt.Tables[1].Rows.Count > 0)
        //            {
        //                objOrm.txtActToatl.Text = "A " + dt.Tables[1].Rows[0]["TotalQty"].ToString();
        //                objOrm.txtTotaPrd.Text = dt.Tables[1].Rows[0]["TotalPers"].ToString() + "%";
        //                objOrm.txtPrdToatl.Text = "P " + (dt.Tables[1].Rows[0]["TotalPlan"].ToString());
        //                if (objOrm.txtPrdToatl.Text == "P 0")
        //                    objOrm.txtPPlan.Text = "P 0";

        //                if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) <= 50)
        //                {
        //                    // BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Red;
        //                    objOrm.imgSmily1.Source = ImgSad;
        //                }
        //                else if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) > 50 && Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) <= 83)
        //                {
        //                    // BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.DarkOrange;
        //                    objOrm.imgSmily1.Source = ImgHalfHappy;
        //                }
        //                else if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) > 83 && Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) <= 100)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Green;
        //                    objOrm.imgSmily1.Source = ImgHappy;
        //                }
        //                else if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) > 100)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Green;
        //                    objOrm.imgSmily1.Source = ImgFullHappy;
        //                }
        //            }
        //            else
        //            {
        //                objOrm.txtActToatl.Text = "A 0";
        //                objOrm.txtTotaPrd.Text = "0.0%";
        //                if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) == 0.0)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Red;
        //                    objOrm.imgSmily1.Source = ImgSad;

        //                }
        //            }
        //            if (dt.Tables[0].Rows.Count > 0)
        //            {
        //                objOrm.txtAPlan.Text = "A " + (dt.Tables[0].Rows[0]["HourPlan"].ToString());
        //                objOrm.txtPPlan.Text = "P " + (dt.Tables[0].Rows[0]["ProductionPlan"].ToString());
        //                objOrm.txtPrdToatl.Text = "P " + (dt.Tables[0].Rows[0]["TotalPlan"].ToString());
        //                objOrm.txtPrdQty.Text = (dt.Tables[0].Rows[0]["HourPers"].ToString() + "%");
        //                objOrm.txtActToatl.Text = "A " + dt.Tables[0].Rows[0]["TotalQty"].ToString();
        //                objOrm.txtTotaPrd.Text = dt.Tables[0].Rows[0]["TotalPers"].ToString() + "%";
        //                HourQty = Convert.ToDecimal(dt.Tables[0].Rows[0]["HourPlan"].ToString());
        //                objOrm.slColorG.Value = Convert.ToDouble(objOrm.txtPrdQty.Text.Replace("%", ""));

        //                if (objOrm.slColorG.Value <= 50)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
        //                    objOrm.txtPrdQty.Foreground = Brushes.Red;
        //                    objOrm.imgSmily2.Source = ImgSad;
        //                }
        //                else if (objOrm.slColorG.Value > 50 && objOrm.slColorG.Value <= 83)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
        //                    objOrm.txtPrdQty.Foreground = Brushes.DarkOrange;
        //                    objOrm.imgSmily2.Source = ImgHalfHappy;
        //                }
        //                else if (objOrm.slColorG.Value > 83 && objOrm.slColorG.Value <= 100)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));
        //                    objOrm.txtPrdQty.Foreground = Brushes.Green;
        //                    objOrm.imgSmily2.Source = ImgHappy;
        //                }
        //                else if (objOrm.slColorG.Value > 100)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
        //                    objOrm.txtPrdQty.Foreground = Brushes.Green;
        //                    objOrm.imgSmily2.Source = ImgFullHappy;
        //                }

        //                if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) <= 50)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Red;
        //                    objOrm.imgSmily1.Source = ImgSad;
        //                }
        //                else if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) > 50 && Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) <= 83)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.DarkOrange;
        //                    objOrm.imgSmily1.Source = ImgHalfHappy;
        //                }
        //                else if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) > 83 && Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) <= 100)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Green;
        //                    objOrm.imgSmily1.Source = ImgHappy;
        //                }
        //                else if (Convert.ToDouble(objOrm.txtTotaPrd.Text.Split('%')[0]) > 100)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
        //                    objOrm.txtTotaPrd.Foreground = Brushes.Green;
        //                    objOrm.imgSmily1.Source = ImgFullHappy;
        //                }
        //                else
        //                {
        //                    objOrm.txtPrdQty.Text = "0.0%";
        //                    objOrm.txtPPlan.Text = "P 0";
        //                    objOrm.slColorG.Value = Convert.ToDouble(0);
        //                    if (objOrm.slColorG.Value == 0)
        //                    {
        //                        //  BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
        //                        objOrm.txtPrdQty.Foreground = Brushes.Red;
        //                        objOrm.imgSmily2.Source = ImgSad;
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                objOrm.txtAPlan.Text = "A 0";
        //                objOrm.txtPrdQty.Text = "0.0%";
        //                objOrm.slColorG.Value = Convert.ToDouble(0);
        //                if (objOrm.slColorG.Value == 0)
        //                {
        //                    //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
        //                    objOrm.txtPrdQty.Foreground = Brushes.Red;
        //                    objOrm.imgSmily2.Source = ImgSad;
        //                }

        //            }

        //        }
        //    }
       // }
    }
}
