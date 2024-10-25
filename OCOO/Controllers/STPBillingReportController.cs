using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using OCOO.Models;
using System.Data;
using System.Net;
using System.Reflection;

namespace OCOO.Controllers
{
    public class STPBillingReportController : Controller
    {

        public ActionResult Login(STP_Billing Login, string btnLogin)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnLogin))
                {
                    DataSet ds = Login.Thirdparty_Login();
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {

                            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                HttpContext.Session.SetString("ThirdpartyId", ds.Tables[0].Rows[0]["Id"].ToString());
                                HttpContext.Session.SetString("ThirdpartyName", ds.Tables[0].Rows[0]["Name"].ToString());
                                HttpContext.Session.SetString("ThirdpartyEmail", ds.Tables[0].Rows[0]["Email"].ToString());
                                HttpContext.Session.SetString("ThirdpartyMobile", ds.Tables[0].Rows[0]["Mobile"].ToString());
                                HttpContext.Session.SetString("ThirdpartyPassword", ds.Tables[0].Rows[0]["Password"].ToString());
                                HttpContext.Session.SetString("ThirdpartyGender", ds.Tables[0].Rows[0]["Gender"].ToString());
                                HttpContext.Session.SetString("ThirdpartyIsactive", ds.Tables[0].Rows[0]["IsActive"].ToString());
                                HttpContext.Session.SetString("AllowReport", ds.Tables[0].Rows[0]["AllowReport"].ToString());
                                if (ds.Tables[0].Rows[0]["AllowReport"].ToString() == "Flow")
                                    return RedirectToAction("Report");
                                else if(ds.Tables[0].Rows[0]["AllowReport"].ToString() == "Complaint")
                                    return RedirectToAction("ComplaintReport");
                            }
                            else
                            {
                                TempData["LoginMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                        else
                        {
                            TempData["LoginMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(Login);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Report(STPAPIresponce obj)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ThirdpartyId")))
            {

                obj.FormDate = string.IsNullOrEmpty(obj.FormDate) ? null : Common.ConvertToSystemDate(obj.FormDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");
                DataSet ds = obj.GetSTPresponce();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.dtDetails = ds.Tables[0];
                }
                return View(obj);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        
        public ActionResult ComplaintReport(ComplaintReport obj)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ThirdpartyId")))
            {
                if (obj.Page == 0 || obj.Page == null)
                {
                    obj.Page = 1;
                }
                DataSet ds = new DataSet();
                obj.FormDate = string.IsNullOrEmpty(obj.FormDate) ? null : Common.ConvertToSystemDate(obj.FormDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");                
                ds = obj.GetComplaintData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.dtDetails = ds.Tables[0];
                    int total = 0;
                    if (obj.dtDetails.Rows.Count > 0)
                    {
                        total = Convert.ToInt32(obj.dtDetails.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(total, obj.Page, SessionManager.Size);
                        obj.Pager = pager;
                    }
                }
            }
            return View(obj);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
