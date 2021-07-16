using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Dash_Board.Pages
{
    /// <summary>
    /// Interaction logic for MachineStop.xaml
    /// </summary>
    public partial class MachineStop : Page
    {
        public MachineStop()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
        int segundo = 60;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        Thread th;
        #endregion
        //private void ShowDateTime()
        //{
        //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 0,100);
        //    dispatcherTimer.Start();
        //}
        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtTime.Text = CommonClasses.CommonVariable.Current_Time;
        //        txtType.Text = CommonClasses.CommonVariable.MachinePlane;
        //        if (txtType.Text.Contains("UNPLANNED"))
        //        {
        //            GReasn.Background = Brushes.DarkOrange;
        //            TxtReason.Text = "UNPLANNED OFF";
        //            txtTimer.Visibility = Visibility.Visible;
        //            DateTime dt = new DateTime();
        //            CommonClasses.CommonVariable.Seconds++;
        //            txtTimer.Text = dt.AddSeconds(CommonClasses.CommonVariable.Seconds).ToString("HH:mm:ss");
        //        }
        //        else
        //        {
        //            TxtReason.Text = CommonClasses.CommonVariable.MachineStatus;
        //            GReasn.Background = Brushes.Green;
        //            txtTimer.Visibility = Visibility.Hidden;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", Dash_Board.CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", CommonClasses.CommonVariable.UserID);
        //        //  CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

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
                        Thread.Sleep(1000);
                        this.Dispatcher.Invoke(() =>
                        {
                            txtTime.Text = CommonClasses.CommonVariable.Current_Time;
                            txtType.Text = CommonClasses.CommonVariable.MachinePlane;
                            if (txtType.Text.Contains("UNPLANNED"))
                            {
                                GReasn.Background = Brushes.DarkOrange;
                                TxtReason.Text = "UNPLANNED OFF";
                                txtTimer.Visibility = Visibility.Visible;
                                DateTime dt = new DateTime();
                                CommonClasses.CommonVariable.Seconds++;
                                txtTimer.Text = dt.AddSeconds(CommonClasses.CommonVariable.Seconds).ToString("HH:mm:ss");
                            }
                            else
                            {
                                TxtReason.Text = CommonClasses.CommonVariable.MachineStatus;
                                GReasn.Background = Brushes.Green;
                                txtTimer.Visibility = Visibility.Hidden;
                            }
                        });

                    }
                }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", CommonClasses.CommonVariable.UserID);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               //  ShowDateTime();
                Start();
                this.Focus();
            }
            catch (Exception ex)
            {
                //   obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", Dash_Board.CommonClasses.CommonVariable.UserID);
                //  CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", CommonClasses.CommonVariable.UserID);

            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                th.Suspend();
            }
            catch (Exception ex)
            {
                //   obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", Dash_Board.CommonClasses.CommonVariable.UserID);
                //  CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_STOP", CommonClasses.CommonVariable.UserID);

            }
        }
    }
}
