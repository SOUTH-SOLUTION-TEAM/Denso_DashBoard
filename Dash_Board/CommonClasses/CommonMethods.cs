using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
namespace Dash_Board.CommonClasses
{
    class CommonMethods
    {
        #region Common_Methods

        //public static DENSO_ORM_Service.Service1 objWS;
        //BUSINESS_LAYER.Masters.Masters obj_Masetr = new BUSINESS_LAYER.Masters.Masters();
        //public static void  MessageBoxShow(string Description, string ErrorType,string  Result)
        //{

        //    try
        //    {
        //        Pages.CustomMessageBox objCustomMsg = new Pages.CustomMessageBox(Description, ErrorType, Result);
        //        objCustomMsg.ShowDialog(); ;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        public static void CreatLogDetails(string ErrorDescrription, string methodName, string ModuleName, string CreatedBy)
        {
            try
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\" + ModuleName + "-" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".txt", true);
                sw.WriteLine(ErrorDescrription + " , " + methodName + " , " + ModuleName + " , " + CreatedBy + " , " + System.DateTime.Now.ToString());
                sw.Dispose();
                sw.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
      
       
        #endregion
    }
}
