using System.Data.SqlClient;
using System.Data;
using OCOO.Models;


namespace OCOO.Models
{
    public class Menu : Common
    {
        public string? MenuId { get; set; }
        public string? MenuName { get; set; }
        public string? SubMenuName { get; set; }
        public int? PermissionId { get; set; }
        public string? Fk_EmpId { get; set; }
        public string? Fk_MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public string? IsDropdown { get; set; }
        public string? LoginId { get; set; }
        public string? UserType { get; set; }
        public string? Password { get; set; }
        public string? IndexNo { get; set; }
        public List<Menu>? menuList { get; set; }
        public List<Menu>? subMenuList { get; set; }
        



        public DataSet GetMenuMaster()
        {
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@MenuId", MenuId),
                    new SqlParameter("@Url", Url),
                    new SqlParameter("@IndexNo", IndexNo),
                    new SqlParameter("@Icon", Icon),
                    new SqlParameter("@IsDropdown", IsDropdown),
                    new SqlParameter("@MenuName", MenuName),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMenuMaster, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetEmployee()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetMenuDetails()
        {
            try
            {
                SqlParameter[] para =
                {
              new SqlParameter("@Fk_MemId", Fk_MemId),
              new SqlParameter("@UserType", UserType),
              new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.BindMenuMaster, para);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
