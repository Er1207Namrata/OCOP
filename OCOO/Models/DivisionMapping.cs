using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class DivisionMapping: Menu
    {
        public string Pk_DivMapId { get; set; }
        public string Pk_DivisionId { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }
        public string DivisionName { get; set; }
        public string Fk_divisionId { get; set; }
        public string EmployeeId { get; set; }
        public string IsChecked { get; set; }
        public DataTable? DivisionDataTable { get; set; }
        public string? SelectedValue { get; set; }
        public List<DivisionMapping> lstData { get; set; }

        public DataSet GetDivisionMapping()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para = {
                new SqlParameter("@Emp_Id",Fk_EmpId)
                };
                ds = Connection.ExecuteQuery("Sp_DivistionMapping",para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCityMapping()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para = {
                new SqlParameter("@Emp_Id",Fk_EmpId)
                };
                ds = Connection.ExecuteQuery("sp_CityMapping", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveMapping()
        {
            try
            {
                SqlParameter[] para = { 
                new SqlParameter("@EmpId",EmployeeId),
                new SqlParameter("@ZoneId",Pk_ZoneId),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@DivisionMapping",DivisionDataTable)
                
                };
               DataSet ds = Connection.ExecuteQuery("Sp_SaveDivisionMapping", para);
               return ds;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveCityMapping()
        {
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@EmpId",EmployeeId),
                new SqlParameter("@ZoneId",Pk_ZoneId),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@CityMapping",DivisionDataTable)
                };
                DataSet ds = Connection.ExecuteQuery("sp_SaveCityMapping", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
