using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Bill
{
    public class BillDocumentVerificationDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
