using Microsoft.AspNetCore.Mvc;
using OCOO.Models;
using System.Collections.Immutable;
using System.Data;

namespace OCOO.Controllers
{
    public class STCController : Controller// STCBaseController
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult GetFirm()
        {
            DailyCapacity model = new DailyCapacity();
            model.OpCode = 2;
            model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
            model.Fk_DesignationId = HttpContext.Session.GetString("Pk_UserId");
            DataSet ds = model.GetFirmRequest();
            model.dtDetails = ds.Tables[0];
            return View(model);
        }

        public JsonResult GetDocumentDetails(string Id)
        {
            DailyCapacity model = new DailyCapacity();
            model.UniqueId = Id;
            model.OpCode = 6;
            model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
            DataSet ds = model.GetFirmRequest();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.Pk_DocumentId = ds.Tables[0].Rows[0]["Pk_DocumentId"].ToString();
                model.UniqueId = ds.Tables[0].Rows[0]["Fk_UniqueId"].ToString();
                model.CoveringLetterURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["CoveringLetterURL"].ToString();
                model.InspectionReportURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["InspectionReportURL"].ToString();
                model.TPIReportsURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["TPIReportsURL"].ToString();
                model.LDSheetURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["LDSheetURL"].ToString();
                model.OtherDocumentsURL = @BaseUrl.ImageURL + ds.Tables[0].Rows[0]["OtherDocumentsURL"].ToString();
            }
            return Json(model);
        }

        public ActionResult GetFirmRequest(string Id)
        {
            DailyCapacity model = new DailyCapacity();
            model.UniqueId = Id;
            model.OpCode = 3;
            DataSet ds = model.GetFirmRequest();
            model.dtDetails = ds.Tables[0];
            return View(model);
        }

        public ActionResult ApproveFirmRequest(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    DailyCapacity model = new DailyCapacity();
                    model.UniqueId = Id;
                    model.Fk_DesignationId = HttpContext.Session.GetString("Fk_UsertypeId"); ;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.OpCode = 4;
                    model.IsApproved = "1";
                    DataSet ds = model.GetFirmRequest();
                    if (ds != null && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                return RedirectToAction("GetFirm");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetFirm");
            }
        }

        public ActionResult DeclineFirmRequest(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    DailyCapacity model = new DailyCapacity();
                    model.UniqueId = Id;
                    model.Fk_DesignationId = HttpContext.Session.GetString("Fk_UsertypeId"); ;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.OpCode = 4;
                    model.IsDeclined = "1";
                    DataSet ds = model.GetFirmRequest();
                    if (ds != null && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                return RedirectToAction("GetFirm");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetFirm");
            }
        }

        public ActionResult STCApprovedBy(string Fk_UniqueId)
        {
            try
            {
                DailyCapacity model = new DailyCapacity();
                if (!string.IsNullOrEmpty(Fk_UniqueId))
                {
                    model.Fk_UsertypeId = HttpContext.Session.GetString("Fk_UsertypeId");
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.UniqueId = Crypto.Decrypt(Fk_UniqueId);
                    DataSet ds = model.GetApprovalStatus();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        List<BindList> list = new List<BindList>();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BindList listData = new BindList();
                            listData.DesignationName = ds.Tables[0].Rows[i]["DesignationName"].ToString();
                            listData.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                            list.Add(listData);
                        }
                        model.BindList = list;
                    }
                }
                return Json(model.BindList);
            }
            catch (Exception)
            {
                return RedirectToAction("GetFirm");
            }
        }

    }
}
