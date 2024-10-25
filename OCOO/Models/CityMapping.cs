using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{   
    public class CityMapping : Menu
    {
        public string Pk_MappingId { get; set; }
        public string Pk_CityId { get; set; }
        public string CityName { get; set; }      
        public string EmployeeId { get; set; }
        public string IsChecked { get; set; }
        public DataTable? CityDataTable { get; set; }
        public string? SelectedValue { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }
        public List<CityMapping> lstData { get; set; }

        
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
       
        public DataSet SaveCityMapping()
        {
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@EmpId",EmployeeId),
                new SqlParameter("@ZoneId",Pk_ZoneId),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@CityMapping",CityDataTable)
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
