using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash_Board.Entity_Layer
{
    public class Transaction
    {

        #region Variables
        static string _machineGroup, _modelName, _time,  _MachineName  ,_Shiftname ,_Type,_ProblemCode,_RefNo ,_NoofItem,_Puls,_Cycletime,_ManPower,_Changesover,_RDPIP;


        public static string Changesover
        {
            get
            {
                return _Changesover;
            }

            set
            {
                _Changesover = value;
            }
        }

        public static string Manpower
        {
            get
            {
                return _ManPower;
            }

            set
            {
                _ManPower = value;
            }
        }
        public static string MachineGroup
        {
            get
            {
                return _machineGroup;
            }

            set
            {
                _machineGroup = value;
            }
        }

        public static string MachineName
        {
            get
            {
                return _MachineName;
            }

            set
            {
                _MachineName = value;
            }
        }

        public static string ModelName
        {
            get
            {
                return _modelName;
            }

            set
            {
                _modelName = value;
            }
        }

       
        public static string ProblemCode
        {
            get
            {
                return _ProblemCode;
            }

            set
            {
                _ProblemCode = value;
            }
        }

       

        

        public static string RefNo
        {
            get
            {
                return _RefNo;
            }

            set
            {
                _RefNo = value;
            }
        }

        public static string Shiftname
        {
            get
            {
                return _Shiftname;
            }

            set
            {
                _Shiftname = value;
            }
        }

        public static string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }

       

        public static string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }
        public static string NoofItem
        {
            get
            {
                return _NoofItem;
            }

            set
            {
               _NoofItem = value;
            }
        }
        public static string  Cycletime
        {
            get
            {
                return _Cycletime;
            }

            set
            {
                 _Cycletime = value;
            }
        }
        public static string Puls
        {
            get
            {
                return _Puls;
            }

            set
            {
                _Puls = value;
            }
        }
        public static string RDPIP
        {
            get
            {
                return _RDPIP;
            }

            set
            {
                _RDPIP = value;
            }
        }
        #endregion

        //#region Properties
        //public static string MachineGroup { get => _machineGroup; set => _machineGroup = value; }
        //public static string ModelName { get => _modelName; set => _modelName = value; }
        //public static string CyscleTime { get => _cyscleTime; set => _cyscleTime = value; }
        //public static string Time { get => _time; set => _time = value; }
        //public static string MachineName { get => _MachineName; set => _MachineName = value; }
        //public static string ProductionPlan { get => _ProductionPlan; set => _ProductionPlan = value; }
        //public static string PLCData { get => _PLCData; set => _PLCData = value; }
        //public static string Client { get => _Client; set => _Client = value; }
        //public static string Shiftname { get => _Shiftname; set => _Shiftname = value; }
        //public static string Type { get => _Type; set => _Type = value; }
        //public static string HourPers { get => _HourPers; set => _HourPers = value; }
        //public static string TotalPers { get => _TotalPers; set => _TotalPers = value; }
        //public static string ProducedTime { get => _ProducedTime; set => _ProducedTime = value; }
        //public static string ProblemCode { get => _ProblemCode; set => _ProblemCode = value; }
        //public static string RefNo { get => _RefNo; set => _RefNo = value; }
        //#endregion
    }
}
