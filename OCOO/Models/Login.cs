using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class Login : Menu
    {
        public string? IpAddress { get; set; }
        public int Fk_UserId { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public int Fk_UserTypeId { get; set; }
        public string? EmailId { get; set; }
        public string? GstNo { get; set; }
        public DataSet CheckLogin()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@IpAddress",IpAddress),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Password",Password)
                    
                };
                ds = Connection.ExecuteQuery(ProcedureName.Login,para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet PasswordChange()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                    new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@OldPassword",OldPassword),
                    new SqlParameter("@NewPassword",NewPassword),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ChangePassword, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataSet GetVisitorCount()
        {
            //SqlParameter[] para = {
            //                          new SqlParameter("@LoginId", LoginId),
            //                          new SqlParameter("@Password", Password),
            //                          new SqlParameter("@IPAddress", IPAddress)
            //   };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.VisitorCount);
            return ds;
        }
    }
    public class Profile : Menu
    {
        public string? Pk_AdminId { get; set; }
        public int Fk_UserId { get; set; }
        public int Fk_UserTypeId { get; set; }
        public string? Name { get; set; }
        public IFormFile? ProfilePic { get; set; }
        public string? Documents { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? GSTNo { get; set; }
        public DataSet ProfileUpdate()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                    new SqlParameter("@Pk_AdminId",Pk_AdminId),
                    new SqlParameter("@Name",Name),
                    new SqlParameter("@MobileNo",MobileNo),
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@ProfilePic",Documents),
                    new SqlParameter("@GSTNo",GSTNo),

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ProfilePicAdmin, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet FirmProfileUpdate()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                    new SqlParameter("@Pk_AdminId",Pk_AdminId),
                    new SqlParameter("@Name",Name),
                    new SqlParameter("@MobileNo",MobileNo),
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@ProfilePic",Documents),
                    new SqlParameter("@GSTNo",GSTNo),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ProfilePic, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
