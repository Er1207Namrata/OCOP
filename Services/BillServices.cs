using DbHelper;
using DTOs.Bill;
using DTOs.BillFlowMapping;
using DTOs.Common;
using DTOs.Employee;
using DTOs.Master;
using DTOs.Zone;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BillServices
    {
        private readonly DbHelperService _dbHelper;
        public readonly string ASEkey;
        public readonly int UserId;
        public BillServices(DbHelperService _DbHelperService)
        {
            _dbHelper = _DbHelperService;
            ASEkey = "";
            UserId = 1;
        }


        #region Mark Bill
        public async Task<CommonDbResponseDTO> BillApprovalProcess(BillApprovalDTO _req) => await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.BillsMarkedApproval, _req.BillId, _req.ProcessType, _req.markedEmployees.ConvertToXml("id"), _req.CoveringLetterFilePath, _req.LDCoveringLetterFilePath, _req.Status, _req.Remarks, _req.Id);
        public async Task<BillReportDTO> GetBillStatusByUserId(int BillId, int UserId) => await _dbHelper.ExecuteQueryAsync<BillReportDTO>(Procedure.GetBillStatusByUserId, BillId, UserId);
        public async Task<BillsDetailsDTO> GetBillsDetailAsync(int BillId, int UserId) => await _dbHelper.ExecuteQueryAsync<BillsDetailsDTO>(Procedure.GetBillsDetails, BillId, UserId);
        public async Task<List<BillDocumentsDTO>> GetBillDocumentsAsync(int BillId, int UserId)
        {
            var _response = await _dbHelper.GetAllAsync<BillDocumentsDTO>(Procedure.GetBillDocuments, BillId);
            return _response.Table ?? new List<BillDocumentsDTO>();
        }

        #endregion
        #region Bill document verification
        public async Task<CommonDbResponseDTO> BillDocumentVerificationAsync(BillDocumentVerificationDTO _req, int UserId) => await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.BillDocumentVerification, UserId, _req.Id, _req.Status, _req.Remark);

        public async Task<CommonDbResponseDTO> UpdateBillBOCODTSSFCAsync(UpdateBillDTO _req, int BillingId, int UserId,int Pk_ZoneId)
        { 
          var response = await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.UpdateBillBOCODTSSFC, UserId, BillingId, Pk_ZoneId, _req.IsBODChanged, _req.IsCODChanged, _req.IsTSSChanged, _req.IsFCChanged, _req.ExistingBODValue, _req.ExistingCODValue, _req.ExistingTSSValue, _req.ExistingFCValue, _req.BODValue, _req.CODValue, _req.TSSValue, _req.FCValue, _req.WithheldRemark, _req.BODCODTSSFCWithheldAttachmentDoc, _req.WithheldAmount);

            return response;
        }
        public async Task<CommonDbResponseDTO> UpdateBillComplaintAsync(UpdateBillDTO _req, int BillingId, int UserId) => await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.UpdateComplaint, UserId, BillingId, _req.IsReceivedComplaintChanged, _req.IsResolvedComplaintChanged, _req.ExistingReceivedComplaintValue, _req.ExistingResolvedComplaintValue, _req.ReceivedComplaint, _req.ResolvedComplaint, _req.ComplaintLD, _req.LDAmount, _req.WithheldRemark, _req.ComplaintWithheldAttachmentDoc, _req.WithheldAmount);
        public async Task<CommonDbResponseDTO> UpdateWaterFlowAsync(UpdateBillDTO _req, int BillingId, int UserId)
        {
            return await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.UpdateWaterFlow, UserId, BillingId, _req.IsActualAchievedChanged, _req.ExistingActualAchievedValue, _req.ActualAchieved, _req.WithheldRemark, _req.WaterDischargeWithheldAttachmentDoc, _req.WithheldAmount);
        }
        public async Task<CommonDbResponseDTO> JEBillApprovalAsync(int UserId, int BillingId, string Remarks, string AttachedFilePath) => await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.BillApprovalByJE, UserId, BillingId, Remarks, AttachedFilePath);
        public async Task<CommonDbResponseDTO> JEBillSTPWiseApprovalAsync(int UserId, int BillingId, int STPId) => await _dbHelper.ExecuteQueryAsync<CommonDbResponseDTO>(Procedure.BillApprovalByJESTPWise, UserId, BillingId, STPId);
        #endregion
    }
}
