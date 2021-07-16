using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dash_Board.Pages
{
    /// <summary>
    /// Interaction logic for OR_Monitoring_System.xaml
    /// </summary>
    public partial class OR_Monitoring_System : Page
    {
        public OR_Monitoring_System()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
       // BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        Thread th = null;
        string ShiftName="",Time="";
        bool Flag = false;
        decimal TotalQty = 0;
        int RefNo = 0,Port = 0; int PlcAddress; string IPAddress;  double CycleTime; int PblCOunt = 0;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        Probem_Code obj_prd = new Probem_Code();
        BitmapImage ImgSad = null;
        BitmapImage ImgHalfHappy = null;
        BitmapImage ImgHappy = null;
        BitmapImage ImgFullHappy = null;
        decimal HourQty = 0;


        #endregion

        //private void ShowDateTime()
        //{
        //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        //    dispatcherTimer.Start();
        //}
        private void Start()
        {
            try
            {
                th = new Thread(new ThreadStart(delegate
               {
                   while (true)
                   {
                       Thread.Sleep(1500);
                       if (Dash_Board.CommonClasses.CommonVariable.ModelName != "")
                       {
                           this.Dispatcher.Invoke(() =>
                           {
                               try
                               {
                                   
                                   txtModelName.Text = Dash_Board.CommonClasses.CommonVariable.ModelName + " - " + Dash_Board.CommonClasses.CommonVariable.CycleTime + " S";
                                   CycleTime = Convert.ToDouble(Dash_Board.CommonClasses.CommonVariable.CycleTime);
                                   Transaction("GetShiftDetails");
                                   Transaction("OrMonitoring");
                               }
                               catch (Exception ex)
                               {
                                   CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);

                               }
                           });
                       }
                   }
               }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);

            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //dispatcherTimer.Stop();
            th.Suspend();
        }

        private void Transaction(string Type)
        {
            if (Type == "GetShiftDetails")
            {
                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                DataTable dt = CommonClasses.CommonVariable.dtShiftDetails;
                CommonClasses.CommonVariable.ShiftName = dt.Rows[0]["ShiftName"].ToString();
                CommonClasses.CommonVariable.Current_Time = dt.Rows[0]["CurrentTime"].ToString();
                CommonClasses.CommonVariable.Time = dt.Rows[0]["Time"].ToString().ToString();
                CommonClasses.CommonVariable.Break = dt.Rows[0]["Break"].ToString().ToString();
                txtAShift.Text = "Cumulative for Shift " + dt.Rows[0]["TimeWorking"].ToString().Split(',')[0].ToString() + " - " + dt.Rows[0]["CurrentTime"].ToString();
                txtTime.Text = dt.Rows[0]["CurrentTime"].ToString();
                txtATimeInterval.Text = "Current Hour " + CommonClasses.CommonVariable.Time + " - " + dt.Rows[0]["CurrentTime"].ToString();
                if (CommonClasses.CommonVariable.Break == "Break")
                {
                    txtTime.Background = (Brush)new BrushConverter().ConvertFrom("#FF0E67E8");
                }
                else
                {
                    txtTime.Background = (Brush)new BrushConverter().ConvertFrom("#FF20F30B");
                }
            }
            if (Type == "OrMonitoring")
            {
               
                    //Dash_Board.Entity_Layer.Transaction.Type = Type;
                    //Dash_Board.Entity_Layer.Transaction.Time = CommonClasses.CommonVariable.Time;
                    //Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                    //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                    //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                    //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                    //Dash_Board.Entity_Layer.Transaction.NoofItem = Dash_Board.CommonClasses.CommonVariable.NoofItems;
                    //Dash_Board.Entity_Layer.Transaction.Puls = Dash_Board.CommonClasses.CommonVariable.Puls;
                    //Dash_Board.Entity_Layer.Transaction.Cycletime = CommonClasses.CommonVariable.CycleTime;

                    //DataSet dt = obj_Tran.BL_DashBoard();
                DataSet dt = CommonClasses.CommonVariable.dtORMDeatile;
                imgSmily1.Source = null;
                if (dt.Tables[1].Rows.Count > 0)
                    {
                        txtActToatl.Text = "A " + dt.Tables[1].Rows[0]["TotalQty"].ToString();
                        txtTotaPrd.Text = dt.Tables[1].Rows[0]["TotalPers"].ToString() + "%";
                        txtPrdToatl.Text = "P " + (dt.Tables[1].Rows[0]["TotalPlan"].ToString());
                        if (txtPrdToatl.Text == "P 0")
                        txtPPlan.Text = "P 0";
                   
                    if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 50)
                        {
                           // BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                            txtTotaPrd.Foreground = Brushes.Red;
                            imgSmily1.Source = ImgSad;
                        }
                        else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 50 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 83)
                        {
                           // BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
                            txtTotaPrd.Foreground = Brushes.DarkOrange;
                            imgSmily1.Source = ImgHalfHappy;
                        }
                        else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 83 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 100)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));
                            txtTotaPrd.Foreground = Brushes.Green;
                            imgSmily1.Source = ImgHappy;
                        }
                        else if(Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 100)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
                            txtTotaPrd.Foreground = Brushes.Green;
                            imgSmily1.Source = ImgFullHappy;
                        }
                    }
                    else
                    {
                        txtActToatl.Text = "A 0";
                        txtTotaPrd.Text = "0.0%";
                        if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) == 0.0)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                            txtTotaPrd.Foreground = Brushes.Red;
                            imgSmily1.Source = ImgSad;

                    }
                    }
                if (dt.Tables[0].Rows.Count > 0)
                {
                    txtAPlan.Text = "A " + (dt.Tables[0].Rows[0]["HourPlan"].ToString());
                    txtPPlan.Text = "P " + (dt.Tables[0].Rows[0]["ProductionPlan"].ToString());
                    txtPrdToatl.Text = "P " + (dt.Tables[0].Rows[0]["TotalPlan"].ToString());
                    txtPrdQty.Text = (dt.Tables[0].Rows[0]["HourPers"].ToString() + "%");
                    txtActToatl.Text = "A " + dt.Tables[0].Rows[0]["TotalQty"].ToString();
                    txtTotaPrd.Text = dt.Tables[0].Rows[0]["TotalPers"].ToString() + "%";
                    HourQty = Convert.ToDecimal(dt.Tables[0].Rows[0]["HourPlan"].ToString());
                    slColorG.Value = Convert.ToDouble(txtPrdQty.Text.Replace("%", ""));
                   
                        if (slColorG.Value <= 50)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                            txtPrdQty.Foreground = Brushes.Red;
                            imgSmily2.Source = ImgSad;
                        }
                        else if (slColorG.Value > 50 && slColorG.Value <= 83)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
                            txtPrdQty.Foreground = Brushes.DarkOrange;
                            imgSmily2.Source = ImgHalfHappy;
                        }
                        else if (slColorG.Value > 83 && slColorG.Value <= 100)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));
                            txtPrdQty.Foreground = Brushes.Green;
                            imgSmily2.Source = ImgHappy;
                        }
                        else if (slColorG.Value > 100)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
                            txtPrdQty.Foreground = Brushes.Green;
                            imgSmily2.Source = ImgFullHappy;
                        }

                        if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 50)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                            txtTotaPrd.Foreground = Brushes.Red;
                            imgSmily1.Source = ImgSad;
                        }
                        else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 50 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 83)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
                            txtTotaPrd.Foreground = Brushes.DarkOrange;
                            imgSmily1.Source = ImgHalfHappy;
                        }
                        else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 83 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 100)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));
                            txtTotaPrd.Foreground = Brushes.Green;
                            imgSmily1.Source = ImgHappy;
                        }
                        else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 100)
                        {
                            //BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));
                            txtTotaPrd.Foreground = Brushes.Green;
                            imgSmily1.Source = ImgFullHappy;
                        }
                        else
                        {
                            txtPrdQty.Text = "0.0%";
                            txtPPlan.Text = "P 0";
                            slColorG.Value = Convert.ToDouble(0);
                            if (slColorG.Value == 0)
                            {
                              //  BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                                txtPrdQty.Foreground = Brushes.Red;
                                imgSmily2.Source = ImgSad;
                            }

                        }
                }
                else
                {
                    txtAPlan.Text = "A 0";
                    txtPrdQty.Text = "0.0%";
                    slColorG.Value = Convert.ToDouble(0);
                    if (slColorG.Value == 0)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                        txtPrdQty.Foreground = Brushes.Red;
                        imgSmily2.Source = ImgTest;
                    }

                }
                    
            }
            //if(Type== "ModelUpdate")
            //{
            //    Dash_Board.Entity_Layer.Transaction.Type = Type;
            //    Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
            //    Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
            //    Dash_Board.Entity_Layer.Transaction.Shiftname = Dash_Board.CommonClasses.CommonVariable.ShiftName;
            //    Dash_Board.Entity_Layer.Transaction.Time = Dash_Board.CommonClasses.CommonVariable.Time;
            //    Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
            //    Dash_Board.Entity_Layer.Transaction.Cycletime = Dash_Board.CommonClasses.CommonVariable.CycleTime;
            //    Dash_Board.Entity_Layer.Transaction.Puls = Dash_Board.CommonClasses.CommonVariable.Puls;
            //    Dash_Board.Entity_Layer.Transaction.NoofItem = Dash_Board.CommonClasses.CommonVariable.NoofItems;
            //    Dash_Board.Entity_Layer.Transaction.Changesover = Dash_Board.CommonClasses.CommonVariable.ChangeOver;
            //    DataTable dt1 = obj_Tran.BL_DashBoard().Tables[0];
            //    if (dt1.Rows.Count > 0)
            //        Dash_Board.CommonClasses.CommonVariable.ChangeOver = dt1.Rows[0][1].ToString();
            //}
        }

        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //txtTime.Text = System.DateTime.Now.ToString("HH:mm");
        //        if (Dash_Board.CommonClasses.CommonVariable.ModelName != "")
        //        {
        //            txtModelName.Text = Dash_Board.CommonClasses.CommonVariable.ModelName + " - " + Dash_Board.CommonClasses.CommonVariable.CycleTime + " S";
        //            CycleTime = Convert.ToDouble(Dash_Board.CommonClasses.CommonVariable.CycleTime);
        //            Transaction("GetShiftDetails");
        //            Transaction("OrMonitoring");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", Dash_Board.CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
        //        //                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //ShowDateTime();
                Start();
                ImgSad = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/SadSmily.png"));
                ImgHalfHappy = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/HallfHappy.jpg"));
                ImgHappy = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/Happy.jpg"));

                ImgFullHappy = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/Dash_Board;component/Images/FullHappy.jpg"));

            }
            catch (Exception ex)
            {
                //   obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", Dash_Board.CommonClasses.CommonVariable.UserID);
                // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
            }
        }
     
    }
}