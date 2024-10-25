using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class DivisionBillFlowMapping : Menu
    {
        public string Pk_DivMapId { get; set; }
        public string Pk_DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string EmpName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string OfficeName { get; set; }
        public string EmployeeType { get; set; }
        public string Fk_divisionId { get; set; }
        public string EmployeeId { get; set; }
        public string IsForword { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }
        public List<DivisionBillFlowMapping> lstData { get; set; }
        public DataTable DivisionDataTable { get; set; }
        public DataSet ManageDivisionBillFlow()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@ZoneId",Pk_ZoneId),
                new SqlParameter("@DivisionId",Pk_DivisionId),
                new SqlParameter("@Department",DepartmentId),
                new SqlParameter("@Designation",DesignationId == "0" ? null:DesignationId)
                
                };
                ds = Connection.ExecuteQuery("Sp_DivisionBillFlowMapping", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveBillFlowMapping()
        {
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@EmployeeType",EmployeeType),
                new SqlParameter("@ZoneId",Pk_ZoneId),
                new SqlParameter("@DivisionId",Pk_DivisionId),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@DivisionBillFlowMapping",DivisionDataTable)

                };
                DataSet ds = Connection.ExecuteQuery("Sp_ManageDivisionBillFlowMaping", para);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
