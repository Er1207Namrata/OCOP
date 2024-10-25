using DTOs.Bill;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using OCOO.Models.FirmMasters;
using Services;
using Services.Extensions;
using System.Data;
using System.Reflection;

namespace OCOO.Controllers
{

    public class BillController : Controller
    {
        private readonly BillServices _billServices;
        IHttpContextAccessor _httpContextAccessor;
        private readonly int UserId = 0;
        private readonly int DesignationId = 0;
        private readonly int DepartmentId = 0;
        private readonly int Pk_ZoneId = 0;
        List<FileBillApprovalDocumentDTO> temporaryFiles = new List<FileBillApprovalDocumentDTO>();
        public BillController(BillServices billServices, IHttpContextAccessor acc)
        {
            _billServices = billServices;
            this._httpContextAccessor = acc;
            UserId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Session.GetString("Pk_UserId"));
            Pk_ZoneId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Session.GetString("Pk_ZoneId"));
            DesignationId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Session.GetString("DesignationId"));
            DepartmentId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Session.GetString("DepartmentId"));
            RedirectToAction("Login", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProcessBillAsync(string id)
        {
            try
            {
              //  if (DesignationId == 4 && new List<int> { 3, 4, 13 }.Contains(DepartmentId)) return RedirectToAction("BillDocument", "Bill", new { id = id });


                ViewBag.EncryptedBillId = id;
                ViewBag.BillId = Convert.ToInt32(Crypto.Decrypt(id));
                var _response = await _billServices.GetBillStatusByUserId(Convert.ToInt32(Crypto.Decrypt(id)), UserId);
                ViewBag.IsPrimary = !_response.IsPrimary?0:1;
                ViewBag.IsBillApprovalDocumentAttached = _response.IsBillApprovalDocumentAttached;
                ViewBag.FirmVerified = _response.FirmVerified;
                ViewBag.DesignationId = _response.DesignationId;
                ViewBag.BillApprovalLevel = _response.BillApprovalLevel.ToString();
                ViewBag.IsChiefEngineerApprovalEnable = _response.IsChiefEngineerApprovalEnable;
                ViewBag.ZoneId = _response.Fk_ZoneId.ToString();
                if (_response != null)
                    ViewBag.Status = _response.Status;
                else
                    ViewBag.Status = "Invalid";
                return View();
            }
            catch (Exception ex) { throw ex; }
        }
        [HttpPost]
        public async Task<JsonResult> MarkedSaveAsync([FromForm] BillApprovalDTO _post)
        {
            try
            {
                _post.IsVerified = _post.IsVerified == "undefined" ? "false" : "true";
                string? foldername = "ApprovalDocument";
                if (_post.CoveringLetter != null && _post.CoveringLetter.Length > 0)
                    _post.CoveringLetterFilePath = await FileManagement.WriteFiles(_post.CoveringLetter, "Approval Documents", foldername);

                if (_post.LDCoveringLetter != null && _post.LDCoveringLetter.Length > 0)
                    _post.LDCoveringLetterFilePath = await FileManagement.WriteFiles(_post.LDCoveringLetter, "Approval Documents", foldername);
                _post.Id = UserId;
                _post.markedEmployees = !string.IsNullOrEmpty(_post.EmployeesId) ? _post.EmployeesId.ConvertToIntArray() : new List<int>();
                var _response = await _billServices.BillApprovalProcess(_post);
                return Json(_response);
            }
            catch (Exception ex) { throw ex; }
        }
        //public async Task<JsonResult> GetBillsDetailsAsync(int id) => Json(await _billServices.GetBillsDetailAsync(id, UserId));

        public async Task<IActionResult> DownloadImage(string filename)
        {
            try
            {
                string FileFullPath = HttpContext.Session.GetString("FilePath").ToString();
                var path = FileFullPath + filename;
                MemoryStream memory = new MemoryStream();
                FileInfo file = new FileInfo(path);
                if (file.Exists.Equals(true))
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, "image/png", filename);
                }
                path = "wwwroot/assets/images/NoRecord.png";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "image/png", "NoRecordFound.png");
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<JsonResult> GetBillsDetailsAsync(int id)
        {
            try
            {
                var _response = await _billServices.GetBillsDetailAsync(id, UserId);
                if (_response != null)
                    _response.BillDocuments = await _billServices.GetBillDocumentsAsync(id, UserId);
                return Json(_response);
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<IActionResult> BillDocumentAsync(int id)
        {
            try
            {
                ViewBag.DesignationId = Convert.ToInt32(HttpContext.Session.GetString("DesignationId"));
                ViewBag.DepartmentId = Convert.ToInt32(HttpContext.Session.GetString("DepartmentId"));
                ViewBag.BillId = id;
                return View();
            }
            catch (Exception ex) { throw ex; }
        }
        [HttpPost]
        public async Task<JsonResult> BillDocumentVerificationAsync([FromForm] BillDocumentVerificationDTO _req) => Json(await _billServices.BillDocumentVerificationAsync(_req, UserId));


        public ActionResult BillApproval(string id, string st)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                model.DesignationId = DesignationId;
                model.DepartmentId = DepartmentId;
                int billId = Convert.ToInt32(Crypto.Decrypt(id));
                int STPID = Convert.ToInt32(Crypto.Decrypt(st));
                ViewBag.BillId = billId;
                ViewBag.STPId = STPID;
                DataSet ds = model.GetBillingDetailsForApproval(billId, UserId, STPID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
        }
       // [Route("{controller}/{action}/{id}")]
        public ActionResult BillSTPWiseApproval(string id, string newId)
        {
            try
            {
                if (!string.IsNullOrEmpty(newId))
                    id = newId;
                FirmDetails model = new FirmDetails();
                model.DesignationId = DesignationId;
                model.DepartmentId = DepartmentId;
                int billId = Convert.ToInt32(Crypto.Decrypt(id));

                ViewBag.BillId = billId;
                DataSet ds = model.GetInvoiceSTPWise(billId, UserId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateBillBOCODTSSFCAsync([FromForm] UpdateBillDTO _req)
        {
            string WithheldAttachment = "";
            string? foldername = "billdocuments";
            if (_req.WithheldAttachment != null && _req.WithheldAttachment.Length > 0)
            {
                WithheldAttachment = FileManagement.WriteFiles(_req.WithheldAttachment, "BillApprovalDocuments", foldername).Result;
                _req.BODCODTSSFCWithheldAttachmentDoc = WithheldAttachment;
            }
            var response = await _billServices.UpdateBillBOCODTSSFCAsync(_req, Convert.ToInt32(Crypto.Decrypt(_req.fieldid_hidden)), UserId, Pk_ZoneId);
            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> UpdateBillComplaintAsync([FromForm] UpdateBillDTO _req)
        {
            string WithheldAttachment = "";
            string? foldername = "billdocuments";
            if (_req.WithheldAttachment != null && _req.WithheldAttachment.Length > 0)
            {
                WithheldAttachment = FileManagement.WriteFiles(_req.WithheldAttachment, "BillApprovalDocuments", foldername).Result;
                _req.ComplaintWithheldAttachmentDoc = WithheldAttachment;
            }
            var response = await _billServices.UpdateBillComplaintAsync(_req, Convert.ToInt32(Crypto.Decrypt(_req.fieldid_hidden)), UserId);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateBillWaterDischargeAsync([FromForm] UpdateBillDTO _req)
        {
            string WithheldAttachment = "";
            string? foldername = "billdocuments";
            if (_req.WaterDischargeWithheldAttachment != null && _req.WaterDischargeWithheldAttachment.Length > 0)
            {
                WithheldAttachment = FileManagement.WriteFiles(_req.WaterDischargeWithheldAttachment, "BillApprovalDocuments", foldername).Result;
                _req.WaterDischargeWithheldAttachmentDoc = WithheldAttachment;
            }
            var response = await _billServices.UpdateBillComplaintAsync(_req, Convert.ToInt32(Crypto.Decrypt(_req.fieldid_hidden)), UserId);
            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> UpdateWaterFlowAsync([FromForm] UpdateBillDTO _req)
        {
            try
            {
                string WithheldAttachment = "";
                string? foldername = "billdocuments";               
                if (_req.WithheldAttachment != null && _req.WithheldAttachment.Length > 0)
                {
                    WithheldAttachment = FileManagement.WriteFiles(_req.WithheldAttachment, "BillApprovalDocuments", foldername).Result;
                    _req.WaterDischargeWithheldAttachmentDoc = WithheldAttachment;
                }
                var response = await _billServices.UpdateWaterFlowAsync(_req, Convert.ToInt32(Crypto.Decrypt(_req.fieldid_hidden)), UserId);
                return Json(response);
            }
            catch (Exception ex)
            { 
            }
            return Json(null);
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JEBillSTPWiseApproval([FromForm] BillApprovalReqesutDTO _req)
        {

            var response = _billServices.JEBillSTPWiseApprovalAsync(UserId, _req.Bill_Id, _req.STPId).Result;
            TempData["Message"] = response.Message;
            return RedirectToAction("BillSTPWiseApproval", "Bill", new { newId = Crypto.Encrypt(_req.Bill_Id.ToString()) });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JEBillApproval([FromForm] BillApprovalReqesutDTO _req)
        {
            string? foldername = "billdocuments";
            if (_req.Attachment != null && _req.Attachment.Length > 0)
                _req.Attachment_Path = FileManagement.WriteFiles(_req.Attachment, "BillApprovalDocuments", foldername).Result;

            var response = _billServices.JEBillApprovalAsync(UserId, _req.Bill_Id, _req.Remarks, _req.Attachment_Path).Result;
            return RedirectToAction("BillApprovalList", "ApproveBilling", new { id = _req.Bill_Id });

        }

        [HttpGet]
        public ActionResult BillApprovalDocument(string id)
        {
            FirmDetails model = new FirmDetails();
            model.DesignationId = DesignationId;
            model.DepartmentId = DepartmentId;
            int billId = Convert.ToInt32(Crypto.Decrypt(id));
            ViewBag.BillId = billId;
            ViewBag.id = id;

            model._DocumentTypeDropdown = model.GetDocumentTypeDropdown();
            ViewBag.ddlDocumentType = model._DocumentTypeDropdown;
            DataSet ds = model.GetBillingDetailsForApproval(billId, UserId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model.dtDetails = ds.Tables[0];
            return View(model);
        }


        [HttpGet]
        public ActionResult SaveFileBillApprovalDocument(string id)
        {
            FirmDetails model = new FirmDetails();
            model.DesignationId = DesignationId;
            model.DepartmentId = DepartmentId;
            int billId = Convert.ToInt32(Crypto.Decrypt(id));
            ViewBag.BillId = billId;

            DataSet ds = model.GetBillingDetailsForApproval(billId, UserId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model.dtDetails = ds.Tables[0];
            return View(model);
        }


        [HttpPost]
        public IActionResult SaveFile(IFormFile file, int AttachmentTypeId, string remark, string saveButton)
        {
            if (saveButton != null)
            {
                temporaryFiles.Add(new FileBillApprovalDocumentDTO { AttachmentTypeId = AttachmentTypeId, Remarks = remark, DocumentAttachment = file });
                return RedirectToAction("AddFile");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("[Controller]/[Action]/{BillId}")]
        public async Task<IActionResult> AddAttachments(int BillId, List<int> AttachmentTypeId, List<string> Remarks, List<IFormFile> attachments)
        {
            if (attachments == null || attachments.Count == 0)
                return BadRequest("No attachments provided.");
            FirmBilling firmBilling = new FirmBilling();
            DataTable multipleImage = new DataTable();
            multipleImage.Columns.Add("Fk_UploadDocId");
            multipleImage.Columns.Add("Doc_Url");
            multipleImage.Columns.Add("Remark");
            int i = 0;
            foreach (var attachment in attachments)
            {
                string FileType = AttachmentTypeId[i] == 2 ? "TPIReports" : (AttachmentTypeId[i] == 3 ? "LDReport" : "OtherDocuments");
                multipleImage.Rows.Add(AttachmentTypeId[i], (attachments[i] != null) ? await FileManagement.WriteFiles(attachments[i], FileType, "GenerateBill") : string.Empty, Remarks[i]);
                i++;
            }
            firmBilling.MultipleImage = multipleImage;
            firmBilling.AddedBy = HttpContext.Session.GetString("Pk_UserId");
            firmBilling.Fk_BillId = BillId.ToString();
            DataSet ds = firmBilling.BillApprovalDocument();
            string message = string.Empty;
            bool status = false;
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    message = ds.Tables[0].Rows[0]["Message"].ToString();
                    status = true;
                }
                else
                {
                    message = ds.Tables[0].Rows[0]["Message"].ToString();
                    status = false;
                }
            return Ok(new { status = status, message = message });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BillApprovalDocument(FirmBilling firmBilling)
        {
            try
            {
                DataTable multipleImage = new DataTable();
                multipleImage.Columns.Add("Doc_Url");
                multipleImage.Columns.Add("Fk_UploadDocId");
                if (firmBilling.TPIReports != null)
                    for (int i = 0; i < firmBilling.TPIReports.Length; i++)
                        multipleImage.Rows.Add(2, (firmBilling.LDSheet[i] != null) ? await FileManagement.WriteFiles(firmBilling.TPIReports[i], "TPIReports", "GenerateBill") : string.Empty);

                if (firmBilling.LDSheet != null)
                    for (int i = 0; i < firmBilling.LDSheet.Length; i++)
                        multipleImage.Rows.Add(3, (firmBilling.LDSheet[i] != null) ? await FileManagement.WriteFiles(firmBilling.LDSheet[i], "LDReport", "GenerateBill") : string.Empty);

                if (firmBilling.OtherDocuments != null)
                    for (int i = 0; i < firmBilling.OtherDocuments.Length; i++)
                        multipleImage.Rows.Add(4, (firmBilling.OtherDocuments[i] != null) ? await FileManagement.WriteFiles(firmBilling.OtherDocuments[i], "OtherDocuments", "GenerateBill") : string.Empty);

                firmBilling.MultipleImage = multipleImage;
                firmBilling.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = firmBilling.BillApprovalDocument();
                if (ds != null && ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    else
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
            }
            catch (Exception Ex) { TempData["ErrorMessage"] = Ex.Message; }
            FirmDetails model = new FirmDetails();
            DataSet _ds = model.GetBillingDetailsForApproval(Convert.ToInt32(firmBilling.Fk_BillId), UserId);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                model.dtDetails = _ds.Tables[0];
            return RedirectToAction("ProcessBill", new { id = Crypto.Encrypt(firmBilling.Fk_BillId) });
        }

        [HttpGet]
        public JsonResult FetchBillApprovalDocument(string xyz, string mpa)
        {
            List<BillApprovalDocumentReportDTO> _DocReport = new List<BillApprovalDocumentReportDTO>();
            List<BillApprovalDocumentDTO> DocumentList = new List<BillApprovalDocumentDTO>();
            try
            {
                InvoiceDetails model = new InvoiceDetails();
                if (xyz != null)
                {
                    model.BillNo = Crypto.Decrypt(xyz);
                    DataSet ds = model.GetBillApprovalDocument();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DocumentList = (from c in ds.Tables[0].AsEnumerable()
                                        select new BillApprovalDocumentDTO
                                        {
                                            Id = c.Field<int>("Id"),
                                            UploadDocName = c.Field<string>("UploadDocName"),
                                            Doc_Url = c.Field<string>("Doc_Url"),
                                            Status = c.Field<string>("Status"),
                                            date = c.Field<string>("date"),
                                            Remarks = c.Field<string>("Remarks")
                                        }).ToList();
                          _DocReport = DocumentList.GroupBy(x => x.UploadDocName).Select(x => new BillApprovalDocumentReportDTO { UploadDocName = x.Key, DocumentList = DocumentList.Where(y => y.UploadDocName == x.Key).Select(z => new BillApprovalDocumentDTO { Id = z.Id, Doc_Url = z.Doc_Url, UploadDocName = z.UploadDocName, Status = z.Status, Remarks = z.Remarks, date = z.date }).ToList() }).ToList();
                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return Json(DocumentList);
        }
        [HttpGet]
        public JsonResult DeleteApprovalDocument(int id)
        {
            DataSet ds = new InvoiceDetails().DeleteApprovalDocument(id, Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId")));
            string message = string.Empty;
            bool status = false;
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                {
                    message = ds.Tables[0].Rows[0]["message"].ToString();
                    status = true;
                }
                else
                {
                    message = ds.Tables[0].Rows[0]["message"].ToString();
                    status = false;
                }
            return Json(new { status = status, message = message });
        }


        public ActionResult ViewDocument(string filepath)
        {
            ViewBag.DocumentFile = filepath;
            return View();
        }
        public JsonResult BillAssignToFirm(int billId, string Remark)
        {
            DataSet ds = new InvoiceDetails().BillAssignToFirm(billId, Remark, Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId")));
            string message = string.Empty;
            bool status = false;
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                {
                    message = ds.Tables[0].Rows[0]["message"].ToString();
                    status = true;
                }
                else
                {
                    message = ds.Tables[0].Rows[0]["message"].ToString();
                    status = false;
                }
            return Json(new { status = status, message = message });
        }

        public ActionResult ElectricityBillApproval(string id, string st,string inv)
        {
            // FirmDetails model = new FirmDetails();
            Deduction model = new Deduction();
            DataSet ds = new DataSet();
            
            int billId = Convert.ToInt32(Crypto.Decrypt(id));
            int STPID = Convert.ToInt32(Crypto.Decrypt(st));
            int Invoics = Convert.ToInt32(Crypto.Decrypt(inv));
            ViewBag.BillId = billId;
            ViewBag.STPId = STPID;  
            ViewBag.Invoic= Invoics;
            ViewBag.id = id;
            ViewBag.st = st;
            ViewBag.InvoiceId = inv;
            ViewBag.inv = inv;
            ds = model.GetElecticitybill(billId, STPID);
           
            model.dtDetails = ds.Tables[0];
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateElectricityBill(Deduction model, string id, string st,string inv)
        {
            model.AddedBy = HttpContext.Session.GetString("Pk_UserId").ToString();
            DataSet ds = model.UpdateElecticitybill();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                else
                    TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
            return RedirectToAction("ElectricityBillApproval", "Bill", new { id = id, st = st , inv= inv });
        }
        [HttpPost]
        public async Task<ActionResult> VerifyElectricityBillAsync(int InvoiceId,IFormFile Attachment, string Remark, string Status,string id, string st,string inv)
        {
            string FilePath = string.Empty;
            Status = "Verified";
            int InvoiId = 0;
            Deduction model = new Deduction();
            DataSet ds = new DataSet();
            if(Attachment != null)
            {
                 FilePath = await FileManagement.WriteFiles(Attachment, "BillApprovalDocuments", "billdocuments");
            }
            model.AddedBy = HttpContext.Session.GetString("Pk_UserId").ToString();
            InvoiId = int.Parse(Crypto.Decrypt(inv));
            ds = model.VerifyElectricityBill(InvoiId, FilePath,Remark,Status);
          
            ViewBag.InvoiceId = InvoiceId;
            ViewBag.inv = inv;
            if (ds.Tables[0].Rows[0]["Flag"].ToString()=="1")
                TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
            else
                TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
            return RedirectToAction("BillSTPWiseApproval", "Bill", new { id = id, st = st, inv = inv });
        }

        public ActionResult ElectricityBill(Deduction obj,string id,string abc)        
        {
            try
            {
                obj.PK_ElectricityId = Crypto.Decrypt(id);
                obj.OpCode = 4;
                DataSet ds = obj.GetElectricity();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(obj);
        }

        public ActionResult DGBill(Deduction model, string id,string st,string Inv)
        {
            try
            {
     
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                model.OpCode = 11;
                 //model.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlSTP = ddlSTP;
                #endregion
               
                

                model.Fk_BillId = string.IsNullOrEmpty(id) ? model.Fk_BillId : Crypto.Decrypt(id);
                model.Pk_STPId = string.IsNullOrEmpty(st) ? model.Fk_BillId : Crypto.Decrypt(st);
                model.PK_InvId = string.IsNullOrEmpty(Inv) ? model.Fk_BillId : Crypto.Decrypt(Inv);
                ViewBag.Fk_BillId = model.Fk_BillId;
                ViewBag.Pk_STPId = model.Pk_STPId;
                ViewBag.PK_InvId = model.PK_InvId;
                DataSet ds = model.GetDeductionBYID();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(model);
        }

        public JsonResult GetDeductionUpdateBYID(Deduction data)
        {
            try
            {
                data.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds =  data.UpdateDeductionByID();
                string message = string.Empty;
                string status = string.Empty;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        message = ds.Tables[0].Rows[0]["Message"].ToString();
                        status = ds.Tables[0].Rows[0]["Flag"].ToString();
                    }
                    else
                    {
                        message = ds.Tables[0].Rows[0]["Message"].ToString();
                        status = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                    
                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<ActionResult> VerifyDGBill(Deduction obj)
        {
            try
            {
                
                if (obj.AttachmentDoc != null)
                {
                    obj.AttachmentURL = await FileManagement.WriteFiles(obj.AttachmentDoc, "", "DGBillingDoc");
                }
                obj.ApprovedBy = HttpContext.Session.GetString("Pk_UserId");
                obj.Status = "verified";
                DataSet ds = obj.GetVerify();
                string message = string.Empty;
                string status = string.Empty;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        message = ds.Tables[0].Rows[0]["Message"].ToString();
                        status = ds.Tables[0].Rows[0]["Flag"].ToString();
                    }
                    else
                    {
                        message = ds.Tables[0].Rows[0]["Message"].ToString();
                        status = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }

                return Json(new { status = status, message = message });

            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
    }
}

