using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace OCOO.Models
{
    public class STPModel : Menu
    {
        public string? Pk_FirmId { get; set; }
        public string? District { get; set; }
        public string? FirmName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? ParentName { get; set; }
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
        public string? SubStationMeterLoad { get; set; }
        public string? PumpingStationName { get; set; }
        public string? PumpingStatisonAccountNo { get; set; }
        public string? LAD { get; set; }
        public string? ContactPerson { get; set; }
        public string? AccountNo { get; set; }
        public string? SubAccountNo { get; set; }
        public string? BillingCycle { get; set; }
        public string? STPType { get; set; }
        public string? Count { get; set; }
        public int? Pk_STPId { get; set; }
        public string? Pk_Id { get; set; }
        public string? InHouse { get; set; }
        public bool? ThirdPartyIncpection { get; set; }
        public string? UPPCB { get; set; }
        public string? SewerLength { get; set; }
        public string? SubSewerLength { get; set; }
        public string? DrainageName { get; set; }
        public string? SubDrainageName { get; set; }
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
        public string? SubPumpingBillCycle { get; set; }
        public string? PeekDesignedDischarge { get; set; }
        public string? PeekDesignedFactor { get; set; }
        public bool IsSPS { get; set; }
        public string? Year { get; set; }
        public string? Search { get; set; }
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
        public bool IsFinalSave { get; set; }
        public bool Isdeclaration { get; set; }
        public string? DocsFileUrl { get; set; }
        public string? CoveringLetterUrl { get; set; }
        public string? InspectionReportUrl { get; set; }
        public string? TPIReportsUrl { get; set; }
        public string? LDSheetUrl { get; set; }
        public string? OtherDocumentsUrl { get; set; }
        public string? AmountOfMLDSTP { get; set; }
        public string? IsSelectValue { get; set; }
        public string? TreatmentTechnology { get; set; }
        public string? SubPumpStationMeterLoad { get; set; }
        public string? SubPeakDischarge { get; set; }
        public string? SubPeekDesignedDischarge { get; set; }
        public string? PeakFactor { get; set; }
        public string? SubPeakFactor { get; set; }
        public string? PeakDischarge { get; set; }
        public string? Fk_ParentId { get; set; }
        public bool? IsBillingBOD { get; set; }
        public bool? IsBillingCOD { get; set; }
        public bool? IsBillingTSS { get; set; }
        public string? NewBOD { get; set; }
        public string? NewCOD { get; set; }
        public string? NewTSS { get; set; }
        public bool? IsBillingFC { get; set; }
        public string? NewFC { get; set; }
        public string? hddnTokenNo { get; set; }

        public bool? IsMain { get; set; }
        public DataSet StpRegistration()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Fk_FirmId),
                    new SqlParameter("@Pk_STPId", Pk_STPId),
                    new SqlParameter("@StpName", STPName),
                    new SqlParameter("@Capacity", Capacity),
                    new SqlParameter("@MLDDay", MLDDay),
                    new SqlParameter("@ElectricityMeterLoad", ElectricityMeterLoad),
                    new SqlParameter("@ElectricityAccountNo", ElectricityAccountNo),
                    new SqlParameter("@UPPCLBillCycle", UPPCLBillCycle),
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
                    new SqlParameter("@PumpingBillCycle", PumpingBillCycle),
                    new SqlParameter("@PeekDesignedDischarge", PeekDesignedDischarge),
                    new SqlParameter("@PeekDesignedFactor", PeekDesignedFactor),
                    new SqlParameter("@IsSPS", IsSPS),
                    new SqlParameter("@SPSCount", PumpingStation==null?"0":PumpingStation),
                    new SqlParameter("@Address", Address),
                    new SqlParameter("@City", Pk_CityId),
                    new SqlParameter("@TreatmentTechnology", TreatmentTechnology),
                    new SqlParameter("@FC_Value", NewFC),
                    new SqlParameter("@IsBillingFC", IsBillingFC),
                    new SqlParameter("@IsBillingBOD", IsBillingBOD),
                    new SqlParameter("@BOD_Value", NewBOD),
                    new SqlParameter("@IsBillingCOD", IsBillingCOD),
                    new SqlParameter("@COD_Value", NewCOD),
                    new SqlParameter("@IsBillingTSS", IsBillingTSS),
                    new SqlParameter("@TSS_Value", NewTSS),
                     new SqlParameter("@TokenNo", hddnTokenNo)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_STPRegistration, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet SavePumpingStation()
        {
            try
            {
                SqlParameter[] para =
                {
                   
                    new SqlParameter("@Pk_Id", Pk_Id),
                    new SqlParameter("@Fk_STPId", Fk_STPId),
                    new SqlParameter("@Name", Name),
                    new SqlParameter("@MeterLoad", PumpStationMeterLoad),
                    new SqlParameter("@ElectricityAccountNumber", PumpingStatisonAccountNo),
                    new SqlParameter("@ElectricityBillCycle", BillingCycle),
                    new SqlParameter("@PeakDesignDischarge", PeakDischarge),
                    new SqlParameter("@PeakFactor", PeakFactor),
                    new SqlParameter("@FlowFromDrain", DrainageName),
                    new SqlParameter("@FlowFromSewer", SewerLength),
                    new SqlParameter("@Fk_ParentId", Fk_ParentId),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@TokenNo", hddnTokenNo), 
                    new SqlParameter("@IsMain", IsMain),
                     new SqlParameter("@STPName", STPName),
                    new SqlParameter("@OpCode", OpCode),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveMainPumpingStation, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet SaveSPS()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_Id", Pk_Id),
                    new SqlParameter("@Fk_STPId", Fk_STPId),
                    new SqlParameter("@SPSName", SPSName),
                    new SqlParameter("@MeterLoad", PumpStationMeterLoad),
                    new SqlParameter("@AccountNo", PumpingStatisonAccountNo),
                    new SqlParameter("@BillingCycle", BillingCycle),
                    new SqlParameter("@SewerLength", SewerLength),
                    new SqlParameter("@DrainageName", DrainageName),
                    new SqlParameter("@PeakDesign", PeekDesignedDischarge),
                    new SqlParameter("@PeakFactor", PeekDesignedFactor),
                    new SqlParameter("@IsSPS", IsSPS),
                    new SqlParameter("@AddedBy", AddedBy),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_SaveSPS, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetPumpingStationList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_Id", Pk_Id),
                    new SqlParameter("@AddedBy", AddedBy),
                     new SqlParameter("@TokenNo", hddnTokenNo),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPumpingStationList, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetSTPList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FirmId", Pk_FirmId=="0"?null:Pk_FirmId),
                    new SqlParameter("@Pk_StpId", Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@Pk_CityId", Pk_CityId=="0"?null:Pk_CityId),
                    new SqlParameter("@TreatmentTechnology", TreatmentTechnology),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetSTPList, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet DeletePumpingStation()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Id", Pk_Id)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeletePumpingStation, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet DeleteSTP()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Id", Pk_Id)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteSTP, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetFirmRegistrationToken()
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

        public DataSet GetFirmStpEdit()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =  {
                    new SqlParameter("@Fk_SPSId", Pk_Id),
                    new SqlParameter("@Fk_STPId", Fk_STPId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.GetSPS_IPSDetails,para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetUpdatePumpingSation()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Pk_Id", Pk_Id),
                    new SqlParameter("@Fk_STPId", Fk_STPId),
                    new SqlParameter("@Name", Name),
                    new SqlParameter("@MeterLoad", PumpStationMeterLoad),
                    new SqlParameter("@ElectricityAccountNumber", PumpingStatisonAccountNo),
                    new SqlParameter("@ElectricityBillCycle", BillingCycle),
                    new SqlParameter("@PeakDesignDischarge", PeekDesignedDischarge),
                    new SqlParameter("@PeakFactor", PeekDesignedFactor),
                    new SqlParameter("@FlowFromDrain", DrainageName),
                    new SqlParameter("@FlowFromSewer", SewerLength),
                    new SqlParameter("@Fk_ParentId", Fk_ParentId),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@TokenNo", hddnTokenNo),
                    new SqlParameter("@IsMain", IsMain),
                     new SqlParameter("@STPName", STPName),
                    new SqlParameter("@OpCode", OpCode),

                };
                ds = Connection.ExecuteQuery(ProcedureName.SaveMainPumpingStation, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
