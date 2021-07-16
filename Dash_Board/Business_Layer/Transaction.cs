using Dash_Board.Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LAYER.Transaction
{
    public class Transaction
    {
        #region Objects
        Dash_Board.Data_Layer.DatabaseConnections obj_DB = new DatabaseConnections();
        #endregion
       
        #region DashBoard
        public DataSet BL_DashBoard()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_DashBoard", Dash_Board.Entity_Layer.Transaction.Type, Dash_Board.Entity_Layer.Transaction.MachineName,Dash_Board.Entity_Layer.Transaction.MachineGroup, Dash_Board.Entity_Layer.Transaction.ModelName, Dash_Board.Entity_Layer.Transaction.ProblemCode, Dash_Board.Entity_Layer.Transaction.Cycletime, Dash_Board.Entity_Layer.Transaction.NoofItem, Dash_Board.Entity_Layer.Transaction.Puls, Dash_Board.Entity_Layer.Transaction.Manpower, Dash_Board.Entity_Layer.Transaction.RefNo, Dash_Board.Entity_Layer.Transaction.Changesover, Dash_Board.Entity_Layer.Transaction.RDPIP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region MachineOnandOff
        public string BL_MachineOnandOffTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_MachineONandOff", "0", Dash_Board.Entity_Layer.Transaction.MachineGroup , Dash_Board.Entity_Layer.Transaction.MachineName,"", Dash_Board.Entity_Layer.Transaction.Shiftname, Dash_Board.Entity_Layer.Transaction.Time, Dash_Board.Entity_Layer.Transaction.ProblemCode, "", "", Dash_Board.Entity_Layer.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
