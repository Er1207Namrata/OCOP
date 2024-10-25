using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace OCOO.Models
{
    public class ZoneData : Common
    {
        public string? textZone { get; set; }
        public string? valueZone { get; set; }
        public string? SelectedValue { get; set; }
        public bool IsSelectValue { get; set; }

        public DataSet GetZoneData()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@EmpId", Value)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_EmpZone, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetEmpZoneData()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@EmpId", Value)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetEmpZone, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
