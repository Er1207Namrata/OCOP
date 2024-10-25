using DTOs.BillFlowMapping;
using DTOs.DashboardDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Newtonsoft.Json;
using OCOO.Filter;
using OCOO.Models;
using OCOO.Models.AdminMasters;
using Services;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using OCOO.Models.FirmMasters;

namespace OCOO.Controllers
{
    //[ServiceFilter(typeof(MenuAuthorizationFilter))]
    public class AdminController : AdminBaseController
    {
        private readonly DashbaordService _dashbaordService;
        IHttpContextAccessor _httpContextAccessor;
        private readonly int UserId = 0;
        public AdminController(DashbaordService dashbaordService, IHttpContextAccessor acc)
        {
            _dashbaordService = dashbaordService;
            this._httpContextAccessor = acc;
            UserId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Session.GetString("Pk_UserId"));

        }
        public IActionResult Index()
        {
            return View();
        }
        //[AllowAnonymous]
        public ActionResult Dashboard(Dashboard dashboard)
        {
            try
            {
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
                dashboard.OpCode = 16;
                DataSet dsMonth = dashboard.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion

                if (!string.IsNullOrEmpty(dashboard.FromDate))
                {
                    dashboard.FromDate = Common.ConvertToSystemDate(dashboard.FromDate, "dd/mm/yyyy");
                }
                if (!string.IsNullOrEmpty(dashboard.ToDate))
                {
                    dashboard.ToDate = Common.ConvertToSystemDate(dashboard.ToDate, "dd/mm/yyyy");
                }
                //if (string.IsNullOrEmpty(dashboard.Fk_MonthId))
                //{
                //    dashboard.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                dashboard.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = dashboard.GetAdminDashboard();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dashboard.dtDetails = ds.Tables[0];
                    dashboard.dtDetails2 = ds.Tables[2];
                }
                TempData["Fk_ZoneId"] = dashboard.Fk_ZoneId;
                TempData["Fk_FirmId"] = dashboard.Fk_FirmId;
                TempData["Fk_STPId"] = dashboard.Fk_STPId;
                TempData["Fk_MonthId"] = dashboard.Fk_MonthId;
                TempData["FromDate"] = dashboard.FromDate;
                TempData["ToDate"] = dashboard.ToDate;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(dashboard);
        }

        #region Admin dashboard
        public ActionResult Dashboard_v1()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetDashboardChartDataAsync([FromBody] PerformanceDashboardFilterDTO _filter)
        {
            _filter.UserId = UserId;
            return Json(await _dashbaordService.GetDashboardChartDataAsync(_filter));
        }
        #endregion admin dashboard

        public ActionResult GetRequestedFirm(Dashboard dashboard, string Id)
        {
            DailyCapacity model = new DailyCapacity();
            try
            {
                model.Fk_BlockId = Id;
                model.OpCode = 2;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetSTC();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }

        //public ActionResult GetBlockDetails(Dashboard dashboard, string Id)
        //{
        //    dashboard.Value = Id;
        //    dashboard.OpCode = 5;
        //    DataSet ds = dashboard.GetMasterData();
        //    dashboard.dtDetails = ds.Tables[0];
        //    return View(dashboard);
        //}

        public ActionResult GetFirmByCity(string Id)
        {
            DailyCapacity model = new DailyCapacity();
            List<SelectListItem> ddlFirm = new List<SelectListItem>();
            try
            {
                model.OpCode = 8;
                model.Value = Id;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(ddlFirm);
        }

        //public ActionResult GetFirmRequest(DailyCapacity model, string Id)
        //{
        //    #region ddlCity
        //    List<SelectListItem> ddlCity = new List<SelectListItem>();
        //    model.OpCode = 1;
        //    model.Value = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
        //    DataSet dataSet1 = model.GetMasterData();
        //    if (dataSet1 != null && dataSet1.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dataSet1.Tables[0].Rows)
        //        {
        //            ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
        //        }
        //    }
        //    ViewBag.ddlCity = ddlCity;
        //    #endregion

        //    #region ddlFirm
        //    List<SelectListItem> ddlFirm = new List<SelectListItem>();
        //    model.OpCode = 8;
        //    model.Value = model.Pk_CityId;
        //    DataSet dataSet2 = model.GetMasterData();
        //    if (dataSet2 != null && dataSet2.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dataSet2.Tables[0].Rows)
        //        {
        //            ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
        //        }
        //    }
        //    ViewBag.ddlFirm = ddlFirm;
        //    #endregion

        //    #region ddlMonth
        //    List<SelectListItem> ddlMonth = new List<SelectListItem>();
        //    DataSet dataSet = new DataSet();
        //    model.OpCode = 6;
        //    model.Value = model.Fk_MonthId;
        //    dataSet = model.GetMasterData();
        //    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dataSet.Tables[0].Rows)
        //        {
        //            ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
        //        }
        //    }
        //    ViewBag.ddlMonth = ddlMonth;
        //    #endregion

        //    model.Pk_ZoneId = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
        //    model.OpCode = 3;
        //    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
        //    DataSet ds = model.GetFirmRequest();
        //    model.dtDetails = ds.Tables[0];
        //    TempData["Pk_ZoneId"] = model != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
        //    return View(model);
        //}

        public ActionResult GetCityByZoneId(String Id)
        {
            DataSet dataset = new DataSet();
            FirmDetails model = new FirmDetails();
            List<FirmDetails> list = new List<FirmDetails>();
            try
            {
                model.OpCode = 1;
                model.Value = Id;
                dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = dataset.Tables[0];
                }
                if (model.dtDetails != null)
                {
                    for (var i = 0; i < model.dtDetails.Rows.Count; i++)
                    {
                        FirmDetails ListData = new FirmDetails();
                        ListData.Pk_CityId = model.dtDetails.Rows[i]["Id"].ToString();
                        ListData.City = model.dtDetails.Rows[i]["Name"].ToString();
                        list.Add(ListData);
                    }
                    model.list = list;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(list);
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult ManageFirm(FirmDetails firm, string Id)
        {
            try
            {

                List<SelectListItem> ddlZone = new List<SelectListItem>();
                firm.OpCode = 4;
                firm.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = firm.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;


                //List<SelectListItem> ddlCity = new List<SelectListItem>();
                //firm.OpCode = 1;
                //DataSet dataset = firm.GetMasterData();
                //if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row in dataset.Tables[0].Rows)
                //    {
                //        ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                //    }
                //}
                //ViewBag.ddlCity = ddlCity;

                firm.OpCode = 4;
                firm.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                firm.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = new DataSet();
                dataSet = firm.GetFirmList();
                if (dataSet != null && dataSet.Tables[0] != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        firm.dtDetails = dataSet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(firm);
        }

        public ActionResult FirmDetails(string Id)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    model.Pk_FirmId = Id;
                    model.OpCode = 4;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                    DataSet ds = model.ManageFirm();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Pk_FirmId = ds.Tables[0].Rows[0]["Pk_FirmId"].ToString();
                        model.FirmName = ds.Tables[0].Rows[0]["FirmName"].ToString();
                        model.ContactPerson = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                        //model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                        //model.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                        model.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        // model.Fk_DistrictId = ds.Tables[0].Rows[0]["Fk_DistrictId"].ToString();
                        // model.Fk_DistrictId = ds.Tables[0].Rows[0]["Fk_DistrictId"].ToString();
                        //model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                        model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                        model.Pk_ZoneId = ds.Tables[0].Rows[0]["Pk_ZoneId"].ToString();
                        model.Fk_STPId = ds.Tables[0].Rows[0]["Fk_STPId"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(model);
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
                        model.OpCode = 2;
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("ManageFirm");
        }

        public ActionResult FirmMasterDelete(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    FirmDetails firm = new FirmDetails();
                    firm.Pk_FirmId = Crypto.Decrypt(id);
                    firm.OpCode = 3;
                    firm.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = firm.ManageFirm();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("ManageFirm");
        }

        public JsonResult GetDesignationByDepartment(string Id)
        {
            FirmDetails model = new FirmDetails();
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            try
            {
                model.OpCode = 20;
                model.Value = Id;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(ddlDesignation);
        }

        // [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult GetEmployeeList(FirmDetails firm, string btnSubmit)
        {
            try
            {
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                firm.OpCode = 4;
                DataSet dsZone = firm.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                List<SelectListItem> ddlDepartment = new List<SelectListItem>();
                firm.OpCode = 2;
                DataSet ds = firm.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDepartment = ddlDepartment;

                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                firm.OpCode = 34;
                DataSet ds1 = firm.GetMasterData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;

                List<SelectListItem> ddlCity = new List<SelectListItem>();
                firm.OpCode = 1;
                DataSet dataset = firm.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlCity = ddlCity;

                firm.OpCode = 4;
                firm.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = new DataSet();
                dataSet = firm.GetEmployee();
                if (dataSet != null && dataSet.Tables[0] != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        firm.dtDetails = dataSet.Tables[0];
                        //firm.dtDetails1 = dataSet.Tables[1];
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(firm);
        }
        // [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteEmployee(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    FirmDetails firm = new FirmDetails();
                    firm.Pk_EmployeeId = Crypto.Decrypt(id);
                    firm.OpCode = 3;
                    firm.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = firm.GetEmployee();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("GetEmployeeList");
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult EmployeeDetails(string Id)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    model.Pk_EmployeeId = Id;
                    model.OpCode = 5;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet ds = model.GetEmployee();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Pk_EmployeeId = ds.Tables[0].Rows[0]["Pk_EmployeeId"].ToString();
                        model.Fk_DesignationId = ds.Tables[0].Rows[0]["Fk_DesignationId"].ToString();
                        model.Fk_ZoneId = ds.Tables[0].Rows[0]["Fk_ZoneId"].ToString();
                        model.Fk_DepartmentId = ds.Tables[0].Rows[0]["Fk_DepartmentId"].ToString();
                        model.Fk_DistrictId = ds.Tables[0].Rows[0]["Fk_CityId"].ToString();
                        model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        model.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                        model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                        model.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                        model.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                        TempData["Fk_DesignationId"] = ds.Tables[0].Rows[0]["Fk_DesignationId"].ToString();
                        TempData["Fk_DepartmentId"] = ds.Tables[0].Rows[0]["Fk_DepartmentId"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(model);
        }

        //public ActionResult EmployeeRegistration(FirmDetails model, string btnSubmit)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(btnSubmit))
        //        {
        //            if (btnSubmit == "Submit")
        //            {
        //                model.OpCode = 1;
        //            }
        //            else
        //            {
        //                model.OpCode = 2;
        //            }
        //            if (model.Pk_EmployeeId != null)
        //            {
        //                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
        //                model.OpCode = 2;
        //                DataSet ds = model.GetEmployee();
        //                if (ds != null && ds.Tables[0].Rows.Count > 0)
        //                {
        //                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
        //                    {
        //                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    }
        //                    else
        //                    {
        //                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
        //                model.OpCode = 1;
        //                DataSet ds = model.GetEmployee();
        //                if (ds != null && ds.Tables[0].Rows.Count > 0)
        //                {
        //                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
        //                    {
        //                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    }
        //                    else
        //                    {
        //                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //        return View();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("GetFirm");
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("GetFirm");

        }

        public ActionResult GetFirmRequest(string Id)
        {
            DailyCapacity model = new DailyCapacity();
            try
            {
                model.UniqueId = Id;
                model.OpCode = 3;
                DataSet ds = model.GetFirmRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
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
                    model.UniqueId = Fk_UniqueId;
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetFirm");

            }
        }

        public ActionResult STPRequest(Dashboard dashboard)
        {
            try
            {
                dashboard.OpCode = 4;
                DataSet ds = dashboard.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dashboard.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(dashboard);
        }

        public ActionResult GetFirm(DailyCapacity model, string Id)
        {
            try
            {
                DataSet dataSet1 = new DataSet();
                #region ddlcity
                //List<SelectListItem> ddlCity = new List<SelectListItem>();
                //model.OpCode = 1;
                //model.Value = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
                //DataSet dataset = model.GetMasterData();
                //if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row in dataset.Tables[0].Rows)
                //    {
                //        ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                //    }
                //}
                //ViewBag.ddlCity = ddlCity;
                #endregion

                #region  ddlSTP Capacity            
                model.Value = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
                List<SelectListItem> ddlSTPCapacity = new List<SelectListItem>();
                model.OpCode = 11;
                dataSet1 = model.GetMasterData();
                if (dataSet1 != null && dataSet1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet1.Tables[0].Rows)
                    {
                        ddlSTPCapacity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlSTPCapacity = ddlSTPCapacity;
                #endregion

                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();
                model.OpCode = 8;
                // model.Value = model.Pk_CityId;
                model.Value = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
                DataSet dataSet2 = model.GetMasterData();
                if (dataSet2 != null && dataSet2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet2.Tables[0].Rows)
                    {
                        ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                DataSet dataSet = new DataSet();
                model.OpCode = 6;
                dataSet = model.GetMasterData();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion

                model.OpCode = 2;
                model.Pk_ZoneId = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetFirmRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                TempData["Pk_ZoneId"] = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult GetSTPName(string Id)
        {

            List<SelectListItem> ddlFirm = new List<SelectListItem>();
            FirmDetails model = new FirmDetails();
            try
            {
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(ddlFirm);
        }

        public ActionResult GetAdminDashboardData(string Fk_ZoneId, string Fk_FirmId, string Fk_STPId, string Fk_MonthId, string FromDate, string ToDate)
        {
            try
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Fk_ZoneId = Fk_ZoneId;
                dashboard.Fk_FirmId = Fk_FirmId;
                dashboard.Fk_STPId = Fk_STPId;
                dashboard.Fk_MonthId = Fk_MonthId;
                dashboard.FromDate = FromDate;
                dashboard.ToDate = ToDate;
                dashboard.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = dashboard.GetAdminDashboard();
                List<CompliantList> list = new List<CompliantList>();
                if (dataSet != null)
                {
                    if (dataSet.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                        {
                            CompliantList listdata = new CompliantList();
                            listdata.complaintsreceived = dataSet.Tables[1].Rows[i]["totalComplaintsReceived"].ToString();
                            listdata.complaintresolved = dataSet.Tables[1].Rows[i]["totalComplaintsResolved"].ToString();
                            listdata.pendingcomplaints = dataSet.Tables[1].Rows[i]["PendingComplaints"].ToString();
                            listdata.category = dataSet.Tables[1].Rows[i]["ReportingDate"].ToString();
                            list.Add(listdata);
                        }
                    }
                }
                dashboard.BarChartList = list;
                TempData["Fk_ZoneId"] = dashboard.Fk_ZoneId;
                TempData["Fk_FirmId"] = dashboard.Fk_FirmId;
                TempData["Fk_STPId"] = dashboard.Fk_STPId;
                TempData["Fk_MonthId"] = dashboard.Fk_MonthId;
                TempData["FromDate"] = dashboard.FromDate;
                TempData["ToDate"] = dashboard.ToDate;
                return Json(dashboard);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Dashboard");

            }
        }

        public ActionResult GetAdminPieChartdData(string Fk_ZoneId, string Fk_FirmId, string Fk_STPId, string Fk_MonthId, string FromDate, string ToDate)
        {
            try
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Fk_ZoneId = Fk_ZoneId;
                dashboard.Fk_FirmId = Fk_FirmId;
                dashboard.Fk_MonthId = Fk_MonthId;
                dashboard.FromDate = FromDate;
                dashboard.ToDate = ToDate;
                dashboard.Fk_STPId = Fk_STPId;
                dashboard.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = dashboard.GetAdminDashboard();
                List<CompliantList> list = new List<CompliantList>();
                if (dataSet != null)
                {
                    if (dataSet.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dataSet.Tables[2].Rows.Count - 1; i++)
                        {
                            CompliantList listdata = new CompliantList();
                            listdata.complaintsreceived = dataSet.Tables[2].Rows[i]["totalComplaintsReceived"].ToString();
                            listdata.complaintresolved = dataSet.Tables[2].Rows[i]["totalComplaintsResolved"].ToString();
                            listdata.pendingcomplaints = dataSet.Tables[2].Rows[i]["PendingComplaints"].ToString();
                            list.Add(listdata);
                        }
                    }
                }
                dashboard.BarChartList = list;
                TempData["Fk_ZoneId"] = dashboard.Fk_ZoneId;
                TempData["Fk_FirmId"] = dashboard.Fk_FirmId;
                TempData["Fk_STPId"] = dashboard.Fk_STPId;
                TempData["Fk_MonthId"] = dashboard.Fk_MonthId;
                TempData["FromDate"] = dashboard.FromDate;
                TempData["ToDate"] = dashboard.ToDate;
                return Json(dashboard);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Dashboard");
            }
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult FirmStpList(FirmDetails model, string Id)
        {
            DataSet dataset = new DataSet();
            try
            {
                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();
                model.OpCode = 8;
                // model.Value = model.Pk_CityId;
                //model.Value = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
                DataSet dataSet2 = model.GetMasterData();
                if (dataSet2 != null && dataSet2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet2.Tables[0].Rows)
                    {
                        ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlFirm = ddlFirm;
                #endregion
                #region ddlSTP
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                #endregion

                #region ddlCity


                List<SelectListItem> ddlCity = new List<SelectListItem>();
                model.OpCode = 1;
                dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlCity = ddlCity;
                #endregion ddlCity

                #region TreatmentTechnology
                List<SelectListItem> ddlTreatmentTechnology = new List<SelectListItem>();
                model.OpCode = 31;
                // model.Value = model.Pk_CityId;
                //model.Value = model.Pk_ZoneId != null ? model.Pk_ZoneId : Crypto.Decrypt(Id);
                DataSet dataSet3 = model.GetMasterData();
                if (dataSet2 != null && dataSet3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet3.Tables[0].Rows)
                    {
                        ddlTreatmentTechnology.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlTreatmentTechnology = ddlTreatmentTechnology;
                #endregion TreatmentTechnology
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                model.OpCode = 3;
                DataSet ds = model.GetFirmSTPList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View(model);
        }

        public JsonResult StpPumpStationList(string Id)
        {
            FirmDetails model = new FirmDetails();
            List<FirmDetails> dtList = new List<FirmDetails>();
            try
            {
                model.OpCode = 6;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Pk_STPId = int.Parse(Id);
                DataSet ds = model.StpRegistration();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FirmDetails list = new FirmDetails();
                        list.Pk_Id = ds.Tables[0].Rows[i]["Pk_Id"].ToString();
                        list.STPName = ds.Tables[0].Rows[i]["StpName"].ToString();
                        list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        list.PumpStationMeterLoad = ds.Tables[0].Rows[i]["MeterLoad"].ToString();
                        list.AccountNo = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                        list.BillingCycle = ds.Tables[0].Rows[i]["BillingCycle"].ToString();
                        list.SPSType = ds.Tables[0].Rows[i]["SPSType"].ToString();
                        list.SewerLength = ds.Tables[0].Rows[i]["SewerLength"].ToString();
                        list.DrainageName = ds.Tables[0].Rows[i]["DrainageName"].ToString();
                        dtList.Add(list);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(dtList);
        }

        public ActionResult DeleteFirmStp(string Id)
        {
            FirmDetails model = new FirmDetails();
            try
            {

                model.OpCode = 7;
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("FirmStpList");
        }

        public ActionResult DeleteStpMeterLoad(string Id)
        {
            FirmDetails model = new FirmDetails();
            try
            {
                model.OpCode = 8;
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("FirmStpList");
        }

        public ActionResult MoM()
        {
            int[] Designation = new int[] { 3, 1, 2, 4, 7 };
            int Fk_UsertypeId= Convert.ToInt32(HttpContext.Session.GetString("Fk_UsertypeId"));
            int DesignationId = Convert.ToInt32(HttpContext.Session.GetString("DesignationId"));
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
            ViewBag.ViewAllow = Designation.Contains(DesignationId) || Fk_UsertypeId == 1 ? "1" : "0";
            return View(mom);
        }

        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult ZoneContractMaster(string Id)
        {
            ZoneModel model = new ZoneModel();
            try
            {
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            ddlZone.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlZone = ddlZone;
                if (!string.IsNullOrEmpty(Id))
                {
                    model.OpCode = 4;
                    model.Pk_ContractId = Id;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataset = model.GetZoneContract();
                    if (dataset != null && dataset.Tables.Count > 0)
                    {
                        if (dataset.Tables[0].Rows.Count > 0)
                        {
                            model.Pk_ContractId = dataset.Tables[0].Rows[0]["Pk_ContractId"].ToString();
                            model.Fk_ZoneId = dataset.Tables[0].Rows[0]["Fk_ZoneId"].ToString();
                            model.ContractAmt = dataset.Tables[0].Rows[0]["ContractAmt"].ToString();
                            model.ContractYears = Convert.ToInt32(dataset.Tables[0].Rows[0]["ContractYear"].ToString());
                            model.ContractFrom = dataset.Tables[0].Rows[0]["ContractFrom"].ToString();
                            model.ContractTo = dataset.Tables[0].Rows[0]["ContractTo"].ToString();
                            model.MinLDCharge = dataset.Tables[0].Rows[0]["min_LD_Charges"].ToString();
                        }
                    }
                }
                else
                {
                    model.OpCode = 4;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataset = model.GetZoneContract();
                    if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = dataset.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ZoneContractMaster(ZoneModel model, string btnSubmit)
        {
            try
            {
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in dataset.Tables[0].Rows)
                        {
                            ddlZone.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlZone = ddlZone;
                if (btnSubmit == "Save")
                {
                    model.OpCode = 1;
                    model.ContractFrom = Common.ConvertToSystemDate(string.IsNullOrEmpty(model.ContractFrom) ? null : model.ContractFrom, "dd-MM-yyyy");
                    model.ContractTo = Common.ConvertToSystemDate(string.IsNullOrEmpty(model.ContractTo) ? null : model.ContractTo, "dd-MM-yyyy");
                    if (!string.IsNullOrEmpty(model.AdditionalContractYearDate))
                    {
                        model.AdditionalContractYearDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(model.AdditionalContractYearDate) ? null : model.AdditionalContractYearDate, "dd-MM-yyyy");
                    }
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet ds = model.GetZoneContract();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
                else
                {
                    model.OpCode = 2;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.ContractFrom = Common.ConvertToSystemDate(string.IsNullOrEmpty(model.ContractFrom) ? null : model.ContractFrom, "dd-MM-yyyy");
                    model.ContractTo = Common.ConvertToSystemDate(string.IsNullOrEmpty(model.ContractTo) ? null : model.ContractTo, "dd-MM-yyyy");
                    if (!string.IsNullOrEmpty(model.AdditionalContractYearDate))
                    {
                        model.AdditionalContractYearDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(model.AdditionalContractYearDate) ? null : model.AdditionalContractYearDate, "dd-MM-yyyy");
                    }
                    DataSet ds = model.GetZoneContract();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("ZoneContractMaster");
        }

        public JsonResult ContractDetails(string Id)
        {
            ZoneModel model = new ZoneModel();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    model.OpCode = 4;
                    model.Pk_ContractId = Id;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataset = model.GetZoneContract();
                    if (dataset != null && dataset.Tables.Count > 0)
                    {
                        if (dataset.Tables[0].Rows.Count > 0)
                        {
                            model.Pk_ContractId = dataset.Tables[0].Rows[0]["Pk_ContractId"].ToString();
                            model.Fk_ZoneId = dataset.Tables[0].Rows[0]["Fk_ZoneId"].ToString();
                            model.ContractAmt = dataset.Tables[0].Rows[0]["ContractAmt"].ToString();
                            model.ContractYears = Convert.ToInt32(dataset.Tables[0].Rows[0]["ContractYear"].ToString());
                            model.ContractFrom = dataset.Tables[0].Rows[0]["ContractFrom"].ToString();
                            model.ContractTo = dataset.Tables[0].Rows[0]["ContractTo"].ToString();
                            model.MinLDCharge = dataset.Tables[0].Rows[0]["min_LD_Charges"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(model);
        }

        public ActionResult DeleteContractMaster(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    ZoneModel model = new ZoneModel();
                    model.OpCode = 3;
                    model.Pk_ContractId = Id;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet ds = model.GetZoneContract();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("ZoneContractMaster");
        }

        public ActionResult StpRegistration(string Id)
        {

            FirmDetails model = new FirmDetails();
            List<SelectListItem> PumpingStationType = new List<SelectListItem>();
            try
            {

                model.OpCode = 12;
                DataSet dataset = model.GetMasterData();
                if (dataset != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        PumpingStationType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.PumpingStationType = PumpingStationType;

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
                model.Pk_STPId = int.Parse(Crypto.Decrypt(Id));
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 3;
                //model.Pk_STPId = Id;
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
                        model.TreatmentTechnology = ds.Tables[0].Rows[0]["TreatmentTechnology"].ToString();
                        // model.FK_CityId = ds.Tables[0].Rows[0]["FK_CityId"].ToString();
                        model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        model.IsFC = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFC"]);
                        model.IsCOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCOD"]);
                        model.IsBOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBOD"]);
                        model.IsTSS = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTSS"]);
                        model.IsInhouse = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsInhouse"]);
                        model.IsUPPCB = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsUPPCB"]);
                        model.IsTPI = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTPI"]);

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult StpRegistration(FirmDetails model, string btnSubmit, string btnUpdate)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    // model.OpCode = model.Pk_STPId>0? 7:1;
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
                if (!string.IsNullOrEmpty(btnUpdate))
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
                    DataSet ds = model.updateStpRegistration();
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
        public ActionResult DailyBillingReport(string Fk_FirmId, string Fk_MonthId, string Pk_STPId, string FromDate, string ToDate, string Pk_ZoneId)
        {
            FirmDetails model = new FirmDetails();
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
                if (FromDate != null && ToDate != null)
                {
                    model.FromDate = Common.ConvertToSystemDate(FromDate, "dd/mm/yyyy");
                    model.ToDate = Common.ConvertToSystemDate(ToDate, "dd/mm/yyyy");

                }

                //if (string.IsNullOrEmpty(model.Fk_MonthId))
                //{
                //    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                model.Fk_STPId = Pk_STPId;
                model.Fk_FirmId = Fk_FirmId;
                model.Fk_MonthId = Fk_MonthId;
                model.Pk_ZoneId = Pk_ZoneId;
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.DailyBillingReportByEmp();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    model.dtDetails1 = ds.Tables[1];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
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

        public JsonResult GetZone(string Id)
        {
            FirmDetails model = new FirmDetails();
            List<SelectListItem> ddlZone = new List<SelectListItem>();
            try
            {


                model.OpCode = 4;
                DataSet dsZone = model.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(ddlZone);
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult PumpingStationReport(PumpingStationReport model)
        {
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

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlFirm = ddlFirm;
                #endregion

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                ddlSTP.Add(new SelectListItem { Text = "--select--", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                DataSet ds = model.GetPumpingStationReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult EmployeeRegistration(FirmDetails model, string btnSubmit, string Id)
        {
            
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
                #endregion ddlZone
                #region ddlDepartment
                List<SelectListItem> ddlDepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDepartment = ddlDepartment;
                #endregion ddlDepartment
                #region ddlDesignation


                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 3;
                DataSet datasetDesignation = model.GetMasterData();
                if (datasetDesignation != null && datasetDesignation.Tables.Count > 0 && datasetDesignation.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in datasetDesignation.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation
                #region ddlCity


                List<SelectListItem> ddlCity = new List<SelectListItem>();
                model.OpCode = 1;
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
                    if (btnSubmit == "Submit")
                    {
                        model.OpCode = 1;
                    }
                    else
                    {
                        model.OpCode = 2;
                    }

                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");

                  model.Fk_ZoneId = HttpContext.Session.GetString("Fk_UsertypeId") !="1"? HttpContext.Session.GetString("ZoneID"): model.Fk_ZoneId;


                    DataSet ds2 = model.GetEmployee();

                    if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    {
                        if (ds2.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (Convert.ToInt32(Request.Form["Count"]) > 0) {
                                for (int i = 0; i <= Convert.ToInt32(Request.Form["Count"]) - 1; i++)
                                {

                                    model.Fk_ZoneId = Request.Form["Pk_Id_" + i].ToString();
                                    model.Pk_EmployeeId = ds2.Tables[0].Rows[0]["Pk_EmployeeId"].ToString();
                                    model.Pk_Id = Request.Form["cHECKBOX_" + i].ToString();

                                    if (!string.IsNullOrEmpty(model.Pk_Id) && model.Pk_Id == "on")
                                    {
                                        model.OpCode = 6;
                                    }
                                    else
                                    {
                                        model.OpCode = 7;
                                    }
                                    DataSet ds1 = model.GetEmployee();
                                }
                            }
                            else
                            {
                                //model.Fk_ZoneId = HttpContext.Session.GetString("ZoneID");
                                model.Pk_EmployeeId = ds2.Tables[0].Rows[0]["Pk_EmployeeId"].ToString();
                                model.Pk_Id ="on";
                                if (!string.IsNullOrEmpty(model.Pk_Id) && model.Pk_Id == "on")
                                    model.OpCode = 6;
                                else
                                    model.OpCode = 7;

                                DataSet ds1 = model.GetEmployee();
                                TempData["Message"] = ds2.Tables[0].Rows[0]["Message"].ToString();
                                return RedirectToAction("GetEmployeeList");
                            }

                            TempData["Message"] = ds2.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("GetEmployeeList");



                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds2.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }

                }
                else if (!string.IsNullOrEmpty(Id))
                {
                    model.OpCode = 5;
                    model.Pk_EmployeeId = Crypto.Decrypt(Id);
                    DataSet ds1 = model.GetEmployee();


                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        model.Pk_CityId = ds1.Tables[0].Rows[0]["Fk_CityId"].ToString();
                        model.Fk_DesignationId = ds1.Tables[0].Rows[0]["Fk_DesignationId"].ToString();
                        model.FirstName = ds1.Tables[0].Rows[0]["FirstName"].ToString();
                        model.MiddleName = ds1.Tables[0].Rows[0]["MiddleName"].ToString();
                        model.LastName = ds1.Tables[0].Rows[0]["LastName"].ToString();
                        model.Office = ds1.Tables[0].Rows[0]["Office"].ToString();
                        model.Password = ds1.Tables[0].Rows[0]["Password"].ToString();
                        model.MobileNo = ds1.Tables[0].Rows[0]["MobileNo"].ToString();
                        model.EmailId = ds1.Tables[0].Rows[0]["EmailId"].ToString();
                        model.Address = ds1.Tables[0].Rows[0]["Address"].ToString();
                        model.Pk_EmployeeId = ds1.Tables[0].Rows[0]["Pk_EmployeeId"].ToString();
                        model.Fk_DepartmentId = ds1.Tables[0].Rows[0]["Fk_DepartmentId"].ToString();
                    }
                }

                return View(model);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }
        public JsonResult GetZoneData(ZoneData model, string Id)
        {

            List<ZoneData> ddlDesignation = new List<ZoneData>();
            try
            {
                
                model.Value = Id;
                DataSet ds = model.GetZoneData();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ZoneData zoneData = new ZoneData();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        ddlDesignation.Add(new ZoneData { textZone = row["Name"].ToString(), valueZone = row["Id"].ToString(), SelectedValue = row["IsZoneApplied"].ToString() });
                    }
                }
                else {
                    model.Value = null;
                    DataSet data = model.GetZoneData();
                    if (data != null && data.Tables[0].Rows.Count > 0)
                    {
                        ZoneData zoneData = new ZoneData();
                        foreach (DataRow row in data.Tables[0].Rows)
                        {

                            ddlDesignation.Add(new ZoneData { textZone = row["Name"].ToString(), valueZone = row["Id"].ToString(), SelectedValue = row["IsZoneApplied"].ToString() });
                        }
                    }
                }
                
                
            }
            catch (Exception)
            {
                throw;
            }
            return Json(ddlDesignation);
        }
        [HttpPost]
        public ActionResult AddSTPBilling(FirmDetails model, string btnSubmit)
        {
            try
            {

                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 2;
                DataSet ds = model.GetSTPBilling();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("DailyBillingReport");
        }
        public JsonResult CheckEmailId(CheckDetails model, string Id)
        {
            try
            {
                model.Email = Id;
                DataSet ds = model.GetEmail();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    model.Flag = ds.Tables[0].Rows[0]["flag"].ToString();
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }
        [HttpPost]
        public async Task<ActionResult> MoM(MoM mom, string btnSubmit)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    mom.OpCode = 1;
                    mom.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    mom.MeetingDate = Common.ConvertToSystemDate(mom.MeetingDate, "dd/mm/yyyy");
                    if (mom.MoMDoc != null)
                    {
                        mom.DocUrl = await FileManagement.WriteFiles(mom.MoMDoc, "pdf", "MOM");
                    }
                    DataSet ds = mom.saveMom();
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("MoM");
        }
        public ActionResult DeleteMoM(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    MoM mom = new MoM();
                    mom.OpCode = 2;
                    mom.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    mom.Pk_MomId = Crypto.Decrypt(id);
                    DataSet ds = mom.saveMom();
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("MoM");
        }
        public ActionResult UserManual()
        {
            return View();

        }
        public ActionResult StpPumpStationListAdmin(string Id)
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

        public IActionResult AdminStpGraph(StpTree stpTree, string klm)
        {
            if (!string.IsNullOrEmpty(klm))
            {
                stpTree.Fk_STPId = Crypto.Decrypt(klm);
            }
            return View(stpTree);
        }
        public ActionResult AdminGetStpTree(StpTree stpTree)
        {
            DataSet dataSet = stpTree.GetStpTree();
            List<ParentStpTree> lstParentStpTree = new List<ParentStpTree>();
            List<ChildStpTree> lstChildStpTree = new List<ChildStpTree>();
            List<Drain> lstDrain = new List<Drain>();
            List<SeverLength> lstSeverLength = new List<SeverLength>();


            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                int i = 0;

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    i = i + 1;
                    string drains = null;
                    string[] drainsData = new string[0];
                    string Sewer = null;
                    string[] SewerData = new string[0];
                    ChildStpTree childStpTree = new ChildStpTree();
                    childStpTree.Name = row["Name"].ToString();
                    childStpTree.ParentName = row["ParentName"].ToString();
                    childStpTree.Fk_ParentId = row["ParentId"].ToString();
                    childStpTree.Pk_Id = row["Pkmainpump_Id"].ToString();

                    // Find children for the current child

                    drains = row["FlowFromDrain"].ToString();
                    drainsData = drains.Split(',');
                    Sewer = row["FlowFromSewer"].ToString();
                    SewerData = Sewer.Split(',');
                    if (row["FlowFromDrain"].ToString() != "0" && !string.IsNullOrEmpty(row["FlowFromDrain"].ToString()))
                    {
                        foreach (var drainitem in drainsData)
                        {
                            if (!string.IsNullOrEmpty(drainitem))
                            {
                                Drain drain = new Drain();
                                drain.Name = drainitem;
                                drain.dName = drainitem + i.ToString();
                                drain.ParentName = row["Name"].ToString();
                                drain.Fk_ParentId = row["ParentId"].ToString();

                                lstDrain.Add(drain);
                            }

                        }
                    }
                    if (row["FlowFromSewer"].ToString() != "0" && !string.IsNullOrEmpty(row["FlowFromSewer"].ToString()))
                    {

                        foreach (var sewerItem in SewerData)
                        {
                            SeverLength severLength = new SeverLength();
                            if (!string.IsNullOrEmpty(sewerItem))
                            {
                                severLength.Name = sewerItem;
                                severLength.sName = sewerItem + i.ToString();
                                severLength.Fk_ParentId = row["ParentId"].ToString();
                                severLength.ParentName = row["Name"].ToString();
                                lstSeverLength.Add(severLength);
                            }
                        }

                    }
                    lstChildStpTree.Add(childStpTree);
                }
                stpTree.lstChild = lstChildStpTree;
                stpTree.lstDrain = lstDrain;
                stpTree.lstSeverLength = lstSeverLength;
            }

            return Json(stpTree);
        }

        public ActionResult TreatedWaterVolume(TreatedWater model)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = model.GetTreatedWater();
                model.dtDetails = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }

            return View(model);
        }

        public ActionResult Payment(Payment obj)
        {
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                obj.OpCode = 4;
                obj.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = obj.GetMasterData();
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

                #region ddlMonth
                List<SelectListItem> ddlMonth = new List<SelectListItem>();
                obj.OpCode = 16;
                DataSet dsMonth = obj.GetMasterData();
                if (dsMonth != null && dsMonth.Tables.Count > 0 && dsMonth.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsMonth.Tables[0].Rows)
                    {
                        ddlMonth.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlMonth = ddlMonth;
                #endregion
                TempData["firm"] = obj.Fk_FirmId;
                TempData["stp"] = obj.Fk_STPId;
                obj.OpCode = 1;
                DataSet ds = obj.GetPayment();
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

        public async Task<ActionResult> ProceedPayment(Payment obj, string abc, string Submit)
        {
            try
            {
                #region Payment Mode
                List<SelectListItem> lstPaymentMode = new List<SelectListItem>();
                obj.OpCode = 33;
                obj.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dsZone = obj.GetMasterData();
                if (dsZone != null && dsZone.Tables.Count > 0 && dsZone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsZone.Tables[0].Rows)
                    {
                        lstPaymentMode.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.PaymentMode = lstPaymentMode;
                #endregion


                if (!string.IsNullOrEmpty(Submit))
                {
                    if (Submit == "Submit")
                    {
                        if (obj.AttachmentURl != null)
                        {
                            obj.AttachmentDoc = await FileManagement.WriteFiles(obj.AttachmentURl, "", "ProceedAttachment");
                        }
                        obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        obj.DrAmount = "0";
                        obj.PaymentDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.PaymentDate) ? null : obj.PaymentDate, "dd-MM-yyyy");
                        DataSet ds = obj.ProceedPaymentAdd();
                        if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("ProceedPayment");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
                else
                {
                    obj.PK_FbillId = Crypto.Decrypt(abc);
                    obj.OpCode = 2;
                    DataSet ds = obj.GetPayment();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Zone = ds.Tables[0].Rows[0]["ZoneName"].ToString();
                        obj.Firm = ds.Tables[0].Rows[0]["FirmName"].ToString();
                        obj.Date = ds.Tables[0].Rows[0]["billDate"].ToString();
                        obj.Month = ds.Tables[0].Rows[0]["month_Name"].ToString();
                        obj.Years = ds.Tables[0].Rows[0]["Bill_Year"].ToString();
                        obj.Bill_Amount = ds.Tables[0].Rows[0]["BillAmount"].ToString();
                        obj.Payable_Amount = ds.Tables[0].Rows[0]["BillAmount"].ToString();
                        obj.PK_FbillId = ds.Tables[0].Rows[0]["PK_FbillId"].ToString();
                        obj.WithheldAmount = ds.Tables[0].Rows[0]["WithheldAmount"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(obj);
        }

        public JsonResult GetEmpDDl(string Id)
        {
            DataSet DS = new DataSet();
            SetPermission obj = new SetPermission();
            List<SelectListItem> ddlEmployee = new List<SelectListItem>();
            DataSet ds = new DataSet();
            obj.OpCode = 32;
            obj.Value = Id;
            ds = obj.GetMasterData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ddlEmployee.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                }
            }
            return Json(ddlEmployee);

        }

        public JsonResult GetMenuDDl(string Id)
        {
            DataSet ds = new DataSet();
            SetPermission obj = new SetPermission();
            List<SelectListItem> ddlMenu = new List<SelectListItem>();
            obj.OpCode = 18;
            ds = obj.GetMasterData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ddlMenu.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                }
            }
            // ViewBag.ddlMenu = ddlMenu;
            return Json(ddlMenu);
        }

        public ActionResult STPList(string Type)
        {

            try
            {
<<<<<<< HEAD
                FirmDetails model = new FirmDetails();
                model.OpCode = 3;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
=======
                int userTypeId = Convert.ToInt32(HttpContext.Session.GetString("Fk_UsertypeId"));
                FirmDetails model = new FirmDetails();
                model.OpCode = 3;
                model.Pk_FirmId = userTypeId == 1 ? null : HttpContext.Session.GetString("Pk_UserId");
>>>>>>> OCOP-UAT
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpLIst(Crypto.Decrypt(Type));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (Crypto.Decrypt(Type) == "ElectricityBill")
                {
                    model.Url = "/Admin/GetelectricityBillList";
                }
                else if (Crypto.Decrypt(Type) == "DGBilling")
                {
                    model.Url = "/Admin/GetDeductionList";
                }

                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }

            return View();
        }
        public ActionResult GetelectricityBillList(Deduction model, string id)
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
                #region ddlips
                List<SelectListItem> ddlIPS = new List<SelectListItem>();
                model.OpCode = 23;
                model.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = model.GetMasterData();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            ddlIPS.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlIPS = ddlIPS;
                #endregion
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 4;
                model.Fk_STPId = string.IsNullOrEmpty(id) ? model.Pk_STPId : Crypto.Decrypt(id);
                DataSet ds = model.GetElectricity();
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

        public ActionResult GetDeductionList(Deduction model, string id)
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
                #region ddlips
                List<SelectListItem> ddlIPS = new List<SelectListItem>();
                model.OpCode = 23;
                model.Value = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = model.GetMasterData();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            ddlIPS.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlIPS = ddlIPS;
                #endregion
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.Pk_STPId = string.IsNullOrEmpty(id) ? model.Pk_STPId : Crypto.Decrypt(id);
                DataSet ds = model.GetDeduction();
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
<<<<<<< HEAD

=======
>>>>>>> OCOP-UAT
    }
}


