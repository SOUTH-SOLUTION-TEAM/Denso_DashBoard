using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Dash_Board.Entity_Layer
{
    public static class Masters
    {
        #region Variables
   
        static string _Type,_MachineGroup, _MachineName;
        static DataTable _Dt;

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

        public static string MachineGroup
        {
            get
            {
                return _MachineGroup;
            }

            set
            {
                _MachineGroup = value;
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

        //public static int RefNo { get => _RefNo; set => _RefNo = value; }
        //public static int MachineGrID { get => _MachineGrID; set => _MachineGrID = value; }
        //public static int Cycletime { get => _Cycletime; set => _Cycletime = value; }
        //public static int Time2Stop { get => _Time2Stop; set => _Time2Stop = value; }
        //public static int Piece { get => _Piece; set => _Piece = value; }
        //public static string Type { get => _Type; set => _Type = value; }
        //public static string UserID { get => _UserID; set => _UserID = value; }
        //public static string UserName { get => _UserName; set => _UserName = value; }
        //public static string GroupID { get => _GroupID; set => _GroupID = value; }
        //public static string Password { get => _Password; set => _Password = value; }
        //public static string Rights { get => _Rights; set => _Rights = value; }
        //public static string LoginID { get => _LoginID; set => _LoginID = value; }
        //public static string PartNo { get => _PartNo; set => _PartNo = value; }
        //public static string MachineGroup { get => _MachineGroup; set => _MachineGroup = value; }
        //public static string MachineName { get => _MachineName; set => _MachineName = value; }
        //public static string Status { get => _Status; set => _Status = value; }
        //public static string ModelName { get => _ModelName; set => _ModelName = value; }
        //public static string OperationType { get => _OperationType; set => _OperationType = value; }
        //public static string OperationCode { get => _OperationCode; set => _OperationCode = value; }
        //public static string OperationName { get => _OperationName; set => _OperationName = value; }
        //public static string Shifttiming { get => _Shifttiming; set => _Shifttiming = value; }
        //public static string Break1 { get => _Break1; set => _Break1 = value; }
        //public static string Break2 { get => _Break2; set => _Break2 = value; }
        //public static string Break3 { get => _Break3; set => _Break3 = value; }
        //public static string Break4 { get => _Break4; set => _Break4 = value; }
        //public static string Break5 { get => _Break5; set => _Break5 = value; }
        //public static string ProcessType { get => _ProcessType; set => _ProcessType = value; }
        //public static string ColorName { get => _ColorName; set => _ColorName = value; }
        //public static string Percentage { get => _Percentage; set => _Percentage = value; }
        //public static string ShiftName { get => _ShiftName; set => _ShiftName = value; }
        //public static DataTable Dt { get => _Dt; set => _Dt = value; }
        #endregion
    }
}
