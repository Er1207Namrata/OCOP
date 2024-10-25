using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Bill
{
    public class BillReportDTO
    {
        public int BillId { get; set; }
        public int UserId { get; set; }
        public int Fk_ZoneId { get; set; }
        public string Status { get; set;}
        public bool IsPrimary { get; set; }
        public string Remarks { get; set; }
        public string ProcessType { get; set; }
        public int IsBillApprovalDocumentAttached { get; set; }
        public string FirmVerified { get; set; }
        public int DesignationId { get; set; }
        public string IsChiefEngineerApprovalEnable { get; set; }
        public int BillApprovalLevel { get; set; }
        public int BillLevel { get; set; }
    }
}
