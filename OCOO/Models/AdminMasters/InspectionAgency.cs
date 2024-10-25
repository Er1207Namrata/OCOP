using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Reflection.Emit;

namespace OCOO.Models
{
    public class InspectionAgency : Menu
    {
         
        public string? Pk_UserId { get; set; }    
        public long FK_InspectionId { get; set; }    
        public string? AgencyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
  
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
        public DataSet ManageInspectionAgency()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_UserId",Pk_UserId),
                    new SqlParameter("@FK_InspectionId",FK_InspectionId),
                    new SqlParameter("@AgencyName", AgencyName),
                    new SqlParameter("@ContactPerson", ContactPerson),
                    new SqlParameter("@MobileNo", MobileNo),
                    new SqlParameter("@Address", Address),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),
                   
                   
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_ManageInspectionAgency, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
