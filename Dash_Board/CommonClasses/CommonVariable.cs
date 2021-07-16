using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash_Board.CommonClasses
{
 public class CommonVariable
    {
        #region Common_Variables
        public static string UserID = "";
        public static int RefNo = 0;
        public static string Result = "";
        public static string MachineGroup = "";
        public static string MachineName = "";
        public static string ModelName = "";
        public static string CycleTime = "0";
        public static string productionPlan = "0000";
        public static string TransactioType = "";
        public static string TotalproductionPlan = "0";
        public static string ShiftName = "";
        public static string MachinePlane = "";
        public static string MachineStatus = "";
        public static string Current_Time = "";
        public static string IP = "";
        public static string PORT = "";
        public static string Time = "";
        public static string Break = "";
        public static string NoofItems = "0";
        public static string Puls = "0";
        public static string PblCOunt = "";
        public static string ChangeOver = "";
        public static string ModuleChange = "";
        public static string MenuName = "";
        public static int Seconds = 0;

        public static Pages.DashBoard DashBoard = null;
        public static Pages.MachineSelection MachineSelection = null;
        public static Pages.MenuSelection MenuSelection = null;
        public static Pages.MachineOn_Off MachineOnOff = null;

        public static DataTable dtShiftDetails = null;
        public static DataSet dtORMDeatile = null;
        public static DataTable dtDekidaka = null;
        public static DataTable dtAndon = null;
        public static DataSet dtKanbanProgress = null;
        public static DataSet dtCycleTimeFlectuation = null;
        public static DataTable dtProblemCode = null;
        public static DataTable dtStockVisual = null;
        public static DataTable dtMachineONOFF = null;
        public static DataTable dt5Mand1E = null;
        public static DataTable dtManPower = null;
        public static DataTable dtMachineSelection = null;
        public static DataTable dtDekidakaTime = null;
        public enum CustomStriing
        {
            YESNO,
            OKCancel,
            OK,
            Error,
            Successfull,
            Information,
            Question,
            Exclamatory,
        }
        #endregion
    }
}
