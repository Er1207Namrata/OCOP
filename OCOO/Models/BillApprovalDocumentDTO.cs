namespace OCOO.Models
{
    public class BillApprovalDocumentDTO
    {
        public int Id { get; set; }
        public string UploadDocName { get; set; }
        public string Doc_Url { get; set; }
        public string Status { get; set; }
        public string date { get; set; }
        public string Remarks { get; set; }            
    }
    public class BillApprovalDocumentReportDTO
    {        
        public string UploadDocName { get; set; }
        public List<BillApprovalDocumentDTO> DocumentList { get; set; }
    }
}
