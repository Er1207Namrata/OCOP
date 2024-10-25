using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Reflection.Emit;

namespace OCOO.Models
{
    public class CallCenter:Menu
    {
        public string? ReportingDate  { get; set; }    
        public string? Pk_CallCenterId  { get; set; }    
        public long Fk_StpId { get; set; }    
        public string? Stp { get; set; } 
        public string? TotalReceivedCall { get; set; }    
        public string? TotalResolveCall { get; set; }

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
        public DataSet ManageCallCenter()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_CallCenterId",Pk_CallCenterId),
                    new SqlParameter("@Fk_Stp_Id", Fk_StpId),
                    new SqlParameter("@TotalCallRecived", TotalReceivedCall),
                    new SqlParameter("@TotalCallResolve", TotalResolveCall),
                    new SqlParameter("@ReportingDate", ReportingDate),
                   
                };
                ds = Connection.ExecuteQuery(ProcedureName.ManageCallCenter, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
