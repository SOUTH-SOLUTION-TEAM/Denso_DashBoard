using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
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
    /// Interaction logic for ManPowerEntry.xaml
    /// </summary>
    public partial class ManPowerEntry : Page
    {
        public ManPowerEntry()
        {
            InitializeComponent();
            txtNoOfMan.Focus();
            ShowDateTime();
        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        bool Flag = false;
        #endregion
        private void ShowDateTime()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                txtNoOfMan.Focus();
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAN_POWER_ENTRY", CommonClasses.CommonVariable.UserID);
                //     CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
            }

        }
      
        private void Transaction(string Type)
        {
            if (Type == "ManPower")
            {
                bool Flag = false;
                Dash_Board.Entity_Layer.Transaction.Type = Type;
                Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                Dash_Board.Entity_Layer.Transaction.Manpower = txtNoOfMan.Text;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);
                txtNoOfMan.Text = "";
                txtNoOfMan.Focus();
            }
        }

        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            try
            {
                KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
                //if (CommonClasses.CommonVariable.PblCOunt == "0")
                //{
                    txtManPower.Content = "Key Pressed";
                    if (eventArgs.KeyData.ToString() == "D0" || eventArgs.KeyData.ToString() == "NumPad0")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "0";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D1" || eventArgs.KeyData.ToString() == "NumPad1")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "1";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D2" || eventArgs.KeyData.ToString() == "NumPad2")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "2";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D3" || eventArgs.KeyData.ToString() == "NumPad3")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "3";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D4" || eventArgs.KeyData.ToString() == "NumPad4")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "4";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D5" || eventArgs.KeyData.ToString() == "NumPad5")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "5";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D6" || eventArgs.KeyData.ToString() == "NumPad6")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "6";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D7" || eventArgs.KeyData.ToString() == "NumPad7")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "7";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D8" || eventArgs.KeyData.ToString() == "NumPad8")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "8";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D9" || eventArgs.KeyData.ToString() == "NumPad9")
                    {
                        if (Flag == false)
                        {
                            txtNoOfMan.Text = txtNoOfMan.Text + "9";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "Return" || eventArgs.KeyData.ToString() == "Enter")
                    {
                        if (Flag == false)
                        {
                            /// txtNoOfMan.Text = txtNoOfMan.Text + "\r";
                            if (txtNoOfMan.Text == "")
                            {
                                txtManPower.Content = "PLEASE ENTER NO OF MAN POWER";
                                return;
                            }
                            Transaction("ManPower");
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "Back")
                    {
                        if (Flag == false)
                        {
                            if (txtNoOfMan.Text != "")
                            {
                                txtNoOfMan.Text = txtNoOfMan.Text.Remove(txtNoOfMan.Text.Length - 1, 1);
                                Flag = true;
                            }
                        }
                        else
                            Flag = false;
                    }

               // }
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAN_POWER_ENTRY", CommonClasses.CommonVariable.UserID);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);
        }
    }
}
