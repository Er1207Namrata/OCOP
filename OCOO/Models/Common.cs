using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using DataTable = System.Data.DataTable;

namespace OCOO.Models
{
    public class Common
    {
        public Pager? Pager { get; set; }
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 30;
        public int? OpCode { get; set; }
        public string? DesignationName { get; set; }
        public string? Fk_DesignationId { get; set; }
        public string? Fk_DepartmentId { get; set; }
        public string? AddedBy { get; set; }
        public string? Fk_UsertypeId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Status { get; set; }
        public string? InvoiceId { get; set; }
        
        public string? ApprovedBy { get; set; }
        public string? Message { get; set; }
        public string? ExportToExcel { get; set; }
        public string? Fk_MemId { get; set; }
        public string? Pk_EmployeeId { get; set; }
        public string? Fk_DistrictId { get; set; }
        public string? Pk_ZoneId { get; set; }
        public string? ZoneID { get; set; }
        public string? Pk_CityId { get; set; }
        public string? City { get; set; }
        public string? Fk_BlockId { get; set; }
        public string? Fk_ZoneId { get; set; }
        public string? Value { get; set; }
        public string? Fk_FirmId { get; set; }
        public string? Firm { get; set; }
        public string? Fk_STPId { get; set; }
        public string? Fk_MonthId { get; set; }
        public List<int>? SelectedMonth { get; set; }
        public string? Months { get; set; }
        public string? Years { get; set; }
        public string? EmployeeName { get; set; }

        public string? PK_InvId { get; set; }
        public string? JsonstringZone { get; set; }
        public string? IsBilled { get; set; }

        public List<BindList>? BindList { get; set; }
        public DataTable? dtDetails { get; set; }
        public DataTable? dtDetails1 { get; set; }
        public DataTable? dtDetails2 { get; set; }

        public List<FirmDetails>? list { get; set; }

        public string? Pk_FirmId { get; set; }

        public string Pk_STPId { get; set; }

        public List<BillApprovalDocumentReportDTO> _DocumentList { get; set; }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string[] DatePart = InputDate.Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            string DateString;
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Year + "-" + Month + "-" + Day;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
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
        public DataSet getEmployeeZone(int empid)
        {
            try
            {
                SqlParameter[] para = { new SqlParameter("@empid", empid)};
                DataSet ds = Connection.ExecuteQuery(ProcedureName.getEmployeeZone, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetDashboardData()
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@Fk_ZoneId", Fk_ZoneId=="0"?null:Fk_ZoneId),
                     new SqlParameter("@Fk_FrimId", Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_MonthId", Fk_MonthId=="0"?null:Fk_MonthId),
                     new SqlParameter("@FromDate", string.IsNullOrEmpty(FromDate)?null:FromDate),
                     new SqlParameter("@ToDate", string.IsNullOrEmpty(ToDate)?null:ToDate),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.AdminDashboardData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAdminDashboard()
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@Fk_ZoneId", Fk_ZoneId=="0"?null:Fk_ZoneId),
                     new SqlParameter("@Fk_FirmId", Fk_FirmId=="0"?null:Fk_FirmId),
                     new SqlParameter("@Fk_STPId", Fk_STPId=="0"?null:Fk_STPId),
                     new SqlParameter("@Fk_MonthId", Fk_MonthId=="0"?null:Fk_MonthId),
                     new SqlParameter("@FromDate", string.IsNullOrEmpty(FromDate)?null:FromDate),
                     new SqlParameter("@ToDate", string.IsNullOrEmpty(ToDate)?null:ToDate),
                     new SqlParameter("@UserId", Pk_EmployeeId=="0"?null:Pk_EmployeeId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.AdminDashboard, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet LatestBillingDate(int STPId=0)
        {
            try
            {
                SqlParameter[] para =
            {
                  new SqlParameter("@Fk_STPId",STPId),

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetLatestBillingDate, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    [Serializable]
    public class Pager
    {
        public Pager(int? totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
        public int? TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; } = 30;
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
    public class SessionManager : IDisposable
    {
        public static int Size => 30;

        public void Dispose()
        {

        }
    }

    public static class FileManagement
    {
        public static async Task<string> WriteFiles(this IFormFile files, string file, string FolderName)
        {
            bool isSaveSuccess = false;

            string obj = "";
            string final = "";

            string fileName;
            try
            {
                var extension = "." + files.FileName.Split('.')[files.FileName.Split('.').Length - 1];
                fileName = file + DateTime.Now.Ticks + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", FolderName);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", FolderName, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }

                obj = "/" + FolderName + "/" + fileName;

                isSaveSuccess = true;
            }
            catch (Exception ex)
            {
                obj = ex.Message;
                return obj;
            }


            return obj;
        }

    }
    public class MPaging
    {
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecords { get; set; }
        public string? SearchKey { get; set; }
        public string? SearchValue { get; set; }
        public Pager? Pager { get; set; }
        public int EndPage { get; private set; }
    }
    public class BaseUrl
    {
        public static string URL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("BaseUrl").Value;
        public static string ImageURL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ImageUrl").Value;
    }
    public class ProcedureName
    {
        public static string? GetMasterData = "GetMasterData";
        public static string? GetSTCDailyCapacity = "GetSTCDailyCapacity";
        public static string? GetApproveSTC = "GetApproveSTC";
        public static string? Login = "Login";
        public static string? ManageFirmMaster = "ManageFirmMaster";
        public static string? FirmRequestList = "FirmRequestList";
        public static string? GetFirmList = "GetFirmList";
        public static string? ManageEmployeeMaster = "ManageEmployeeMaster";
        public static string? GetApprovalStatus = "GetApprovalStatus";
        public static string? ManageSTPMaster = "ManageSTPMaster";
        public static string? GetSTPDetails = "GetSTPDetails";
        public static string? DocumentsMaster = "DocumentsMaster";
        public static string? AdminDashboardData = "AdminDashboardData";
        public static string? AdminDashboard = "AdminDashboard";
        public static string? STPRegistration = "STPRegistration";
        public static string? ManageCallCenter = "ManageCallCenter";
        public static string? SPInspectionMaster = "SPInspectionMaster";
        public static string? SP_PumpingStation = "SP_PumpingStation";
        public static string? SP_ManageInspectionAgency = "SP_ManageInspectionAgency";
        public static string? SP_BillingMaster = "SP_BillingMaster";
        public static string? GetCallLogBySTP = "GetCallLogBySTP";
        public static string? SP_ZoneMaster = "SP_ZoneMaster";
        public static string? BillingApproval = "BillingApproval";
        public static string? GetBellReportForAgency = "GetBellReportForAgency";
        public static string? UpdateBillingStatus = "UpdateBillingStatus";
        public static string? SP_DailyBillReport = "SP_DailyBillReport";
        public static string? SP_STPwiseBillReport = "SP_STPwiseBillReport";
        public static string? AgencyDashboard = "AgencyDashboard";
        public static string? STPValidationRegistration = "STPValidationRegistration";
        public static string? SP_STPBillRequestList = "SP_STPBillRequestList";
        public static string? SP_GetSTPBillSummary = "SP_GetSTPBillSummary";
        public static string? ChangePassword = "ChangePassword";
        public static string? ProfilePic = "ProfilePic";
        public static string? ProfilePicAdmin = "Admin_ProfilePic";
        public static string? SP_GenerateBill = "SP_GenerateBill";
        public static string? SP_InvoiceMASTReport = "SP_InvoiceMASTReport";
        public static string? SP_ViewBillReport = "SP_ViewBillReport";
        public static string? SP_PrintBill = "SP_PrintBill";
        public static string? DepartmentMaster = "DepartmentMaster";
        public static string? DeginationMaster = "DeginationMaster";
        public static string? GetPemissionData = "GetPemissionData";
        public static string? UserRollPermission = "UserRollPermission";
        public static string? BindMenuMaster = "BindMenuMaster";
        public static string? GetMenuMaster = "GetMenuMaster";
        public static string? SP_ZonewisePendingBills = "SP_ZonewisePendingBills";
        public static string? SP_PrintZoneBills = "SP_PrintZoneBills";
        public static string? FirmDashoardData = "FirmDashoardData";
        public static string? FirmDashboardGraph = "FirmDashboardGraph";
        public static string? GetPumpingstation = "GEtPumpingstation";
        public static string? SP_STPwisePendingBills = "SP_STPwisePendingBills";
        public static string? AgencyDashboardGraph = "AgencyDashboardGraph";
        public static string? SP_CheckGeneratedBill = "SP_CheckGeneratedBill";
        public static string? SP_InvoiceApproval = "SP_InvoiceApproval";
        public static string? SP_GetDesignation = "SP_GetDesignation";
        public static string? SP_Dept_Des_Link = "SP_Dept_Des_Link";
        public static string? UserFormPermission = "UserFormPermission";
        public static string? GetSubMenuMaster = "GetSubMenuMaster";
        public static string? SP_EmpZone = "SP_EmpZone";
        public static string? SP_GetEmpZone = "SP_GetEmpZone";
        public static string? PumpingStationDetailsByAdmin = "PumpingStationDetailsByAdmin";
        public static string? SP_GetSTPCapacity = "SP_GetSTPCapacity";
        public static string? ZoneWisePaidBills = "SP_ZonewisePaidBills";
        public static string? ManageFirmList = "ManageFirmList";
        public static string? GetSTPList = "GetSTPList";
        public static string? SP_GenerateFirmBill = "SP_GenerateFirmBill";
        public static string? SP_FirmPendingBills = "SP_FirmPendingBills";
        public static string? SP_GetFirmBillSummary = "SP_GetFirmBillSummary";
        public static string? SP_GetApprovalHistory = "SP_GetApprovalHistory";
        public static string? SP_ZonePendingBills = "SP_ZonePendingBills";
        public static string? GetEmpApprovalList = "GetEmpApprovalList";
        public static string? SP_FirmBillsApproval = "SP_FirmBillsApproval";
        public static string? GetDailyBillReport = "GetDailyBillReport";
        public static string? SP_STPRegistration = "SP_STPRegistration";
        public static string? SaveMainPumpingStation = "SaveMainPumpingStation";
        public static string? SP_SaveSPS = "SP_SaveSPS";
        public static string? GetPumpingStationList = "GetPumpingStationList";
        public static string? SP_GetSTPList = "SP_GetSTPList";
        public static string? DeletePumpingStation = "DeletePumpingStation";
        public static string? DeleteSTP = "DeleteSTP";
        public static string? SaveDeductionRelease = "SaveDeductionRelease";
        public static string? UpdateDeductionRelease = "UpdateDeductionRelease";
        public static string? DeleteDeductionRelease = "DeleteDeductionRelease";
        public static string? GetDeductionRelease = "GetDeductionRelease";
        public static string? ElectricityBill = "SP_ElectricityBill";
        public static string? GetStatus = "SP_GetStatus";
        public static string? ElectricityBillReport = "SP_ElectricityBillReport";
        public static string? DGBillReport = "SP_DGBillReport";
        public static string? GetsameDischarge = "SP_GetsameDischarge";
        public static string? SP_GetStpdate = "SP_GetStpdate";
        public static string? GetNotification = "SP_GetNotification";
        public static string? GetBillingdata = "SP_GetBillingdata";
        public static string? UPloadDocMaster = "Sp_UPloadDocMaster";
        public static string? GetBillDocuments = "SP_GetBillDocuments";
        public static string? FirmwisePaidBills = "SP_FirmwisePaidBills";
        public static string? GetBillingDetails = "SP_GetBillingDetails";
        public static string? CheckEmail = "SP_CheckEmail";
        public static string? DeclineDoc = "SP_DeclineDoc";
        public static string? UploadDecDoc = "SP_UploadDecDoc";
        public static string? GetElectricitydate = "SP_GetElectricitydate";
        public static string? SP_GetBilling = "SP_GetBillingdet";
        public static string? GetBillingDetailsForApproval = "SP_BillingDetails_ForApproval";
        public static string? MoM = "SP_MoM";
        public static string? Sp_UpdateAccountVerification = "Sp_UpdateAccountVerification";
        public static string VisitorCount = "SP_VisitorCount";
        public static string FirmRegistrationToken = "SP_GetFirmRegistrationToken";
        public static string? BillApprovalDocument = "sp_InsertBillApprovalDocument";
        public static string? GetBillApprovalDocument = "SP_GetBillApprovalDocument";
        public static string GetSPS_IPSDetails = "SP_GetSPS_IPSDetails";
        public static string GetViewBillReportForVerification = "SP_GetViewBillReportForVerification";
        public static string FirmMeasurementAcceptance = "SP_FirmMeasurementAcceptance";
       //public static string GetSPS_IPSDetails = "SP_GetSPS_IPSDetails";
        public static string SP_FirmTPIDocuments = "SP_FirmTPIDocuments";
        public static string Sp_STPTree = "Sp_STPTree";
        public static string? DeleteApprovalDocument = "sp_DeleteApprovalDocument";
        public static string? GetDocumentTypeMaster = "sp_GetDocumentTypeMaster";
        public static string? BillAssignToFirm = "sp_BillAssignToFirm";
        public static string GetFirmVerificationDetails = "SP_GetFirmVerificationDetails";
        public static string GetZonewiseSTP = "SP_GetZonewiseSTP";
        public static string STPWiseOfficerMapping = "SP_STPWiseOfficerMapping";
        public static string GetEmployee = "SP_getEmployee";
        public static string ReadNotification = "SP_ReadNotification";
        public static string InsertUPJNData = "InsertUPJNData";
        public static string STPAPIresponce = "sp_STPAPIresponce";
        public static string Thirdparty_Login = "sp_Thirdparty_Login";
        public static string GetKeys = "sp_GetApiKys";
        public static string ViewBillReportOCEMS = "SP_ViewBillReportOCEMS";
        public static string sp_GetTreatedWaterVolume = "sp_GetTreatedWaterVolume";
        public static string GetTreatedWaterVolumeForFirm = "sp_GetTreatedWaterVolumeForFirm";
        public static string sp_GetUtilizationOfSTP = "sp_GetUtilizationOfSTP";
        public static string Sp_getutilizationofstpForFirm = "Sp_getutilizationofstpForFirm";
        public static string ComplaintsRecevied = "SP_ComplaintsRecevied";
        public static string ComplaintsReceviedForFirm = "SP_ComplaintsReceviedForFirm";
        public static string ComplaintsResolved = "sp_ComplaintsResolved";
        public static string ComplaintsResolvedForFirm = "sp_ComplaintsResolvedForFirm";
        public static string ComplaintsPending = "SP_ComplaintsPending";
        public static string ComplaintsPendingForFirm = "sp_ComplaintsPendingForFirm";
        public static string GetFirmSTP = "Sp_GetFirmSTP";
        public static string Payment = "SP_Payment";
        public static string ProceedPayment = "SP_ProceedPayment";
        public static string? GetInvoiceSTPWise = "sp_GetInvoiceSTPWise";
        public static string UpdateSTPRegistration = "UpdateSTPRegistration";
        public static string GetElectricityBillByID = "Sp_GetElectricityBillByID";
        public static string GetLatestBillingDate = "GetLatestBillingDate";

        public static string sp_GetElectricityBill = "sp_GetElectricityBill";
        public static string sp_UpdateElectricityBill = "sp_UpdateElectricityBill";
        public static string sp_VerifyElectricityBill = "sp_VerifyElectricityBill";

        public static string GetDGBills = "sp_GetDGBills";
        public static string sp_UpdateDeductionRelease = "sp_UpdateDeductionRelease";
        public static string VerifyDGBill = "sp_VerifyDGBill";
        public static string Sp_OCMSSheet = "Sp_OCMSSheet";
        public static string GetElectricityBillDate = "GetElectricityBillDate";
        public static string SP_GetNotificationlist = "SP_GetNotificationlist";
        public static string Sp_Complaint = "Sp_Complaint";
        public static string sp_ComplianceReport = "sp_ComplianceReport";
        public static string Sp_BillLog = "Sp_BillLog";
        public static string DivisionMaster = "Sp_DivisionMaster";
        public static string DistrictMaster = "SP_DistrictMaster";
        public static string GetCities = "sp_getcities";

        public static string? getEmployeeZone = "sp_getEmployeeZone";
    }
    public class BindList
    {
        public string? Status { get; set; }
        public string? DesignationName { get; set; }
    }
    public class CompliantList
    {
        public string? complaintsreceived { get; set; }
        public string? complaintresolved { get; set; }
        public string? pendingcomplaints { get; set; }
        public string? category { get; set; }
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

    }

}