using DTOs.Master;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Bill
{
    public class BillApprovalDTO: EntityDTO
    {
        public int BillId { get; set; }
        public int ProcessType { get; set; }
        public string EmployeesId {  get; set; }
        public List<int> markedEmployees { get; set; }
        public string Status { get; set; }  
        public string Remarks { get; set; }
        public IFormFile CoveringLetter { get; set; }
        public string CoveringLetterFilePath { get; set; }
        public IFormFile LDCoveringLetter { get; set; }
        public string LDCoveringLetterFilePath { get; set; }
        public string IsVerified { get; set; }
    }
}
