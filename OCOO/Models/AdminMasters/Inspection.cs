using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Reflection.Emit;

namespace OCOO.Models
{
    public class Inspection:Menu
    {
         
        public string? PK_InspectionId { get; set; }    
        public long FK_InspectionTypeId { get; set; }    
        public string? InspectionName { get; set; }
        //public IFrom DocumentFile { get; set; }

        public DataSet GetMasterData()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@OpCode", OpCode),
                new SqlParameter("@Value", Value)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ManageInspection()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@PK_InspectionId",PK_InspectionId),
                    new SqlParameter("@FK_InspectionTypeId",FK_InspectionTypeId),
                    new SqlParameter("@Name", InspectionName),
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),
                   
                   
                };
                ds = Connection.ExecuteQuery(ProcedureName.SPInspectionMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
