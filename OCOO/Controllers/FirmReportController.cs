using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using OCOO.Models.FirmMasters;
using System.Data;

namespace OCOO.Controllers
{
    public class FirmReportController : FirmBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult PaidBillReport(PaidBillReport model)
        {
            try
            {
                DataSet ds = new DataSet();

                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 16;
                DataSet dsMonth = model.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                if (!string.IsNullOrEmpty(model.FromDate))
                {
                    model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty(model.ToDate))
                {
                    model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                }

                if (string.IsNullOrEmpty(model.Fk_MonthId))
                {
                    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                }
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.BillStatus = "Paid";
                model.IsBilled = "1";
                if (model.SelectedMonth != null)
                {
                    model.Months = string.Join(",", model.SelectedMonth);
                }
                ds = model.FirmwisePaidBills();
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
        public ActionResult ViewInvoiceDetail(InvoiceDetails model, string abc, string mpa)
        {
            try
            {

                DataSet ds = new DataSet();
                if (abc != null)
                {
                    model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                    model.Pk_BillingId = Crypto.Decrypt(abc);
                    ds = model.GetViewBillDetail();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = ds.Tables[0];
                    }
                    if (!string.IsNullOrEmpty(mpa))
                    {
                        ViewBag.Data = Crypto.Decrypt(mpa);
                    }
                }
                else
                {
                    return Redirect("/FirmReport/InvoiceDetails");
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return Redirect("/FirmReport/InvoiceDetails");

            }
            return View(model);
        }

        public ActionResult PendingBillReport(PaidBillReport model)
        {
            DataSet ds = new DataSet();
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 16;
                DataSet dsMonth = model.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                if (!string.IsNullOrEmpty(model.FromDate))
                {
                    model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty(model.ToDate))
                {
                    model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                }

                if (string.IsNullOrEmpty(model.Fk_MonthId))
                {
                    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                }
                if (model.SelectedMonth != null)
                {
                    model.Months = string.Join(",", model.SelectedMonth);
                }
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.BillStatus = "Pending";
                model.IsBilled = "0";
                ds = model.FirmwisePaidBills();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                model.Fk_UsertypeId = HttpContext.Session.GetString("Fk_UsertypeId");
               DataSet ds11 = model.ReadNotification();

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);

        }
        public ActionResult DeclineDocuments(DeclineDoc model)
        {
            try
            {
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetDeclineDoc();
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
        public ActionResult BillDocument(InvoiceDetails model, string xyz, string mpa)
        {
            try
            {
                if (xyz != null)
                {
                    model.BillNo = Crypto.Decrypt(xyz);
                    DataSet ds = model.GetBillDoc();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = ds.Tables[0];
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        model.dtDetails1 = ds.Tables[1];
                    }
                    if (!string.IsNullOrEmpty(mpa))
                    {
                        ViewBag.Data = Crypto.Decrypt(mpa);
                    }
                    else
                    {
                        ViewBag.Data = "/FirmReport/PendingBillReport";

                    }
                }
                else
                {
                    return Redirect("/FirmReport/PendingBillReport");
                }
            }
            catch (Exception Ex) { TempData["ErrorMessage"] = Ex.Message; }
            return View(model);
        }
        public async Task<ActionResult> UploadDeclineDoc(DeclineDoc model)
        {
            try
            {
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                if (model.Files != null && model.Files.Length > 0)
                {
                    if (model.Fk_DocId != null && model.Fk_DocId != "0")
                    {
                        if (model.Fk_DocId == "1")
                        {
                            model.Doc_url = await FileManagement.WriteFiles(model.Files, "CoveringLetter", "GenerateBill");
                        }
                        if (model.Fk_DocId == "2")
                        {
                            model.Doc_url = await FileManagement.WriteFiles(model.Files, "TPIReports", "GenerateBill");
                        }
                        if (model.Fk_DocId == "3")
                        {
                            model.Doc_url = await FileManagement.WriteFiles(model.Files, "LDReport", "GenerateBill");
                        }
                        if (model.Fk_DocId == "4")
                        {
                            model.Doc_url = await FileManagement.WriteFiles(model.Files, "OtherDocuments", "GenerateBill");
                        }
                    }
                }
                DataSet ds = model.UploadDeclineDoc();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("DeclineDocuments");
        }
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

        [HttpGet]
        public ActionResult BillApprovalDocument(InvoiceDetails model, string xyz, string mpa)
        {
            try
            {
                if (xyz != null)
                {
                    model.BillNo = Crypto.Decrypt(xyz);
                    DataSet ds = model.GetBillApprovalDocument();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<BillApprovalDocumentDTO> DocumentList = (from c in ds.Tables[0].AsEnumerable()
                                                                      select new BillApprovalDocumentDTO
                                                                      {
                                                                          Id = c.Field<int>("Id"),
                                                                          UploadDocName = c.Field<string>("UploadDocName"),
                                                                          Doc_Url = c.Field<string>("Doc_Url"),
                                                                          Status = c.Field<string>("Status"),
                                                                          date = c.Field<string>("date"),
                                                                          Remarks = c.Field<string>("Remarks")
                                                                      }).ToList();
                        List<BillApprovalDocumentReportDTO> _DocReport = DocumentList.GroupBy(x => x.UploadDocName).Select(x => new BillApprovalDocumentReportDTO { UploadDocName = x.Key, DocumentList = DocumentList.Where(y => y.UploadDocName == x.Key).Select(z => new BillApprovalDocumentDTO { Doc_Url = z.Doc_Url, UploadDocName = z.UploadDocName, Status = z.Status, Remarks = z.Remarks, date = z.date }).ToList() }).ToList();
                        model.dtDetails = ds.Tables[0];
                        model._DocumentList = _DocReport;
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        model.dtDetails1 = ds.Tables[1];
                    }
                    if (!string.IsNullOrEmpty(mpa))
                    {
                        ViewBag.Data = Crypto.Decrypt(mpa);
                    }
                    else
                    {
                        ViewBag.Data = "/FirmReport/PendingBillReport";

                    }
                }
                else
                {
                    return Redirect("/FirmReport/PendingBillReport");
                }
            }
            catch (Exception Ex) { TempData["ErrorMessage"] = Ex.Message; }
            return View(model);
        }

        
        public ActionResult ViewDocument(string filepath)
        {
            ViewBag.DocumentFile = filepath;
            return View();
        }

         public ActionResult ViewDocfile(string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                ViewBag.filepath = filepath;
            }
            else
            {
                ViewBag.pdffilepath = "NA";
            }
            return View();
        }

        public ActionResult ApprovedBill(ApproveBill model)
        {
            DataSet ds = new DataSet();
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion

                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 16;
                DataSet dsMonth = model.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion

                if (!string.IsNullOrEmpty(model.FromDate))
                {
                    model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty(model.ToDate))
                {
                    model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                }

                
                model.IsBilled = "0";
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                ds = model.GetBills();
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
        public ActionResult Utilizationofstp(Utilization model)
        {
            DataSet ds = new DataSet();
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion

                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 16;
                DataSet dsMonth = model.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                ds = model.getutilizationofstpForFirm();
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
        public ActionResult TreatedWaterVolume(TreatedWater model)
        {
            DataSet ds = new DataSet();
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion


                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 16;
                DataSet dsMonth = model.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                ds = model.GetTreatedWaterVolumeForFirm();
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
        public ActionResult ComplaintsReceived(ComplaintsRecevied obj)
        {
            try
            {

                Dashboard dashboard = new Dashboard();
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                dashboard.OpCode = 4;
                dashboard.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = dashboard.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion
                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                dashboard.OpCode = 6;
                DataSet dataSet2 = dashboard.GetMasterData();
                if (dataSet2 != null && dataSet2.Tables.Count > 0)
                {
                    if (dataSet2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet2.Tables[0].Rows)
                        {
                            ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion


                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");
                obj.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                DataSet ds = obj.GetComplaintsReceviedForFirm();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.dtDetails = ds.Tables[0];
                }
                return View(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public ActionResult ComplaintsResolved(ComplaintsRecevied obj)
        {
            try
            {

                Dashboard dashboard = new Dashboard();
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                dashboard.OpCode = 4;
                dashboard.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = dashboard.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion
                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                dashboard.OpCode = 6;
                DataSet dataSet2 = dashboard.GetMasterData();
                if (dataSet2 != null && dataSet2.Tables.Count > 0)
                {
                    if (dataSet2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet2.Tables[0].Rows)
                        {
                            ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion


                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");
                obj.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                DataSet ds = obj.GetComplaintsResolvedForFirm();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.dtDetails = ds.Tables[0];
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public ActionResult ComplaintsPending(ComplaintsRecevied obj)
        {
            try
            {

                Dashboard dashboard = new Dashboard();
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                dashboard.OpCode = 4;
                dashboard.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = dashboard.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion
                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                dashboard.OpCode = 6;
                DataSet dataSet2 = dashboard.GetMasterData();
                if (dataSet2 != null && dataSet2.Tables.Count > 0)
                {
                    if (dataSet2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet2.Tables[0].Rows)
                        {
                            ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion


                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");
                obj.UserId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                DataSet ds = obj.GetComplaintsPending();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.dtDetails = ds.Tables[0];
                }
                return View(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
