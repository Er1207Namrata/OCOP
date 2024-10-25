using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace OCOO.Models
{
    public class PumpingStationReport:Menu
    {
        public DataSet GetPumpingStationReport()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Fk_StpId",Fk_STPId=="0"?null:Fk_STPId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.GetPumpingstation, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
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
        public DataSet GetPumpingStationDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@Fk_StpId",Fk_STPId=="0"?null:Fk_STPId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.PumpingStationDetailsByAdmin, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
