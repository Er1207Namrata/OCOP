using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DashboardDTO
{

    public class ChartDTO
    {
        public string ChartName { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int Value { get; set; }
    }

    public class ChartDataDTO
    {
        public string ChartName { get; set; }
        public List<YearlyChartDataDTO> YearlyData { get; set; }
    }

    public class YearlyChartDataDTO
    {
        public string Year { get; set; }
        public List<string> Months { get; set; }
        public List<int> Value { get; set; }
    }  

}
