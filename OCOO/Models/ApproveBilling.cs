using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class ApproveBilling : Menu
    {
        public string? Name { get; set; }
        public string? ZoneName { get; set; }
        public string? StpName { get; set; }
        public string? Inv_Month { get; set; }
        public string? MonthName { get; set; }
        public string? Inv_Year { get; set; }
        public string? BillStatus { get; set; }
        public IFormFile? ApproveDocuments { get; set; }
        public string? ApproveDocumentsURL { get; set; }
        public string? ld_DocumentsURL { get; set; }
        public string? ApproveOrder { get; set; }
        public string? IsEnabled { get; set; }
        public string? ApprovedDate { get; set; }
        public string? Year { get; set; }
        public string? Remarks { get; set; }
        public string? Isdeclaration { get; set; }
        public string? ForwardTo { get; set; }
        public string? ApproveAndForward { get; set; }
        public string? ForwardRemarks { get; set; }
        public string? BillId { get; set; }
        public string? FuelCharges { get; set; }
        public IFormFile? FuelChargesDocuments { get; set; }
        public string? FuelChargesDocumentsURL { get; set; }
        public string? OtherLdCharges { get; set; }
        public IFormFile? OtherLdChargesDocuments { get; set; }
        public string? OtherLdChargesDocumentsURL { get; set; }
        public string? ProcessType { get; set; }
        public string? ToRemark { get; set; }
        public string? ForwardedBy { get; set; }
        public string? Color { get; set; }
        public string? AddedOn { get; set; }

        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string Pk_ElectricityBillId { get; set; }
        
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
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@Pk_EmpId",Pk_EmployeeId),
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
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@PK_InvId",PK_InvId=="0"?null:PK_InvId),
                    new SqlParameter("@FK_ZoneId",Fk_ZoneId== "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Month",Fk_MonthId=="0"?null:Fk_MonthId)
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_ViewBillReport, para);
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
                    new SqlParameter("@ToDate",ToDate)
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_STPwisePendingBills, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet ApproveBill()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FirmId", Firm),
                    new SqlParameter("@FK_FbillId", BillId),
                    new SqlParameter("@Remarks", Remarks),
                    new SqlParameter("@Doc_Url", ApproveDocumentsURL),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@ForwardTo", ForwardTo),
                    new SqlParameter("@FuleCharges", FuelCharges),
                    new SqlParameter("@FuleCharges_Doc", FuelChargesDocumentsURL),
                    new SqlParameter("@OtherLDCharges", OtherLdCharges),
                    new SqlParameter("@OtherLDCharges_Doc", OtherLdChargesDocumentsURL),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_FirmBillsApproval, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetBillApprovalList()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Month",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@Year",Year=="0"?null:Year),
                    new SqlParameter("@Fk_STPId",Fk_STPId=="0"?null:Fk_STPId),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@Pk_EmpId",Pk_EmployeeId == "0" ? null : Pk_EmployeeId),
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_ZonePendingBills, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetEmpApprovalList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_EmpId", Fk_EmpId),
                    new SqlParameter("@FK_FirmId", Fk_FirmId),
                    new SqlParameter("@FK_FbillId", BillId),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetEmpApprovalList, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet ApproveBillHistory()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_FirmId", Firm),
                    new SqlParameter("@FK_FbillId", BillId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetApprovalHistory, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetElectricityByID()
        {
            try
            {
                SqlParameter[] para = { 
                new SqlParameter("@Pk_ElectricityBillId",Pk_ElectricityBillId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetElectricityBillByID, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet getBillLog()
        {
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@Fk_BillId",BillId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.Sp_BillLog, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

public class EmployeeList
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}