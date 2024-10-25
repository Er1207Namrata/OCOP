using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace OCOO.Models
{
    public class DailyData:Menu
    {
        public string? STPName { get; set; }
        public string? Fk_MonthId { get; set; }
        public string? DetailType { get; set; }
        public string? JsonString { get; set; }
        public string? IsPermanent { get; set; }
        public string? Fk_UniqueId { get; set; }   
        public string? EntryDate { get; set; }   
        public string? AmountOf72MLDStp { get; set; }   
        public string? FlowCapacity { get; set; }   
        public string? FlowActualAchived { get; set; }   
        public string? BODReportedValue { get; set; }   
        public string? CODReportedValue { get; set; }   
        public string? TSSReportedValue { get; set; }   
        public string? FCReportedValue { get; set; }   
        public string? ComplaintReceived { get; set; }   
        public string? ComplaintResolved { get; set; }   
        public string? Remarks { get; set; }
        public bool SaveAsDraft { get; set; }
        public bool SavePermanent { get; set; }
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
        public string? Pk_DocumentId { get; set; }               
        public string? Pk_FirmId { get; set; }  
        public string? FirmName { get; set; }  
        public string? ContactPerson { get; set; }  
        public string? MobileNo { get; set; }  
        public string? Address { get; set; }  
        public string? Password { get; set; }
        public string? LoginId { get; set; }
        public string? Fk_STPId { get; set; }

        public DataSet ManageFirm()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_FirmId",Pk_FirmId),
                    new SqlParameter("@Pk_ZoneId",Pk_ZoneId),
                    new SqlParameter("@Fk_CityId",Pk_CityId=="0"?null:Pk_CityId),
                    new SqlParameter("@Fk_designstionId",Fk_DesignationId),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@FirmName", FirmName),
                    new SqlParameter("@ContactPerson", ContactPerson),
                    new SqlParameter("@MobileNo",MobileNo),
                    new SqlParameter("@Address", Address),
                    new SqlParameter("@Fk_STPId", Fk_STPId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.ManageFirmMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet UploadDailydata()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@UniqueId",Fk_UniqueId),
                    new SqlParameter("@EntryDate",EntryDate),
                    new SqlParameter("@AmountOf72MLDStp",AmountOf72MLDStp),
                    new SqlParameter("@FlowCapacity",FlowCapacity),
                    new SqlParameter("@FlowActualAchived",FlowActualAchived),
                    new SqlParameter("@BODReportedValue",BODReportedValue),
                    new SqlParameter("@CODReportedValue",CODReportedValue),
                    new SqlParameter("@TSSReportedValue",TSSReportedValue),
                    new SqlParameter("@FCReportedValue",FCReportedValue),
                    new SqlParameter("@ComplaintReceived",ComplaintReceived),
                    new SqlParameter("@ComplaintResolved",ComplaintResolved),
                    new SqlParameter("@Remarks",Remarks),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@SaveAsDraft",SaveAsDraft),
                    new SqlParameter("@SavePermanent",SavePermanent),
                    new SqlParameter("@OpCode",OpCode)
                };
                ds = Connection.ExecuteQuery(ProcedureName.ManageSTPMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
        public DataSet GetSTCDailyData()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_MonthId",Fk_MonthId),
                    new SqlParameter("@AddedBy",AddedBy)
                   
                };
                ds = Connection.ExecuteQuery(ProcedureName.GetSTPDetails, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
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
                    new SqlParameter("@Fk_UniqueId", Fk_UniqueId),
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

