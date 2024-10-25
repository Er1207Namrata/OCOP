using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models.AdminMasters
{
    public class CheckDetails
    {
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Flag { get; set; }
        public DataSet GetEmail()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@Email", Email)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CheckEmail, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
