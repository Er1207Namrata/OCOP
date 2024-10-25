using DTOs.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BillFlowMapping
{
    public class BillFlowMappingDTO : UserInfoDTO
    {
        public int ZoneId { get; set; }
        public List<int> MarkedEmployeeIds { get; set; }
        public List<int> ForwardedEmployeeIds { get; set; }
        public List<BillFlowMappedEmployeesDTO> BillFlowMappedEmployees { get; set; }
    }
    public class BillFlowMappedEmployeesDTO
    {
        public int EmployeeId { get; set; }
        public bool Forword { get; set; }
        public bool DocumentVerification { get; set; }
        public bool FinalApproval { get; set; }
    }
}
