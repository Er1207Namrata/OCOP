using DbHelper;
using DTOs.BillFlowMapping;
using DTOs.DashboardDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Extensions;

namespace Services
{
    public class DashbaordService
    {
        private readonly DbHelperService _dbHelper;
        public readonly string ASEkey;
        public DashbaordService(DbHelperService _DbHelperService)
        {
            _dbHelper = _DbHelperService;
            ASEkey = "";

        }
        public async Task<List<ChartDataDTO>> GetDashboardChartDataAsync(PerformanceDashboardFilterDTO _objFilter)
        {
            if (_objFilter == null)
                _objFilter = new PerformanceDashboardFilterDTO();
            if (_objFilter.Days.Count == 0 && _objFilter.Months.Count == 0 && _objFilter.Years.Count == 0 && _objFilter.Quarters.Count == 0)
                _objFilter.DateFilterType = null;
            _objFilter.Quarters = _objFilter.Quarters.Count > 0 ? UtilsServices.GetDatesQuarterWise(_objFilter.Quarters) : _objFilter.Quarters;
            var _response = await _dbHelper.GetAllAsync<ChartDTO>(Procedure.GetDashboardChartData, _objFilter.DateFilterType, _objFilter.Years.ConvertToXml("year"), _objFilter.Quarters.ConvertToXml("quarter"), _objFilter.Months.ConvertToXml("month"), _objFilter.Days.ConvertToXml("day"), _objFilter.ZoneIds.ConvertToXml("zone_id"), _objFilter.STPIds.ConvertToXml("stp_id"), _objFilter.TestingAgencyIds.ConvertToXml("id"), _objFilter.UserId);
            List<ChartDTO> _ChartList = _response.Table;
            List<ChartDataDTO> _ChartData = new List<ChartDataDTO>();
            if (_ChartList != null && _ChartList.Count > 0)
                _ChartData = await ConvertToChartDataDTOAsync(_ChartList);
            return _ChartData;
        }
        public static async Task<List<ChartDataDTO>> ConvertToChartDataDTOAsync(List<ChartDTO> chartDTOs)
        {
            return await Task.Run(() =>
            {
                var groupedData = chartDTOs
                    .GroupBy(item => item.ChartName)
                    .Select(chartGroup => new ChartDataDTO
                    {
                        ChartName = chartGroup.Key,
                        YearlyData = chartGroup
                            .GroupBy(item => item.Year)
                            .Select(yearGroup => new YearlyChartDataDTO
                            {
                                Year = yearGroup.Key,
                                Months = yearGroup.Select(x => x.Month).ToList(),
                                Value = yearGroup.Select(x => x.Value).ToList()
                            }).ToList()
                    }).ToList();
                return groupedData;
            });
        }

    }
}
