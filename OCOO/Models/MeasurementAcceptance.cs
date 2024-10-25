using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace OCOO.Models
{
    public class MeasurementAcceptance : Menu
    {
       
        public int Fk_StatusId { get; set; }
        public string? Pk_BillingId { get; set; }
        public IFormFile? Documents { get; set; }
        public string? DocumentName { get; set; }
       
        public string? Remark { get; set; }
        public string? flag { get; set; }
        public string? msg { get; set; }

        public DataSet VerifyPendingBill()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Remarks",Remark),
                    new SqlParameter("@DocUrl",DocumentName),
                    new SqlParameter("@Fk_StatusId",Fk_StatusId==0?null:Fk_StatusId),
                    new SqlParameter("@FK_FbillId",Pk_BillingId=="0"?null:Pk_BillingId),
                    new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                };
                ds = Connection.ExecuteQuery(ProcedureName.FirmMeasurementAcceptance, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetViewBillDetail()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId",Pk_BillingId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId),
                    new SqlParameter("@FK_StpId",Fk_STPId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.GetViewBillReportForVerification, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
     
    }


    public class ViewInvoiceDetails 
    {

        public string? STPName { get; set; }
        public string? Capacity { get; set; }
        public int? Pk_STPId { get; set; }
        public string? BillingDate { get; set; }
        public string? ComplaintReceived { get; set; }
        public string? ComplaintResolved { get; set; }
        public string? Pk_BillingId { get; set; }
        public string? UserId { get; set; }
        public string? PumpingBillCycle { get; set; }
        public string? PeekDesignedDischarge { get; set; }
        public string? PeekDesignedFactor { get; set; }
        public string? ConsiderFlow { get; set; }
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
        //public string? PK_InvId { get; set; }
        public string? AmountOfMLDSTP { get; set; }
      
        public string? Remark { get; set; }
        public string? IsFlowUpdated { get; set; }
        public string? UpdatedColor { get; set; }
        public string? FlowUpdatedDate { get; set; }
        public string? FlowUpdatedBy { get; set; }
        public string? Waterdischarge { get; set; }
    
        public string? IsFlowLD { get; set; }
        public string? FlowLDAmount { get; set; }
        public string? IsBODUpdated { get; set; }
        public string? BODUpdatedDate { get; set; }
        public string? BODUpdatedBy { get; set; }
        public string? TestingCountColor { get; set; }
        public string? TestingAgency { get; set; }
        public string? TotalBillAmount { get; set; }
        public string? CRUpdatedBy { get; set; }
        public string? ComplaintLDAmount { get; set; }
        public string? IsCRUpdated { get; set; }
        public string? IsFCLD { get; set; }
        public string? FCUpdatedDate { get; set; }
        public string? IsFCUpdated { get; set; }
        public string? IsTSSLD { get; set; }
        public string? TSSUpdatedDate { get; set; }
        public string? TSSUpdatedBy { get; set; }
        public string? CODUpdatedDate { get; set; }
        public string? IsTSSUpdated { get; set; }
        public string? IsCODUpdated { get; set; }
        public string? FCUpdatedBy { get; set; }
        public string? IsBODLD { get; set; }
        public string? IsSame { get; set; }
        public string? CODUpdatedBy { get; set; }
        public string? TestingCount { get; set; }
        public string? HeaderText { get; set; }
    }
    
}
