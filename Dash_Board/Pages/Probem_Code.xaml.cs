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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dash_Board.Pages
{
    /// <summary>
    /// Interaction logic for Probem_Code.xaml
    /// </summary>
    public partial class Probem_Code : Page
    {
        public Probem_Code()
        {
            InitializeComponent();
            txtPrdCode.Focus();

        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
        // BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        bool Flag = false;
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
        //        //  Transaction("GetShiftDetails");
        //        Transaction("ProblemCode");
        //        txtPrdCode.Focus();         // Set Logical Focus
        //        //Keyboard.Focus(txtPrdCode);

        //        //if (Keyboard.IsKeyDown(Key.D0))
        //        //{
        //        //if (Keyboard.GetKeyStates(Key.D0) > 0 || Keyboard.GetKeyStates(Key.NumPad0) > 0)
        //        //{
        //        //    txtPrdCode.Text = txtPrdCode.Text + "1";

        //        // //   Keyboard.PrimaryDevice.GetKeyStates()
        //        //    //System.Windows.Forms.SendKeys.SendWait("@");
        //        //    //KeyboardDevice ko=
        //        //    TxtPrdCode_KeyDown(sender, new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, Key.D0));
        //        //    //Keyboard. = null; ;
        //        //    //  System.Diagnostics.Process.GetCurrentProcess().Kill();
        //        //}
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        // obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
        //        //     CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

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
                                   Transaction("ProblemCode");
                               }
                               catch (Exception ex)
                               {
                                   CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                               }
                           });
                       }
                   }
               }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "ProblemCodeUpdate")
            {
                if (txtPrdCode.Text != "")
                {
                    Entity_Layer.Transaction.ProblemCode = txtPrdCode.Text;
                    Entity_Layer.Transaction.RefNo = txtRefNo.Text;
                    Entity_Layer.Transaction.Type = Type;
                    DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                    txtResult.Visibility = Visibility.Visible;
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Columns[0].ColumnName == "Error")
                        {
                            txtResult.Text = dt.Rows[0]["Error"].ToString();
                            txtPrdCode.Text = "";
                        }
                        else
                        {
                            txtResult.Text = "";
                            txtResult.Visibility = Visibility.Hidden;
                            txtPrdCode.Text = "";
                            CommonClasses.CommonVariable.PblCOunt = "0";
                            CommonClasses.CommonVariable.Break = "";
                        }
                    }
                }
            }
            if (Type == "ProblemCode")
            {
                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.Time = CommonClasses.CommonVariable.Time;
                //Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                //DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                DataTable dt = CommonClasses.CommonVariable.dtProblemCode;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtReason.Text = "Machine Stop - " + dt.Rows[i]["FromTime"].ToString() + " To " + dt.Rows[i]["ToTime"].ToString() + ". ";
                    txtRefNo.Text = dt.Rows[i]["RefNo"].ToString();
                    if (txtReason.Text != "")
                        CommonClasses.CommonVariable.PblCOunt = "1";
                    else
                        CommonClasses.CommonVariable.PblCOunt = "0";

                    txtPrdCode.Focus();
                }
                if (dt.Rows.Count == 0)
                {
                    txtReason.Text = "";
                    txtRefNo.Text = "";
                    CommonClasses.CommonVariable.PblCOunt = "0";
                }
            }
        }
        private void TxtPrdCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    Transaction("ProblemCodeUpdate");
                }
                
            }
            catch(Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                //CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtPrdCode.Focus();
                //ShowDateTime();
                txtPrdCode.Text = "";
                Start();
                KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);

            }
            catch (Exception ex)
            {
                //   obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                //  CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);

            }
        }
        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            try
            {
                KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
                txtPrdCode.IsReadOnly = true;
                if (CommonClasses.CommonVariable.PblCOunt == "1")
                {
                    if (eventArgs.KeyData.ToString() == "D0" || eventArgs.KeyData.ToString() == "NumPad0")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "0";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D1" || eventArgs.KeyData.ToString() == "NumPad1")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "1";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D2" || eventArgs.KeyData.ToString() == "NumPad2")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "2";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D3" || eventArgs.KeyData.ToString() == "NumPad3")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "3";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D4" || eventArgs.KeyData.ToString() == "NumPad4")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "4";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D5" || eventArgs.KeyData.ToString() == "NumPad5")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "5";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D6" || eventArgs.KeyData.ToString() == "NumPad6")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "6";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D7" || eventArgs.KeyData.ToString() == "NumPad7")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "7";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D8" || eventArgs.KeyData.ToString() == "NumPad8")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "8";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D9" || eventArgs.KeyData.ToString() == "NumPad9")
                    {
                        if (Flag == false)
                        {
                            txtPrdCode.Text = txtPrdCode.Text + "9";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "Return")
                    {
                        if (Flag == false)
                        {
                            //txtPrdCode.Text = txtPrdCode.Text + "\r";
                            Transaction("ProblemCodeUpdate");
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "Back")
                    {
                        if (Flag == false)
                        {
                            if (txtPrdCode.Text != "")
                            {
                                txtPrdCode.Text = txtPrdCode.Text.Remove(txtPrdCode.Text.Length - 1, 1);
                                Flag = true;
                            }
                        }
                        else
                            Flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);
             th.Suspend();
            //dispatcherTimer.Stop();
        }
    }
}
