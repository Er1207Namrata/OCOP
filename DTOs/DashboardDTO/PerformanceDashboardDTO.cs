using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DashboardDTO
{
    public class PerformanceDashboardDTO
    {
        public long ZoneId { get; set; }
        public string ZoneName { get; set; }
        public long FirmId { get; set; }
        public string FirmName { get; set;}
        public long STPId { get; set; }
        public string STPName { get; set;}
        public int TotalOperationDays { get;set; }
        public int BODFailDays { get;set;}
        public bool BODFailDaysStatus { get;set; }
        public int CODFailDays { get;set;}
        public bool CODFailDaysStatus { get;set; }
        public int TSSFailDays { get;set; }
        public bool TSSFailDaysStatus { get;set; }
        public int FCFailDays { get; set; }
        public bool FCFailDaysStatus { get; set; }
        public int QuantityKPI { get; set; }
        public bool QuantityKPIStatus { get;set; }
        public float AverageOfActualFlow { get;set; }
        public bool OverallKPIStatus { get;set; }
    }
    public class PerformanceDashboardZoneWiseDTO
    {
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        public List<PerformanceDashboardDTO>STPsDataList { get; set; }
    }
    public class PerformanceDashboardResponseDTO
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<PerformanceDashboardZoneWiseDTO> Data { get; set; }
    }
}
