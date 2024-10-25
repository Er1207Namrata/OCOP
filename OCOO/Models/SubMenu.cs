using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class SubMenu : Menu
    {
        public DataSet SubMenuMaster()
        {
            try
            {
                SqlParameter[] para =
                {
               
                new SqlParameter("@Pk_SubMenuId", SubMenuId),
                new SqlParameter("@FK_MenuId", MenuId),
                new SqlParameter("@SubMenuName", SubMenuName),
                new SqlParameter("@AddedBy", AddedBy),
                new SqlParameter("@OpCode", OpCode),
                new SqlParameter("@Url", Url),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetSubMenuMaster, para);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
