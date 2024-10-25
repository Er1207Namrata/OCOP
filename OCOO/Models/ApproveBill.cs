using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class ApproveBill :Menu
    {
        public string? UserId { get; set; }
       
        public DataSet GetBills()
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
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)
                };
                DataSet ds = Connection.ExecuteQuery("Sp_ApprovedBill", para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
