using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace OCOO.Models
{
    public class STP_Billing
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Moble { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }

        public DataSet Thirdparty_Login()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@Password",Password)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.Thirdparty_Login, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
