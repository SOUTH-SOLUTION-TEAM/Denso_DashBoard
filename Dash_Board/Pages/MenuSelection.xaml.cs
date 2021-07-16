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
    /// Interaction logic for Selection.xaml
    /// </summary>
    public partial class MenuSelection : Window
    {
        public MenuSelection()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
        Business_Layer.Masters obj_Mast = new Business_Layer.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        int Index = -1, Index1 = 0;
        bool Flag = false;
        DataTable ManuList = new DataTable();
        string strKey = "";
        #endregion


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ManuList = new DataTable();
                ManuList.Columns.Add("Menu");
                ManuList.Rows.Add("MODELS LIST");
                ManuList.Rows.Add("PLANNED MACHINE OFF");
                ManuList.Rows.Add("UNPLANNED MACHINE OFF");
                lstMenuList.ItemsSource = ManuList.DefaultView;
                lstMenuList.DisplayMemberPath = "Menu";

                KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MENU_SELECTION", CommonClasses.CommonVariable.UserID);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);
        }

        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            try
            {
                KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
                if (strKey == "D6")
                {
                    Index = lstMenuList.SelectedIndex;
                    if (eventArgs.KeyData.ToString() == "NumPad2" || eventArgs.KeyData.ToString() == "D2")
                    {
                        if (Flag == false)
                        {
                            if (Index == lstMenuList.Items.Count - 1)
                            {
                                Index = 0;
                                lstMenuList.SelectedIndex = 0;
                                Flag = true;
                                return;
                            }
                            lstMenuList.SelectedIndex = Index + 1;
                            lstMenuList.ScrollIntoView(lstMenuList.SelectedItem);

                            strKey = "Enter";
                            Index++;
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "NumPad8" || eventArgs.KeyData.ToString() == "D8")
                    {
                        if (Index > 0)
                        {
                            if (Flag == false)
                            {
                                lstMenuList.SelectedIndex = Index - 1;
                                lstMenuList.ScrollIntoView(lstMenuList.SelectedItem);

                                Index--;
                                Flag = true;
                            }
                            else
                                Flag = false;
                        }
                    }
                }
                else
                {
                    Index = lstMenuList.SelectedIndex;
                    if (eventArgs.KeyData.ToString() == "NumPad2" || eventArgs.KeyData.ToString() == "D2")
                    {

                        if (Flag == false)
                        {
                            if (Index == lstMenuList.Items.Count - 1)
                            {
                                Index = 0;
                                lstMenuList.SelectedIndex = 0;
                                Flag = true;
                                return;
                            }
                            lstMenuList.SelectedIndex = Index + 1;
                            lstMenuList.ScrollIntoView(lstMenuList.SelectedItem);
                            strKey = "Enter";
                            Index++;
                            Flag = true;
                        }
                        else
                            Flag = false;
                    }
                    if (eventArgs.KeyData.ToString() == "NumPad8" || eventArgs.KeyData.ToString() == "D8")
                    {
                        if (Index > 0)
                        {
                            if (Flag == false)
                            {
                                lstMenuList.SelectedIndex = Index - 1;
                                lstMenuList.ScrollIntoView(lstMenuList.SelectedItem);

                                Index--;
                                Flag = true;
                            }
                            else
                                Flag = false;
                        }
                    }
                }
                if (eventArgs.KeyData.ToString() == "Return" || eventArgs.KeyData.ToString() == "Enter")
                {
                    if (Flag == false)
                    {
                        if (strKey == "Enter")
                        {
                            for (int i = 0; i < lstMenuList.SelectedItems.Count; i++)
                            {
                                DataRowView dv = (DataRowView)lstMenuList.SelectedItems[i];
                                CommonClasses.CommonVariable.MenuName = dv[0].ToString();


                                if (CommonClasses.CommonVariable.MenuName == "MODELS LIST")
                                {
                                    if (CommonClasses.CommonVariable.ModuleChange != "MachineSelection")
                                    {
                                        if (CommonClasses.CommonVariable.MachineSelection == null)
                                        {
                                            CommonClasses.CommonVariable.ModuleChange = "MachineSelection";
                                            CommonClasses.CommonVariable.MachineSelection = new Pages.MachineSelection();
                                            CommonClasses.CommonVariable.MenuSelection.Close();
                                            CommonClasses.CommonVariable.MenuSelection = null;
                                            CommonClasses.CommonVariable.MachineSelection.ShowDialog();
                                        }
                                    }
                                }
                                else if (CommonClasses.CommonVariable.MenuName == "PLANNED MACHINE OFF")
                                {
                                    if (CommonClasses.CommonVariable.ModuleChange != "MachinePlannedOff")
                                    {
                                        if (CommonClasses.CommonVariable.MachineOnOff == null)
                                        {
                                            CommonClasses.CommonVariable.ModuleChange = "MachinePlannedOff";
                                            CommonClasses.CommonVariable.MachineOnOff = new Pages.MachineOn_Off();
                                            CommonClasses.CommonVariable.MenuSelection.Close();
                                            CommonClasses.CommonVariable.MenuSelection = null;
                                            CommonClasses.CommonVariable.MachineOnOff.ShowDialog();
                                        }
                                    }
                                }
                                else if (CommonClasses.CommonVariable.MenuName == "UNPLANNED MACHINE OFF")
                                {
                                    if (CommonClasses.CommonVariable.ModuleChange != "MachineUnPlannedOff")
                                    {
                                        if (CommonClasses.CommonVariable.MachineOnOff == null)
                                        {
                                            CommonClasses.CommonVariable.ModuleChange = "MachineUnPlannedOff";
                                            CommonClasses.CommonVariable.MachineOnOff = new Pages.MachineOn_Off();
                                            CommonClasses.CommonVariable.MenuSelection.Close();
                                            CommonClasses.CommonVariable.MenuSelection = null;
                                            CommonClasses.CommonVariable.MachineOnOff.ShowDialog();
                                        }
                                    }
                                }
                            }

                            if (CommonClasses.CommonVariable.MenuName == "")
                            {
                                MessageBox.Show("PLEASE SELECT MENU");
                               // CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MENU", CustomMessageBox.CustomStriing.Information.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                                // lstModelList.Focus();
                                return;
                            }
                        }
                        Flag = true;
                    }
                    else
                        Flag = false;
                }

                if (eventArgs.KeyData.ToString() == "NumPad6" || eventArgs.KeyData.ToString() == "D6")
                {
                    if (Flag == false)
                    {
                        strKey = "D6";
                        Index = -1;
                        //lstModelList.Focus();
                        Flag = true;
                    }
                    else
                        Flag = false;
                }
                if (eventArgs.KeyData.ToString() == "Back")
                {
                    if (Flag == false)
                    {
                        strKey = "D6";
                        Index = -1;
                        //lstModelList.Focus();
                        Flag = true;
                    }
                    else
                        Flag = false;
                }
                if (eventArgs.KeyData.ToString() == "Multiply" || eventArgs.KeyData.ToString() == "Oemmultiply")
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                if (eventArgs.KeyData.ToString() == "Add" || eventArgs.KeyData.ToString() == "Oemplus")
                {
                    if (CommonClasses.CommonVariable.DashBoard == null)
                    {
                        this.Close();
                        if (CommonClasses.CommonVariable.MachineSelection != null)
                        {
                            CommonClasses.CommonVariable.ModuleChange = "DashBoard";
                            CommonClasses.CommonVariable.DashBoard = new Pages.DashBoard();
                            CommonClasses.CommonVariable.MachineSelection.Close();
                            CommonClasses.CommonVariable.MachineSelection = null;
                            CommonClasses.CommonVariable.DashBoard.ShowDialog();
                        }
                        else if (CommonClasses.CommonVariable.MenuSelection != null)
                        {
                            CommonClasses.CommonVariable.ModuleChange = "DashBoard";
                            CommonClasses.CommonVariable.DashBoard = new Pages.DashBoard();
                            CommonClasses.CommonVariable.MenuSelection.Close();
                            CommonClasses.CommonVariable.MenuSelection = null;
                            CommonClasses.CommonVariable.DashBoard.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MENU_SELECTION", CommonClasses.CommonVariable.UserID);
            }
        }
    }
}
