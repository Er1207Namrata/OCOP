using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Reflection.Emit;

namespace OCOO.Models
{
    public class PumpingStation:Menu
    {
        public string? Pk_PumpingStationId { get; set; }    
        public string? FK_FirmId { get; set; }    
        public long Fk_StpId { get; set; }    
        public string? PumpingStationName { get; set; } 
        public string? AccountNumber { get; set; }    
      

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
        public DataSet ManagePumpingStation()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_PumpingStationId",Pk_PumpingStationId),
                    new SqlParameter("@FK_FirmId", FK_FirmId),
                    new SqlParameter("@Name", PumpingStationName),
                    new SqlParameter("@AccountNumber", AccountNumber),
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),
                   
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_PumpingStation, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
