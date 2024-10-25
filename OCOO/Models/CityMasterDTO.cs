using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class CityMasterDTO : Menu
    {
        public int CityId { get; set; }
        public int ZoneId { get; set; }
        public string? City { get; set; }          
        public DataSet GetCities(int EmpId)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = { new SqlParameter("@EmpId", EmpId) };
                ds = Connection.ExecuteQuery(ProcedureName.GetCities, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
