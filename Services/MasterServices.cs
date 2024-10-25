using DbHelper;
using DTOs.BillFlowMapping;
using DTOs.Common;
using DTOs.DashboardDTO;
using DTOs.Employee;
using DTOs.Master;
using DTOs.Zone;
using Services.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MasterServices
    {
        private readonly DbHelperService _dbHelper;
        public readonly string ASEkey;
        public readonly int UserId;
        public MasterServices(DbHelperService _DbHelperService)
        {
            _dbHelper = _DbHelperService;
            ASEkey = "";
            UserId = 1;
        }
        #region Dropdwon master
        public async Task<List<EmployeeDropdownDTO>> GetEmployeeDropdownAsync(FilterDTO _filter)
        {
            var _response = await _dbHelper.GetAllAsync<EmployeeDropdownDTO>(Procedure.GetEmployees, _filter.EmployeeId, _filter.DepartmentId, _filter.DesignationId, _filter.ZoneId);
            return _response.Table;
        }

        public async Task<List<DepartmentDTO>> GetDepartmentDropdownAsync()
        {
            var _response = await _dbHelper.GetAllAsync<DepartmentDTO>(Procedure.GetDepartment, 0);
            return _response.Table;
        }

        public async Task<List<DesignationDTO>> GetDesignationDropdownAsync(int DepartmentId)
        {
            var _response = await _dbHelper.GetAllAsync<DesignationDTO>(Procedure.GetDesignation, 0, DepartmentId);
            return _response.Table;
        }
        public async Task<List<ZoneDropdownDTO>> GetZoneDropdownByEmployeeIdAsync(int UserId)
        {
            var _response = await _dbHelper.GetAllAsync<ZoneDropdownDTO>(Procedure.GetZoneByEmployeeId, UserId);
            return _response.Table;
        }
        public async Task<List<CommonDropdownDTO>> GetSTPByZoneAsync(DashboardFilterDTO _filter)
        {
            var _response = await _dbHelper.GetAllAsync<CommonDropdownDTO>(Procedure.GetSTPsByUserId, _filter.UserId, _filter.ZoneIds.ConvertToXml("id"));
            return _response.Table;
        }
        public async Task<List<CommonDropdownDTO>> GetInspectionAgencyAsync(DashboardFilterDTO _filter)
        {
            var _response = await _dbHelper.GetAllAsync<CommonDropdownDTO>(Procedure.GetInspectionAgency, _filter.UserId, _filter.ZoneIds.ConvertToXml("id"));
            return _response.Table;
        }



        public async Task<MerkedForwardedEmployeesDTO> GetMarkedAndForwardedEmployeeDropdownAsync(FilterDTO _req, int userid, int billId)
        {
            _req.Id = billId;
            MerkedForwardedEmployeesDTO _BillFlowMappingDTO = new MerkedForwardedEmployeesDTO();
            var _Markedresponse = await _dbHelper.GetAllAsync<EmployeeDropdownDTO>(Procedure.GetMarkedAndForwardedEmployee, _req.Id, MasterSetting.Marked, userid);
            _BillFlowMappingDTO.MarkedEmployees = _Markedresponse.Table;
            var _Forwardedresponse = await _dbHelper.GetAllAsync<EmployeeDropdownDTO>(Procedure.GetMarkedAndForwardedEmployee, _req.Id, MasterSetting.Forwarded, userid);
            _BillFlowMappingDTO.ForwardedEmployees = _Forwardedresponse.Table;

            var _BillForwardedresponse = await _dbHelper.GetAllAsync<EmployeeDropdownDTO>(Procedure.GetMarkedAndForwardedEmployee, _req.Id, MasterSetting.Decline, userid);
            _BillFlowMappingDTO.BillForwardedEmployees = _BillForwardedresponse.Table;
            return _BillFlowMappingDTO;
        }

        public async Task<List<EmployeeDropdownDTO>> GetReforwardEmployees(int billid)
        {
            var ReforwardEmployees = await _dbHelper.GetAllAsync<EmployeeDropdownDTO>(Procedure.GetReforwardEmployees, billid);
            return ReforwardEmployees.Table;
        }
        #endregion

        #region Bill Flow Mapping
        public async Task<CommonDbResponseDTO> SaveBillFlowMapping(BillFlowMappingDTO _BillFlowMapping) => await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.SaveBillFlowMapping, _BillFlowMapping.BillFlowMappedEmployees.ToXML(), _BillFlowMapping.MarkedEmployeeIds.ConvertToXml("id"), _BillFlowMapping.ForwardedEmployeeIds.ConvertToXml("id"), _BillFlowMapping.ZoneId, _BillFlowMapping.Id);
        public async Task<BillFlowMappingDTO> GetBillFlowMappingAsync(BillFlowMappingDTO _req)
        {
            var _response = await _dbHelper.GetAllAsync<BillFlowMappingMasterDTO>(Procedure.GetBillFlowMapping, _req.Id, _req.ZoneId);
            List<BillFlowMappingMasterDTO> _List = _response.Table;
            BillFlowMappingDTO _BillFlowMappingDTO = new BillFlowMappingDTO();
            if (_List != null && _List.Count > 0)
            {
                _BillFlowMappingDTO.ZoneId = _List.FirstOrDefault().ZoneId;
                _BillFlowMappingDTO.MarkedEmployeeIds = _List.Where(x => x.Type == MasterSetting.Marked).Select(x => x.EmployeeId).ToList();
                _BillFlowMappingDTO.ForwardedEmployeeIds = _List.Where(x => x.Type == MasterSetting.Forwarded).Select(x => x.EmployeeId).ToList();
                _BillFlowMappingDTO.BillFlowMappedEmployees = _List.Where(x => x.Type == MasterSetting.Forwarded).Select(x => new BillFlowMappedEmployeesDTO { EmployeeId = x.EmployeeId, Forword = x.IsForwardAllow, DocumentVerification = x.IsDocumentVerificationAllow, FinalApproval = x.IsFinalApprovalAllow }).ToList();
            }
            return _BillFlowMappingDTO;
        }
        public async Task<PerformanceDashboardResponseDTO> GetPerformanceDashboard(PerformanceDashboardFilterDTO _objFilter, int UserId)
        {
            try
            {
                if (_objFilter == null)
                    _objFilter = new PerformanceDashboardFilterDTO();

                if (_objFilter.Days.Count == 0 && _objFilter.Months.Count == 0 && _objFilter.Years.Count == 0 && _objFilter.Quarters.Count == 0)
                    _objFilter.DateFilterType = null;
                _objFilter.UserId = UserId;
                _objFilter.Quarters = _objFilter.Quarters.Count > 0 ? UtilsServices.GetDatesQuarterWise(_objFilter.Quarters) : _objFilter.Quarters;
                var _response = await _dbHelper.GetAllAsync<PerformanceDashboardDTO>(Procedure.GetPerformanceDashboardData, _objFilter.DateFilterType, _objFilter.Years.ConvertToXml("year"), _objFilter.Quarters.ConvertToXml("quarter"), _objFilter.Months.ConvertToXml("month"), _objFilter.Days.ConvertToXml("day"), _objFilter.ZoneIds.ConvertToXml("zone_id"), _objFilter.STPIds.ConvertToXml("stp_id"), _objFilter.TestingAgencyIds.ConvertToXml("id"), _objFilter.UserId);
                if (_response != null)
                    return new PerformanceDashboardResponseDTO { Status = true, Message = "Success", Data = _response.Table.GroupBy(x => x.ZoneName).Select(x => new PerformanceDashboardZoneWiseDTO { ZoneName = x.Key, STPsDataList = x.Where(y => y.ZoneName == x.Key).ToList() }).ToList() };
                else
                    return new PerformanceDashboardResponseDTO { Status = false, Message = "No data found", Data = new List<PerformanceDashboardZoneWiseDTO> { } };
            }
            catch (Exception ex)
            {
                return new PerformanceDashboardResponseDTO { Status = false, Message = ex.Message, Data = new List<PerformanceDashboardZoneWiseDTO> { } };
            }
        }
        
        #endregion
    }
}
