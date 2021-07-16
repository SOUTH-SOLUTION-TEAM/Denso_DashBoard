using Dash_Board.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Dash_Board
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Dash_Board.Business_Layer.LogCreation obj_Log = new Business_Layer.LogCreation();

        private void StartUP(object sender, StartupEventArgs e)
        {
            try
            {
                bool Running;

                Mutex mutex = new Mutex(true, "DASH_BOARD", out Running);
                if (Running == true)
                {
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Log"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log");
                    }
                    string data = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                    if (data != "")
                    {
                        string[] DataSplit = data.Split(',');
                        if (DataSplit.Length > 0)
                        {
                            Dash_Board.Entity_Layer.DatabaseSettings.SqldbServer = DataSplit[0].ToString();
                            Dash_Board.Entity_Layer.DatabaseSettings.SqlDBUserID = DataSplit[1].ToString();
                            Dash_Board.Entity_Layer.DatabaseSettings.SqlDBPassword = DataSplit[2].ToString();
                            Dash_Board.Entity_Layer.DatabaseSettings.SqlDBName = DataSplit[3].ToString();
                            Dash_Board.Pages.AndonSMSandCallForSmallManitor obj_AndonSMSandCallForSmallManitor = new Pages.AndonSMSandCallForSmallManitor();

                            obj_AndonSMSandCallForSmallManitor.Show();


                            // CommonClasses.CommonVariable.ModuleChange = "MachineSelection";
                            CommonClasses.CommonVariable.MachineSelection = new Pages.MachineSelection();
                            CommonClasses.CommonVariable.MachineSelection.ShowDialog();

                            //Dash_Board.Pages.AndonSMSandCallForSmallManitor obj_AndonSMSandCallForSmallManitor = new Pages.AndonSMSandCallForSmallManitor();

                            //obj_AndonSMSandCallForSmallManitor.Show();
                                                       // App.Current.MainWindow.Content = CommonClasses.CommonVariable.obj_Login;
                        }
                        else
                        {
                           // CommonClasses.CommonMethods.MessageBoxShow("INCORRECT DATABASE SETTING!!", CustomMessageBox.CustomStriing.Information.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                            CommonClasses.CommonMethods.CreatLogDetails("INCORRECT DATABASE SETTING!!", MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);

                // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
            }
        }


        private void Grid_MouseLeftButtonUp_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
            catch (Exception ex)
            {
                //   obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);
                // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);

            }
        }
    }
}
