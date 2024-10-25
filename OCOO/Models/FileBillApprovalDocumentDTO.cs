namespace OCOO.Models
{
    public class FileBillApprovalDocumentDTO
    {
        public int AttachmentTypeId { get; set; }
        public IFormFile DocumentAttachment { get; set; }
        public string Remarks { get; set; }
    }

}
