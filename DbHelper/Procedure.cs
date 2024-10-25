using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper
{
    public static class Procedure
    {
        public static readonly string GetDepartment = "sp_GetDepartment";
        public static readonly string GetDesignation = "sp_Designation";        
        public static readonly string GetEmployees = "sp_GetEmployees";
        public static readonly string SaveBillFlowMapping = "sp_SaveBillFlowMapping";
        public static readonly string GetBillFlowMapping = "sp_GetBillFlowMapping";
        public static readonly string GetZoneByEmployeeId = "sp_GetZoneByEmployeeId";
        public static readonly string GetMarkedAndForwardedEmployee = "sp_GetMarkedAndForwardedEmployee";
        public static readonly string BillsMarkedApproval = "sp_BillsMarkedApproval";
        public static readonly string GetBillStatusByUserId = "sp_GetBillStatusByUserId";
        public static readonly string GetSTPsByUserId = "sp_GetSTPsByUserId";
        public static readonly string GetInspectionAgency = "sp_GetInspectionAgency";
        public static readonly string GetDashboardChartData = "sp_GetDashboardChartData";
        public static readonly string GetDashboardGridData = "sp_GetDashboardGridData";
        public static readonly string GetBillsDetails = "SP_BillsDetails";
        public static readonly string GetBillDocuments = "SP_GetBillDocuments";

        public static readonly string BillDocumentVerification = "sp_BillDocumentVerification";
        public static readonly string UpdateBillBOCODTSSFC = "sp_UpdateBillBOCODTSSFC";
        public static readonly string UpdateComplaint = "sp_UpdateComplaint";
        public static readonly string UpdateWaterFlow = "sp_UpdateWaterFlow";
        public static readonly string BillApprovalByJE = "sp_BillApprovalByJE";
        public static readonly string GetReforwardEmployees = "sp_GetReforwardEmployees";
        public static readonly string BillApprovalByJESTPWise = "sp_BillApprovalByJESTPWise";
        public static readonly string GetPerformanceDashboardData = "sp_GetPerformanceDashboardData";

    }
}
