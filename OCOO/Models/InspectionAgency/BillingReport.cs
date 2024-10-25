using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace OCOO.Models
{
    public class BillingReport:Menu
    {
        public string? BillStatus { get; set; }
        public string? Remark { get; set; }
        public string? Pk_StpBillindId { get; set; }
        public bool IsTerm { get; set; }
        public IFormFile? DocumentFile { get; set; }
        public string? DocumentUrl { get; set; }

        public DataSet GetBillingReport()
        {
            try
            {
                SqlParameter[] para =
                {
               
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBellReportForAgency, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataSet UpdateStatus()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Status", BillStatus),
                    new SqlParameter("@Remark", Remark),
                    new SqlParameter("@DocumentUrl", DocumentUrl),
                    new SqlParameter("@Pk_BillingId", Pk_StpBillindId),
                    new SqlParameter("@FK_UserId", AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateBillingStatus, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
