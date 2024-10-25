using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace OCOO.Models.FirmMasters
{
    public class Deduction : Menu 
    {
        public int DesignationId { get; set; }
        public string? Pk_DeductionId { get; set; }
        public string? DeductionDate { get; set; }
        public string? ElectricityCutStartTime { get; set; }
        public string? ElectricityCutEndTime { get; set; }
        public string? MeterReading { get; set; }
        public string? DGStartTime { get; set; }
        public string? DGEndTime { get; set; }
        public string? DieselConsumes { get; set; }
        public string? DieselRateInMonth { get; set; }
        public string? Pk_STPId { get; set; }
        public string? Fk_BillId { get; set; }
        public string? PK_IPS { get; set; }
        public string? TotalDGDuration { get; set; }
        public string? NetAmount { get; set; }
        public string? PK_ElectricityId { get; set; }
        public string? MonthStartDate { get; set; }
        public string? MonthEndDate { get; set; }
        public string? StartMeterReading { get; set; }
        public string? EndMeterReading { get; set; }
        public string? ElectricityUnit { get; set; }
        public string? UnitRate { get; set; }
        public string? FixCharges { get; set; }
        public string? OtherCharges { get; set; }
        public string? Penalty { get; set; }
        public string? Remark { get; set; }
       public string? IsModified { get; set; }
        public string? Attachment { get; set; }
        public string? AttachmentURL { get; set; }
        public IFormFile? AttachmentDoc { get; set; }
        public string? Declaration { get; set; }
        public string? STP { get; set; }
        public string? MainPumpingStation { get; set; }
        public string LatestBillingDate { get; set; }
        public DataSet SaveDeduction()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_DeductionId", Pk_DeductionId),
                    new SqlParameter("@DeductionDate", DeductionDate),
                    new SqlParameter("@ElectricityCutStartTime", ElectricityCutStartTime),
                    new SqlParameter("@ElectricityCutEndTime", ElectricityCutEndTime),
                    new SqlParameter("@MeterReading", MeterReading),
                    new SqlParameter("@DGStartTime", DGStartTime),
                    new SqlParameter("@DGEndTime", DGEndTime),
                    new SqlParameter("@DieselConsumes", DieselConsumes),
                    new SqlParameter("@DieselRateInMonth", DieselRateInMonth),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@FK_IPS", PK_IPS),
                    new SqlParameter("@Fk_STPId", Pk_STPId),
                    new SqlParameter("@TotalDGDuration", TotalDGDuration),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Narration", Remark)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveDeductionRelease, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet UpdateDeduction()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_DeductionId", Pk_DeductionId),
                    new SqlParameter("@DeductionDate", DeductionDate),
                    new SqlParameter("@ElectricityCutStartTime", ElectricityCutStartTime),
                    new SqlParameter("@ElectricityCutEndTime", ElectricityCutEndTime),
                    new SqlParameter("@MeterReading", MeterReading),
                    new SqlParameter("@DGStartTime", DGStartTime),
                    new SqlParameter("@DGEndTime", DGEndTime),
                    new SqlParameter("@DieselConsumes", DieselConsumes),
                    new SqlParameter("@DieselRateInMonth", DieselRateInMonth),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@FK_IPS", PK_IPS),
                    new SqlParameter("@Fk_STPId", Pk_STPId),
                    new SqlParameter("@TotalDGDuration", TotalDGDuration),
                    new SqlParameter("@Narration", Remark)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateDeductionRelease, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet DeleteDeduction()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_DeductionId", Pk_DeductionId),
                    new SqlParameter("@AddedBy", AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteDeductionRelease, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetDeductionBYID()
        {
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@Fk_BillId",Fk_BillId=="0"?null:Fk_BillId),
                    new SqlParameter("@FK_STPId",Pk_STPId=="0"?null:Pk_STPId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDGBills, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetDeduction()
        {
            try
            {
                SqlParameter[] para =
                {
              new SqlParameter("@Pk_DeductionId", Pk_DeductionId),
              new SqlParameter("@AddedBy", AddedBy),
              new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
              new SqlParameter("@Fk_STPId",Pk_STPId=="0"?null:Pk_STPId)
          };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDeductionRelease, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

      
        public DataSet GetElectricity()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@PK_ElectricityId", PK_ElectricityId),
                    new SqlParameter("@Pk_STPId",Pk_STPId=="0"?null:Pk_STPId),
                    new SqlParameter("@PK_IPS", PK_IPS),
                    new SqlParameter("@MonthStartDate", MonthStartDate),
                    new SqlParameter("@MonthEndDate", MonthEndDate),
                    new SqlParameter("@StartMeterReading", StartMeterReading),
                    new SqlParameter("@EndMeterReading", EndMeterReading),
                    new SqlParameter("@ElectricityUnit", ElectricityUnit),
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Remark", Remark),
                    new SqlParameter("@UnitRate", UnitRate),
                    new SqlParameter("@FixCharges", FixCharges),
                    new SqlParameter("@OtherCharges", OtherCharges),
                    new SqlParameter("@Penalty", Penalty),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ElectricityBill, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetSTPDate()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_STPID", Pk_STPId),
                    new SqlParameter("@FK_SPSID", PK_IPS),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId)
                  
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetStpdate, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    
        public DataSet GetValidDate()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_STPID", Pk_STPId),
                    new SqlParameter("@FK_SPSID", PK_IPS),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@MonthStartDate", MonthStartDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetElectricitydate, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetElecticitybill(int billId, int STPID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@Fk_BillId",billId),
                     new SqlParameter("@Fk_STPId", STPID),
                };
                ds = Connection.ExecuteQuery(ProcedureName.sp_GetElectricityBill, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet UpdateElecticitybill()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@ElectricityId ",PK_ElectricityId),
                    new SqlParameter("@MonthStartDate", MonthStartDate),
                    new SqlParameter("@MonthEndDate", MonthEndDate),
                    new SqlParameter("@StartMeterReading", StartMeterReading),
                    new SqlParameter("@EndMeterReading", EndMeterReading),
                    new SqlParameter("@ElectricityUnit", ElectricityUnit),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Penalty", Penalty),
                    new SqlParameter("@OtherCharges", OtherCharges),
                    new SqlParameter("@FixCharges", FixCharges),
                    new SqlParameter("@UnitRate", UnitRate),
                    new SqlParameter("@Remark", Remark)
                };
                ds = Connection.ExecuteQuery(ProcedureName.sp_UpdateElectricityBill,para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet VerifyElectricityBill(int InvoiceId , string filepath,string Remark,string Status)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@InvoiceId ",InvoiceId),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@Remark", Remark),
                    new SqlParameter("@ApprovedBy",AddedBy),
                    new SqlParameter("@Attachment",filepath),

                };
                ds = Connection.ExecuteQuery(ProcedureName.sp_VerifyElectricityBill, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet UpdateDeductionByID()
        {
           
            try
            {
                SqlParameter[] para =
                {

                  new SqlParameter("@DeductionId", Pk_DeductionId),
                    new SqlParameter("@ElectricityCutStartTime", ElectricityCutStartTime),
                    new SqlParameter("@ElectricityCutEndTime", ElectricityCutEndTime),
                    new SqlParameter("@MeterReading", MeterReading),
                    new SqlParameter("@DGStartTime", DGStartTime),
                    new SqlParameter("@DGEndTime", DGEndTime),
                    new SqlParameter("@DieselConsumes", DieselConsumes),
                    new SqlParameter("@DieselRateInMonth", DieselRateInMonth),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@TotalDGDuration", TotalDGDuration),
                    new SqlParameter("@Remark", Remark == "0" ? null : Remark),
                    new SqlParameter("@Attachment", Attachment == "0" ? null : Attachment)
                };


                DataSet ds = Connection.ExecuteQuery(ProcedureName.sp_UpdateDeductionRelease, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public DataSet GetVerify()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@InvoiceId",PK_InvId),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@Remark", Remark),
                    new SqlParameter("@ApprovedBy", ApprovedBy),
                    new SqlParameter("@Attachment", AttachmentURL),
                    
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.VerifyDGBill, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
