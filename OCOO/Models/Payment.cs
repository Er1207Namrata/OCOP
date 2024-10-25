
using Microsoft.Office.Interop.Word;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace OCOO.Models
{
    public class Payment:Menu
    {
        public string? PK_FbillId { get; set; }
        public string? Bill_Month { get; set; }
        public string? Bill_Year { get; set; }
        public string? FK_FirmId { get; set; }
        public string? BillNo { get; set; }
        public Decimal Amount { get; set; }
        public string? PaymentMode { get; set; }
        public string? RefrenceNo { get; set; }
        public IFormFile? AttachmentURl { get; set; }
        public string? AttachmentDoc { get; set; }
        public string? Remark { get; set; }
        public string? Month { get; set; }
        public string? Bill_Amount { get; set; }
        public string? Payable_Amount { get; set; }
        public string? Zone { get; set; }
        public string? Date { get; set; }
        public string? PaymentDate { get; set; }
        public string? CrAmount { get; set; }
        public string? DrAmount { get; set; }
        public string? WithheldAmount { get; set; }
      
       
        public DataSet GetPayment()
        {
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@PK_FbillId",PK_FbillId == "0"? null:PK_FbillId),
                new SqlParameter("@Fk_ZoneId",Pk_ZoneId == "0"? null:Pk_ZoneId),
                new SqlParameter("@FK_FirmId",Fk_FirmId == "0"? null:Fk_FirmId),
                new SqlParameter("@Bill_Month",Fk_MonthId == "0" ? null : Fk_MonthId),
                new SqlParameter("@Bill_Year",Years == "0" ? null:Years),
                new SqlParameter("@OpCode",OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.Payment, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ProceedPaymentAdd()
        {
            try
            {
                
                SqlParameter[] para = {
                new SqlParameter("@PK_FbillId",PK_FbillId),
                new SqlParameter("@Amount",Bill_Amount),
                new SqlParameter("@WithheldAmount",WithheldAmount),
                new SqlParameter("@DrAmount",DrAmount),
                new SqlParameter("@PaymentMode",PaymentMode),
                new SqlParameter("@RefrenceNo",RefrenceNo),
                new SqlParameter("@Attachment_URL",AttachmentDoc),
                new SqlParameter("@PaymentDate",PaymentDate),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Remark",Remark)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ProceedPayment, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
