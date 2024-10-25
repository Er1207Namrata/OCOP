using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BillFlowMapping
{
    public class BillFlowMappingMasterDTO
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int EmployeeId { get; set; }
        public int Type { get; set; }
        public bool IsForwardAllow { get; set; }
        public bool IsDocumentVerificationAllow { get; set; }
        public bool IsFinalApprovalAllow { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
