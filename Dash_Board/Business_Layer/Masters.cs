      using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash_Board.Business_Layer
{
        public class Masters
        {
        #region Objects
        Dash_Board.Data_Layer.DatabaseConnections obj_DB = new Data_Layer.DatabaseConnections();
        #endregion

        #region MachineGroup
     
        public DataSet BL_MachineGroupDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_MachineGroupMaster", 0, Entity_Layer.Masters.MachineGroup, Entity_Layer.Masters.MachineName, "", "", "", Entity_Layer.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ModuleMaster
       
        public DataSet BL_ModuleMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ModuleMaster", 0, Entity_Layer.Masters.MachineGroup, Entity_Layer.Masters.MachineName, "", "", "", "", "", "","", Entity_Layer.Masters.Type,"","");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}


