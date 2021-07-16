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
    /// Interaction logic for AndonSMSandCall.xaml
    /// </summary>
    public partial class AndonSMSandCall : Page
    {
        public AndonSMSandCall()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Business_Layer.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        // Dash_Board.Business_Layer.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        int RefNo = 0;
        Thread th = null;
        #endregion

        //private void ShowDateTime()
        //{
        //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        //    dispatcherTimer.Start();
        //}

        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Transaction("AndonCall");

        //    }
        //    catch (Exception ex)
        //    {
        //        //  obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", Dash_Board.CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", CommonClasses.CommonVariable.UserID);
        //        //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
        //    }
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
                                   Transaction("AndonCall");
                               }
                               catch (Exception ex)
                               {
                                   CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", CommonClasses.CommonVariable.UserID);
                               }
                           });
                       }
                   }
               }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", CommonClasses.CommonVariable.UserID);
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "AndonCall")
            {

                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                DataTable dt = CommonClasses.CommonVariable.dtAndon;
                brd1.Background = Brushes.White;
                brd2.Background = Brushes.White;
                brd3.Background = Brushes.White;
                brd4.Background = Brushes.White;
                brd5.Background = Brushes.White;
                brd6.Background = Brushes.White;
                brd7.Background = Brushes.White;
                brd8.Background = Brushes.White;
                brd9.Background = Brushes.White;
                Station1.Text = "";
                Station2.Text = "";
                Station3.Text = "";
                Line1.Text = "";
                Line2.Text = "";
                Quality.Text = "";
                DataTable dt2 = new DataTable();
                DataView dv = new DataView(dt);
                DataTable dt1= dv.ToTable(true, "Station");
                DataView dv1 = new DataView(dt);
                if (dt1.Rows.Count >= 1)
                {
                    dv1.RowFilter = "station='"+dt1.Rows[0][0]+"'";
                    dt2 = dv1.ToTable();

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["andonType"].ToString() == "QUALITY")
                        {
                            Station1.Text = dt2.Rows[i]["Station"].ToString();
                            Line1.Text = CommonClasses.CommonVariable.MachineName;

                            if (brd7.Background == Brushes.White)
                                brd7.Background = Brushes.DeepPink;
                        }

                        if (dt2.Rows[i]["andonType"].ToString() == "MAINTENANCE")
                        {
                            Station1.Text = dt2.Rows[i]["Station"].ToString();
                            Line1.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd1.Background == Brushes.White)
                                brd1.Background = Brushes.Red;
                        }
                        if (dt2.Rows[i]["andonType"].ToString() == "PART FEADER")
                        {
                            Station1.Text = dt2.Rows[i]["Station"].ToString();
                            Line1.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd4.Background == Brushes.White)
                                brd4.Background = Brushes.Yellow;

                        }
                    }
                }
                if (dt1.Rows.Count >= 2)
                {
                    dv1.RowFilter = "station='" + dt1.Rows[1][0] + "'";
                    dt2 = dv1.ToTable();

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["andonType"].ToString() == "QUALITY")
                        {
                            Station2.Text = dt2.Rows[i]["Station"].ToString();
                            Line2.Text = CommonClasses.CommonVariable.MachineName;

                            if (brd8.Background == Brushes.White)
                                brd8.Background = Brushes.DeepPink;
                        }

                        if (dt2.Rows[i]["andonType"].ToString() == "MAINTENANCE")
                        {
                            Station2.Text = dt2.Rows[i]["Station"].ToString();
                            Line2.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd2.Background == Brushes.White)
                                brd2.Background = Brushes.Red;
                        }
                        if (dt2.Rows[i]["andonType"].ToString() == "PART FEADER")
                        {
                            Station2.Text = dt2.Rows[i]["Station"].ToString();
                            Line2.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd5.Background == Brushes.White)
                                brd5.Background = Brushes.Yellow;

                        }
                    }
                }
                if (dt1.Rows.Count >= 3)
                {
                    dv1.RowFilter = "station='" + dt1.Rows[2][0] + "'";
                    dt2 = dv1.ToTable();

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["andonType"].ToString() == "QUALITY")
                        {
                            Station3.Text = dt2.Rows[i]["Station"].ToString();
                            Quality.Text = CommonClasses.CommonVariable.MachineName;

                            if (brd9.Background == Brushes.White)
                                brd9.Background = Brushes.DeepPink;
                        }

                        if (dt2.Rows[i]["andonType"].ToString() == "MAINTENANCE")
                        {
                            Station3.Text = dt2.Rows[i]["Station"].ToString();
                            Quality.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd3.Background == Brushes.White)
                                brd3.Background = Brushes.Red;
                        }
                        if (dt2.Rows[i]["andonType"].ToString() == "PART FEADER")
                        {
                            Station3.Text = dt2.Rows[i]["Station"].ToString();
                            Quality.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd6.Background == Brushes.White)
                                brd6.Background = Brushes.Yellow;

                        }
                    }
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               // ShowDateTime();
                Start();
            }
            catch (Exception ex)
            {
                //obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", Dash_Board.CommonClasses.CommonVariable.UserID);
                // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", CommonClasses.CommonVariable.UserID);

            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            th.Suspend();
           // dispatcherTimer.Stop();
        }
    }
}