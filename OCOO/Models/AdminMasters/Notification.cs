using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class Notification
    {
        public string? Firm { get; set; }
        public string? Time { get; set; }
        public string? STP { get; set; }
        public string? Capacity { get; set; }
        public string? Row_count { get; set; }
        public string? Zone { get; set; }
        public string? DesignCapacity { get; set; }
        public string? Fk_EmpId { get; set; }
        public string? Fk_userTypeId { get; set; }
        public string? Message { get; set; }
        public string? Fk_FirmId { get; set; }
        public string? Fk_ZoneId { get; set; }
        public DataSet GetNotification()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@Fk_FirmId", Fk_FirmId),
                new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                new SqlParameter("@Fk_userTypeId", Fk_userTypeId),
                new SqlParameter("@Fk_EmpId", Fk_EmpId)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetNotification, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }    
    }
}
