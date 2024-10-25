using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Bill
{
    public class UpdateBillDTO
    {
        public string? fieldid_hidden { get; set; }
        public int BillingId { get; set; }
        public bool IsBODChanged { get; set; }
        public bool IsCODChanged { get; set; }
        public bool IsTSSChanged { get; set; }
        public bool IsFCChanged { get; set; }

        public decimal ExistingBODValue { get; set; }
        public decimal ExistingCODValue { get; set; }
        public decimal ExistingTSSValue { get; set; }
        public decimal ExistingFCValue { get; set; }

        public decimal BODValue { get; set; }
        public decimal CODValue { get; set; }
        public decimal TSSValue { get; set; }
        public decimal FCValue { get; set; }

        //Used for the Complaint
        public bool IsReceivedComplaintChanged { get; set; }
        public bool IsResolvedComplaintChanged { get; set; }
        public int ExistingReceivedComplaintValue { get; set; }
        public int ExistingResolvedComplaintValue { get; set; }
        public int ReceivedComplaint { get; set; }
        public int ResolvedComplaint { get; set; }
        public int ComplaintLD { get; set; }
        public decimal LDAmount { get; set; }
        public bool IsActualAchievedChanged { get; set; }
        public decimal  ExistingActualAchievedValue {  get; set; }
        public decimal ActualAchieved { get; set; }
        public string? WithheldAmount { get; set; }
        public IFormFile? WithheldAttachment { get; set; }
        public string? WithheldAttachmentDoc { get; set; }
        public string? WithheldRemark { get; set; }
        //ask
        public string? BODCODTSSFCWithheldAmount { get; set; }
        public string? BODCODTSSFCWithheldRemark { get; set; }
        public IFormFile? BODCODTSSFCWithheldAttachment { get; set; }
        public string? BODCODTSSFCWithheldAttachmentDoc { get; set; }

        public string? ComplaintWithheldAmount { get; set; }
        public string? ComplaintWithheldRemark { get; set; }
        public IFormFile? ComplaintWithheldAttachment { get; set; }
        public string? ComplaintWithheldAttachmentDoc { get; set; }

        public string? WaterDischargeWithheldAmount { get; set; } = "0";
        public string WaterDischargeWithheldRemark { get; set; } = "N/A";
        public IFormFile? WaterDischargeWithheldAttachment { get; set; }
        public string WaterDischargeWithheldAttachmentDoc { get; set; } = "";
    }
    public class BillApprovalReqesutDTO
    {
        public int Bill_Id { get; set; }
        public string Remarks { get; set; }
        public IFormFile Attachment { get; set; }
        public string Attachment_Path { get; set; }
        public int STPId { get; set; }



    }
}
