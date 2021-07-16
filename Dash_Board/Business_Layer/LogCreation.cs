using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash_Board.Business_Layer
{
    public class LogCreation
    {
        #region Objects
        Dash_Board.Data_Layer.DatabaseConnections obj_DB = new Data_Layer.DatabaseConnections();
        #endregion

        #region LogCreation
        public void CreateLog(string ErrorDescription, string MethodName, string ModulName, string UserId)
        {
            try
            {
                obj_DB.ExecuteProcedureParam("Proc_LogDetails", ErrorDescription, MethodName, ModulName, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}