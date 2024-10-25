using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace OCOO.Models
{
    public class InvoiceDetails : FirmDetails
    {

        public string? ZoneName { get; set; }
        public string? StpName { get; set; }
        public string? Inv_Month { get; set; }
        public string? MonthName { get; set; }
        public string? Inv_Year { get; set; }
        public string? ActualAchieved { get; set; }
        public string? CBFixCharges_60 { get; set; }
        public string? Actual_FixCharges_60 { get; set; }
        public string? TotalBODReportedValue { get; set; }
        public string? TotalBODAmount { get; set; }
        public string? TotalBODLDCount { get; set; }
        public string? TotalBODLDAmount { get; set; }
        public string? TotalCODReportedValue { get; set; }
        public string? TotalCODAmount { get; set; }
        public string? TotalCODLDCount { get; set; }
        public string? TotalCODLDAmount { get; set; }
        public string? TotalTSSReportedValue { get; set; }
        public string? TotalTSSAmount { get; set; }
        public string? TotalTSSLDCount { get; set; }
        public string? TotalTSSLDAmount { get; set; }
        public string? TotalFCReportedValue { get; set; }
        public string? TotalFCAmount { get; set; }
        public string? TotalFCLDCount { get; set; }
        public string? TotalFCLDAmount { get; set; }
        public string? BillStatus { get; set; }
        public string? IsFlowUpdated { get; set; }
        public string? UpdatedColor { get; set; }
        public DataSet GetInvoiceDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Month",Fk_MonthId== "0" ? null : Fk_MonthId),
                    new SqlParameter("@Year",Year=="0"?null:Year),
                    new SqlParameter("@BillStatus",BillStatus=="0"?null:BillStatus),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate)
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_ZonewisePendingBills, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetViewBillDetail()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId",Pk_BillingId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_ViewBillReport, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ViewBillReportOCEMS()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId",Pk_BillingId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.ViewBillReportOCEMS, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPrintBill()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_ZoneId",Fk_ZoneId== "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Month",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@Year",Year == "0" ? null : Year)
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_PrintZoneBills, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSTPReport()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@FK_STPId",Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Month",Fk_MonthId== "0" ? null : Fk_MonthId),
                    new SqlParameter("@Year",Year),
                    new SqlParameter("@BillStatus",BillStatus),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@UserId",Fk_EmpId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_STPwisePendingBills, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBillDoc()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId", BillNo),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBillDocuments, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetBillApprovalDocument()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId", BillNo),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBillApprovalDocument, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet DeleteApprovalDocument(int id, int AddedBy)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@AddedBy", AddedBy),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteApprovalDocument, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet BillAssignToFirm(int BillId, string Remark, int AddedBy)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@BillId", BillId),
                    new SqlParameter("@Remark", Remark),
                    new SqlParameter("@AddedBy", AddedBy),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.BillAssignToFirm, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
