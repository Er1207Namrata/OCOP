using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;

namespace OCOO.Models
{
    public class FirmDashboard: Menu
    {
        public List<ComplaintData>? listComplaint { get; set; }
        public List<LDCountData>? listLDCount { get; set; }
        public LDCountData? PieChartLDCountData { get; set; }
        public DataTable? dtBillData { get; set; }
        public string? CurrentYear { get; set; }
        public string? IsGstUpdated { get; set; }
        public DataSet GetFirmDashbord()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_FirmId",Fk_FirmId),
                    new SqlParameter("@Fk_StpFirmId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Fk_MonthId", Fk_MonthId=="0"?null:Fk_MonthId),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@CurrentYear", CurrentYear),
                    new SqlParameter("@Months", Months),
                    new SqlParameter("@Years", Years),
                    //new SqlParameter("@IsGstUpdated",IsGstUpdated),
                };

                DataSet dataSet = new DataSet();
                dataSet = Connection.ExecuteQuery(ProcedureName.FirmDashoardData, para);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetChartData()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_StpFirmId", Fk_STPId),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@Fk_MonthId",  Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@Years", Years),
                    new SqlParameter("@Months", Months)

                };

                DataSet dataSet = new DataSet();
                dataSet = Connection.ExecuteQuery(ProcedureName.FirmDashboardGraph, para);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class ComplaintData
    {
        public string? ReceivedCall { get; set; }    
        public string? ResolvedCall { get; set; }    
        public string? PendingCall { get; set; }    
        public string? TotalCall { get; set; }    
        public string? MonthName { get; set; }    
    }
    public class LDCountData
    {
        public string? BODCount { get; set; }
        public string? MonthName { get; set; }
        public string? CODCount { get; set; }
        public string? TSS { get; set; }
        public string? FC { get; set; }
        public string? TotalLDAmount { get; set; }
    }
}
