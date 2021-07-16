using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
    /// Interaction logic for CycleTimeFluctuation.xaml
    /// </summary>
    public partial class CycleTimeFluctuation : Page
    {
        public CycleTimeFluctuation()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        ObservableCollection<KeyValuePair<double, double>> Power1 = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> Power2 = new ObservableCollection<KeyValuePair<double, double>>();
        Dash_Board.Business_Layer.LogCreation obj_Log = new Business_Layer.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();

        //  BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
     //   System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int RefNo = 0,j=0;
        DataTable Dt_Graph = new DataTable();
        DataTable Dt_Graph1 = new DataTable();
        Thread th = null;
        #endregion

        //private void ShowDateTime()
        //{
        //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        //    dispatcherTimer.Start();
        //}

        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtPartName.Text = CommonClasses.CommonVariable.ModelName + " - " + CommonClasses.CommonVariable.CycleTime + " S";
        //        Transaction("CycleTime");
        //    }
        //    catch (Exception ex)
        //    {
        //        //  obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
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
                       Thread.Sleep(1500);
                       
                           if (Dash_Board.CommonClasses.CommonVariable.ModelName != "")
                           {
                               this.Dispatcher.Invoke(() =>
                               {
                                   try
                                   {
                                       txtPartName.Text = CommonClasses.CommonVariable.ModelName + " - " + CommonClasses.CommonVariable.CycleTime + " S";
                                       Transaction("CycleTime");
                                   }
                                   catch (Exception ex)
                                   {
                                       CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
                                   }
                               });
                           }
                       
                   }
               }));
                th.Start();
            }
            catch (Exception ex)
            {
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
            }
        }
        private void LoadBarChartData()
        {
           
          ((LineSeries)mcChart.Series[0]).DataContext = Power1;
           ((LineSeries)mcChart.Series[1]).DataContext = Power2;
         
            var style = new Style();
            style.TargetType = typeof(Polyline);
            style.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, 3d));
            Actual.PolylineStyle = style;

            style = new Style();
            style.TargetType = typeof(Polyline);
            style.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, 3d));
            Target.PolylineStyle = style;
        }
        private void Transaction(string Type)
        {
        
            if (Type == "CycleTime")
            {

                //Dash_Board.Entity_Layer.Transaction.Type = Type;
                //Dash_Board.Entity_Layer.Transaction.Time = CommonClasses.CommonVariable.Time;
                //Dash_Board.Entity_Layer.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                //Dash_Board.Entity_Layer.Transaction.MachineGroup = Dash_Board.CommonClasses.CommonVariable.MachineGroup;
                //Dash_Board.Entity_Layer.Transaction.MachineName = Dash_Board.CommonClasses.CommonVariable.MachineName;
                //Dash_Board.Entity_Layer.Transaction.ModelName = Dash_Board.CommonClasses.CommonVariable.ModelName;
                //DataSet dt = obj_Tran.BL_DashBoard();


                DataSet dt = CommonClasses.CommonVariable.dtCycleTimeFlectuation;
          
                bool Flag = true;
            

                for (int i = Power1.Count; i < dt.Tables[0].Rows.Count; i++)
                {
                    //Dt_Graph.Rows.Add(dt.Tables[0].Rows[i]["SLNO"].ToString(), dt.Tables[0].Rows[i]["Vaues"].ToString());
                      Power1.Add(new KeyValuePair<double, double>(Convert.ToDouble( dt.Tables[0].Rows[i]["SLNO"]), Convert.ToDouble( dt.Tables[0].Rows[i]["Vaues"].ToString())));
                
                    if (Convert.ToInt32( dt.Tables[0].Rows[i]["Vaues"].ToString())>100)
                    {
                        Flag = false;
                    }
                }
                for (int i = Power2.Count; i < dt.Tables[1].Rows.Count; i++)
                {
                    
                     Power2.Add(new KeyValuePair<double, double>(Convert.ToDouble(dt.Tables[1].Rows[i]["SLNO"]), Convert.ToDouble(dt.Tables[1].Rows[i]["Vaues"].ToString())));

                    // Dt_Graph1.Rows.Add(dt.Tables[1].Rows[i]["SLNO"].ToString(), dt.Tables[1].Rows[i]["Vaues"].ToString());
                }
                if (Flag==false)
              {
                    mcChart.Height =600;
                    mcChart.Margin = new Thickness(-17,-320,0,0);
                }
                else
                {
                    mcChart.Height = 290;
                    mcChart.Margin = new Thickness(-9, -8, 0, 0);
                }
                if(dt.Tables[2].Rows.Count>0)
                {
                    txtAvg.Text = "Avg = "+dt.Tables[2].Rows[0]["Avg"].ToString();
                    txtMax.Text = "Max = "+ dt.Tables[2].Rows[0]["max"].ToString();
                    txtMin.Text = "Min = "+ dt.Tables[2].Rows[0]["Min"].ToString();
                }
                else
                {

                    txtAvg.Text = "Avg = 0";
                    txtMax.Text = "Max = 0";
                    txtMin.Text = "Min = 0";
                }

            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
             th.Suspend();
            //dispatcherTimer.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Dt_Graph = new DataTable();
                //Dt_Graph.Columns.Add("Key");
                //Dt_Graph.Columns[0].DataType = System.Type.GetType("System.Double");
                //Dt_Graph.Columns.Add("Value");
                //Dt_Graph.Columns[1].DataType = System.Type.GetType("System.Double");
                //Dt_Graph1.Columns.Add("Key");
                //Dt_Graph1.Columns[0].DataType = System.Type.GetType("System.Double");
                //Dt_Graph1.Columns.Add("Value");
                //Dt_Graph1.Columns[1].DataType = System.Type.GetType("System.Double");
                // ShowDateTime();
                LoadBarChartData();
                Start();
            }
            catch (Exception ex)
            {
                // obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CustomMessageBox.CustomStriing.Error.ToString(), CustomMessageBox.CustomStriing.OK.ToString());
                CommonClasses.CommonMethods.CreatLogDetails(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);

            }
        }

     
       
    }
}