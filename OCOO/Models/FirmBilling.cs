using System.Data.SqlClient;
using System.Data;
using NuGet.Common;

namespace OCOO.Models
{
    public class FirmBilling : Menu
    {
        public string? Pk_STPId { get; set; }
        public string? Year { get; set; }
        public string? BillingDate { get; set; }
        public string? MLDDay { get; set; }
        public string? Capacity { get; set; }
        public string? Actual_Achieved { get; set; }
        public string? Treated_for_Payement { get; set; }
        public string? AsPerCBFixCharges_60 { get; set; }
        public string? AsPerActual_FixCharges_60 { get; set; }
        public string? AsPerCBFixCharges_40 { get; set; }
        public string? AsPerActual_FixCharges_40 { get; set; }
        public string? BODReportedValue { get; set; }
        public string? BODAmount { get; set; }
        public string? BODLDAmount { get; set; }
        public string? CODReportedValue { get; set; }
        public string? CODAmount { get; set; }
        public string? CODLDAmount { get; set; }
        public string? TSSReportedValue { get; set; }
        public string? TSSAmount { get; set; }
        public string? TSSLDAmount { get; set; }
        public string? FCReportedValue { get; set; }
        public string? FCAmount { get; set; }
        public string? FCLDAmount { get; set; }
        public string? TotalVerifiedAmount { get; set; }
        public string? TotalLDAmount { get; set; }
        public string? IsFinalSave { get; set; }
        public IFormFile? CoveringLetter { get; set; }
        public IFormFile? InspectionReport { get; set; }
        public IFormFile[]? TPIReports { get; set; }
        public IFormFile[]? LDSheet { get; set; }
        public IFormFile[]? OtherDocuments { get; set; }
        public string? Remarks { get; set; }        
        public string? Isdeclaration { get; set; }
        public string? ComplaintsReceived24 { get; set; }
        public string? ComplaintsResolved24 { get; set; }
        public string? ComplaintsReceivedInBillingMonth { get; set; }
        public string? ComplaintsResolvedInBillingMonth { get; set; }
        public string? LDBondedComplaints { get; set; }
        public string? BalanceComplaints { get; set; }
        public string? CoveringLetterUrl { get; set; }
        public string? InspectionReportUrl { get; set; }
        public string? TPIReportsUrl { get; set; }
        public string? LDSheetUrl { get; set; }
        public string? OtherDocumentsUrl { get; set; }
        public string? Fk_BillId { get; set; }
        public string? Electricity { get; set; }
        public string? Measurement { get; set; }
        public string? DG { get; set; }
        public DataTable? MultipleImage { get; set; }
        public int? TPIReportsCount { get; set; }
        public int? LDReportCount { get; set; }
        public int? OtherDocumentsCount { get; set; }
        public string? Files { get; set; }   
        public string? tokenNo { get; set; }
        public string? PK_FTDId { get; set; }
        public string? Remark { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? DocFormat { get; set; }
        public List<FirmBilling>? Tpidetail { get; set; }

        public DataSet GetBillRequestList()
        {
            try
            {
                SqlParameter[] para =
               {        new SqlParameter("@Fk_FirmId", Fk_FirmId),
                        new SqlParameter("@Fk_MonthId", Fk_MonthId),
                        new SqlParameter("@Year", Year),
                        new SqlParameter("@AddedBy", AddedBy),
                        new SqlParameter("@OpCode", OpCode),
                        new SqlParameter("@Fk_ZoneId", Fk_ZoneId)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_FirmPendingBills, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDailyBillingReport()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Fk_MonthId", Fk_MonthId),
                    new SqlParameter("@Year", Year),
                    new SqlParameter("@fromDate", FromDate),
                    new SqlParameter("@toDate", ToDate),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_DailyBillReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetSTPBillSummary()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@FK_InvId", PK_InvId),
                    new SqlParameter("@AddedBy", AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetSTPBillSummary, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GenerateBill()
        {
            try
            {
                SqlParameter[] para =
               {          new SqlParameter("@FK_FirmId", Fk_FirmId),
                          new SqlParameter("@FK_FbillId", Fk_BillId),
                          new SqlParameter("@BillingDate", BillingDate),
                          new SqlParameter("@Remarks", Remarks),
                          new SqlParameter("@AddedBy", AddedBy),
                          new SqlParameter("@CoveringLetter", CoveringLetterUrl),
                          new SqlParameter("@InspectionReport", InspectionReportUrl),
                          new SqlParameter("@TPIReports", TPIReportsUrl),
                          new SqlParameter("@LDSheet", LDSheetUrl),
                          new SqlParameter("@OtherDocuments", OtherDocumentsUrl),
                          new SqlParameter("@IsFinalSave", IsFinalSave),
                          new SqlParameter("@Isdeclaration", Isdeclaration),
                          new SqlParameter("@ComplaintsReceived24", ComplaintsReceived24),
                          new SqlParameter("@ComplaintsResolved24", ComplaintsResolved24),
                          new SqlParameter("@ComplaintsReceivedInBillingMonth", ComplaintsReceivedInBillingMonth),
                          new SqlParameter("@ComplaintsResolvedInBillingMonth", ComplaintsResolvedInBillingMonth),
                          new SqlParameter("@LDBondedComplaints", LDBondedComplaints),
                          new SqlParameter("@BalanceComplaints", BalanceComplaints),
                          new SqlParameter("@FK_UserTypeId", Fk_UsertypeId),
                          new SqlParameter("@MultipleImage", MultipleImage),
                          new SqlParameter("@TokenNo", tokenNo)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GenerateFirmBill, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetFirmBillSummary()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@FK_FbillId", Fk_BillId),
                    new SqlParameter("@AddedBy", AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetFirmBillSummary, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetStatus()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Fk_BillId", Fk_BillId),
                    new SqlParameter("@Year", Year),
                    new SqlParameter("@MonthName", Fk_MonthId),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetStatus, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetElectricityBillReport()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@Monthname", Months),
                    new SqlParameter("@Year", Year),
                    new SqlParameter("@Fk_BillId", Fk_BillId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ElectricityBillReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetDGBillReport()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@Monthname", Months),
                    new SqlParameter("@Year", Year),
                                        new SqlParameter("@Fk_BillId", Fk_BillId)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DGBillReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet BillApprovalDocument()
        {
            try
            {
                SqlParameter[] para =
               {          
                          new SqlParameter("@BillId", Fk_BillId),                         
                          new SqlParameter("@AddedBy", AddedBy),                      
                          new SqlParameter("@Remark", Remarks),                      
                          new SqlParameter("@MultipleImage", MultipleImage)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.BillApprovalDocument, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet Save_Document()        
        {
            try
            {
                SqlParameter[] para =
                {         new SqlParameter("@PK_FTDId", PK_FTDId),
                          new SqlParameter("@FirmId", Fk_FirmId),
                          new SqlParameter("@FK_BillId", Fk_BillId),                         
                          new SqlParameter("@DocumentTypeId",DocumentTypeId),
                          new SqlParameter("@DocUrl",Files),
                          new SqlParameter("@Remarks", Remark),
                          new SqlParameter("@TokenNo", tokenNo),
                          new SqlParameter("@AddedBy",AddedBy),
                          new SqlParameter("@OpCode", OpCode),                                                
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_FirmTPIDocuments, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFirmTpiToken()
        {
            try
            {

                DataSet ds = Connection.ExecuteQuery(ProcedureName.FirmRegistrationToken);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
