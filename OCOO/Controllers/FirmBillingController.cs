using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using OCOO.Models;
using System;
using System.Data;
using System.Reflection;
using System.Xml.Linq;

namespace OCOO.Controllers
{
    public class FirmBillingController : FirmBaseController
    {
        public ActionResult STPBillingRequest(FirmBilling model)
        {
            try
            {
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 1;
                DataSet ds = model.GetBillRequestList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }

        public ActionResult FirmBillingRequest(FirmBilling model)
        {
            try
            {
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 1;
                DataSet ds = model.GetBillRequestList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);

        }

        public ActionResult FirmBillingDetails(string mth, string yr, string mn, string Pk_STPId, string mnth)
        {
            try
            {
                FirmBilling model = new FirmBilling();
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                model.OpCode = 11;
                model.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataset.Tables[0].Rows)
                        {
                            ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlSTP = ddlSTP;
                #endregion

                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_MonthId = Crypto.Decrypt(mnth);
                model.Year = Crypto.Decrypt(yr);
                model.Fk_STPId = Pk_STPId;
                DataSet ds = model.GetDailyBillingReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    model.dtDetails1 = ds.Tables[1];
                }
                ViewBag.Year = Crypto.Decrypt(yr);
                ViewBag.Fk_MonthId = Crypto.Decrypt(mnth);
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View();
        }

        public ActionResult GenerateFirmBill(FirmBilling firmBilling, string SData, string MData, string YData, string IData)
        {
            try
            {
                if (firmBilling.BillingDate == null)
                {
                    var dateTime = DateTime.Now;
                    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                    firmBilling.BillingDate = dateValue2;
                }
                if (!string.IsNullOrEmpty(SData))
                {
                    firmBilling.Fk_FirmId = Crypto.Decrypt(SData);
                }
                if (!string.IsNullOrEmpty(IData))
                {
                    firmBilling.Fk_MonthId = Crypto.Decrypt(IData);
                }
                if (!string.IsNullOrEmpty(YData))
                {
                    firmBilling.Year = Crypto.Decrypt(YData);
                }
                if (!string.IsNullOrEmpty(MData))
                {
                    firmBilling.Fk_BillId = Crypto.Decrypt(MData);

                }
                if (firmBilling.BillingDate == null)
                {
                    var dateTime = DateTime.Now;
                    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                    firmBilling.BillingDate = dateValue2;

                }
                string tokenNo = "0001";
                DataSet dsTken = firmBilling.GetFirmTpiToken();
                if (dsTken.Tables[0].Rows.Count > 0)
                {
                    tokenNo = dsTken.Tables[0].Rows[0]["Token"].ToString();
                }
                ViewBag.TOkenNo = tokenNo;


                ViewBag.BillingDate = firmBilling.BillingDate.Replace("-", "/");
                firmBilling.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                firmBilling.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet ds = firmBilling.GetFirmBillSummary();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        firmBilling.Fk_ZoneId = ds.Tables[0].Rows[0]["Zone"].ToString();
                        firmBilling.Fk_FirmId = ds.Tables[0].Rows[0]["Pk_FirmId"].ToString();
                        firmBilling.Firm = ds.Tables[0].Rows[0]["FirmName"].ToString();
                        firmBilling.Year = ds.Tables[0].Rows[0]["BillingYear"].ToString();
                        firmBilling.Fk_MonthId = ds.Tables[0].Rows[0]["MonthName"].ToString();
                        firmBilling.MLDDay = ds.Tables[0].Rows[0]["AmountOfMLDSTP"].ToString();
                        firmBilling.Capacity = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        firmBilling.Actual_Achieved = ds.Tables[0].Rows[0]["Actual_Achieved"].ToString();
                        firmBilling.Treated_for_Payement = ds.Tables[0].Rows[0]["Treated_for_Payement"].ToString();
                    }
                }
                return View(firmBilling);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(firmBilling);
        }

        [HttpPost]
        public async Task<ActionResult> GenerateFirmBill(FirmBilling firmBilling)
        {
            try
            {
                if (firmBilling.BillingDate == null)
                {
                    var dateTime = DateTime.Now;
                    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                    firmBilling.BillingDate = dateValue2;

                }
                ViewBag.BillingDate = firmBilling.BillingDate.Replace("-", "/");
                DataTable multipleImage = new DataTable();
                multipleImage.Columns.Add("Doc_Url");
                multipleImage.Columns.Add("Fk_UploadDocId");
                string Doc_Url = "";
                if (firmBilling.CoveringLetter != null)
                {
                    Doc_Url = await FileManagement.WriteFiles(firmBilling.CoveringLetter, "CoveringLetter", "GenerateBill");
                    multipleImage.Rows.Add(1, Doc_Url);
                }
                if (firmBilling.TPIReports != null)
                {
                    for (int i = 0; i < firmBilling.TPIReportsCount; i++)
                    {
                        Doc_Url = (i < firmBilling.TPIReportsCount && firmBilling.TPIReports != null)
                         ? await FileManagement.WriteFiles(firmBilling.TPIReports[i], "TPIReports", "GenerateBill")
                         : string.Empty;

                        multipleImage.Rows.Add(2, Doc_Url);
                    }
                }

                if (firmBilling.LDSheet != null)
                {
                    for (int i = 0; i < firmBilling.LDReportCount; i++)
                    {
                        Doc_Url = (firmBilling.LDSheet[i] != null)
                            ? await FileManagement.WriteFiles(firmBilling.LDSheet[i], "LDReport", "GenerateBill")
                            : string.Empty;
                        multipleImage.Rows.Add(3, Doc_Url);

                    }
                }

                if (firmBilling.OtherDocuments != null)
                {
                    for (int i = 0; i < firmBilling.OtherDocumentsCount; i++)
                    {
                        Doc_Url = (firmBilling.OtherDocuments[i] != null)
                            ? await FileManagement.WriteFiles(firmBilling.OtherDocuments[i], "OtherDocuments", "GenerateBill")
                            : string.Empty;
                        multipleImage.Rows.Add(4, Doc_Url);
                    }
                }
                firmBilling.MultipleImage = multipleImage;
                firmBilling.BillingDate = string.IsNullOrEmpty(firmBilling.BillingDate) ? null : Common.ConvertToSystemDate(firmBilling.BillingDate, "dd/MM/yyyy");
                firmBilling.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                firmBilling.Fk_UsertypeId = HttpContext.Session.GetString("Fk_UsertypeId");
                DataSet ds = firmBilling.GenerateBill();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        return RedirectToAction("FirmBillingRequest");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex) { TempData["ErrorMessage"] = Ex.Message; }
            return View(firmBilling);
        }
        public ActionResult Varify(FirmBilling firmBilling, string SData, string MData, string YData, string IData, string IsFinalSubmited)
        {
            try
            {
                if (!string.IsNullOrEmpty(SData))
                {
                    firmBilling.Fk_FirmId = Crypto.Decrypt(SData);
                }
                if (!string.IsNullOrEmpty(IData))
                {
                    firmBilling.Fk_MonthId = Crypto.Decrypt(IData);
                }
                if (!string.IsNullOrEmpty(YData))
                {
                    firmBilling.Year = Crypto.Decrypt(YData);
                }
                if (!string.IsNullOrEmpty(MData))
                {
                    firmBilling.Fk_BillId = Crypto.Decrypt(MData);
                }
                firmBilling.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                firmBilling.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet ds = firmBilling.GetStatus();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        firmBilling.Measurement = ds.Tables[0].Rows[0]["Measurement"].ToString();
                        firmBilling.Electricity = ds.Tables[1].Rows[0]["Electricity"].ToString();
                        firmBilling.DG = ds.Tables[2].Rows[0]["DG"].ToString();
                    }
                    if (ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
                    {
                        firmBilling.dtDetails = ds.Tables[3];
                    }
                    if (ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
                    {
                        firmBilling.dtDetails1 = ds.Tables[4];
                    }
                    if (ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
                    {
                        firmBilling.dtDetails2 = ds.Tables[5];
                    }
                    if (!string.IsNullOrEmpty(IsFinalSubmited))
                    {
                        if (firmBilling.Measurement == "Complete" || firmBilling.Electricity == "Complete" || firmBilling.DG == "Complete")
                        {
                            SData = Crypto.Encrypt(firmBilling.Fk_FirmId);
                            IData = Crypto.Encrypt(firmBilling.Fk_MonthId);
                            YData = Crypto.Encrypt(firmBilling.Year);
                            MData = Crypto.Encrypt(firmBilling.Fk_BillId);
                            return RedirectToAction("GenerateFirmBill", "FirmBilling", new { SData = SData, MData = MData, YData = YData, IData = IData });
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Please Fill All Bills !";

                        }
                    }
                }
                return View(firmBilling);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(firmBilling);
        }
        public ActionResult ElectricityBillDetails(string mth, string yr, string mn, string Pk_STPId)
        {
            try
            {
                FirmBilling model = new FirmBilling();
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_BillId = Crypto.Decrypt(mth);
                model.Months = Crypto.Decrypt(mn);
                model.Year = Crypto.Decrypt(yr);
                model.Fk_STPId = Pk_STPId;
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet ds = model.GetElectricityBillReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    model.dtDetails1 = ds.Tables[1];
                }
                ViewBag.Month = Crypto.Decrypt(mn);
                ViewBag.Year = Crypto.Decrypt(yr);
                ViewBag.MonthName = Crypto.Decrypt(mn);
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View();

        }
        public ActionResult DGBillDetails(string mth, string yr, string mn, string Pk_STPId)
        {
            try
            {
                FirmBilling model = new FirmBilling();
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_BillId = Crypto.Decrypt(mth);
                model.Year = Crypto.Decrypt(yr);
                model.Months = Crypto.Decrypt(mn);
                model.Fk_STPId = Pk_STPId;
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet ds = model.GetDGBillReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    model.dtDetails1 = ds.Tables[1];
                }
                ViewBag.Month = Crypto.Decrypt(mn);
                ViewBag.Year = Crypto.Decrypt(yr);
                ViewBag.MonthName = Crypto.Decrypt(mn);
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View();
        }
        public async Task<ActionResult> AddTpiDocument(FirmBilling firmBilling, string Remarks, IFormFile[]?TPIReports, string Fk_BillId,string tokenNo, string save)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                            
                firmBilling.Fk_BillId = Fk_BillId;                
                firmBilling.tokenNo = tokenNo;
                firmBilling.TPIReports =TPIReports;
                firmBilling.Remark = Remarks;
                firmBilling.DocumentTypeId = 2;
                firmBilling.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                firmBilling.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                if (!string.IsNullOrEmpty(save))
                {                   
                    if (save == "Add")
                    {
                        var file = Request.Form.Files["file"];
                        if (file != null)
                        {

                            string fileName = await FileManagement.WriteFiles(file, "Document", HttpContext.Session.GetString("FilePath").ToString());
                            firmBilling.Files = fileName;
                        }
                        firmBilling.OpCode = 2;
                        DataSet dataset = firmBilling.Save_Document();
                    }
                }
                return Json(firmBilling);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ActionResult>TpiDocumentDetails(string Fk_BillId, string tokenNo)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                FirmBilling firmBilling = new FirmBilling();                               
                firmBilling.OpCode = 1;
                firmBilling.Fk_BillId = Fk_BillId;
                firmBilling.tokenNo = tokenNo;
                firmBilling.DocumentTypeId = 2;
                firmBilling.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataset = firmBilling.Save_Document();
                if (dataset != null)
                {
                    List<FirmBilling> lst = new List<FirmBilling>();
                    for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
                    {
                        FirmBilling create = new FirmBilling();
                        create.DocumentTypeId = Convert.ToInt32(dataset.Tables[0].Rows[i]["DocumentTypeId"]);
                        create.Remark = dataset.Tables[0].Rows[i]["Remarks"].ToString();
                        create.Fk_BillId = dataset.Tables[0].Rows[i]["Fk_BillId"].ToString();
                        create.tokenNo = dataset.Tables[0].Rows[i]["TokenNo"].ToString();
                        create.PK_FTDId = dataset.Tables[0].Rows[i]["PK_FTDId"].ToString();
                        create.Files = dataset.Tables[0].Rows[i]["DocUrl"].ToString();                        
                        create.DocFormat = dataset.Tables[0].Rows[i]["DocFormat"].ToString();                        
                        lst.Add(create);
                    }
                    firmBilling.Tpidetail = lst;
                }
                return Json(firmBilling);
            }
            catch (Exception ex) { throw ex; }

        }
        public async Task<IActionResult> DownloadImage(string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                string FileFullPath = HttpContext.Session.GetString("FilePath").ToString();
                var path = FileFullPath + "/" + filename;// Path.GetFullPath(FileFullPath + imagepath);
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
        public async Task<ActionResult> DeleteTpiDetails(string PK_FTDId)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                FirmBilling firmBilling = new FirmBilling();
                firmBilling.OpCode = 3;
                firmBilling.PK_FTDId = PK_FTDId;                              
                DataSet dataset = firmBilling.Save_Document();
                return Json(firmBilling);               
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
