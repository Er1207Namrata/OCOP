using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DashboardDTO
{
    public class DashboardFilterDTO
    {
      
        public List<int> ZoneIds { get; set; }
        public List<int> STPIds { get; set; }
        public List<int> InspectionAgencyIds { get; set; }
        public int UserId { get; set; }

    }
}
