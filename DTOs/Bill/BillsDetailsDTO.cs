using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Bill
{
    public class BillsDetailsDTO
    {
        public string HeaderText { get; set; }
        public string FirmName { get; set; }
        public string BillDate { get; set; }
        public string BillNo { get; set; }
        public string Zone { get; set; }
        public string MonthName { get; set; }
        public int Inv_Year { get; set; }
        public decimal NetLDCharges { get; set; }
        public decimal FuelCharges { get; set; }
        public decimal Grandtotal { get; set; }
        public bool IsFinalApprovalAllow { get; set; } = false;
        public int filePendingCount { get; set; } = 0;
        public List<BillDocumentsDTO> BillDocuments { get; set; }

    }
}
