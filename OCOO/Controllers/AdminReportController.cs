using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Office.Interop.Word;
using OCOO.Filter;
using OCOO.Models;
using OCOO.Models.AdminMasters;
using System.Data;
using System.Reflection;
using System.Security.Policy;

namespace OCOO.Controllers
{
    public class AdminReportController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetSTPDDl(string Id)
        {
            List<SelectListItem> ddlSTP = new List<SelectListItem>();
            try
            {
                #region ddlSTP
                Dashboard dashboard = new Dashboard();
                dashboard.OpCode = 11;
                DataSet dsSTP = dashboard.GetMasterData();
                if (dsSTP != null && dsSTP.Tables.Count > 0 && dsSTP.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsSTP.Tables[0].Rows)
                    {
                        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(ddlSTP);

        }
        public ActionResult InvoiceDetails(InvoiceDetails model, string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            try
            {

                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();
                model.OpCode = 8;
                DataSet dataSet1 = model.GetMasterData();
                if (dataSet1 != null && dataSet1.Tables.Count > 0)
                {
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet1.Tables[0].Rows)
                        {
                            ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlFirm = ddlFirm;
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

                if (FromDate != null && ToDate != null)
                {
                    model.FromDate = Common.ConvertToSystemDate(FromDate, "dd/mm/yyyy");
                    model.ToDate = Common.ConvertToSystemDate(ToDate, "dd/mm/yyyy");

                }
                if (string.IsNullOrEmpty(model.Fk_MonthId))
                {
                    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                }
                ds = model.GetInvoiceDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ViewBag.Data = ds.Tables[0].Rows[i]["Pk_ZoneId"].ToString();
                        ViewBag.Data1 = ds.Tables[0].Rows[i]["Pk_FirmId"].ToString();
                        ViewBag.Data2 = ds.Tables[0].Rows[i]["Inv_Month"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }

        public ActionResult ViewInvoiceDetail(InvoiceDetails model, string abc, string xyz, string mpa)
        {
            try
            {
                DataSet ds = new DataSet();
                if (abc != null && xyz != null)
                {
                    model.Fk_FirmId = Crypto.Decrypt(xyz);
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
                    else
                    {
                        ViewBag.Data = "/AdminReport/InvoiceDetails";

                    }
                }
                else
                {
                    return Redirect("/AdminReport /InvoiceDetails");
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
                return Redirect("/AdminReport /InvoiceDetails");

            }
            return View(model);
        }

        public ActionResult PrintInvoiceBill(InvoiceDetails model, string abc, string xyz, string mvy, string mvw)
        {

            DataSet ds = new DataSet();
            try
            {
                model.Fk_FirmId = Crypto.Decrypt(xyz);
                model.Fk_ZoneId = Crypto.Decrypt(abc);
                if (!string.IsNullOrEmpty(mvy))
                {
                    model.Year = Crypto.Decrypt(mvy);
                }
                if (!string.IsNullOrEmpty(mvw))
                {
                    model.Fk_MonthId = Crypto.Decrypt(mvw);
                }
                ds = model.GetPrintBill();
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
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult STPInvoiceReport(InvoiceDetails model, string FromDate, string ToDate, string Fk_MonthId, string btnSubmit)
        {
            DataSet ds = new DataSet();
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                model.Value = HttpContext.Session.GetString("Pk_UserId");
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

                ddlFirm.Add(new SelectListItem { Text = "--All--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--All--", Value = "0" });
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
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");

                //if (!string.IsNullOrEmpty(btnSubmit))
                //{
                //    model.Fk_MonthId = Fk_MonthId;

                //}
                //else
                //{

                //    Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                //if (string.IsNullOrEmpty(model.Fk_MonthId))
                //{
                //    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                ds = model.GetSTPReport();
                TempData["Fk_ZoneId"] = model.Fk_ZoneId;
                TempData["Fk_FirmId"] = model.Fk_FirmId;
                TempData["Fk_STPId"] = model.Fk_STPId;
                //  TempData["Fk_MonthId"] = model.Fk_MonthId;
                TempData["FromDate"] = model.FromDate;
                TempData["ToDate"] = model.ToDate;
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
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult PaidBillReport(PaidBillReport model)
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

                //if (string.IsNullOrEmpty(model.Fk_MonthId))
                //{
                //    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                model.BillStatus = "Paid";
                model.IsBilled ="1";
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;

                ds = model.GetZoneWisePaidBills();
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
                        ViewBag.Data = "/AdminReport/InvoiceDetails";

                    }
                }
                else
                {
                    return Redirect("/ApproveBilling/BillApprovalList");
                }
            }
            catch (Exception Ex) { TempData["ErrorMessage"] = Ex.Message; }
            return View(model);
        }
        public ActionResult ChangePassword(Login login, string BtnChange)
        {
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(BtnChange))
                {

                    login.Fk_UserTypeId = int.Parse(HttpContext.Session.GetString("Fk_UsertypeId"));
                    login.OpCode = 1;
                    login.Fk_UserId = Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId"));
                    DataSet dataSet = login.PasswordChange();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        login.dtDetails = dataSet.Tables[0];
                    }
                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("Login", "Home");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(login);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(login);

        }
        public async Task<ActionResult> Profile(Profile profile, string Update)
        {
            try
            {

                if (!string.IsNullOrEmpty(Update))
                {
                    profile.Fk_UserTypeId = int.Parse(HttpContext.Session.GetString("Fk_UsertypeId"));
                    profile.Pk_AdminId = HttpContext.Session.GetString("Pk_UserId");
                    profile.OpCode = 2;
                    if (profile.ProfilePic != null)
                    {
                        string Filepath = HttpContext.Session.GetString("FilePath").ToString();
                        profile.Documents = await FileManagement.WriteFiles(profile.ProfilePic, "ProfilePic", "ProfilePic");
                    }

                    DataSet dataSet = profile.ProfileUpdate();
                    if (dataSet != null)
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            profile.dtDetails = dataSet.Tables[0];
                        }
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (profile.Documents!=null)
                            {
                                HttpContext.Session.SetString("ProfilePic", profile.Documents);
                            }
                            

                            if (HttpContext.Session.GetString("Fk_UsertypeId") == "1")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "2")
                            {
                                return RedirectToAction("Dashboard", "Firm");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "3")
                            {
                                return RedirectToAction("Dashboard", "InspectionAgency");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "4")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                return RedirectToAction("Dashboard", "Admin");
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(profile);
                        }

                    }
                }
                profile.OpCode = 1;
                profile.Fk_UserTypeId = int.Parse(HttpContext.Session.GetString("Fk_UsertypeId"));
                profile.Pk_AdminId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataset = profile.ProfileUpdate();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    profile.Name = dataset.Tables[0].Rows[0]["Name"].ToString();
                    profile.MobileNo = dataset.Tables[0].Rows[0]["MobileNo"].ToString();
                    profile.Email = dataset.Tables[0].Rows[0]["Email"].ToString();
                    profile.Documents = dataset.Tables[0].Rows[0]["ProfilePic"].ToString();
                    profile.Pk_AdminId = dataset.Tables[0].Rows[0]["Pk_AdminId"].ToString();
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(profile);

        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
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

                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--All--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--All--", Value = "0" });
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

                //if (string.IsNullOrEmpty(model.Fk_MonthId))
                //{
                //    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                model.IsBilled = "0";

                ds = model.GetZoneWisePaidBills();
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

        //ask
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult StpAPIreport(STPAPIresponce obj)
        {
            DataSet ds = new DataSet();
            Common common = new Common();
            try
            {
                obj.FormDate = string.IsNullOrEmpty(obj.FormDate) ? null : Common.ConvertToSystemDate(obj.FormDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");
                ds = obj.GetSTPresponce();
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
            catch (Exception ex)
            {
                throw ex;
            }
            return View(obj);
           
        }


        public ActionResult VerifySPS(InvoiceDetails model, string abc, string xyz, string mpa)
        {
            try
            {

                DataSet ds = new DataSet();
                if (abc != null && xyz != null)
                {
                    model.Fk_FirmId = Crypto.Decrypt(xyz);
                    model.Pk_BillingId = Crypto.Decrypt(abc);
                    ds = model.ViewBillReportOCEMS();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = ds.Tables[0];
                    }
                    if (!string.IsNullOrEmpty(mpa))
                    {
                        ViewBag.Data = Crypto.Decrypt(mpa);
                    }
                    else
                    {
                        ViewBag.Data = "/AdminReport/InvoiceDetails";

                    }
                }
                else
                {
                    return Redirect("/AdminReport /InvoiceDetails");
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
                return Redirect("/AdminReport /InvoiceDetails");

            }
            return View(model);
        }


        //[TypeFilter(typeof(MenuAuthorizationFilter))]

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
                model.UserId = HttpContext.Session.GetString("Pk_UserId");
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                ds = model.GetTreatedWater();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
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

       // [TypeFilter(typeof(MenuAuthorizationFilter))]

     
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

                ddlFirm.Add(new SelectListItem { Text = "--All--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--All--", Value = "0" });
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
                model.UserId = HttpContext.Session.GetString("Pk_UserId");
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                ds = model.Getutilization();
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
       // [TypeFilter(typeof(MenuAuthorizationFilter))]
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
                obj.UserId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                DataSet ds = obj.GetComplaintsRecevied();
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
        //[TypeFilter(typeof(MenuAuthorizationFilter))]
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
                obj.UserId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                DataSet ds = obj.GetComplaintsResolved();
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

       // [TypeFilter(typeof(MenuAuthorizationFilter))]

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

        public ActionResult OCMSSheet(OCMSSheet obj ,string Id,string Zone,string Firm)
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

                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/mm/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/mm/yyyy");
                obj.UserId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                if(!string.IsNullOrEmpty(Id))
                {
                    obj.Fk_STPId = string.IsNullOrEmpty(Id) ? obj.Fk_STPId : Crypto.Decrypt(Id);
                    TempData["stp"] = obj.Fk_STPId;
                }
                if(!string.IsNullOrEmpty(Firm))
                {
                    obj.Fk_FirmId = string.IsNullOrEmpty(Firm) ? obj.Fk_FirmId : Crypto.Decrypt(Firm);
                    TempData["firm"] = obj.Fk_FirmId;
                }
                if(!string.IsNullOrEmpty(Zone))
                {
                    obj.Pk_ZoneId = string.IsNullOrEmpty(Zone) ? obj.Pk_ZoneId : Crypto.Decrypt(Zone);
                    TempData["Zone"] = obj.Pk_ZoneId;
                }
                DataSet ds = obj.GetOCMSSheet();
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

        public ActionResult OCMS_Sheet(OCMSSheet obj)
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

                obj.UserId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                
                    DataSet ds = obj.StpLIstAdmin();
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


        [ServiceFilter(typeof(MenuAuthorizationFilter))]
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

                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                model.IsBilled = "0";

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

    }
}