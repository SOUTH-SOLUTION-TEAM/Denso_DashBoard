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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Controls.DataVisualization.Charting;
using System.Reflection;
using System.Threading;
using System.Collections.ObjectModel;

namespace Dash_Board.Pages
{
    /// <summary>
    /// Interaction logic for StockVisualisation.xaml
    /// </summary>
    public partial class StockVisualisation : Page
    {
        public StockVisualisation()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        ObservableCollection<KeyValuePair<string, double>> Power1 = new ObservableCollection<KeyValuePair<string, double>>();

        Dash_Board.Business_Layer.LogCreation obj_Log = new Business_Layer.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        //  BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        DataTable Dt_Graph = new DataTable();
        Thread th = null;

        #endregion
        //private void ShowDateTime()
        //{
        //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    dispatcherTimer.Interval = new TimeSpan(0, 2, 0);
        //    dispatcherTimer.Start();
        //}
        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Transaction("StockView");

        //    }
        //    catch (Exception ex)
        //    {
        //        // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "STOCK_VISUALISATION", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "STOCK_VISUALISATION", CommonClasses.CommonVariable.UserID);
        //        //                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());

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
                       Thread.Sleep(3000);
                       if (Dash_Board.CommonClasses.CommonVariable.ModelName != "")
                       {
                           this.Dispatcher.Invoke(() =>
                           {
                               try
                               {
                                   Transaction("StockView");
                               }
                               catch (Exception ex)
                               {
                                   CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "STOCK_VISUALISATION", CommonClasses.CommonVariable.UserID);
                               }
                           });
                       }
                   }
               }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "STOCK_VISUALISATION", CommonClasses.CommonVariable.UserID);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Dt_Graph = new DataTable();
                Dt_Graph.Columns.Add("Key");
                Dt_Graph.Columns.Add("Value");
                Dt_Graph.Columns[1].DataType = System.Type.GetType("System.Double");

                //  ShowDateTime();
                LoadBarChartData();
                Start();
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "STOCK_VISUALISATION", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "STOCK_VISUALISATION", CommonClasses.CommonVariable.UserID);

            }
        }
        private void LoadBarChartData()
        {
           // ((ColumnSeries)mcChart.Series[0]).ItemsSource = null;
            ((ColumnSeries)mcChart.Series[0]).DataContext = Power1;
        }
        private void Transaction(string Type)
        {

            if (Type == "StockView")
            {
                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                //DataSet dt = obj_Tran.BL_DashBoard();
                DataTable dt = CommonClasses.CommonVariable.dtStockVisual;
                // Dt_Graph.Rows.Clear();
                Power1.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
               //     Dt_Graph.Rows.Add(dt.Rows[i]["ModelName"].ToString(), dt.Rows[i]["QTY"].ToString());
                    Power1.Add(new KeyValuePair<string, double>(dt.Rows[i]["ModelName"].ToString(), Convert.ToDouble(dt.Rows[i]["QTY"])));

                }
               // LoadBarChartData();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
           // dispatcherTimer.Stop();
            th.Suspend();
        }
    }
}
