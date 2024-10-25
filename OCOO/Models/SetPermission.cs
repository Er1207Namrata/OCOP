using System.Data.SqlClient;
using System.Data;
using OCOO.Models;

namespace OCOO.Models
{
    public class SetPermission : Menu
    {
        public string? UserTypeId { get; set; }
        public string? FormSave { get; set; }
        public string? FormView { get; set; }
        public string? FormUpdate { get; set; }
        public string? FormDelete { get; set; }
        public bool IsSaveValue { get; set; }
        public bool IsUpdateValue { get; set; }
        public bool IsSelectValue { get; set; }
        public bool IsDeleteValue { get; set; }
        public string? SelectedValue { get; set; }
        public string? CoupanAmount { get; set; }

        //public List<Common>? BindList { get; set; }
        public string? Fk_EmpId { get; set; }
        public string? Text { get; set; }
        public string? Id { get; set; }
        public string? FK_MenuId { get; set; }
        public string? Checkbox { get; set; }
        public DataTable? UserTypeFormPermission { get; set; }
        public List<SetPermission> lstPermission = new List<SetPermission>();

        public List<SetPermission> BindList = new List<SetPermission>();
        public DataSet GetFormPermission()
        {
            try
            {
                SqlParameter[] para =
                {
                new SqlParameter("@Fk_EmpId", Fk_EmpId),
                new SqlParameter("@MenuId", MenuId),
                new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPemissionData, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet DeletePermission()
        {
            try
            {
                SqlParameter[] para =
                {
                new SqlParameter("@Fk_EmpId", Fk_EmpId),
                new SqlParameter("@MenuId", MenuId),
                new SqlParameter("@AddedBy", AddedBy),
                new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPemissionData, para);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet UserRollPermission()
        {
            try
            {
                SqlParameter[] para =
                {
                new SqlParameter("@UserTypeFormPerMission", UserTypeFormPermission),
                new SqlParameter("@FK_UserTypeId", Fk_EmpId),
                new SqlParameter("@MenuId", MenuId),
                new SqlParameter("@AddedBy", AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UserRollPermission, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet SavePermission()
        {
            try
            {
                SqlParameter[] para =
                {
                
                new SqlParameter("@PermissionId", PermissionId),
                new SqlParameter("@Fk_EmpId", Fk_EmpId),
                new SqlParameter("@MenuId", MenuId),
                new SqlParameter("@AddedBy", AddedBy),
                new SqlParameter("@UserTypeFormPerMission", UserTypeFormPermission),

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UserFormPermission, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet BindMenuDetails()
        {
            try
            {
                SqlParameter[] para =
                {
              new SqlParameter("@Fk_MemId", Fk_MemId),
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
