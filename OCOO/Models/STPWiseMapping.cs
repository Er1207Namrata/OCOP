using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class STPWiseMapping:Menu
    {
        public DataTable? dttable {  get; set; }
        public DataSet GetZonewiseSTP()
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@FK_ZoneId", Pk_ZoneId),
                     new SqlParameter("@Fk_EmpId", Fk_EmpId),
                     new SqlParameter("@Fk_DepartmentId", Fk_DepartmentId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetZonewiseSTP, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SaveZonewiseSTP()
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@Fk_ZoneId", Pk_ZoneId),
                     new SqlParameter("@Fk_DeptId", Fk_DepartmentId),
                     new SqlParameter("@Fk_EmpId", Fk_EmpId),
                     new SqlParameter("@AddedBy", AddedBy),
                     new SqlParameter("@typeSTPMapping", dttable)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.STPWiseOfficerMapping, para);
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
                     new SqlParameter("@Fk_ZoneId", Pk_ZoneId),
                     new SqlParameter("@Fk_departmentid", Fk_DepartmentId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetEmployee, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetEmpbyID()
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@Pk_DepartmentId", Fk_DepartmentId == "0" ? null : Fk_DepartmentId),
                     new SqlParameter("@Pk_DesignationId", Fk_DesignationId =="0" ? null: Fk_DesignationId)
                };
                DataSet ds = Connection.ExecuteQuery("Sp_GetEmpByID", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
