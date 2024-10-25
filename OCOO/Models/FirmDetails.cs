using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Office.Interop.Word;
using OCOO.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace OCOO.Models
{
    public class FirmDetails : Menu
    {
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string? Pk_FirmId { get; set; }
        public string? District { get; set; }
        public string? FirmName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Office { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? PinCode { get; set; }
        public string? Address { get; set; }
        public string? STPName { get; set; }
        public string? Capacity { get; set; }
        public string? MLDDay { get; set; }
        public string? ElectricityMeterLoad { get; set; }
        public string? ElectricityAccountNo { get; set; }
        public string? UPPCLBillCycle { get; set; }
        public string? BOD { get; set; }
        public string? COD { get; set; }
        public string? FC { get; set; }
        public string? TSS { get; set; }
        public string? PumpingStation { get; set; }
        public string? Name { get; set; }
        public string? PumpStationMeterLoad { get; set; }
        public string? SubPumpStationMeterLoad { get; set; }
        public string? PumpingStationName { get; set; }
        public string? PumpingStatisonAccountNo { get; set; }
        public string? LAD { get; set; }
        //public string? Fk_STPId { get; set; }
        public string? ContactPerson { get; set; }
        public string? AccountNo { get; set; }
        public string? BillingCycle { get; set; }
        public string? STPType { get; set; }
        public string? Count { get; set; }
        public int? Pk_STPId { get; set; }
        public string? Pk_Id { get; set; }
        public string? InHouse { get; set; }
        public bool? ThirdPartyIncpection { get; set; }
        public string? UPPCB { get; set; }
        public string? SewerLength { get; set; }
        public string? DrainageName { get; set; }
        public string? SPSType { get; set; }
        public string? SPSName { get; set; }
        public string? BillingDate { get; set; }
        public string? WaterDischarge { get; set; }
        public string? ComplaintReceived { get; set; }
        public string? ComplaintResolved { get; set; }
        public string? Pk_BillingId { get; set; }
        public string? BillNo { get; set; }
        public string? BiWaterDischargellNo { get; set; }
        public string? In_House { get; set; }
        public string? InHouseStatus { get; set; }
        public string? ThirdParty { get; set; }
        public string? ThirdPartyStatus { get; set; }
        public string? UPPCBStatus { get; set; }
        public string? InHouseBOD { get; set; }
        public string? InHouseCOD { get; set; }
        public string? InHouseFC { get; set; }
        public string? InHouseTSS { get; set; }
        public string? ThirdPartyBOD { get; set; }
        public string? ThirdPartyCOD { get; set; }
        public string? ThirdPartyFC { get; set; }
        public string? ThirdPartyTSS { get; set; }
        public string? UPPCBBOD { get; set; }
        public string? UPPCBCOD { get; set; }
        public string? UPPCBFC { get; set; }
        public string? UPPCBTSS { get; set; }
        public string? Remarks { get; set; }
        public string? UserId { get; set; }
        public string? DOCS { get; set; }
        public bool IsInhouse { get; set; }
        public bool IsUPPCB { get; set; }
        public bool IsTPI { get; set; }
        public bool IsCOD { get; set; }
        public bool IsBOD { get; set; }
        public bool IsTSS { get; set; }
        public bool IsFC { get; set; }
        public string? PumpingBillCycle { get; set; }
        public string? PeekDesignedDischarge { get; set; }
        public string? PeekDesignedFactor { get; set; }
        public bool IsSPS { get; set; }
        public string? Year { get; set; }
        public string? Search { get; set; }
        public string? ConsiderFlow { get; set; }
        public IFormFile? DocsFile { get; set; }
        public IFormFile? CoveringLetter { get; set; }
        public IFormFile? InspectionReport { get; set; }
        public IFormFile? TPIReports { get; set; }
        public IFormFile? LDSheet { get; set; }
        public IFormFile? OtherDocuments { get; set; }
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
        public bool IsFinalSave { get; set; }
        public bool Isdeclaration { get; set; }
        public string? DocsFileUrl { get; set; }
        public string? CoveringLetterUrl { get; set; }
        public string? InspectionReportUrl { get; set; }
        public string? TPIReportsUrl { get; set; }
        public string? LDSheetUrl { get; set; }
        public string? OtherDocumentsUrl { get; set; }
        //public string? PK_InvId { get; set; }
        public string? AmountOfMLDSTP { get; set; }
        public string? IsSelectValue { get; set; }
        public string? TreatmentTechnology { get; set; }
        public string? PeakDischarge { get; set; }
        public string? PeakFactor { get; set; }
        public bool? IsSame { get; set; }
        public string? Pk_UserId { get; set; }
        public List<FirmDetails> lstDetails { get; set; }
        public string? Remark { get; set; }
        public List<SelectListItem> _DocumentTypeDropdown { get; set; }
        public string DocumentType { get; set; }
        public string? IsAccountVerify { get; set; }
        public string? NewPassword { get; set; }
        //ask
        public int OrderByAssAndDess { get; set; }
        public int OrderByDateID { get; set; }
        public string? FK_CityId { get; set; }
        public string IsBillingBOD { get; set; }
        public string NewBOD { get; set; }
        public string? NewCOD { get; set; }
        public string? NewTSS { get; set; }
        public bool? IsBillingFC { get; set; }
        public string? NewFC { get; set; }
        public string? hddnTokenNo { get; set; }
        public string IsBillingCOD { get; set; }
        public bool? IsMain { get; set; }
        public string IsBillingTSS { get; set; }
         public string SubPeakFactor { get; set; }
          public string? SubPeakDischarge { get; set; }
        public string? SubPumpingBillCycle { get; set; }
        public string? SubAccountNo { get; set; }
        public string SubStationMeterLoad { get; set; }
        public string SubSewerLength { get; set; }
        public string SubDrainageName { get; set; }

        public string MeterLoad { get; set; }
        public string? LastBillingDate { get; set; }
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
                    new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                    //new SqlParameter("@UserId", Fk_EmpId=="0" ? null:Fk_EmpId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.ManageFirmMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet GetElecticitybill(int billId,int STPID)
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
        public DataSet GetFirm()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@DesignationId", Fk_DesignationId),
                    new SqlParameter("@Fk_DistrictId",Fk_DistrictId),
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                };
                ds = Connection.ExecuteQuery(ProcedureName.GetFirmList, para);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetEmployee()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_EmployeeId", Pk_EmployeeId),
                    new SqlParameter("@Fk_DesignationId", Fk_DesignationId),
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId),
                    new SqlParameter("@Password", Password),
                    new SqlParameter("@FirstName", FirstName),
                    new SqlParameter("@MiddleName", MiddleName),
                    new SqlParameter("@LastName", LastName),
                    new SqlParameter("@Office", Office),
                    new SqlParameter("@EmailId", EmailId),
                    new SqlParameter("@MobileNo", MobileNo),
                    new SqlParameter("@PinCode", PinCode),
                    new SqlParameter("@Address", Address),
                    new SqlParameter("@Pk_CityId", Pk_CityId),
                    new SqlParameter("@Fk_DepartmentId", Fk_DepartmentId),
                    new SqlParameter("@IsAccountVerify",Fk_DesignationId=="1"?false:true)
                };
                ds = Connection.ExecuteQuery(ProcedureName.ManageEmployeeMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetDropdown()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Value",Value)
                };
                ds = Connection.ExecuteQuery(ProcedureName.ManageEmployeeMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet StpRegistration()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Pk_FirmId),
                    new SqlParameter("@Pk_STPId", Pk_STPId),
                    new SqlParameter("@StpName", STPName),
                    new SqlParameter("@Capacity", Capacity),
                    new SqlParameter("@MLDDay", MLDDay),
                    new SqlParameter("@ElectricityMeterLoad", ElectricityMeterLoad),
                    new SqlParameter("@ElectricityAccountNo", ElectricityAccountNo),
                    new SqlParameter("@UPPCLBillCycle", UPPCLBillCycle),
                    new SqlParameter("@MeterLoad",MeterLoad),
                    new SqlParameter("@BOD", BOD),
                    new SqlParameter("@COD", COD),
                    new SqlParameter("@FC", FC),
                    new SqlParameter("@TSS", TSS),
                    new SqlParameter("@PumpingStation", PumpingStation),
                    new SqlParameter("@PumpingStationName", Name),
                    new SqlParameter("@PumpStationMeterLoad", PumpStationMeterLoad),
                    new SqlParameter("@PumpingStationAccountNo", PumpingStatisonAccountNo),
                    new SqlParameter("@BillingCycle", BillingCycle),
                    new SqlParameter("@STPType", STPType),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Pk_Id", Pk_Id),
                    new SqlParameter("@SPSType", SPSName),
                    new SqlParameter("@SewerLength", SewerLength),
                    new SqlParameter("@DrainageName", DrainageName),
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@IsInhouse", IsInhouse),
                    new SqlParameter("@IsUPPCB", IsUPPCB),
                    new SqlParameter("@IsTPI", IsTPI),
                    new SqlParameter("@IsBOD", IsBOD),
                    new SqlParameter("@IsCOD", IsCOD),
                    new SqlParameter("@IsTSS", IsTSS),
                    new SqlParameter("@IsFC", IsFC),
                    //new SqlParameter("@IsSPS",IsSPS),
                    new SqlParameter("@PumpingBillCycle", PumpingBillCycle),
                    new SqlParameter("@PeekDesignedDischarge", PeekDesignedDischarge),
                    new SqlParameter("@PeekDesignedFactor", PeekDesignedFactor),
                    new SqlParameter("@IsSPS", IsSPS),
                    new SqlParameter("@SPSCount", PumpingStation),
                    new SqlParameter("@Address",Address),
                    new SqlParameter("@City", Pk_CityId),
                    new SqlParameter("@TreatmentTechnology",TreatmentTechnology),
                   // new SqlParameter("@FC_Value", NewFC),
                   // new SqlParameter("@IsBillingFC", IsBillingFC),
                   // new SqlParameter("@IsBillingBOD", IsBillingBOD),
                   // new SqlParameter("@BOD_Value", NewBOD),
                   // new SqlParameter("@IsBillingCOD", IsBillingCOD),
                   // new SqlParameter("@COD_Value", NewCOD),
                   // new SqlParameter("@IsBillingTSS", IsBillingTSS),
                   // new SqlParameter("@TSS_Value", NewTSS),
                   // new SqlParameter("@TokenNo", hddnTokenNo),
                   // new SqlParameter("@Pk_Id",Pk_Id),
                    //new SqlParameter("@SPSType",SPSType)

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.STPRegistration, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataSet updateStpRegistration()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Pk_FirmId),
                    new SqlParameter("@Pk_STPId", Pk_STPId),
                    new SqlParameter("@StpName", STPName),
                    new SqlParameter("@Capacity", Capacity),
                    new SqlParameter("@MLDDay", MLDDay),
                    new SqlParameter("@ElectricityMeterLoad", ElectricityMeterLoad),
                    new SqlParameter("@ElectricityAccountNo", ElectricityAccountNo),
                    new SqlParameter("@UPPCLBillCycle", UPPCLBillCycle),
                    new SqlParameter("@MeterLoad",MeterLoad),
                    new SqlParameter("@BOD", BOD),
                    new SqlParameter("@COD", COD),
                    new SqlParameter("@FC", FC),
                    new SqlParameter("@TSS", TSS),
                    new SqlParameter("@PumpingStation", PumpingStation),
                    new SqlParameter("@PumpingStationName", Name),
                    new SqlParameter("@PumpStationMeterLoad", PumpStationMeterLoad),
                    new SqlParameter("@PumpingStationAccountNo", PumpingStatisonAccountNo),
                    new SqlParameter("@BillingCycle", BillingCycle),
                    new SqlParameter("@STPType", STPType),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Pk_Id", Pk_Id),
                    new SqlParameter("@SPSType", SPSName),
                    new SqlParameter("@SewerLength", SewerLength),
                    new SqlParameter("@DrainageName", DrainageName),
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@IsInhouse", IsInhouse),
                    new SqlParameter("@IsUPPCB", IsUPPCB),
                    new SqlParameter("@IsTPI", IsTPI),
                    new SqlParameter("@IsBOD", IsBOD),
                    new SqlParameter("@IsCOD", IsCOD),
                    new SqlParameter("@IsTSS", IsTSS),
                    new SqlParameter("@IsFC", IsFC),
                    //new SqlParameter("@IsSPS",IsSPS),
                    new SqlParameter("@PumpingBillCycle", PumpingBillCycle),
                    new SqlParameter("@PeekDesignedDischarge", PeekDesignedDischarge),
                    new SqlParameter("@PeekDesignedFactor", PeekDesignedFactor),
                    new SqlParameter("@IsSPS", IsSPS),
                    new SqlParameter("@SPSCount", PumpingStation),
                    new SqlParameter("@Address",Address),
                    new SqlParameter("@City", Pk_CityId),
                    new SqlParameter("@TreatmentTechnology",TreatmentTechnology),
                     new SqlParameter("@FC_Value", NewFC),
                    new SqlParameter("@IsBillingFC", IsBillingFC),
                    new SqlParameter("@IsBillingBOD ", IsBillingBOD ),
                    new SqlParameter("@BOD_Value", NewBOD),
                    new SqlParameter("@IsBillingCOD", IsBillingCOD),
                    new SqlParameter("@COD_Value", NewCOD),
                    new SqlParameter("@IsBillingTSS", IsBillingTSS),
                    new SqlParameter("@TSS_Value", NewTSS),
                    new SqlParameter("@TokenNo", hddnTokenNo),
                   // new SqlParameter("@Pk_Id",Pk_Id),
                    //new SqlParameter("@SPSType",SPSType)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateSTPRegistration, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }





        public DataSet GetSTPBilling()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_BillingId", Pk_BillingId),
                    new SqlParameter("@Fk_STPId", Pk_STPId==0?null:Pk_STPId),
                    new SqlParameter("@BillingDate", BillingDate),
                    new SqlParameter("@WaterDischarge", WaterDischarge),
                    new SqlParameter("@ComplaintReceived", ComplaintReceived),
                    new SqlParameter("@ComplaintResolved", ComplaintResolved),
                    new SqlParameter("@IsIn_House", InHouse=="on"?"true":"false"),
                    new SqlParameter("@InHouseStatus", InHouseStatus),
                    new SqlParameter("@IsThirdParty", ThirdParty=="on"?"true":"false"),
                    new SqlParameter("@ThirdPartyStatus", ThirdPartyStatus),
                    new SqlParameter("@ISUPPCB", UPPCB=="on"?"true":"false"),
                    new SqlParameter("@UPPCBStatus", UPPCBStatus),
                    new SqlParameter("@InHouseBOD", InHouseBOD),
                    new SqlParameter("@InHouseCOD", InHouseCOD),
                    new SqlParameter("@InHouseTSS", InHouseTSS),
                    new SqlParameter("@InHouseFC", InHouseFC),
                    new SqlParameter("@ThirdPartyBOD", ThirdPartyBOD),
                    new SqlParameter("@ThirdPartyCOD", ThirdPartyCOD),
                    new SqlParameter("@ThirdPartyTSS", ThirdPartyTSS),
                    new SqlParameter("@ThirdPartyFC", ThirdPartyFC),
                    new SqlParameter("@UPPCBBOD", UPPCBBOD),
                    new SqlParameter("@UPPCBCOD", UPPCBCOD),
                    new SqlParameter("@UPPCBTSS", UPPCBTSS),
                    new SqlParameter("@UPPCBFC", UPPCBFC),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId),
                    new SqlParameter("@Months",Months),
                    new SqlParameter("@Years",Years),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@IsSame",IsSame),
                    new SqlParameter("@Fk_EmpId",Fk_EmpId),
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_BillingMaster, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetCallLogBySTP()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_Stp_Id", Fk_STPId),
                    new SqlParameter("@ReportingDate", BillingDate)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCallLogBySTP, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet UpdateStatus()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_BillingId", Pk_BillingId),
                    new SqlParameter("@UserId", UserId),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@BOD", BOD),
                    new SqlParameter("@COD", COD),
                    new SqlParameter("@TSS", TSS),
                    new SqlParameter("@FC", FC),
                    new SqlParameter("@Docs", DOCS),
                    new SqlParameter("@Remarks", Remarks),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.BillingApproval, para);
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
                    new SqlParameter("@Fk_ZoneId", Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Fk_MonthId", Fk_MonthId=="0"?null:Fk_MonthId),
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

        public DataSet GetBillingReport()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_BillingId", Pk_BillingId),
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@Fk_STPId", Fk_STPId),
                    new SqlParameter("@fromDate", FromDate),
                    new SqlParameter("@toDate", ToDate)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_STPwiseBillReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet StpValidation()
        {
            try
            {

                SqlParameter[] para =
                       {
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@Fk_FirmId", AddedBy),
                    new SqlParameter("@Capacity",string.IsNullOrEmpty(Capacity)?"0":Capacity)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.STPValidationRegistration, para);
                return ds;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public DataSet Gettype()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {

                };
                ds = Connection.ExecuteQuery("GetOCMES_Data", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
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
        public DataSet GetBillRequestList()
        {
            try
            {
                SqlParameter[] para =
               {        new SqlParameter("@Fk_STPId", Fk_STPId),
                        new SqlParameter("@Fk_MonthId", Fk_MonthId),
                        new SqlParameter("@Year", Year),
                        new SqlParameter("@AddedBy", AddedBy),
                        new SqlParameter("@OpCode", OpCode)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_STPBillRequestList, para);
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
               {          new SqlParameter("@Fk_FirmId", Fk_FirmId),
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
                        new SqlParameter("@FK_InvId", PK_InvId),
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GenerateFirmBill, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetInvoiceMSTList()
        {
            try
            {

                SqlParameter[] para =
               {          new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                         new SqlParameter("@BillStatus",Status=="0"?null:Status),
                         new SqlParameter("@Fk_MonthId",Fk_MonthId=="0"?null:Fk_MonthId),
                         new SqlParameter("@FromDate",FromDate),
                         new SqlParameter("@ToDate",ToDate)

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_InvoiceMASTReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetViewInvoiceDetails()
        {
            try
            {

                SqlParameter[] para =
               {          new SqlParameter("@Fk_FirmId", Fk_FirmId),
                         new SqlParameter("@PK_InvId",PK_InvId)

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_ViewBillReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetGentatedBill()
        {
            try
            {

                SqlParameter[] para =
                {          new SqlParameter("@Fk_STPId", Fk_STPId),
                         new SqlParameter("@PK_InvId",PK_InvId)

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_PrintBill, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet CheckGeneratedBill()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Fk_STPId", Fk_STPId),
                    new SqlParameter("@BillingDate", BillingDate),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_CheckGeneratedBill, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet CheckWaterDischargeBill()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@FK_STPId", Fk_STPId),
                    new SqlParameter("@Flow", WaterDischarge),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetSTPCapacity, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetFirmList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@Pk_ZoneId", Pk_ZoneId),
                    new SqlParameter("@Pk_FirmId", Pk_FirmId),
                    new SqlParameter("@UserId", Fk_EmpId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ManageFirmList, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetFirmSTPList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Capacity", Capacity),
                    new SqlParameter("@Pk_StpId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Pk_Id", Pk_Id=="0" ?null:Pk_Id),
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@UserId", Fk_EmpId=="0"?null:Fk_EmpId),
                    new SqlParameter("@TreatmentTechnology", TreatmentTechnology=="0"?null:TreatmentTechnology),
                    new SqlParameter("@Fk_CityId", Pk_CityId=="0"?null:Pk_CityId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetSTPList, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet DailyBillingReportByEmp()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Fk_MonthId", Fk_MonthId=="0"?null:Fk_MonthId),
                    new SqlParameter("@fromDate", FromDate),
                    new SqlParameter("@toDate", ToDate),
                    new SqlParameter("@UserId", Fk_EmpId=="0"?null:Fk_EmpId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDailyBillReport, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetsameDischarge()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@FK_STPId", Fk_STPId),
                    new SqlParameter("@Capacity", WaterDischarge),
                    new SqlParameter("@BillDate", BillingDate),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetsameDischarge, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetBillingdata()
        {
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@Fk_EmpId",Fk_EmpId),
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId),
                    new SqlParameter("@Fk_userTypeId",Fk_UsertypeId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBillingdata, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetBillingDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@Fk_STPId", Pk_STPId==0?null:Pk_STPId),
                    new SqlParameter("@fromDate", FromDate),
                    new SqlParameter("@toDate", ToDate),
                    new SqlParameter("@Months", Months=="0"?null:Months),
                    new SqlParameter("@Years", Years),
                    new SqlParameter("@OrderByAssAndDess", OrderByAssAndDess == 0 ? null:OrderByAssAndDess),
                    new SqlParameter("@OrderByDateID", OrderByDateID == 0 ? null:OrderByDateID)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBillingDetails, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetBillingDetailsForApproval(int billId, int UserId, int STPId=0)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId", billId),
                    new SqlParameter("@FK_UserId", UserId),
                    new SqlParameter("@STPId", STPId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBillingDetailsForApproval, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetInvoiceSTPWise(int billId, int UserId)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FbillId", billId),
                    new SqlParameter("@FK_UserId", UserId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetInvoiceSTPWise, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetVeifyAccount()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_EmployeeId", Fk_EmpId),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@NewPassword",NewPassword)   
                };
                ds = Connection.ExecuteQuery(ProcedureName.Sp_UpdateAccountVerification, para);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public List<SelectListItem> GetDocumentTypeDropdown()
        {
            List<SelectListItem> _List = new List<SelectListItem>();
            SqlParameter[] para = { };
            var ds = Connection.ExecuteQuery(ProcedureName.GetDocumentTypeMaster, para);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string value = row["Id"].ToString();
                    string text = row["DocumentType"].ToString();
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Value = value,
                        Text = text
                    };
                    selectListItems.Add(selectListItem);
                }
            return selectListItems;
        }

        public DataSet GetSTPByID()
        {
            try
            {
                SqlParameter[] para = {

                new SqlParameter("@Pk_Stp_Id",Pk_STPId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFirmSTP, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet StpLIst(string Type)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Pk_FirmId),
                    new SqlParameter("@Pk_STPId", Pk_STPId),
                    new SqlParameter("@Type", Type)

                };
                DataSet ds = Connection.ExecuteQuery("StpLIst", para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetNotification()
        {
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@Fk_EmpId",Fk_EmpId),
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId),
                    new SqlParameter("@Fk_userTypeId",Fk_UsertypeId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetNotificationlist, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }


    public class TreatedWater : Menu
    {
        //public string Fk_ZoneId { get; set; }
        //public string? Fk_FirmId { get; set; }
        // public string Fk_STPId { get; set; }
        //public string? Fk_MonthId { get; set; }
        //public string? FromDate { get; set; }
        //public string ToDate { get; set; }
        public string? UserId { get; set; }

        public string? Pk_STPId { get; set; }
        public DataSet GetTreatedWater()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@UserId",UserId),
                     new SqlParameter("@ToDate",ToDate),
                     new SqlParameter("@FromDate",FromDate),
                     new SqlParameter("@Fk_MonthId",Fk_MonthId=="0"?null:Fk_MonthId),
                     new SqlParameter("@Fk_StpId",Fk_STPId=="0"?null:Fk_STPId),
                     new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),

                };
                ds = Connection.ExecuteQuery(ProcedureName.sp_GetTreatedWaterVolume, para);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetTreatedWaterVolumeForFirm()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@UserId",UserId),
                     new SqlParameter("@ToDate",ToDate),
                     new SqlParameter("@FromDate",FromDate),
                     new SqlParameter("@Fk_MonthId",Fk_MonthId=="0"?null:Fk_MonthId),
                     new SqlParameter("@Fk_StpId",Fk_STPId=="0"?null:Fk_STPId),
                     new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),

                };
                ds = Connection.ExecuteQuery(ProcedureName.GetTreatedWaterVolumeForFirm, para);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


    }

    public class Utilization : Menu
    {
        //public string Fk_ZoneId { get; set; }
        //public string? Fk_FirmId { get; set; }
        //public string Fk_STPId { get; set; }
        //public string? Fk_MonthId { get; set; }
        //public string? FromDate { get; set; }
        //public string ToDate { get; set; }
        public string? UserId { get; set; }

        public DataSet Getutilization()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@UserId",UserId),
                     new SqlParameter("@ToDate",ToDate),
                     new SqlParameter("@FromDate",FromDate),
                     new SqlParameter("@Fk_MonthId",Fk_MonthId=="0"?null:Fk_MonthId),
                     new SqlParameter("@Fk_STPId",Fk_STPId=="0"?null:Fk_STPId),
                     new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),

                };
                ds = Connection.ExecuteQuery(ProcedureName.sp_GetUtilizationOfSTP, para);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getutilizationofstpForFirm()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@UserId",UserId),
                     new SqlParameter("@ToDate",ToDate),
                     new SqlParameter("@FromDate",FromDate),
                     new SqlParameter("@Fk_MonthId",Fk_MonthId=="0"?null:Fk_MonthId),
                     new SqlParameter("@Fk_STPId",Fk_STPId=="0"?null:Fk_STPId),
                     new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),

                };
                ds = Connection.ExecuteQuery(ProcedureName.Sp_getutilizationofstpForFirm, para);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


    }


}









