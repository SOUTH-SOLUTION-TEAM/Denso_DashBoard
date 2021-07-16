using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MachineOn_Off.xaml
    /// </summary>
    public partial class MachineOn_Off : Window
    {
        public MachineOn_Off()
        {
            InitializeComponent();
        }
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        bool Flag = false;
        string Key = "";
        string Key1 = "";
        bool isPressed = false;
        int Index2 = -1;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Key = "Time";
                txtPlan.Text =CommonClasses.CommonVariable.MachineName +" - "+ CommonClasses.CommonVariable.MenuName;
                txtTime.Visibility = Visibility.Visible;
                lblTime.Visibility = Visibility.Visible;
                if (txtPlan.Text.Contains("UNPLANNED"))
                {
                    if (CommonClasses.CommonVariable.ShiftName == "First Shift")
                    {
                        txtTime.Text = "15:00";
                        txtTime.Visibility = Visibility.Hidden;
                        lblTime.Visibility = Visibility.Hidden;
                    }
                    if (CommonClasses.CommonVariable.ShiftName == "Second Shift")
                    {
                        txtTime.Text = "23:30";
                        txtTime.Visibility = Visibility.Hidden;
                        lblTime.Visibility = Visibility.Hidden;
                    }
                    if (CommonClasses.CommonVariable.ShiftName == "Third Shift")
                    {
                        txtTime.Text = "06:30";
                        txtTime.Visibility = Visibility.Hidden;
                        lblTime.Visibility = Visibility.Hidden;
                    }
                }
                
                KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
            }
            catch(Exception ex)
            {

            }

        }
        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {try
            {
                KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
                if (Key == "Code")
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
                if (Key == "Time")
                {
                    if (eventArgs.KeyData.ToString() == "D0" || eventArgs.KeyData.ToString() == "NumPad0")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "0";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D1" || eventArgs.KeyData.ToString() == "NumPad1")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "1";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D2" || eventArgs.KeyData.ToString() == "NumPad2")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "2";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D3" || eventArgs.KeyData.ToString() == "NumPad3")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "3";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D4" || eventArgs.KeyData.ToString() == "NumPad4")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "4";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D5" || eventArgs.KeyData.ToString() == "NumPad5")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "5";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D6" || eventArgs.KeyData.ToString() == "NumPad6")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "6";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D7" || eventArgs.KeyData.ToString() == "NumPad7")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "7";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D8" || eventArgs.KeyData.ToString() == "NumPad8")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "8";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "D9" || eventArgs.KeyData.ToString() == "NumPad9")
                    {
                        if (Flag == false)
                        {
                            txtTime.Text = txtTime.Text + "9";
                            Key1 = "";
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }

                    if (eventArgs.KeyData.ToString() == "Back")
                    {
                        if (Flag == false)
                        {
                            if (txtTime.Text != "")
                            {
                                Key1 = "Back";
                                txtTime.Text = txtTime.Text.Remove(txtTime.Text.Length - 1, 1);
                                Flag = true;
                            }
                        }
                        else
                            Flag = false;
                    }
                }
              
                if (eventArgs.KeyData.ToString() == "Multiply" || eventArgs.KeyData.ToString() == "OemMultiply")
                {
                    if (Flag == false)
                    {
                        Key = "Time";
                    }
                    else
                        Flag = false;
                }
                if (eventArgs.KeyData.ToString() == "Return" || eventArgs.KeyData.ToString() == "Enter")
                {
                    if (Flag == false)
                    {
                        //txtPrdCode.Text = txtPrdCode.Text + "\r";
                        //Transaction("ProblemCodeUpdate");
                        if (isPressed == true)
                        {


                            if (txtTime.Text == "")
                            {
                                TxtResult.Text = "ENTER TIME";
                                txtTime.Text = "";
                                Key = "Time";
                                Flag = true;
                                return;
                            }
                            if (txtTime.Text.Length < 5)
                            {
                                TxtResult.Text = "INVALID TIME";
                                txtTime.Text = "";
                                Flag = true;
                                Key = "Time";
                                return;
                            }
                            if (TimeSpan.Parse(txtTime.Text) <= TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm")))
                            {
                                TxtResult.Text = "TIME SHOULD BE MORE THAN PRESENT TIME";
                                txtTime.Text = "";
                                Flag = true;
                                Key = "Time";
                                return;
                            }
                            if (txtPrdCode.Text == "")
                            {
                                TxtResult.Text = "ENTER PROBLEM CODE";
                                Flag = true;
                                Key = "Code";
                                return;
                            }

                            if (txtPlan.Text.Contains("UNPLANNED"))
                                Dash_Board.Entity_Layer.Transaction.Type = "UnPlanned Off";
                            else
                                Dash_Board.Entity_Layer.Transaction.Type = "Planned Off";

                            Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                            Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                            Dash_Board.Entity_Layer.Transaction.Shiftname = Dash_Board.CommonClasses.CommonVariable.ShiftName;
                            Dash_Board.Entity_Layer.Transaction.Time = txtTime.Text;
                            Dash_Board.Entity_Layer.Transaction.ProblemCode = txtPrdCode.Text;
                            string Result = obj_Tran.BL_MachineOnandOffTransaction();
                            if (Result == "Saved")
                            {
                                if (CommonClasses.CommonVariable.ModuleChange != "DashBoard")
                                {
                                    if (CommonClasses.CommonVariable.DashBoard == null)
                                    {
                                        this.Close();
                                        CommonClasses.CommonVariable.ModuleChange = "DashBoard";
                                        CommonClasses.CommonVariable.DashBoard = new Pages.DashBoard();
                                        CommonClasses.CommonVariable.MachineOnOff.Close();
                                        CommonClasses.CommonVariable.MachineOnOff = null;
                                        CommonClasses.CommonVariable.DashBoard.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                TxtResult.Text = Result;
                                if (Result.Contains("PROBLEM CODE"))
                                {
                                    txtPrdCode.Text = "";
                                    Key = "Code";
                                }
                                else
                                    Key = "Time";
                            }

                            Flag = true;
                        }
                    }
                    else
                        Flag = false;


                }
                if (eventArgs.KeyData.ToString() == "Divide" || eventArgs.KeyData.ToString() == "OemDivide")
                {
                    if (Flag == false)
                    {
                        Key = "Code";
                    }
                    else
                        Flag = false;
                }
                if (eventArgs.KeyData.ToString() == "Subtract" || eventArgs.KeyData.ToString() == "OemMinus")
                {
                    if (CommonClasses.CommonVariable.ModuleChange != "MenuSelection")
                    {
                        if (CommonClasses.CommonVariable.MenuSelection == null)
                        {
                            CommonClasses.CommonVariable.ModuleChange = "MenuSelection";
                            CommonClasses.CommonVariable.MenuSelection = new Pages.MenuSelection();
                            CommonClasses.CommonVariable.MachineOnOff.Close();
                            CommonClasses.CommonVariable.MachineOnOff = null;
                            CommonClasses.CommonVariable.MenuSelection.ShowDialog();
                        }
                    }
                }
                if (eventArgs.KeyData.ToString() == "Add" || eventArgs.KeyData.ToString() == "Oemplus")
                {
                    if (CommonClasses.CommonVariable.ModuleChange != "DashBoard")
                    {
                        if (CommonClasses.CommonVariable.DashBoard == null)
                        {
                            CommonClasses.CommonVariable.ModuleChange = "DashBoard";
                            CommonClasses.CommonVariable.DashBoard = new Pages.DashBoard();
                            CommonClasses.CommonVariable.MachineOnOff.Close();
                            CommonClasses.CommonVariable.MachineOnOff = null;
                            CommonClasses.CommonVariable.DashBoard.ShowDialog();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                TxtResult.Text = "INVALID DATA";
            }
        }

        private void TxtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTime.Text != "")
            {
                if (Key1 == "")
                {
                    isPressed = true;
                    if (txtTime.Text.Length == 2)
                    {
                        txtTime.Text = txtTime.Text.Insert(2, ":");
                        return;
                    }
                }
                if(txtTime.Text.Length==5)
                    Key = "Code";
                else
                    Key = "Time";

            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);

        }
    }
}
