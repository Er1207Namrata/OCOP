using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Bill
{
    public class BillDocumentsDTO
    {
        public int Id { get; set; }
        public string UploadDocName { get; set; }
        public string Doc_Url { get; set; }
        public string Status { get; set;}
        public string date { get; set;}
        public string Remarks { get; set;}
        public string Sincedate { get; set;}
        public string Color { get; set;}
        public bool IsValid { get; set;}=true;
    }
}
