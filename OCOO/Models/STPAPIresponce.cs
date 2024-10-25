using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class STPAPIresponce : Menu
    {
        public int StationId { get; set; }
        public int DeviceId { get; set; }
        public string? Parameter { get; set; }
        public string? Unit { get; set; }
        public string? Timestamp { get; set; }
        public string? Flag { get; set; }
        public string? AddedOn { get; set; }
        public string? IsDeleted { get; set; }
        public string? IsVerified { get; set; }
        public string? VerifiedDate { get; set; }
        public string? FormDate { get; set; }
       
        
        public System.Data.DataTable? dtDetails { get; set; }

        public DataSet GetSTPresponce()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@FormDate",FormDate ==  "0" ? null : FormDate ),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate),
                    new SqlParameter("@Flag",Flag == "0" ? null : Flag),
                    new SqlParameter("@Parameter",Parameter == "0" ? null : Parameter),
                    new SqlParameter("@Unit",Unit == "0" ? null : Unit),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.STPAPIresponce, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
