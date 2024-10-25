using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using OCOO.Models;
using OCOO.Models.AdminMasters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace OCOO.Controllers
{
    public class FirmController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard(FirmDashboard firmDashboard)
        {
            try
            {
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                firmDashboard.OpCode = 11;
                firmDashboard.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsSTP = firmDashboard.GetMasterData();
                if (dsSTP != null && dsSTP.Tables.Count > 0 && dsSTP.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsSTP.Tables[0].Rows)
                    {
                        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                //ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                firmDashboard.OpCode = 16;
                DataSet dsMonth = firmDashboard.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                //if (firmDashboard.FromDate == null)
                //{
                //    var dateTime = DateTime.Now;
                //    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                //    firmDashboard.FromDate = dateValue2;


                //}
                //if (firmDashboard.ToDate == null)
                //{
                //    var dateTime1 = DateTime.Now;
                //    var dateValue1 = dateTime1.ToString("dd/MM/yyyy");
                //    firmDashboard.ToDate = dateValue1;

                //}
                //ViewBag.ToDate = firmDashboard.ToDate.Replace("-", "/");
                //ViewBag.FromDate = firmDashboard.FromDate.Replace("-", "/");

                if (string.IsNullOrEmpty(firmDashboard.IsGstUpdated))
                {
                    firmDashboard.IsGstUpdated = HttpContext.Session.GetString("IsGstUpdated");
                }
                if (string.IsNullOrEmpty(firmDashboard.Fk_MonthId))
                {
                    firmDashboard.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                }
                firmDashboard.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                if (firmDashboard.SelectedMonth != null)
                {
                    firmDashboard.Months = string.Join(",", firmDashboard.SelectedMonth);
                }
                DataSet ds = firmDashboard.GetFirmDashbord();

                if (ds != null && ds.Tables.Count > 0)
                {
                    firmDashboard.dtDetails = ds.Tables[0];
                    if (ds.Tables.Count > 1)
                    {
                        firmDashboard.dtBillData = ds.Tables[1];
                    }
                }
                TempData["Fk_STPId"] = firmDashboard.Fk_STPId;
                TempData["Years"] = firmDashboard.Years;
                TempData["Months"] = firmDashboard.Months;
                //TempData["ToDate"] = firmDashboard.ToDate;
                return View(firmDashboard);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(firmDashboard);
        }

        [HttpPost]
        public ActionResult Dashboard(FirmDashboard firmDashboard, string btnsubmit)
        {
            try
            {
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                firmDashboard.OpCode = 11;
                firmDashboard.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsSTP = firmDashboard.GetMasterData();
                if (dsSTP != null && dsSTP.Tables.Count > 0 && dsSTP.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsSTP.Tables[0].Rows)
                    {
                        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                firmDashboard.OpCode = 16;
                DataSet dsMonth = firmDashboard.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                //if (firmDashboard.FromDate == null)
                //{
                //    var dateTime = DateTime.Now;
                //    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                //    firmDashboard.FromDate = dateValue2;


                //}
                //if (firmDashboard.ToDate == null)
                //{
                //    var dateTime1 = DateTime.Now;
                //    var dateValue1 = dateTime1.ToString("dd/MM/yyyy");
                //    firmDashboard.ToDate = dateValue1;

                //}
                //ViewBag.ToDate = firmDashboard.ToDate.Replace("-", "/");
                //ViewBag.FromDate = firmDashboard.FromDate.Replace("-", "/");
                firmDashboard.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                firmDashboard.FromDate = string.IsNullOrEmpty(firmDashboard.FromDate) ? null : Common.ConvertToSystemDate(firmDashboard.FromDate, "dd/mm/yyyy");
                firmDashboard.ToDate = string.IsNullOrEmpty(firmDashboard.ToDate) ? null : Common.ConvertToSystemDate(firmDashboard.ToDate, "dd/mm/yyyy");
                if (firmDashboard.SelectedMonth != null)
                {
                    firmDashboard.Months = string.Join(",", firmDashboard.SelectedMonth);
                }
                DataSet ds = firmDashboard.GetFirmDashbord();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    firmDashboard.dtDetails = ds.Tables[0];
                }
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        firmDashboard.dtBillData = ds.Tables[1];
                    }
                }
                TempData["Fk_STPId"] = firmDashboard.Fk_STPId;
                TempData["Years"] = firmDashboard.Years;
                TempData["Months"] = firmDashboard.Months;
                //TempData["ToDate"] = firmDashboard.ToDate;
                return View(firmDashboard);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(firmDashboard);
        }

        public ActionResult BindChartData(FirmDashboard firmDashboard, string btnsubmit)
        {
            try
            {
                firmDashboard.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                firmDashboard.Fk_STPId = firmDashboard.Fk_STPId == "0" ? null : firmDashboard.Fk_STPId;
                //if(!string.IsNullOrEmpty(firmDashboard.FromDate))
                //{

                //firmDashboard.FromDate = Common.ConvertToSystemDate(firmDashboard.FromDate, "dd/mm/yyyy");
                //}
                //if (!string.IsNullOrEmpty(firmDashboard.ToDate))
                //{
                //    firmDashboard.ToDate = Common.ConvertToSystemDate(firmDashboard.ToDate, "dd/mm/yyyy");
                //}

                DataSet ds = firmDashboard.GetChartData();
                List<ComplaintData> list = new List<ComplaintData>();
                List<LDCountData> listLDCount = new List<LDCountData>();
                if (ds != null)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        LDCountData piChartLDCount = new LDCountData();
                        piChartLDCount.BODCount = ds.Tables[2].Rows[0]["TotalBODLDCount"].ToString();
                        piChartLDCount.CODCount = ds.Tables[2].Rows[0]["TotalCODLDCount"].ToString();
                        piChartLDCount.TSS = ds.Tables[2].Rows[0]["TotalTSSLDCount"].ToString();
                        piChartLDCount.FC = ds.Tables[2].Rows[0]["TotalFCLDCount"].ToString();
                        piChartLDCount.TotalLDAmount = ds.Tables[2].Rows[0]["TotalLDCount"].ToString();
                        firmDashboard.PieChartLDCountData = piChartLDCount;
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int j = 0; j <= ds.Tables[1].Rows.Count - 1; j++)
                        {
                            ComplaintData listdata = new ComplaintData();
                            listdata.ReceivedCall = ds.Tables[1].Rows[j]["CompReceived"].ToString();
                            listdata.ResolvedCall = ds.Tables[1].Rows[j]["CompResolved"].ToString();
                            listdata.PendingCall = ds.Tables[1].Rows[j]["CompPending"].ToString();
                            listdata.MonthName = ds.Tables[1].Rows[j]["MonthName"].ToString();
                            list.Add(listdata);
                        }
                        firmDashboard.listComplaint = list;
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            LDCountData lDCount = new LDCountData();
                            lDCount.BODCount = ds.Tables[0].Rows[i]["TotalBODLDCount"].ToString();
                            lDCount.CODCount = ds.Tables[0].Rows[i]["TotalCODLDCount"].ToString();
                            lDCount.TSS = ds.Tables[0].Rows[i]["TotalTSSLDCount"].ToString();
                            lDCount.FC = ds.Tables[0].Rows[i]["TotalFCLDCount"].ToString();
                            lDCount.MonthName = ds.Tables[0].Rows[i]["MonthName"].ToString();
                            lDCount.TotalLDAmount = ds.Tables[0].Rows[i]["TotalLDCount"].ToString();

                            listLDCount.Add(lDCount);
                        }
                        firmDashboard.listLDCount = listLDCount;
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(firmDashboard);
        }
        public JsonResult GetBlockByDistrict(string Id)
        {
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 5;
                model.Value = Id;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                return Json(ddlDesignation);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(ddlDesignation);
        }

        public ActionResult STCDailyCapacity(DailyCapacity model)
        {
            try
            {
                List<SelectListItem> ddlDivision = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        ddlDivision.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDivision = ddlDivision;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                model.OpCode = 5;
                ddlBlock.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;

                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 2;
                DataSet ds = model.GetSTC();
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

        [HttpPost]
        public ActionResult STCDailyCapacity(DailyCapacity model, string btnsubmit)
        {
            try
            {
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                if (!string.IsNullOrEmpty(btnsubmit))
                {
                    model.OpCode = 1;
                    if (string.IsNullOrEmpty(model.UniqueId))
                    {
                        model.UniqueId = DateTime.Now.ToString("ddmmyyyyhhmmss");
                    }
                    if (model.IsPermanent == "Save as Draft")
                    {
                        model.SaveAsDraft = true;
                        model.SavePermanent = false;
                        for (int i = 1; i < Convert.ToInt32(Request.Form["Count"]); i++)
                        {
                            model.EntryDate = Request.Form["EntryDate_" + i].ToString();
                            model.AmountOf72MLDStp = Request.Form["AmountOf72MLDStp_" + i].ToString();
                            model.FlowCapacity = Request.Form["FlowCapacity_" + i].ToString();
                            model.FlowActualAchived = Request.Form["FlowActualAchived_" + i].ToString();
                            model.TreatedForPaymentMLD = Request.Form["TreatedForPaymentMLD_" + i].ToString();
                            model.FixChargeAsPerCB = Request.Form["FixChargeAsPerCB_" + i].ToString();
                            model.FixChargeAsPerActual = Request.Form["FixChargeAsPerActual_" + i].ToString();
                            model.VariableChargesAsPerCB = Request.Form["RequestChargesAsPerCB_" + i].ToString();
                            model.VariableChargesAsperActual = Request.Form["VariableChargesAsPerActual_" + i].ToString();
                            model.BODReportedValue = Request.Form["BODReportedValue_" + i].ToString();
                            model.BOD50 = Request.Form["BOD50_" + i].ToString();
                            model.BODLD = Request.Form["BODLD_" + i].ToString();
                            model.CODReportedValue = Request.Form["CODReportedValue_" + i].ToString();
                            model.COD15 = Request.Form["COD15_" + i].ToString();
                            model.CODLD = Request.Form["CODLD_" + i].ToString();
                            model.TSSReportedValue = Request.Form["TSSReportedValue"].ToString();
                            model.TSS25 = Request.Form["TSS25"].ToString();
                            model.TSSLD = Request.Form["TSSLD"].ToString();
                            model.FCReportedValue = Request.Form["FCReportedValue_" + i].ToString();
                            model.FC10 = Request.Form["FC10_" + i].ToString();
                            model.FCLD = Request.Form["FCLD_" + i].ToString();
                            model.AfterDeductedParaAmt = Request.Form["AfterDeductedParaAmt_" + i].ToString();
                            model.LD = Request.Form["LD_" + i].ToString();

                            DataSet ds = model.GetSTC();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                {
                                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                }
                                else
                                {
                                    TempData["errorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                }
                            }
                            else
                            {
                                TempData["errorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < Convert.ToInt32(Request.Form["Count"]); i++)
                        {
                            model.EntryDate = Request.Form["EntryDate_" + i].ToString();
                            model.AmountOf72MLDStp = Request.Form["AmountOf72MLDStp_" + i].ToString();
                            model.FlowCapacity = Request.Form["FlowCapacity_" + i].ToString();
                            model.FlowActualAchived = Request.Form["FlowActualAchived_" + i].ToString();
                            model.TreatedForPaymentMLD = Request.Form["TreatedForPaymentMLD_" + i].ToString();
                            model.FixChargeAsPerCB = Request.Form["FixChargeAsPerCB_" + i].ToString();
                            model.FixChargeAsPerActual = Request.Form["FixChargeAsPerActual_" + i].ToString();
                            model.VariableChargesAsPerCB = Request.Form["RequestChargesAsPerCB_" + i].ToString();
                            model.VariableChargesAsperActual = Request.Form["VariableChargesAsPerActual_" + i].ToString();
                            model.BODReportedValue = Request.Form["BODReportedValue_" + i].ToString();
                            model.BOD50 = Request.Form["BOD50_" + i].ToString();
                            model.BODLD = Request.Form["BODLD_" + i].ToString();
                            model.CODReportedValue = Request.Form["CODReportedValue_" + i].ToString();
                            model.COD15 = Request.Form["COD15_" + i].ToString();
                            model.CODLD = Request.Form["CODLD_" + i].ToString();
                            model.TSSReportedValue = Request.Form["TSSReportedValue"].ToString();
                            model.TSS25 = Request.Form["TSS25"].ToString();
                            model.TSSLD = Request.Form["TSSLD"].ToString();
                            model.FCReportedValue = Request.Form["FCReportedValue_" + i].ToString();
                            model.FC10 = Request.Form["FC10_" + i].ToString();
                            model.FCLD = Request.Form["FCLD_" + i].ToString();
                            model.AfterDeductedParaAmt = Request.Form["AfterDeductedParaAmt_" + i].ToString();
                            model.LD = Request.Form["LD_" + i].ToString();
                            if (model.AmountOf72MLDStp == "0.00" || model.AmountOf72MLDStp == "" && model.FlowCapacity == "0.00" || model.FlowCapacity == "" && model.FlowActualAchived == "0.00" || model.FlowActualAchived == "")
                            {
                                model.SaveAsDraft = true;
                                model.SavePermanent = false;
                            }
                            else
                            {
                                model.SaveAsDraft = false;
                                model.SavePermanent = true;
                            }

                            DataSet ds = model.GetSTC();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                {
                                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                }
                                else
                                {
                                    TempData["errorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                }
                            }
                            else
                            {
                                TempData["errorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                }

                model.OpCode = 2;
                DataSet dataset = model.GetSTC();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = dataset.Tables[0];
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }

        public ActionResult STCRequestList(DailyCapacity stc)
        {
            try
            {
                stc.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = stc.FirmRequestList();
                if (ds != null && ds.Tables[0] != null)
                {
                    stc.dtDetails = ds.Tables[0];
                }
                return View(stc);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(stc);
        }

        public ActionResult GetFirmRequest(string Id)
        {
            DailyCapacity model = new DailyCapacity();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    model.UniqueId = Crypto.Decrypt(Id);
                    model.OpCode = 3;
                    DataSet ds = model.GetFirmRequest();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = ds.Tables[0];
                    }
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }

        public async Task<ActionResult> UploadDocuments(DailyData model, string btnSubmit)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Fk_UniqueId))
                {
                    model.Fk_UniqueId = DateTime.Now.ToString("ddmmyyyyhhmmss");
                }
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    string? foldername = model.Fk_UniqueId;
                    if (btnSubmit == "Save")
                    {
                        if (model.CoveringLetter != null && model.CoveringLetter.Length > 0)
                        {
                            string fileLocation = await FileManagement.WriteFiles(model.CoveringLetter, "CoveringLetter", foldername);
                            model.CoveringLetterURL = fileLocation;
                        }
                        if (model.InspectionReport != null && model.InspectionReport.Length > 0)
                        {
                            string fileLocation = await FileManagement.WriteFiles(model.InspectionReport, "InspectionReport", foldername);
                            model.InspectionReportURL = fileLocation;
                        }
                        if (model.TPIReports != null && model.TPIReports.Length > 0)
                        {
                            string fileLocation = await FileManagement.WriteFiles(model.TPIReports, "TPIReports", foldername);
                            model.TPIReportsURL = fileLocation;
                        }
                        if (model.LDSheet != null && model.LDSheet.Length > 0)
                        {
                            string fileLocation = await FileManagement.WriteFiles(model.LDSheet, "LDSheet", foldername);
                            model.LDSheetURL = fileLocation;
                        }
                        if (model.OtherDocuments != null && model.OtherDocuments.Length > 0)
                        {
                            string fileLocation = await FileManagement.WriteFiles(model.OtherDocuments, "OtherDocuments", foldername);
                            model.OtherDocumentsURL = fileLocation;
                        }
                        model.OpCode = 1;
                        model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        DataSet ds = model.GetUploadDocuments();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                }
                return RedirectToAction("DailyCapacity", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("DailyCapacity", model);
            }
        }

        public async Task<ActionResult> DailyCapacity(DailyData dailydata, string btnsubmit, string btngetDetails, string btnUpload)
        {
            try
            {
                List<SelectListItem> ddlFirm = new List<SelectListItem>();
                FirmDetails model = new FirmDetails();
                model.OpCode = 11;
                model.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsFirm = model.GetMasterData();
                if (dsFirm != null && dsFirm.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsFirm.Tables[0].Rows)
                    {
                        ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlFirm = ddlFirm;

                DataSet dataSet = new DataSet();
                dailydata.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                #region Month
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                dailydata.OpCode = 6;
                dailydata.Value = HttpContext.Session.GetString("Fk_CityId");
                dataSet = dailydata.GetMasterData();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                dailydata.OpCode = 4;
                DataSet dsZone = dailydata.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;

                #region  ddlSTP Capacity            
                dailydata.Value = HttpContext.Session.GetString("Pk_UserId");
                List<SelectListItem> ddlSTPCapacity = new List<SelectListItem>();
                dailydata.OpCode = 7;
                dataSet = dailydata.GetMasterData();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        ddlSTPCapacity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlSTPCapacity = ddlSTPCapacity;
                #endregion

                if (!string.IsNullOrEmpty(btnUpload))
                {
                    string fileLocation = "";
                    string? foldername = dailydata.Fk_UniqueId;
                    if (btnUpload == "Save")
                    {
                        if (dailydata.CoveringLetter != null && dailydata.CoveringLetter.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.CoveringLetter, "CoveringLetter", foldername);
                            dailydata.CoveringLetterURL = fileLocation;
                        }
                        if (dailydata.InspectionReport != null && dailydata.InspectionReport.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.InspectionReport, "InspectionReport", foldername);
                            dailydata.InspectionReportURL = fileLocation;
                        }
                        if (dailydata.TPIReports != null && dailydata.TPIReports.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.TPIReports, "TPIReports", foldername);
                            dailydata.TPIReportsURL = fileLocation;
                        }
                        if (dailydata.LDSheet != null && dailydata.LDSheet.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.LDSheet, "LDSheet", foldername);
                            dailydata.LDSheetURL = fileLocation;
                        }
                        if (dailydata.OtherDocuments != null && dailydata.OtherDocuments.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.OtherDocuments, "OtherDocuments", foldername);
                            dailydata.OtherDocumentsURL = fileLocation;
                        }
                        dailydata.OpCode = 1;
                        dataSet = dailydata.GetUploadDocuments();
                        if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["ErrorMessage"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(btngetDetails))
                {
                    DataSet ds = dailydata.GetSTCDailyData();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dailydata.dtDetails = ds.Tables[0];
                    }
                }
                if (!string.IsNullOrEmpty(btnsubmit))
                {
                    dailydata.OpCode = 1;
                    if (string.IsNullOrEmpty(dailydata.Fk_UniqueId))
                    {
                        dailydata.Fk_UniqueId = DateTime.Now.ToString("ddmmyyyyhhmmss");
                    }
                    if (dailydata.IsPermanent == "0")
                    {
                        dailydata.SaveAsDraft = true;
                        dailydata.SavePermanent = false;
                    }
                    else
                    {
                        dailydata.SaveAsDraft = false;
                        dailydata.SavePermanent = true;
                    }
                    DataTable dt = new DataTable();
                    dt = dailydata.JsonStringToDataTable(dailydata.JsonString);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dailydata.EntryDate = dt.Rows[i]["DATE"].ToString();
                                dailydata.AmountOf72MLDStp = dt.Rows[i]["WATER_FLOW_IN_MLD"].ToString();
                                dailydata.FlowCapacity = dt.Rows[i]["FLOW_CAPACITY"].ToString();
                                dailydata.FlowActualAchived = dt.Rows[i]["ACTUAL_FLOW_ACHIEVED"].ToString();
                                dailydata.BODReportedValue = dt.Rows[i]["BOD_REPORTED_VALUE"].ToString();
                                dailydata.CODReportedValue = dt.Rows[i]["COD_REPORTED_VALUE"].ToString();
                                dailydata.TSSReportedValue = dt.Rows[i]["TSS_REPORTED_VALUE"].ToString();
                                dailydata.FCReportedValue = dt.Rows[i]["FC_REPORTED_VALUE"].ToString();
                                dailydata.ComplaintResolved = dt.Rows[i]["NO_OF_COMPLAINTS_RESOLVED"].ToString();
                                dailydata.ComplaintReceived = dt.Rows[i]["NO_OF_COMPLAINTS_RECIEVED"].ToString();
                                dailydata.Remarks = dt.Rows[i]["REMARKS"].ToString();
                                dataSet = dailydata.UploadDailydata();
                                if (dataSet != null && dataSet.Tables[0] != null)
                                {
                                    if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                    {
                                        TempData["UploadBeneficiaryDetails"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                    }
                                }
                            }
                        }

                    }
                }
                return View(dailydata);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(dailydata);
        }

        public JsonResult GetDocumentDetails(string Id)
        {
            DailyCapacity model = new DailyCapacity();
            try
            {
                model.UniqueId = Id;
                model.OpCode = 6;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetFirmRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Pk_DocumentId = ds.Tables[0].Rows[0]["Pk_DocumentId"].ToString();
                    model.UniqueId = ds.Tables[0].Rows[0]["Fk_UniqueId"].ToString();
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CoveringLetterURL"].ToString()))
                    {
                        model.CoveringLetterURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["CoveringLetterURL"].ToString();
                    }
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["InspectionReportURL"].ToString()))
                    {
                        model.InspectionReportURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["InspectionReportURL"].ToString();
                    }
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TPIReportsURL"].ToString()))
                    {
                        model.TPIReportsURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["TPIReportsURL"].ToString();
                    }
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LDSheetURL"].ToString()))
                    {
                        model.LDSheetURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["LDSheetURL"].ToString();
                    }
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OtherDocumentsURL"].ToString()))
                    {
                        model.OtherDocumentsURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["OtherDocumentsURL"].ToString();
                    }

                }
                return Json(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public async Task<ActionResult> UploadSTPDocuments(DailyData dailydata, string btnUpload)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnUpload))
                {
                    string fileLocation = "";
                    string? foldername = dailydata.Fk_UniqueId;
                    if (btnUpload == "Save")
                    {
                        if (dailydata.CoveringLetter != null && dailydata.CoveringLetter.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.CoveringLetter, "CoveringLetter", foldername);
                            dailydata.CoveringLetterURL = fileLocation;
                        }
                        if (dailydata.InspectionReport != null && dailydata.InspectionReport.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.InspectionReport, "InspectionReport", foldername);
                            dailydata.InspectionReportURL = fileLocation;
                        }
                        if (dailydata.TPIReports != null && dailydata.TPIReports.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.TPIReports, "TPIReports", foldername);
                            dailydata.TPIReportsURL = fileLocation;
                        }
                        if (dailydata.LDSheet != null && dailydata.LDSheet.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.LDSheet, "LDSheet", foldername);
                            dailydata.LDSheetURL = fileLocation;
                        }
                        if (dailydata.OtherDocuments != null && dailydata.OtherDocuments.Length > 0)
                        {
                            fileLocation = await FileManagement.WriteFiles(dailydata.OtherDocuments, "OtherDocuments", foldername);
                            dailydata.OtherDocumentsURL = fileLocation;
                        }
                        dailydata.OpCode = 1;
                        dailydata.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        DataSet dataSet = new DataSet();
                        dataSet = dailydata.GetUploadDocuments();
                        if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["ErrorMessage"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                }
                return RedirectToAction("STCRequestList");
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return RedirectToAction("STCRequestList");

            }
        }

        public ActionResult GetSTPName(string Id)
        {
            List<SelectListItem> ddlFirm = new List<SelectListItem>();
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 1;
                model.Value = Id;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                return Json(ddlFirm);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(ddlFirm);
        }

        [HttpPost]
        public ActionResult FirmRegistration(FirmDetails model, string btnSubmit)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (model.Pk_FirmId != null)
                    {
                        model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        model.OpCode = 5;
                        DataSet ds = model.ManageFirm();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                    else
                    {
                        model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        model.OpCode = 1;
                        DataSet ds = model.ManageFirm();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                }
                return RedirectToAction("DailyCapacity");
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return RedirectToAction("DailyCapacity");
            }
        }

        public ActionResult StpRegistration(int Id)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                //List<SelectListItem> PumpingStationType = new List<SelectListItem>();
                //model.OpCode = 12;
                //DataSet dataset = model.GetMasterData();
                //if (dataset != null && dataset.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row in dataset.Tables[0].Rows)
                //    {
                //        PumpingStationType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                //    }
                //}
                //ViewBag.PumpingStationType = PumpingStationType;
                #region ddlPumpingStationCount

                List<SelectListItem> PumpingStationCount = new List<SelectListItem>();
                model.OpCode = 13;
                DataSet dataset1 = model.GetMasterData();
                if (dataset1 != null && dataset1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset1.Tables[0].Rows)
                    {
                        PumpingStationCount.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.PumpingStationCount = PumpingStationCount;
                #endregion ddlPumpingStationCount

                #region ddlCity


                List<SelectListItem> ddlCity = new List<SelectListItem>();
                model.OpCode = 21;
                model.Value = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlCity = ddlCity;
                #endregion ddlCity


                model.OpCode = 3;
                model.Pk_STPId = Id;
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpRegistration();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Pk_STPId = int.Parse(ds.Tables[0].Rows[0]["Pk_Stp_Id"].ToString());
                        model.STPName = ds.Tables[0].Rows[0]["StpName"].ToString();
                        model.Capacity = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        model.MLDDay = ds.Tables[0].Rows[0]["MLDDay"].ToString();
                        model.ElectricityMeterLoad = ds.Tables[0].Rows[0]["ElectricityMeterLoad"].ToString();
                        model.ElectricityAccountNo = ds.Tables[0].Rows[0]["ElectricityAccountNo"].ToString();
                        model.UPPCLBillCycle = ds.Tables[0].Rows[0]["UPPCLBillCycle"].ToString();
                        model.BOD = ds.Tables[0].Rows[0]["BOD"].ToString();
                        model.COD = ds.Tables[0].Rows[0]["COD"].ToString();
                        model.FC = ds.Tables[0].Rows[0]["FC"].ToString();
                        model.TSS = ds.Tables[0].Rows[0]["TSS"].ToString();
                        model.Name = ds.Tables[0].Rows[0]["PumpingStationName"].ToString();
                        model.PumpStationMeterLoad = ds.Tables[0].Rows[0]["PumpStationMeterLoad"].ToString();
                        model.PumpingStatisonAccountNo = ds.Tables[0].Rows[0]["PumpStationAccountNo"].ToString();
                        model.SewerLength = ds.Tables[0].Rows[0]["SewerLength"].ToString();
                        model.DrainageName = ds.Tables[0].Rows[0]["DrainageName"].ToString();
                        model.SPSName = ds.Tables[0].Rows[0]["SPSName"].ToString();
                        model.IsFC = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFC"]);
                        model.IsCOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCOD"]);
                        model.IsBOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBOD"]);
                        model.IsTSS = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTSS"]);
                        model.IsInhouse = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsInhouse"]);
                        model.IsUPPCB = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsUPPCB"]);
                        model.IsTPI = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTPI"]);
                    }
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
        }

        [HttpPost]
        public ActionResult StpRegistration(FirmDetails model, string btnSubmit)
        {
            try
            {
                #region ddlPumpingStationCount

                List<SelectListItem> PumpingStationCount = new List<SelectListItem>();
                model.OpCode = 13;
                DataSet dataset1 = model.GetMasterData();
                if (dataset1 != null && dataset1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset1.Tables[0].Rows)
                    {
                        PumpingStationCount.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.PumpingStationCount = PumpingStationCount;
                #endregion ddlPumpingStationCount

                #region ddlCity


                List<SelectListItem> ddlCity = new List<SelectListItem>();
                model.OpCode = 21;
                model.Value = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlCity = ddlCity;
                #endregion ddlCity

                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    model.OpCode = model.Pk_STPId > 0 ? 7 : 1;
                    model.OpCode = 1;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    if (model.IsFC == false)
                    {
                        model.FC = "0";
                    }
                    if (model.IsTSS == false)
                    {
                        model.IsTSS = true;
                    }
                    if (model.IsBOD == false)
                    {
                        model.IsBOD = true;
                    }
                    if (model.IsCOD == false)
                    {
                        model.IsCOD = true;
                    }
                    if (model.IsInhouse == false)
                    {
                        model.IsInhouse = true;
                    }
                    if (model.IsUPPCB == false)
                    {
                        model.IsUPPCB = true;

                    }
                    if (model.IsTPI == false)
                    {
                        model.IsTPI = true;
                    }
                    DataSet ds = model.StpRegistration();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            if (!string.IsNullOrEmpty(model.PumpingStation) && model.PumpingStation != "0")
                            {

                                for (int i = 1; i <= Convert.ToInt32(Request.Form["Count"]); i++)
                                {

                                    model.SPSName = Request.Form["SPSName_" + i].ToString();
                                    model.ElectricityMeterLoad = Request.Form["PumpStationMeterLoad_" + i].ToString();
                                    model.PumpingStatisonAccountNo = Request.Form["PumpingStatisonAccountNo_" + i].ToString();
                                    model.BillingCycle = Request.Form["PumpingBillCycle_" + i].ToString();
                                    model.DrainageName = Request.Form["DrainageName_" + i].ToString();
                                    model.SewerLength = Request.Form["SewerLength_" + i].ToString();
                                    model.PeekDesignedDischarge = Request.Form["PeekDesignedDischarge_" + i].ToString();
                                    model.PeekDesignedFactor = Request.Form["PeekDesignedFactor_" + i].ToString();
                                    model.Pk_STPId = int.Parse(ds.Tables[0].Rows[0]["Pk_STPId"].ToString());
                                    model.OpCode = 2;
                                    if (!string.IsNullOrEmpty(model.SPSName))
                                    {
                                        model.IsSPS = true;
                                        DataSet ds1 = model.StpRegistration();
                                        //if (ds != null && ds.Tables.Count > 0)
                                        //{

                                        //    if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                        //    {
                                        //        TempData["Message"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        TempData["errorMessage"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    TempData["errorMessage"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                                        //}
                                    }
                                }

                            }

                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return View(model);
                        }
                    }
                }
                // else
                else
                {

                    model.OpCode = 7;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    if (model.IsFC == false)
                    {
                        model.FC = "0";
                    }
                    if (model.IsTSS == false)
                    {
                        model.IsTSS = true;
                    }
                    if (model.IsBOD == false)
                    {
                        model.IsBOD = true;
                    }
                    if (model.IsCOD == false)
                    {
                        model.IsCOD = true;
                    }
                    if (model.IsInhouse == false)
                    {
                        model.IsInhouse = true;
                    }
                    if (model.IsUPPCB == false)
                    {
                        model.IsUPPCB = true;

                    }
                    if (model.IsTPI == false)
                    {
                        model.IsTPI = true;
                    }
                    DataSet ds = model.StpRegistration();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();

                        }
                        else
                        {

                            TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return View(model);

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("FirmStpList");
        }

        public ActionResult FirmStpList(string Id)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 3;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpRegistration();
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

        public ActionResult StpPumpStationList(string Id)
        {

            FirmDetails model = new FirmDetails();
            try
            {
                model.OpCode = 4;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Pk_STPId = int.Parse(Crypto.Decrypt(Id));
                ViewBag.FK_STPId = model.Pk_STPId;
                DataSet ds = model.StpRegistration();
                model.dtDetails = ds.Tables[0];
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);

        }

        public ActionResult DeleteFirmStp(string Id)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 5;
                model.Pk_STPId = int.Parse(Id);
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpRegistration();
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
                return RedirectToAction("FirmStpList");
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return RedirectToAction("FirmStpList");

            }
        }

        public ActionResult DeleteStpMeterLoad(string Id)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 6;
                model.Pk_Id = Id;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpRegistration();
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
                return RedirectToAction("FirmStpList");
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return RedirectToAction("FirmStpList");

            }
        }

        public ActionResult STPBillingList(FirmDetails model, string btnSearch, string id,string Zone,string Firm)
        {
            try
            {
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

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 6;
                DataSet dataSet2 = model.GetMasterData();
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

                if (!string.IsNullOrEmpty(model.FromDate))
                {
                    model.FromDate = Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty(model.ToDate))
                {
                    model.ToDate = Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty("btnSearch"))
                {
                    if (model.Fk_MonthId == "0")
                    {
                        model.Fk_MonthId = null;
                    }
                    else
                    {
                        model.Fk_MonthId = model.Fk_MonthId;
                    }
                }
                else
                {
                    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                }
                //model.Fk_MonthId = !string.IsNullOrEmpty("btnSearch") == true ? model.Fk_MonthId : HttpContext.Session.GetString("Pk_MonthId");
                if (!string.IsNullOrEmpty(Zone))
                {
                    model.Fk_ZoneId = string.IsNullOrEmpty(Zone) ? model.Fk_ZoneId : Crypto.Decrypt(Zone);
                }
                else
                {
                    model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                }
                //model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                if (!string.IsNullOrEmpty(Firm))
                {
                    model.AddedBy = string.IsNullOrEmpty(Firm) ? model.AddedBy : Crypto.Decrypt(Firm);
                }
                else
                {
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                }
                if (model.SelectedMonth != null)
                {
                    model.Months = string.Join(",", model.SelectedMonth);
                }
                model.Pk_STPId = string.IsNullOrEmpty(id) ? model.Pk_STPId : int.Parse(Crypto.Decrypt(id));
                DataSet ds = model.GetBillingDetails();

                model.dtDetails = ds.Tables[0];

                //model.dtDetails1 = ds.Tables[1];
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View(model);

            }
        }

        public JsonResult GetAgencyList(string Id)
        {
            List<FirmDetails> dtList = new List<FirmDetails>();
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 14;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FirmDetails list = new FirmDetails();
                        list.Pk_Id = ds.Tables[0].Rows[i]["Id"].ToString();
                        list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        dtList.Add(list);
                    }
                }
                return Json(dtList);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(dtList);
        }

        public ActionResult MoM()
        {
            MoM mom = new MoM();
            try
            {
                mom.OpCode = 4;
                DataSet ds = mom.saveMom();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    mom.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(mom);
        }
        [HttpGet]
        public ActionResult AddSTPBilling(string id)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                DataSet ds = new DataSet();
                model.Pk_STPId = string.IsNullOrEmpty(id) ? 0 : int.Parse(Crypto.Decrypt(id));
                // model.Fk_STPId = id.ToString();

                ds = model.LatestBillingDate(model.Pk_STPId??0);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.LastBillingDate = ds.Tables[0].Rows[0]["BillingDate"].ToString();
                    model.BillingDate = model.LastBillingDate;
                    // string billingDateStr = ds.Tables[0].Rows[0]["BillingDate"].ToString();
                    //model.dtDetails1 = ds.Tables[0];
                }
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
                //model.Pk_STPId = string.IsNullOrEmpty(id)?0: int.Parse(Crypto.Decrypt(id));
                model.OpCode = 3;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds1 = model.StpRegistration();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds1.Tables[0];
                }

                return View(model);
            }
            catch (Exception Ex) { TempData["ErrorMessage"] = Ex.Message; return View(); }
        }
        [HttpPost]
        public ActionResult AddSTPBilling(FirmDetails model, string btnSubmit)
        {
            try
            {
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




                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                    model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                    model.OpCode = 1;
                    string billingDate = Common.ConvertToSystemDate(model.BillingDate, "dd/mm/yyyy");
                    model.BillingDate = billingDate;
                    DataSet ds = model.GetSTPBilling();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("AddSTPBilling", new { id = Crypto.Encrypt(model.Pk_STPId.ToString()) });
                        }
                        else
                        {
                            model.OpCode = 3;
                            model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                            model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                            DataSet ds1 = model.StpRegistration();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                model.dtDetails = ds1.Tables[0];
                            }
                            TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return View(model);
                        }
                    }
                }



            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("STPBillingList");
        }

        public JsonResult GetSTPCallLog(string stpId, string BillingDate)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.Fk_STPId = stpId;
                model.BillingDate = Common.ConvertToSystemDate(BillingDate, "dd/mm/yyyy");
                DataSet ds = model.GetCallLogBySTP();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Pk_Id = ds.Tables[0].Rows[0]["Pk_CallCenterId"].ToString();
                    model.ComplaintReceived = ds.Tables[0].Rows[0]["TotalCallRecived"].ToString();
                    model.ComplaintResolved = ds.Tables[0].Rows[0]["TotalCallResolve"].ToString();
                }
                return Json(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public ActionResult DeleteSTPBilling(string Id, string StpId)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                model.Pk_BillingId = Id;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 3;

                DataSet ds = model.GetSTPBilling();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                return RedirectToAction("STPBillingList", new { id = Crypto.Encrypt(StpId) });
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
                return RedirectToAction("STPBillingList", new { id = Crypto.Encrypt(StpId) });
            }
        }

        public JsonResult BillingDetails(string Id)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.Pk_BillingId = Id;
                model.OpCode = 4;
                DataSet ds = model.GetSTPBilling();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        model.Pk_STPId = int.Parse(ds.Tables[0].Rows[0]["FK_STPId"].ToString());
                        model.Pk_BillingId = ds.Tables[0].Rows[0]["Pk_BillingId"].ToString();
                        model.BillNo = ds.Tables[0].Rows[0]["BillNo"].ToString();
                        model.BillingDate = ds.Tables[0].Rows[0]["BillingDate"].ToString();
                        model.WaterDischarge = ds.Tables[0].Rows[0]["Actual_Achieved"].ToString();
                        model.ComplaintReceived = ds.Tables[0].Rows[0]["ComplaintReceived"].ToString();
                        model.ComplaintResolved = ds.Tables[0].Rows[0]["ComplaintResolved"].ToString();
                        model.InHouseBOD = ds.Tables[0].Rows[0]["InHouseBOD"].ToString();
                        model.InHouseCOD = ds.Tables[0].Rows[0]["InHouseCOD"].ToString();
                        model.InHouseTSS = ds.Tables[0].Rows[0]["InHouseTSS"].ToString();
                        model.InHouseFC = ds.Tables[0].Rows[0]["InHouseFC"].ToString();
                        model.ThirdPartyBOD = ds.Tables[0].Rows[0]["ThirdPartyBOD"].ToString();
                        model.ThirdPartyCOD = ds.Tables[0].Rows[0]["ThirdPartyCOD"].ToString();
                        model.ThirdPartyTSS = ds.Tables[0].Rows[0]["ThirdPartyTSS"].ToString();
                        model.ThirdPartyFC = ds.Tables[0].Rows[0]["ThirdPartyFC"].ToString();
                        model.UPPCBBOD = ds.Tables[0].Rows[0]["UPPCBBOD"].ToString();
                        model.UPPCBCOD = ds.Tables[0].Rows[0]["UPPCBCOD"].ToString();
                        model.UPPCBTSS = ds.Tables[0].Rows[0]["UPPCBTSS"].ToString();
                        model.UPPCBFC = ds.Tables[0].Rows[0]["UPPCBFC"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public JsonResult GetSTPBillDetails(int Id)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.Pk_STPId = Id;
                model.OpCode = 3;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.IsBOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBOD"].ToString());
                        model.IsCOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCOD"].ToString());
                        model.IsTSS = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTSS"].ToString());
                        model.IsFC = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFC"].ToString());
                        model.Pk_STPId = Convert.ToInt32(ds.Tables[0].Rows[0]["Pk_Stp_Id"].ToString());
                        model.BOD = ds.Tables[0].Rows[0]["BOD"].ToString();
                        model.COD = ds.Tables[0].Rows[0]["COD"].ToString();
                        model.TSS = ds.Tables[0].Rows[0]["TSS"].ToString();
                        model.FC = ds.Tables[0].Rows[0]["FC"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public ActionResult DailyBillingReport(string Pk_STPId, string Fk_MonthId, string FromDate, string ToDate)
        {
            try
            {
                FirmDetails model = new FirmDetails();
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                model.OpCode = 11;
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

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 6;
                DataSet dataSet2 = model.GetMasterData();
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

                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                if (!string.IsNullOrEmpty(FromDate))
                {
                    model.FromDate = Common.ConvertToSystemDate(FromDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    model.ToDate = Common.ConvertToSystemDate(ToDate, "dd/mm/yyyy");
                }
                model.Fk_STPId = Pk_STPId;
                model.Fk_MonthId = Fk_MonthId;
                DataSet ds = model.GetDailyBillingReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    model.dtDetails1 = ds.Tables[1];
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();

            }
        }

        public ActionResult STPBillingRequestList(FirmDetails firmDetails)
        {
            try
            {
                firmDetails.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                firmDetails.OpCode = 1;
                DataSet ds = firmDetails.GetBillRequestList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    firmDetails.dtDetails = ds.Tables[0];
                }
                return View(firmDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; return View(firmDetails);
            }
        }

        public JsonResult STPBillingRequestDetails(string Id, string MonthId, string Year, string PK_InvId)
        {
            List<FirmDetails> list = new List<FirmDetails>();
            try
            {
                FirmDetails firmDetails = new FirmDetails();
                firmDetails.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                firmDetails.Fk_STPId = Id;
                firmDetails.Fk_MonthId = MonthId;
                firmDetails.PK_InvId = PK_InvId;
                firmDetails.OpCode = 2;

                DataSet ds = firmDetails.GetBillRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FirmDetails firm = new FirmDetails();
                        firm.Pk_BillingId = ds.Tables[0].Rows[i]["Pk_BillingId"].ToString();
                        firm.STPName = ds.Tables[0].Rows[i]["StpName"].ToString();
                        firm.BillNo = ds.Tables[0].Rows[i]["BillNo"].ToString();
                        firm.BillingDate = ds.Tables[0].Rows[i]["BillingDate"].ToString();
                        firm.Capacity = ds.Tables[0].Rows[i]["DesignCapacity"].ToString();
                        firm.WaterDischarge = ds.Tables[0].Rows[i]["Waterdischarge"].ToString();
                        firm.InHouseBOD = ds.Tables[0].Rows[i]["InHouse_BOD"].ToString();
                        firm.InHouseCOD = ds.Tables[0].Rows[i]["InHouse_COD"].ToString();
                        firm.InHouseTSS = ds.Tables[0].Rows[i]["InHouse_TSS"].ToString();
                        firm.InHouseFC = ds.Tables[0].Rows[i]["InHouse_FC"].ToString();
                        firm.ThirdPartyBOD = ds.Tables[0].Rows[i]["ThirdParty_BOD"].ToString();
                        firm.ThirdPartyCOD = ds.Tables[0].Rows[i]["ThirdParty_COD"].ToString();
                        firm.ThirdPartyTSS = ds.Tables[0].Rows[i]["ThirdParty_TSS"].ToString();
                        firm.ThirdPartyFC = ds.Tables[0].Rows[i]["ThirdPartyFC"].ToString();
                        firm.UPPCBBOD = ds.Tables[0].Rows[i]["UPPCB_BOD"].ToString();
                        firm.UPPCBCOD = ds.Tables[0].Rows[i]["UPPCB_COD"].ToString();
                        firm.UPPCBTSS = ds.Tables[0].Rows[i]["UPPCB_TSS"].ToString();
                        firm.UPPCBFC = ds.Tables[0].Rows[i]["UPPCB_FC"].ToString();
                        firm.ComplaintReceived = ds.Tables[0].Rows[i]["ComplaintReceived"].ToString();
                        firm.ComplaintResolved = ds.Tables[0].Rows[i]["ComplaintResolved"].ToString();
                        list.Add(firm);
                    }
                }
                return Json(list);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; return Json(list);
            }
        }

        public ActionResult GenerateBill(FirmDetails firmDetails, string SData, string MData, string YData, string IData)
        {
            try
            {
                if (firmDetails.BillingDate == null)
                {
                    var dateTime = DateTime.Now;
                    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                    firmDetails.BillingDate = dateValue2;


                }
                if (!string.IsNullOrEmpty(SData))
                {
                    firmDetails.Fk_STPId = Crypto.Decrypt(SData);


                }
                if (!string.IsNullOrEmpty(MData))
                {
                    firmDetails.Fk_MonthId = Crypto.Decrypt(MData);


                }
                if (!string.IsNullOrEmpty(YData))
                {
                    firmDetails.Year = Crypto.Decrypt(YData);

                }
                if (!string.IsNullOrEmpty(IData))
                {
                    firmDetails.PK_InvId = Crypto.Decrypt(IData);

                }
                if (firmDetails.BillingDate == null)
                {
                    var dateTime = DateTime.Now;
                    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                    firmDetails.BillingDate = dateValue2;

                }
                ViewBag.BillingDate = firmDetails.BillingDate.Replace("-", "/");
                firmDetails.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = firmDetails.GetSTPBillSummary();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //firmDetails.Fk_STPId = ds.Tables[0].Rows[0]["FK_STPId"].ToString();
                        firmDetails.STPName = ds.Tables[0].Rows[0]["StpName"].ToString();
                        firmDetails.Year = ds.Tables[0].Rows[0]["BillingYear"].ToString();
                        firmDetails.Fk_MonthId = ds.Tables[0].Rows[0]["MonthName"].ToString();
                        firmDetails.MLDDay = ds.Tables[0].Rows[0]["AmountOfMLDSTP"].ToString();
                        firmDetails.Capacity = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        firmDetails.Actual_Achieved = ds.Tables[0].Rows[0]["Actual_Achieved"].ToString();
                        firmDetails.Treated_for_Payement = ds.Tables[0].Rows[0]["Treated_for_Payement"].ToString();
                        firmDetails.AsPerCBFixCharges_60 = ds.Tables[0].Rows[0]["AsPerCBFixCharges_60"].ToString();
                        firmDetails.AsPerActual_FixCharges_60 = ds.Tables[0].Rows[0]["AsPerActual_FixCharges_60"].ToString();
                        firmDetails.AsPerCBFixCharges_40 = ds.Tables[0].Rows[0]["AsPerCBFixCharges_40"].ToString();
                        firmDetails.AsPerActual_FixCharges_40 = ds.Tables[0].Rows[0]["AsPerActual_FixCharges_40"].ToString();
                        firmDetails.BODReportedValue = ds.Tables[0].Rows[0]["BODReportedValue"].ToString();
                        firmDetails.BODAmount = ds.Tables[0].Rows[0]["BODAmount"].ToString();
                        firmDetails.BODLDAmount = ds.Tables[0].Rows[0]["BODLDAmount"].ToString();
                        firmDetails.CODReportedValue = ds.Tables[0].Rows[0]["CODReportedValue"].ToString();
                        firmDetails.CODAmount = ds.Tables[0].Rows[0]["CODAmount"].ToString();
                        firmDetails.CODLDAmount = ds.Tables[0].Rows[0]["CODLDAmount"].ToString();
                        firmDetails.TSSReportedValue = ds.Tables[0].Rows[0]["TSSReportedValue"].ToString();
                        firmDetails.TSSAmount = ds.Tables[0].Rows[0]["TSSAmount"].ToString();
                        firmDetails.TSSLDAmount = ds.Tables[0].Rows[0]["TSSLDAmount"].ToString();
                        firmDetails.FCReportedValue = ds.Tables[0].Rows[0]["FCReportedValue"].ToString();
                        firmDetails.FCAmount = ds.Tables[0].Rows[0]["FCAmount"].ToString();
                        firmDetails.FCLDAmount = ds.Tables[0].Rows[0]["FCLDAmount"].ToString();
                        firmDetails.TotalVerifiedAmount = ds.Tables[0].Rows[0]["TotalVerifiedAmount"].ToString();
                        firmDetails.TotalLDAmount = ds.Tables[0].Rows[0]["TotalLDAmount"].ToString();
                    }
                }
                return View(firmDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> GenerateBill(FirmDetails firmDetails)
        {
            try
            {
                if (firmDetails.BillingDate == null)
                {
                    var dateTime = DateTime.Now;
                    var dateValue2 = dateTime.ToString("dd/MM/yyyy");
                    firmDetails.BillingDate = dateValue2;

                }
                ViewBag.BillingDate = firmDetails.BillingDate.Replace("-", "/");
                if (firmDetails.CoveringLetter != null && firmDetails.CoveringLetter.Length > 0)
                {
                    string fileLocationCover = await FileManagement.WriteFiles(firmDetails.CoveringLetter, "CoveringLetter", "GenerateBill");
                    firmDetails.CoveringLetterUrl = fileLocationCover;

                }
                if (firmDetails.InspectionReport != null && firmDetails.InspectionReport.Length > 0)
                {
                    string fileInspectionReportLocation = await FileManagement.WriteFiles(firmDetails.InspectionReport, "InspectionReport", "GenerateBill");
                    firmDetails.InspectionReportUrl = fileInspectionReportLocation;
                }
                if (firmDetails.TPIReports != null && firmDetails.TPIReports.Length > 0)
                {
                    string fileTPILocation = await FileManagement.WriteFiles(firmDetails.TPIReports, "TPIReports", "GenerateBill");
                    firmDetails.TPIReportsUrl = fileTPILocation;
                }
                if (firmDetails.LDSheet != null && firmDetails.LDSheet.Length > 0)
                {
                    string fileLDLocation = await FileManagement.WriteFiles(firmDetails.LDSheet, "LDReport", "GenerateBill");
                    firmDetails.LDSheetUrl = fileLDLocation;
                }
                if (firmDetails.OtherDocuments != null && firmDetails.OtherDocuments.Length > 0)
                {
                    string fileOtherLocation = await FileManagement.WriteFiles(firmDetails.OtherDocuments, "OtherDocuments", "GenerateBill");
                    firmDetails.OtherDocumentsUrl = fileOtherLocation;
                }
                firmDetails.BillingDate = string.IsNullOrEmpty(firmDetails.BillingDate) ? null : Common.ConvertToSystemDate(firmDetails.BillingDate, "dd/MM/yyyy");
                firmDetails.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = firmDetails.GenerateBill();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        return RedirectToAction("STPBillingRequestList");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                return View(firmDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; return View();
            }
        }

        public ActionResult InvoiceDetails(FirmDetails model, string FromDate, string ToDate, string Fk_MonthId, string btnSubmit)
        {
            try
            {
                DataSet ds = new DataSet();
                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                model.OpCode = 6;
                DataSet dataSet2 = model.GetMasterData();
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
                if (FromDate != null && ToDate != null)
                {
                    model.FromDate = Common.ConvertToSystemDate(FromDate, "dd/mm/yyyy");
                    model.ToDate = Common.ConvertToSystemDate(ToDate, "dd/mm/yyyy");
                }

                Fk_MonthId = string.IsNullOrEmpty("btnSubmit") == true ? HttpContext.Session.GetString("Pk_MonthId") : Fk_MonthId;
                model.Fk_MonthId = Fk_MonthId;
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                ds = model.GetInvoiceMSTList();
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

        [HttpGet]
        public JsonResult ViewInvoiceBill(string fk_stpid, string fk_invId)
        {
            List<FirmDetails> dtList = new List<FirmDetails>();
            try
            {
                FirmDetails model = new FirmDetails();
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.PK_InvId = fk_invId;
                DataSet ds = model.GetViewInvoiceDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FirmDetails list = new FirmDetails();
                        list.BillingDate = ds.Tables[0].Rows[i]["Dates"].ToString();
                        list.AmountOfMLDSTP = ds.Tables[0].Rows[i]["AmountOfMLDSTP"].ToString();
                        list.Capacity = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        list.Actual_Achieved = ds.Tables[0].Rows[i]["Actual_Achieved"].ToString();
                        list.Treated_for_Payement = ds.Tables[0].Rows[i]["Treated_for_Payement"].ToString();
                        list.AsPerCBFixCharges_60 = ds.Tables[0].Rows[i]["AsPerCBFixCharges_60"].ToString();
                        list.AsPerActual_FixCharges_60 = ds.Tables[0].Rows[i]["AsPerActual_FixCharges_60"].ToString();
                        list.AsPerCBFixCharges_40 = ds.Tables[0].Rows[i]["AsPerCBFixCharges_40"].ToString();
                        list.AsPerActual_FixCharges_40 = ds.Tables[0].Rows[i]["AsPerActual_FixCharges_40"].ToString();
                        list.BODReportedValue = ds.Tables[0].Rows[i]["BODReportedValue"].ToString();
                        list.BODAmount = ds.Tables[0].Rows[i]["BODAmount"].ToString();
                        list.BODLDAmount = ds.Tables[0].Rows[i]["BODLDAmount"].ToString();
                        list.CODReportedValue = ds.Tables[0].Rows[i]["CODReportedValue"].ToString();
                        list.CODAmount = ds.Tables[0].Rows[i]["CODAmount"].ToString();
                        list.CODLDAmount = ds.Tables[0].Rows[i]["CODLDAmount"].ToString();
                        list.TSSReportedValue = ds.Tables[0].Rows[i]["TSSReportedValue"].ToString();
                        list.TSSAmount = ds.Tables[0].Rows[i]["TSSAmount"].ToString();
                        list.TSSLDAmount = ds.Tables[0].Rows[i]["TSSLDAmount"].ToString();
                        list.FCReportedValue = ds.Tables[0].Rows[i]["FCReportedValue"].ToString();
                        list.FCAmount = ds.Tables[0].Rows[i]["FCAmount"].ToString();
                        list.FCLDAmount = ds.Tables[0].Rows[i]["FCLDAmount"].ToString();
                        list.TotalVerifiedAmount = ds.Tables[0].Rows[i]["TotalVerifiedAmount"].ToString();
                        list.TotalLDAmount = ds.Tables[0].Rows[i]["TotalLDAmount"].ToString();

                        dtList.Add(list);
                    }
                }
                return Json(dtList);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return Json(dtList);

            }
        }

        public ActionResult PrintInvoiceBill(string fk_stpid, string fk_invId)
        {
            FirmDetails model = new FirmDetails();
            DataSet ds = new DataSet();
            try
            {
                model.Fk_STPId = Crypto.Decrypt(fk_stpid);
                model.PK_InvId = Crypto.Decrypt(fk_invId);
                ds = model.GetGentatedBill();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View(model);

            }
            return View(model);
        }

        public JsonResult CheckGeneratedBill(string billDate, string Fk_STPId)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.BillingDate = Common.ConvertToSystemDate(billDate, "dd/mm/yyyy");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_STPId = Fk_STPId;
                model.OpCode = 1;
                DataSet ds = model.CheckGeneratedBill();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public JsonResult CheckEntryBill(string billDate, string Fk_STPId)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.BillingDate = Common.ConvertToSystemDate(billDate, "dd/mm/yyyy");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                model.Fk_STPId = Fk_STPId;
                model.OpCode = 2;
                DataSet ds = model.CheckGeneratedBill();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public JsonResult CheckDischargeEntryBill(string WaterDischarge, string Fk_STPId)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                //model.BillingDate = Common.ConvertToSystemDate(billDate, "dd/mm/yyyy");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.WaterDischarge = WaterDischarge;
                model.Fk_STPId = Fk_STPId;
                //model.OpCode = 2;
                DataSet ds = model.CheckWaterDischargeBill();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;

            }
            return Json(model);
        }
        public JsonResult ChecksameDischarge(string WaterDischarge, string Fk_STPId, string BillingDate)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.WaterDischarge = WaterDischarge;
                model.Fk_STPId = Fk_STPId;
                model.BillingDate = Common.ConvertToSystemDate(BillingDate, "dd/mm/yyyy");
                DataSet ds = model.GetsameDischarge();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }
        public ActionResult UserManual()
        {
            return View();
        }



        public ActionResult EditStpPumpStation(STPModel model)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = model.GetFirmStpEdit();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Pk_Id = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                    model.Fk_STPId = ds.Tables[0].Rows[0]["Fk_StpId"].ToString();
                    model.Fk_ParentId = ds.Tables[0].Rows[0]["Fk_ParentId"].ToString();
                    model.STPName = ds.Tables[0].Rows[0]["STPName"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                    model.ElectricityMeterLoad = ds.Tables[0].Rows[0]["MeterLoad"].ToString();
                    model.AccountNo = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                    model.BillingCycle = ds.Tables[0].Rows[0]["BillingCycle"].ToString();
                    model.PeekDesignedDischarge = ds.Tables[0].Rows[0]["PeekDesignedDischarge"].ToString();
                    model.PeekDesignedFactor = ds.Tables[0].Rows[0]["PeekDesignedFactor"].ToString();
                    model.SewerLength = ds.Tables[0].Rows[0]["SewerLength"].ToString();
                    model.DrainageName = ds.Tables[0].Rows[0]["DrainageName"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }


        public JsonResult GetSPS_IPSDropDown(string FK_STPId)
        {
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 25;
                model.Value = FK_STPId;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                return Json(ddlDesignation);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(ddlDesignation);

        }
        [HttpPost]
        public JsonResult GetUpdatePumpingStation(STPModel model)
        {
            try
            {
                // model.OpCode = 2;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetUpdatePumpingSation();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                    model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                {
                    model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                    model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult AddPumpingStation(STPModel model)
        {
            try
            {

                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.SavePumpingStation();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                    model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                {
                    model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                    model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetSTP(string STP_id)
        {
            FirmDetails model = new FirmDetails();
          
            try
            {
                model.Pk_STPId = Convert.ToInt32(STP_id);
                DataSet ds = model.GetSTPByID();
                List<FirmDetails> lstSTP = new List<FirmDetails>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                }
                else {
                    model.Status = "Data Not Found";
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return Json(model);
        }        

        public ActionResult STPList(string Type)
        {

            try
            {
                FirmDetails model = new FirmDetails();
                model.OpCode = 3;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpLIst(Crypto.Decrypt(Type));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (Crypto.Decrypt(Type) == "UploadMeasurement")
                {
                    model.Url = "/Firm/STPBillingList";
                }
                else if (Crypto.Decrypt(Type) == "ElectricityBill")
                {
                    model.Url = "/DeductionRelease/GetelectricityBillList";
                }
                else if (Crypto.Decrypt(Type) == "DGBilling")
                {
                    model.Url = "/DeductionRelease/GetDeductionList";
                }

                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }

            return View();
        }

        public JsonResult GetFirmDDl(string Id)
        {
            #region ddlFirm
            Dashboard dashboard = new Dashboard();
            List<SelectListItem> ddlFirm = new List<SelectListItem>();
            try
            {
                dashboard.Value = Id;
                dashboard.OpCode = 29;
                DataSet dsFirm = dashboard.GetMasterData();
                if (dsFirm != null && dsFirm.Tables.Count > 0 && dsFirm.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsFirm.Tables[0].Rows)
                    {
                        ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            #endregion
            return Json(ddlFirm);
        }

        public JsonResult GetSTPDDl(string Id)
        {
            #region ddlSTP
            Dashboard dashboard = new Dashboard();
            List<SelectListItem> ddlSTP = new List<SelectListItem>();
            try
            {

                dashboard.Value = Id;
                dashboard.OpCode = 30;
                DataSet dsSTP = dashboard.GetMasterData();
                if (dsSTP != null && dsSTP.Tables.Count > 0 && dsSTP.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsSTP.Tables[0].Rows)
                    {
                        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            #endregion
            return Json(ddlSTP);
        }
    }
}
