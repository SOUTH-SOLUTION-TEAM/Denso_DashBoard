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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace Dash_Board.Pages
{
    /// <summary>
    /// Interaction logic for MachineSelection.xaml
    /// </summary>
    public partial class MachineSelection : Window
    {
        public MachineSelection()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        Dash_Board.Business_Layer.LogCreation obj_Log = new Dash_Board.Business_Layer.LogCreation();
        Business_Layer.Masters obj_Mast = new Business_Layer.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        int Index = -1,Index1=-1, Index2=-1;
        bool Flag = false;
        DataTable ModuleList = new DataTable();
        string strKey = "";
        Thread th = null;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        #endregion

        //private void ShowDateTime()
        //{
        //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    dispatcherTimer.Interval = new TimeSpan(0, 0,0,0,1);
        //    dispatcherTimer.Start();
        //}
        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Transaction("GetMachineGroupAndName");
        //        Transaction("ModuleName");
        //        if (CommonClasses.CommonVariable.ModuleChange != "MachineSelection")
        //        {
        //            if (CommonClasses.CommonVariable.ModuleChange != "DashBoard")
        //            {
        //                if (CommonClasses.CommonVariable.DashBoard == null)
        //                {
        //                    this.Close();
        //                    CommonClasses.CommonVariable.ModuleChange = "DashBoard";
        //                    CommonClasses.CommonVariable.DashBoard = new Pages.DashBoard();
        //                    CommonClasses.CommonVariable.MachineSelection.Close();
        //                    CommonClasses.CommonVariable.MachineSelection = null;
        //                    CommonClasses.CommonVariable.DashBoard.ShowDialog();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
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
                                    if (txtmachinegrp.Text == "")
                                    {
                                        Transaction("GetMachineGroupAndName");
                                    }
                                    Transaction("ModuleName");
                                    if (CommonClasses.CommonVariable.ModuleChange != "MachineSelection")
                                    {
                                        if (CommonClasses.CommonVariable.ModuleChange != "DashBoard")
                                        {
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
                                catch (Exception ex)
                                {
                                    CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                                }
                            });
                        }
                    }
                }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string ipAddress = "";
                if (Dns.GetHostAddresses(Dns.GetHostName()).Length > 0)
                {
                    txtRdpIP.Text = "RDP IP = " + Dns.GetHostAddresses(Dns.GetHostName())[1].ToString();
                }
                // ShowDateTime();
                Start();
                Transaction("GetMachineGroupAndName");
                Transaction("ModuleName");
                Transaction("GetModuleName");
                KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
            }
        }

        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            try
            {
                KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;

                if (strKey == "D6")
                {
                    Index = lstModelList.SelectedIndex;
                    if (eventArgs.KeyData.ToString() == "NumPad2" || eventArgs.KeyData.ToString() == "D2")
                    {
                        if (Flag == false)
                        {
                            if (Index == lstModelList.Items.Count - 1)
                            {
                                Index = 0;
                                lstModelList.SelectedIndex = 0;
                                Flag = true;
                                return;
                            }
                            lstModelList.SelectedIndex = Index + 1;
                            lstModelList.ScrollIntoView(lstModelList.SelectedItem);

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
                                lstModelList.SelectedIndex = Index - 1;
                                lstModelList.ScrollIntoView(lstModelList.SelectedItem);

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
                    Index = lstModelList.SelectedIndex;
                    if (eventArgs.KeyData.ToString() == "NumPad2" || eventArgs.KeyData.ToString() == "D2")
                    {


                        if (Flag == false)
                        {
                            if (Index == lstModelList.Items.Count - 1)
                            {
                                Index = 0;
                                lstModelList.SelectedIndex = 0;
                                Flag = true;
                                return;
                            }
                            lstModelList.SelectedIndex = Index + 1;
                            lstModelList.ScrollIntoView(lstModelList.SelectedItem);
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
                                lstModelList.SelectedIndex = Index - 1;
                                lstModelList.ScrollIntoView(lstModelList.SelectedItem);

                                Index--;
                                Flag = true;
                            }
                            else
                                Flag = false;
                        }
                    }
                }
                if(CommonClasses.CommonVariable.dtMachineSelection.Rows.Count>1)
                {
                    if (eventArgs.KeyData.ToString() == "NumPad3" || eventArgs.KeyData.ToString() == "D3")
                    {


                        if (Flag == false)
                        {
                            if (Index2 == CommonClasses.CommonVariable.dtMachineSelection.Rows.Count-1)
                            {
                                //Index1 = -1;

                                Flag = true;
                                return;
                            }
                           
                            txtmachinegrp.Text = CommonClasses.CommonVariable.dtMachineSelection.Rows[Index2+1]["MachineGroup"].ToString();
                            txtmachinename.Text = CommonClasses.CommonVariable.dtMachineSelection.Rows[Index2+1]["machineName"].ToString();
                            Dash_Board.CommonClasses.CommonVariable.MachineGroup = txtmachinegrp.Text;
                            Dash_Board.CommonClasses.CommonVariable.MachineName = txtmachinename.Text;
                            Transaction("ModuleNameForMultiple");
                            if (Index2 != CommonClasses.CommonVariable.dtMachineSelection.Rows.Count)
                            {
                                Index2++;
                            }
                            Flag = true;
                        }
                        else
                            Flag = false;

                    }
                    if (eventArgs.KeyData.ToString() == "NumPad9" || eventArgs.KeyData.ToString() == "D9")
                    {
                        if (Index2 > 0)
                        {
                            if (Flag == false)
                            {
                               // Index1--;
                                txtmachinegrp.Text = CommonClasses.CommonVariable.dtMachineSelection.Rows[Index2-1]["MachineGroup"].ToString();
                                txtmachinename.Text = CommonClasses.CommonVariable.dtMachineSelection.Rows[Index2-1]["machineName"].ToString();
                                Dash_Board.CommonClasses.CommonVariable.MachineGroup = txtmachinegrp.Text;
                                Dash_Board.CommonClasses.CommonVariable.MachineName = txtmachinename.Text;
                                Transaction("ModuleNameForMultiple");
                                Index2--;
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
                            for (int i = 0; i < lstModelList.SelectedItems.Count; i++)
                            {
                                DataRowView dv = (DataRowView)lstModelList.SelectedItems[i];
                                CommonClasses.CommonVariable.ModelName = dv[0].ToString();
                                CommonClasses.CommonVariable.CycleTime = dv[1].ToString();
                                CommonClasses.CommonVariable.NoofItems = dv[2].ToString();
                                CommonClasses.CommonVariable.Puls = dv[3].ToString();

                                Transaction("ModelUpdate");
                                if (CommonClasses.CommonVariable.ModuleChange != "DashBoard")
                                {
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
                            if (CommonClasses.CommonVariable.ModelName == "")
                            {
                                MessageBox.Show("PLEASE SELECT MODEL");
                               // CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MODEL", CustomMessageBox.CustomStriing.Information.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
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
                    if (CommonClasses.CommonVariable.ModuleChange != "DashBoard")
                    {
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
                if (eventArgs.KeyData.ToString() == "Subtract" || eventArgs.KeyData.ToString() == "OemMinus")
                {
                    if (CommonClasses.CommonVariable.ModuleChange != "MenuSelection")
                    {
                        if (CommonClasses.CommonVariable.MenuSelection == null)
                        {
                            CommonClasses.CommonVariable.ModuleChange = "MenuSelection";
                            CommonClasses.CommonVariable.MenuSelection = new Pages.MenuSelection();
                            CommonClasses.CommonVariable.MachineSelection.Close();
                            CommonClasses.CommonVariable.MachineSelection = null;
                            CommonClasses.CommonVariable.MenuSelection.ShowDialog();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
            }
        }

        //private void Cmbmachinegrp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Cmbmachinegrp.SelectedIndex > -1)
        //        {
        //            CommonClasses.CommonVariable.MachineGroup = Cmbmachinegrp.SelectedValue.ToString();
        //            Transaction("GetMachinename");
                
        //        }
        //        else
        //            CommonClasses.CommonMethods.UNFill_ComboBox(cmbmachinename);
        //    }
        //    catch (Exception ex)
        //    {
        //        //obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

        //    }
        //}
      
        private void Transaction(string Type)
        {

            //if (Type == "GetMachineGroupname")
            //{
            //    Entity_Layer.Masters.Type = Type;
            //    DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
            //    CommonClasses.CommonMethods.FillComboBox(Cmbmachinegrp, dt, "MachineGrName", "MachineGrName");
            //}
            //if (Type == "GetMachinename")
            //{
            //    Entity_Layer.Masters.Type = Type;
            //    Entity_Layer.Masters.MachineGroup = Cmbmachinegrp.SelectedValue.ToString();
            //    DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
            //    CommonClasses.CommonMethods.FillComboBox(cmbmachinename, dt, "MachineName", "MachineName");
            //}
            if (Type == "ModuleNameForMultiple")
            {
                Entity_Layer.Masters.Type = "ModuleName";
                Entity_Layer.Masters.MachineGroup = txtmachinegrp.Text;
                Entity_Layer.Masters.MachineName = txtmachinename.Text;
                ModuleList = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                lstModelList.ItemsSource = null;
                lstModelList.ItemsSource = ModuleList.DefaultView;
                lstModelList.DisplayMemberPath = "ModelName";

                // CommonClasses.CommonMethods.FillComboBox(cmbModel, dt, "ModelName", "ModelName2");

            }
            if (Type == "ModuleName")
            {
                Entity_Layer.Masters.Type = Type;
                Entity_Layer.Masters.MachineGroup =txtmachinegrp.Text;
                Entity_Layer.Masters.MachineName = txtmachinename.Text;
                 ModuleList = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                
                    if (lstModelList.Items.Count == 0)
                    {
                        lstModelList.ItemsSource = null;
                        lstModelList.ItemsSource = ModuleList.DefaultView;
                        lstModelList.DisplayMemberPath = "ModelName";

                    }
               
               // CommonClasses.CommonMethods.FillComboBox(cmbModel, dt, "ModelName", "ModelName2");

            }
            if (Type == "GetShiftDetails")
            {
                Dash_Board.Entity_Layer.Transaction.Type = Type;
                Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                CommonClasses.CommonVariable.ShiftName = dt.Rows[0]["ShiftName"].ToString();
                CommonClasses.CommonVariable.Time = dt.Rows[0]["Time"].ToString().Split(' ')[0].ToString();
            }
            if(Type== "ModelUpdate")
            {
                Dash_Board.Entity_Layer.Transaction.Type = Type;
                Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                Dash_Board.Entity_Layer.Transaction.Shiftname = Dash_Board.CommonClasses.CommonVariable.ShiftName;
                Dash_Board.Entity_Layer.Transaction.Time = Dash_Board.CommonClasses.CommonVariable.Time;
                Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                Dash_Board.Entity_Layer.Transaction.Cycletime = Dash_Board.CommonClasses.CommonVariable.CycleTime;
                Dash_Board.Entity_Layer.Transaction.Puls = Dash_Board.CommonClasses.CommonVariable.Puls;
                Dash_Board.Entity_Layer.Transaction.NoofItem = Dash_Board.CommonClasses.CommonVariable.NoofItems;
                Dash_Board.Entity_Layer.Transaction.Changesover = System.DateTime.Now.ToString("HH:mm:ss");
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
            }
            //if (Type == "GetLineSelection")
            //{
            //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LineSelection.txt"))
            //    {
            //        StreamReader Sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\LineSelection.txt");
            //        string Data = Sr.ReadToEnd();
            //        Sr.Close();
            //        Data = Data.Replace("\r\n", "|");
            //        string[] data1 = Data.Split('|');
            //        if(data1.Length>0)
            //        {
            //            Cmbmachinegrp.Text = data1[0];
            //            if(data1.Length>1)
            //                cmbmachinename.Text = data1[1];
            //            Cmbmachinegrp.IsEnabled = false;
            //            cmbmachinename.IsEnabled = false;

            //        }
            //    }
            //}
            if (Type == "GetMachineGroupAndName")
            {
                Dash_Board.Entity_Layer.Transaction.Type = Type;
                Dash_Board.Entity_Layer.Transaction.RDPIP = txtRdpIP.Text.Split('=')[1].ToString().Trim();
                CommonClasses.CommonVariable.dtMachineSelection = new DataTable();
                CommonClasses.CommonVariable.dtMachineSelection  = obj_Tran.BL_DashBoard().Tables[0];
                if (CommonClasses.CommonVariable.dtMachineSelection.Rows.Count > 0)
                {
                    txtmachinegrp.Text = CommonClasses.CommonVariable.dtMachineSelection.Rows[0]["MachineGroup"].ToString();
                    txtmachinename.Text = CommonClasses.CommonVariable.dtMachineSelection.Rows[0]["machineName"].ToString();
                    Dash_Board.CommonClasses.CommonVariable.MachineGroup = txtmachinegrp.Text;
                    Dash_Board.CommonClasses.CommonVariable.MachineName = txtmachinename.Text;
                }
            }
            if (Type == "GetModuleName")
            {
                if (CommonClasses.CommonVariable.ModelName == "")
                {
                    Dash_Board.Entity_Layer.Masters.Type = Type;
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
                else
                {
                    Dash_Board.Entity_Layer.Masters.Type = Type;
                    Dash_Board.Entity_Layer.Masters.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                    Dash_Board.Entity_Layer.Masters.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                    DataTable dt = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        CommonClasses.CommonVariable.ModelName = dt.Rows[0]["ModelName"].ToString();
                        for (int i = 0; i < lstModelList.Items.Count; i++)
                        {
                            DataRowView dv = (DataRowView)lstModelList.Items[i];
                            if (dv[0].ToString() == CommonClasses.CommonVariable.ModelName)
                            {
                                lstModelList.SelectedIndex = i;
                                Index1 = i;
                                return;
                            }

                        }
                    }
                }

            }
        }

        //private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {

        //        if (cmbmachinename.SelectedIndex > -1)
        //        {
        //            CommonClasses.CommonVariable.MachineName = cmbmachinename.SelectedValue.ToString();
        //            Transaction("ModuleName");
        //            Transaction("GetModuleName");

        //            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LineSelection.txt"))
        //            {
        //                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\LineSelection.txt");
        //            }
        //            StreamWriter sW = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LineSelection.txt", true);
        //            sW.WriteLine(CommonClasses.CommonVariable.MachineGroup);
        //            sW.WriteLine(CommonClasses.CommonVariable.MachineName);
        //            sW.Close();
        //            Transaction("GetShiftDetails");
        //        }
        //        else
        //            lstModelList.ItemsSource = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
        //       // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

        //    }
        //}

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            th.Suspend();
            //dispatcherTimer.Stop();
            KeyboardListener.s_KeyEventHandler -= new EventHandler(KeyboardListener_s_KeyEventHandler);

        }

        //private bool ControlValidation()
        //{
        //    if(Cmbmachinegrp.SelectedIndex==-1)
        //    {
        //        CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE GROUP", CustomMessageBox.CustomStriing.Information.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

        //        Cmbmachinegrp.Focus();
        //        return false;
        //    }
        //    if(cmbmachinename.SelectedIndex==-1)
        //    {
        //        CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE NAME", CustomMessageBox.CustomStriing.Information.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
        //        cmbmachinename.Focus();
        //        return false;
        //    }
          
        //    return true;
        //}
    }
}
