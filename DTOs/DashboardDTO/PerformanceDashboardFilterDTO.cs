using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DashboardDTO
{
    public class PerformanceDashboardFilterDTO
    {
        public string DateFilterType { get; set; }
        public List<int> Years { get; set; }
        public List<string> Quarters { get; set;}
        public List<string> Months { get; set; }
        public List<string> Days { get; set;}
        public List<int> ZoneIds { get; set;}
        public List<int> STPIds { get;set;}
        public List<int> TestingAgencyIds { get; set; }
        public int UserId {  get; set; }
    }
}
