using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace OCOO.Models
{
    public class DailyCapacity : Menu
    {
        public string? Pk_STCDailyCapacity { get; set; }
        public string? STPName { get; set; }
        public string? EntryDate { get; set; }
        public string? AmountOf72MLDStp { get; set; }
        public string? FlowCapacity { get; set; }
        public string? FlowActualAchived { get; set; }
        public string? TreatedForPaymentMLD { get; set; }
        public string? FixChargeAsPerCB { get; set; }
        public string? FixChargeAsPerActual { get; set; }
        public string? VariableChargesAsPerCB { get; set; }
        public string? VariableChargesAsperActual { get; set; }
        public string? BODReportedValue { get; set; }
        public string? BOD50 { get; set; }
        public string? BODLD { get; set; }
        public string? CODReportedValue { get; set; }
        public string? COD15 { get; set; }
        public string? CODLD { get; set; }
        public string? TSSReportedValue { get; set; }
        public string? TSS25 { get; set; }
        public string? TSSLD { get; set; }
        public string? FCReportedValue { get; set; }
        public string? FC10 { get; set; }
        public string? FCLD { get; set; }
        public string? LD { get; set; }
        public string? AfterDeductedParaAmt { get; set; }
        public bool SaveAsDraft { get; set; }
        public bool SavePermanent { get; set; }
        public string? Pk_ApproveId { get; set; }
        public string? UniqueId { get; set; }
        public string? IsPermanent { get; set; }
        public string? IsApproved { get; set; }
        public string? IsDeclined { get; set; }
        public string? Pk_DocumentId { get; set; }
        public IFormFile? CoveringLetter { get; set; }
        public IFormFile? InspectionReport { get; set; }
        public IFormFile? TPIReports { get; set; }
        public IFormFile? LDSheet { get; set; }
        public IFormFile? OtherDocuments { get; set; }
        public string? CoveringLetterURL { get; set; }
        public string? InspectionReportURL { get; set; }
        public string? TPIReportsURL { get; set; }
        public string? LDSheetURL { get; set; }
        public string? OtherDocumentsURL { get; set; }
        
        public DataSet GetSTC()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_STCDailyCapacity", string.IsNullOrEmpty(Pk_STCDailyCapacity) ? "0" : Pk_STCDailyCapacity),
                    new SqlParameter("@EntryDate", string.IsNullOrEmpty(EntryDate)? null : EntryDate),
                    new SqlParameter("@AmountOf72MLDStp", string.IsNullOrEmpty(AmountOf72MLDStp) ? 0 : AmountOf72MLDStp),
                    new SqlParameter("@FlowCapacity", string.IsNullOrEmpty(FlowCapacity) ? 0 : FlowCapacity),
                    new SqlParameter("@FlowActualAchived", string.IsNullOrEmpty(FlowActualAchived) ? 0 : FlowActualAchived),
                    new SqlParameter("@TreatedForPaymentMLD", string.IsNullOrEmpty(TreatedForPaymentMLD) ? 0 : TreatedForPaymentMLD),
                    new SqlParameter("@FixChargeAsPerCB", string.IsNullOrEmpty(FixChargeAsPerCB) ? 0 : FixChargeAsPerCB),
                    new SqlParameter("@FixChargeAsPerActual", string.IsNullOrEmpty(FixChargeAsPerActual) ? 0 : FixChargeAsPerActual),
                    new SqlParameter("@VariableChargesAsPerCB", string.IsNullOrEmpty(VariableChargesAsPerCB)     ? 0 : VariableChargesAsPerCB),
                    new SqlParameter("@VariableChargesAsperActual", string.IsNullOrEmpty(VariableChargesAsperActual) ? 0 : VariableChargesAsperActual),
                    new SqlParameter("@BODReportedValue", string.IsNullOrEmpty(BODReportedValue) ? 0 : BODReportedValue),
                    new SqlParameter("@BOD50", string.IsNullOrEmpty(BOD50) ? 0 : BOD50),
                    new SqlParameter("@BODLD", string.IsNullOrEmpty(BODLD) ? 0 : BODLD),
                    new SqlParameter("@CODReportedValue", string.IsNullOrEmpty(CODReportedValue) ? 0 : CODReportedValue),
                    new SqlParameter("@COD15",  string.IsNullOrEmpty(COD15) ? 0 : COD15),
                    new SqlParameter("@CODLD", string.IsNullOrEmpty(CODLD) ? 0 : CODLD),
                    new SqlParameter("@TSSReportedValue", string.IsNullOrEmpty(TSSReportedValue) ?  0 : TSSReportedValue),
                    new SqlParameter("@TSS25", string.IsNullOrEmpty(TSS25) ? 0 : TSS25),
                    new SqlParameter("@TSSLD", string.IsNullOrEmpty(TSSLD) ? 0 : TSSLD),
                    new SqlParameter("@FCReportedValue", string.IsNullOrEmpty(FCReportedValue) ? 0 : FCReportedValue),
                    new SqlParameter("@FC10", string.IsNullOrEmpty(FC10) ? 0 : FC10),
                    new SqlParameter("@FCLD", string.IsNullOrEmpty(FCLD) ? 0 : FCLD),
                    new SqlParameter("@AfterDeductedParaAmt", string.IsNullOrEmpty(AfterDeductedParaAmt) ? 0 : AfterDeductedParaAmt),
                    //new SqlParameter("@LD", string.IsNullOrEmpty(LD)? 0 : LD),
                    new SqlParameter("@LD", LD == "" || LD == "NaN" ? 0 : LD),
                    new SqlParameter("@SaveAsDraft", SaveAsDraft),
                    new SqlParameter("@UniqueId", UniqueId),
                    new SqlParameter("@SavePermanent", SavePermanent),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@OpCode", OpCode)
                   
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetSTCDailyCapacity, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetFirmRequest()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_ApproveId", Pk_ApproveId),
                    new SqlParameter("@Fk_UniqueId", UniqueId),
                    new SqlParameter("@Fk_ZoneId", Pk_ZoneId),
                    new SqlParameter("@Fk_CityId", Pk_CityId),
                    new SqlParameter("@Month", Fk_MonthId == "0" ? null:Fk_MonthId),
                    new SqlParameter("@Fk_DesignationId", Fk_DesignationId),
                    new SqlParameter("@IsApproved", IsApproved),
                    new SqlParameter("@IsDeclined", IsDeclined),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@BlockName", Pk_ZoneId),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetApproveSTC, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FirmRequestList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@AddedBy", AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FirmRequestList, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetApprovalStatus()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_UsertypeId", Fk_UsertypeId),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Fk_UniqueId", UniqueId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetApprovalStatus, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetUploadDocuments()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_DocumentId", Pk_DocumentId),
                    new SqlParameter("@CoveringLetterURL", CoveringLetterURL),
                    new SqlParameter("@InspectionReportURL", InspectionReportURL),
                    new SqlParameter("@TPIReportsURL", TPIReportsURL),
                    new SqlParameter("@LDSheetURL", LDSheetURL),
                    new SqlParameter("@OtherDocumentsURL", OtherDocumentsURL),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Fk_UniqueId", UniqueId),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DocumentsMaster, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
