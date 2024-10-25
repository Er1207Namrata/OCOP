using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class InspectionDetails : Menu
    {
        public string? Pk_BillingId { get; set; }
        public string? UserId { get; set; }
        public string? BOD { get; set; }
        public string? COD { get; set; }
        public string? TSS { get; set; }
        public string? FC { get; set; }
        public string? DOCS { get; set; }
        public string? Remarks { get; set; }

        public DataSet UpdateStatus()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_BillingId", Pk_BillingId),
                    new SqlParameter("", UserId),
                    new SqlParameter("", Status),
                    new SqlParameter("", BOD),
                    new SqlParameter("", COD),
                    new SqlParameter("", TSS),
                    new SqlParameter("", FC),
                    new SqlParameter("", DOCS),
                    new SqlParameter("", Remarks),
                    new SqlParameter("", AddedBy),
                    new SqlParameter("", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.BillingApproval, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
