using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class OCMSSheet:Menu
    {
        public int PK_FbillId { get; set; }
        public string? UserId { get; set; }
        public string? Type { get; set; }

        public DataSet GetOCMSSheet()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Pk_ZoneId == "0" ? null : Pk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.Sp_OCMSSheet, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet StpLIstAdmin()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_ZoneId", Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Pk_STPId", Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Type", Type)

                };
                DataSet ds = Connection.ExecuteQuery("StpLIst", para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
