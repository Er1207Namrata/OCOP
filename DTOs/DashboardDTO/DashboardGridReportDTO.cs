using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DashboardDTO
{
    public class DashboardGridReportDTO
    {
        public string? ZoneName { get; set; }
        public int TotalOperationDays { get; set; }
        public int BODFailDays { get; set; }
        public int BODFailDaysStatus { get; set; }
        public int CODFailDays { get; set; }
        public int CODFailDaysStatus { get; set; }
        public int TSSFailDays { get; set; }
        public int TSSFailDaysStatus { get; set; }
        public int FCFailDays { get; set; }
        public int FCFailDaysStatus { get; set; }
        public int QualityKPIStatus { get; set; }
        public double QuantityKPI { get; set; }
        public int QuantityKPIStatus { get; set; }
        public double AverageOfActualFlow { get; set; }
        public int OverallKPIStatus { get; set; }

    }
    public class DashboardGridDataDTO
    {
        public string ZoneName { get; set; }
        public List<DashboardGridReportDTO> _Data { get; set; }
    }
}
