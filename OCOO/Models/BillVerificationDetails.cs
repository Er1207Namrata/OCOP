using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class BillVerificationDetails 
    {
        public string? VerifiedRemarks { get; set; }
        public string? VerifiedDoc { get; set; }
        public string? StpName { get; set; }
        public string? Status { get; set; }
        public string? Capacity { get; set; }
        public string? HeaderText { get; set; }
        public string? Pk_BillingId { get; set; }
        public string? DocFormat { get; set; }
        public string? UserType { get; set; }
  
        public DataSet GetFirmVerificationDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId",Pk_BillingId),
                };
                ds = Connection.ExecuteQuery(ProcedureName.GetFirmVerificationDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
