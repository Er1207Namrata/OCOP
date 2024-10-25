using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class AgencyDashboard : Menu
    {
        public List<CompliantList>? BarChartList { get; set; }
        public string? Fk_InsAgencyId { get; set; }
        public string? Fk_InspectionTypeId { get; set; }
        public List<ComplaintData>? listComplaint { get; set; }
        public List<LDCountData>? listLDCount { get; set; }
        public LDCountData? PieChartLDCountData { get; set; }

        public DataSet GetAgencyDashboard()
        {
			try
			{
				SqlParameter[] para =
				{
					new SqlParameter("@Fk_InsAgencyId", Fk_InsAgencyId),
					new SqlParameter("@Fk_InspectionTypeId", Fk_InspectionTypeId),
					new SqlParameter("@Fk_STPId", Fk_STPId),
					new SqlParameter("@Fk_MonthId", Fk_MonthId),
					new SqlParameter("@FromDate", FromDate),
					new SqlParameter("@ToDate", ToDate)
				};
				DataSet ds = Connection.ExecuteQuery(ProcedureName.AgencyDashboard, para);
				return ds;
			}
			catch (Exception)
			{
				throw;
			}
        }

        public DataSet GetMasterData()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@OpCode", OpCode),
                new SqlParameter("@Value", Value)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetChartData()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_InsAgencyId", Fk_InsAgencyId),
                    new SqlParameter("@Fk_InspectionTypeId", Fk_InspectionTypeId),
                    new SqlParameter("@Fk_STPId", Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@Fk_MonthId",  Fk_MonthId == "0" ? null : Fk_MonthId),

                };

                DataSet dataSet = new DataSet();
                dataSet = Connection.ExecuteQuery(ProcedureName.AgencyDashboardGraph, para);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
