using DTOs.BillFlowMapping;
using DTOs.DashboardDTO;
using DTOs.Master;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Filter;
using OCOO.Models;
using Services;
using System.Data;
using System.Security.Cryptography;

namespace OCOO.Controllers
{
    public class MasterController : AdminBaseController
    {
        private readonly MasterServices _masterServices;
        IHttpContextAccessor _httpContextAccessor;
        private readonly int UserId = 0;
        public MasterController(MasterServices masterServices, IHttpContextAccessor acc)
        {
            _masterServices = masterServices;
            this._httpContextAccessor = acc;
            UserId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Session.GetString("Pk_UserId"));
            if (UserId == 0)
                RedirectToAction("Login", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Dropdown masters
        [HttpPost]
        public async Task<JsonResult> GetEmployeeDropdownAsync([FromBody] FilterDTO _filter)
        {
            _filter.EmployeeId = UserId;
            return Json(await _masterServices.GetEmployeeDropdownAsync(_filter));
        }
        [HttpGet]
        public async Task<JsonResult> GetDepartmentDropdownAsync() => Json(await _masterServices.GetDepartmentDropdownAsync());
        [HttpGet]
        [Route("{Controller}/{Action}/{DepartmentId}")]
        public async Task<JsonResult> GetDesignationDropdownAsync(int DepartmentId) => Json(await _masterServices.GetDesignationDropdownAsync(DepartmentId));

        [HttpGet]
        public async Task<JsonResult> GetZoneDropdownByEmployeeIdAsync() => Json(await _masterServices.GetZoneDropdownByEmployeeIdAsync(UserId));

        [HttpPost]
        public async Task<JsonResult> GetSTPAsync([FromBody] DashboardFilterDTO _filter) => Json(await _masterServices.GetSTPByZoneAsync(_filter));

        [HttpPost]
        public async Task<JsonResult> GetInspectionAgencyAsync([FromBody] DashboardFilterDTO _filter) => Json(await _masterServices.GetInspectionAgencyAsync(_filter));

        [HttpGet]
        [Route("{Controller}/{Action}/{billId}")]
        public async Task<JsonResult> GetMarkedAndForwardedEmployeeDropdownAsync(FilterDTO _filter, int billId) => Json(await _masterServices.GetMarkedAndForwardedEmployeeDropdownAsync(_filter, UserId, billId));

        [HttpGet]
        public async Task<JsonResult> GetReforwardEmployeeDropdownAsync(int id) => Json(await _masterServices.GetReforwardEmployees(id));

        #endregion

        #region Bill flow mapping master
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public IActionResult BillFlowMapping()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveBillFlowMappingAsync([FromBody] BillFlowMappingDTO _BillFlowMapping)
        {
            _BillFlowMapping.Id = UserId;
            return Json(await _masterServices.SaveBillFlowMapping(_BillFlowMapping));
        }
        [HttpGet]
        public async Task<JsonResult> GetBillFlowMappingAsync(int id) => Json(await _masterServices.GetBillFlowMappingAsync(new BillFlowMappingDTO { Id = UserId, ZoneId = id }));


        #endregion
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        [HttpGet]
        public IActionResult STPWiseMapping()
        {
            try
            {
                ViewBag.Fk_EmpId = 0;
                STPWiseMapping model = new STPWiseMapping();
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                model.Value = UserId.ToString();
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds1 = model.GetMasterData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlOfficer
                List<SelectListItem> ddlOfficer = new List<SelectListItem>();
                model.OpCode = 28;
                DataSet ds2 = model.GetMasterData();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        ddlOfficer.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlOfficer = ddlOfficer;
                #endregion ddlOfficer
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public IActionResult STPWiseMapping(STPWiseMapping model, string GetDetails, string Save)
        {
            try
            {
                ViewBag.Fk_EmpId = model.Fk_EmpId;
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 4;
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds1 = model.GetMasterData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlOfficer
                List<SelectListItem> ddlOfficer = new List<SelectListItem>();
                model.OpCode = 28;
                DataSet ds2 = model.GetMasterData();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        ddlOfficer.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlOfficer = ddlOfficer;
                #endregion ddlOfficer
                if (!string.IsNullOrEmpty(GetDetails))
                {
                    DataSet data = model.GetZonewiseSTP();
                    if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = data.Tables[0];
                    }
                }
                if (!string.IsNullOrEmpty(Save))
                {

                    string Fk_STPId = "";
                    string hdrows = Request.Form["hdRows"];
                    DataTable dttable = new DataTable();

                    dttable.Columns.Add("Fk_STPId");
                    for (int i = 0; i < int.Parse(hdrows); i++)
                    {
                        if (Request.Form["chkSelect_" + i].ToString() == "on")
                        {

                            Fk_STPId = Request.Form["Fk_STPId_" + i].ToString();
                            dttable.Rows.Add(Fk_STPId);
                        }
                    }
                    model.dttable = dttable;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet data = model.SaveZonewiseSTP();
                    if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        if (data.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = data.Tables[0].Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            TempData["ErrorMessage"] = data.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(model);
        }
        public JsonResult GetOfficerDDl(string Id, string Fk_ZoneId)
        {

            #region OfficerDDl
            STPWiseMapping dashboard = new STPWiseMapping();
            List<SelectListItem> OfficerDDl = new List<SelectListItem>();
            try
            {

                dashboard.Fk_DepartmentId = Id;
                dashboard.Pk_ZoneId = Fk_ZoneId;
                DataSet ds = dashboard.GetEmployee();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OfficerDDl.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            #endregion
            return Json(OfficerDDl);
        }
        [HttpPost]
        public async Task<JsonResult> GetPerformanceDashboard([FromBody] PerformanceDashboardFilterDTO _objFilter, List<int> ZoneIds) => Json(await _masterServices.GetPerformanceDashboard(_objFilter, UserId));

        [HttpGet]
        public IActionResult DivisionMapping(DivisionMapping obj, string Emp_id, string Zone_id, string Department_id, string Designation_id)
        {
            try
            {
                ViewBag.ZoneBySession = HttpContext.Session.GetString("ZoneId");

                STPWiseMapping model = new STPWiseMapping();
                List<DivisionMapping> lst = new List<DivisionMapping>();
                if (Zone_id != null)
                {
                    string ZoneId;
                    byte[] Zo = System.Convert.FromBase64String(Zone_id);
                    ZoneId = System.Text.ASCIIEncoding.ASCII.GetString(Zo);
                    obj.Pk_ZoneId = ZoneId;
                }
                if (Emp_id != null)
                {
                    string Emp;
                    byte[] EmpId = System.Convert.FromBase64String(Emp_id);
                    Emp = System.Text.ASCIIEncoding.ASCII.GetString(EmpId);
                    obj.Fk_EmpId = Emp;
                }
                //Dep
                if (Department_id != null)
                {
                    string Dep;
                    byte[] datadep = System.Convert.FromBase64String(Department_id);
                    Dep = System.Text.ASCIIEncoding.ASCII.GetString(datadep);
                    obj.DepartmentId = Dep;
                }
                //Desi
                if (Designation_id != null)
                {
                    string Desi;
                    byte[] datadesig = System.Convert.FromBase64String(Designation_id);
                    Desi = System.Text.ASCIIEncoding.ASCII.GetString(datadesig);
                    obj.DesignationId = Desi;
                }

                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 35;
                model.Value = UserId.ToString();
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds6 = model.GetMasterData();
                if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds6.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 34;
                DataSet ds8 = model.GetMasterData();
                if (ds8 != null && ds8.Tables.Count > 0 && ds8.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds8.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation
                #region ddlOfficer
                List<SelectListItem> ddlOfficer = new List<SelectListItem>();
                model.OpCode = 38;
                DataSet ds2 = model.GetMasterData();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        ddlOfficer.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlOfficer = ddlOfficer;
                #endregion ddlOfficer

                if (!string.IsNullOrEmpty(obj.Fk_EmpId))
                {
                    DataSet ds3 = obj.GetDivisionMapping();
                    if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds3.Tables[0].Rows)
                        {
                            DivisionMapping division = new DivisionMapping()
                            {
                                Pk_DivisionId = dr["Pk_DivisionId"].ToString(),
                                Fk_divisionId = dr["Fk_DivisionId"].ToString(),
                                Fk_EmpId = dr["Fk_EmployeeId"].ToString(),
                                DivisionName = dr["DivisionName"].ToString(),
                                Pk_DivMapId = dr["Pk_divMapId"].ToString(),
                                IsChecked = dr["ischecked"].ToString()
                            };
                            lst.Add(division);
                        }
                    }
                    obj.lstData = lst;
                    ViewBag.EmployeeId = obj.Fk_EmpId;
                    ViewBag.Pk_ZoneId = obj.Pk_ZoneId;
                }
                else
                {

                    return View();
                }
                return View(obj);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        public IActionResult DivisionMapping(DivisionMapping obj, string Save, string Emp_id, string Zone_id)
        {
            try
            {
                STPWiseMapping model = new STPWiseMapping();
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 35;
                model.Value = UserId.ToString();
                DataSet ds = model.getEmployeeZone(UserId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds6 = model.GetMasterData();
                if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds6.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 34;
                DataSet ds8 = model.GetMasterData();
                if (ds8 != null && ds8.Tables.Count > 0 && ds8.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds8.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation
                #region ddlOfficer
                List<SelectListItem> ddlOfficer = new List<SelectListItem>();
                model.OpCode = 38;
                DataSet ds2 = model.GetMasterData();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        ddlOfficer.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlOfficer = ddlOfficer;
                #endregion ddlOfficer


                if (!string.IsNullOrEmpty(Save))
                {
                    if (Save == "Save")
                    {
                        obj.OpCode = 1;
                    }
                    else
                    {
                        obj.OpCode = 2;
                    }

                    string hdRows = Request.Form["RowCount"];
                    string pk_DivisionId = "";
                    string divisionName = "";
                    string IsChecked = "";

                    DataTable DivMap = new DataTable();

                    DivMap.Columns.Add("Pk_DivisionId");
                    DivMap.Columns.Add("DivisionName");
                    DivMap.Columns.Add("IsChecked");
                    for (int i = 0; i < int.Parse(hdRows); i++)
                    {
                        if (Request.Form["DivisionName_" + i].ToString() == "on")
                        {
                            IsChecked = "1";
                        }
                        else
                        {
                            IsChecked = "0";
                        }
                        pk_DivisionId = Request.Form["Pk_DivisionId_" + i].ToString();
                        divisionName = Request.Form["DivisionName_" + i].ToString();
                        DivMap.Rows.Add(pk_DivisionId, divisionName, IsChecked);
                    }

                    obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    obj.DivisionDataTable = DivMap;
                    DataSet ds4 = obj.SaveMapping();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        if (ds4.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds4.Tables[0].Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds4.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }
       
        [HttpGet]
        public IActionResult DivisionBillFlowMapping(DivisionBillFlowMapping obj, string Zone_id, string Division_id, string Department_id, string Designation_id)
        {
            STPWiseMapping model = new STPWiseMapping();
            List<DivisionBillFlowMapping> lst = new List<DivisionBillFlowMapping>();
            try
            {
                ViewBag.ZoneBySession = HttpContext.Session.GetString("ZoneId");
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 35;
                model.Value = UserId.ToString();
                DataSet ds = model.getEmployeeZone(UserId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddlDivision
                List<SelectListItem> ddlDivision = new List<SelectListItem>();
                model.OpCode = 36;
                model.Value = UserId.ToString();
                DataSet ds5 = model.GetMasterData();
                if (ds != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds5.Tables[0].Rows)
                    {
                        ddlDivision.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDivision = ddlDivision;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds1 = model.GetMasterData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 34;
                DataSet ds3 = model.GetMasterData();
                if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds3.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation

                if (Zone_id != null)
                {
                    string ZoneId;
                    byte[] Zo = System.Convert.FromBase64String(Zone_id);
                    ZoneId = System.Text.ASCIIEncoding.ASCII.GetString(Zo);
                    obj.Pk_ZoneId = ZoneId;
                }
                if (Division_id != null)
                {
                    string Divi;
                    byte[] datadivi = System.Convert.FromBase64String(Division_id);
                    Divi = System.Text.ASCIIEncoding.ASCII.GetString(datadivi);
                    obj.Pk_DivisionId = Divi;
                }
                //Dep
                if (Department_id != null)
                {
                    string Dep;
                    byte[] datadep = System.Convert.FromBase64String(Department_id);
                    Dep = System.Text.ASCIIEncoding.ASCII.GetString(datadep);
                    obj.DepartmentId = Dep;
                }
                //Desi
                if (Designation_id != null)
                {
                    string Desi;
                    byte[] datadesig = System.Convert.FromBase64String(Designation_id);
                    Desi = System.Text.ASCIIEncoding.ASCII.GetString(datadesig);
                    obj.DesignationId = Desi;
                }
               


                DataSet DataSet = obj.ManageDivisionBillFlow();
                if (DataSet != null && DataSet.Tables.Count > 0 && DataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in DataSet.Tables[0].Rows)
                    {
                        DivisionBillFlowMapping division = new DivisionBillFlowMapping()
                        {
                            Pk_ZoneId = dr["Pk_ZoneId"].ToString(),
                            Pk_EmployeeId = dr["Pk_AdminId"].ToString(),
                            Pk_DivisionId = dr["Pk_DivisionId"].ToString(),
                            Pk_DivMapId = dr["Pk_divMapId"].ToString(),
                            EmpName = dr["Name"].ToString(),
                            DepartmentName = dr["DepartmentName"].ToString(),
                            OfficeName = dr["Office"].ToString(),
                            DesignationName = dr["DesignationName"].ToString(),
                            IsForword = dr["IsForward"].ToString()
                        };
                        lst.Add(division);
                    }
                }
                obj.lstData = lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult DivisionBillFlowMapping(DivisionBillFlowMapping obj, string Save)
        {
            STPWiseMapping model = new STPWiseMapping();
            try
            {
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 35;
                model.Value = UserId.ToString();
                DataSet ds = model.getEmployeeZone(UserId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddlDivision
                List<SelectListItem> ddlDivision = new List<SelectListItem>();
                model.OpCode = 36;
                model.Value = UserId.ToString();
                DataSet ds5 = model.GetMasterData();
                if (ds != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds5.Tables[0].Rows)
                    {
                        ddlDivision.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDivision = ddlDivision;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds1 = model.GetMasterData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 34;
                DataSet ds3 = model.GetMasterData();
                if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds3.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation

                if (!string.IsNullOrEmpty(Save))
                {
                    if (Save == "Save")
                    {
                        obj.OpCode = 1;
                    }


                    string hdRows = Request.Form["RowCount"];
                    string Pk_EmployeId = "";
                    string EmployeeName = "";
                    string IsForward = "";

                    DataTable DivMap = new DataTable();

                    DivMap.Columns.Add("Pk_EmployeId");
                    DivMap.Columns.Add("EmployeeName");
                    DivMap.Columns.Add("IsForward");
                    for (int i = 0; i < int.Parse(hdRows); i++)
                    {
                        if (Request.Form["EmpName_" + i].ToString() == "on")
                        {
                            IsForward = "1";
                        }
                        else
                        {
                            IsForward = "0";
                        }
                        Pk_EmployeId = Request.Form["Pk_EmployeeId_" + i].ToString();
                        EmployeeName = Request.Form["EmpName_" + i].ToString();
                        DivMap.Rows.Add(Pk_EmployeId, EmployeeName, IsForward);
                    }

                    obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    obj.DivisionDataTable = DivMap;
                    DataSet ds4 = obj.SaveBillFlowMapping();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        if (ds4.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds4.Tables[0].Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds4.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public JsonResult GetEmployee(string Pk_DepartmentId, string Pk_DesignationId)
        {

            #region OfficerDDl
            STPWiseMapping dashboard = new STPWiseMapping();
            List<SelectListItem> OfficerDDl = new List<SelectListItem>();
            try
            {

                dashboard.Fk_DepartmentId = Pk_DepartmentId;
                dashboard.Fk_DesignationId = Pk_DesignationId;
                
                DataSet ds = dashboard.GetEmpbyID();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OfficerDDl.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            #endregion
            return Json(OfficerDDl);
        }


        [HttpGet]
        public IActionResult CityMapping(CityMapping obj, string Emp_id, string Zone_id, string Department_id, string Designation_id)
        {
            try
            {
                ViewBag.ZoneBySession = HttpContext.Session.GetString("ZoneId");
                STPWiseMapping model = new STPWiseMapping();
                List<CityMapping> lst = new List<CityMapping>();
                //Zone
                if (Zone_id != null)
                    obj.Pk_ZoneId = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Zone_id));
                //Emp
                if (Emp_id != null)               
                    obj.Fk_EmpId = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Emp_id)); ;
                
                //Dep
                if (Department_id != null)              
                    obj.DepartmentId = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Department_id));

                //Desi
                if (Designation_id != null)
                    obj.DesignationId = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Designation_id));

                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 35;
                model.Value = UserId.ToString();
                DataSet ds = model.getEmployeeZone(Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId")));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow row in ds.Tables[0].Rows)
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });

                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds6 = model.GetMasterData();
                if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                    foreach (DataRow row in ds6.Tables[0].Rows)
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });

                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 34;
                DataSet ds8 = model.GetMasterData();
                if (ds8 != null && ds8.Tables.Count > 0 && ds8.Tables[0].Rows.Count > 0)
                    foreach (DataRow row in ds8.Tables[0].Rows)
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation
                #region ddlOfficer
                List<SelectListItem> ddlOfficer = new List<SelectListItem>();
                model.OpCode = 39;
                model.Value = UserId.ToString();
                DataSet ds2 = model.GetMasterData();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    foreach (DataRow row in ds2.Tables[0].Rows)
                        ddlOfficer.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                
                ViewBag.ddlOfficer = ddlOfficer;
                #endregion ddl Officer
                if (!string.IsNullOrEmpty(obj.Fk_EmpId))
                {
                    DataSet ds3 = obj.GetCityMapping();
                    if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds3.Tables[0].Rows)
                        {
                            CityMapping _ = new CityMapping()
                            {
                                Pk_CityId = dr["Pk_CityId"].ToString(),                               
                                Fk_EmpId = dr["Fk_EmployeeId"].ToString(),
                                CityName = dr["CityName"].ToString(),
                                Pk_MappingId = dr["Pk_MappingId"].ToString(),
                                IsChecked = dr["ischecked"].ToString()
                            };
                            lst.Add(_);
                        }
                    }
                    obj.lstData = lst;
                    ViewBag.EmployeeId = obj.Fk_EmpId;
                    ViewBag.Pk_ZoneId = obj.Pk_ZoneId;
                }
                else
                {
                    return View();
                }
                return View(obj);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public IActionResult CityMapping(CityMapping obj, string Save, string Emp_id, string Zone_id)
        {
            try
            {
                STPWiseMapping model = new STPWiseMapping();
                #region ddlZone
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                model.OpCode = 35;
                model.Value = UserId.ToString();
                DataSet ds = model.getEmployeeZone(UserId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion ddlZone
                #region ddldepartment
                List<SelectListItem> ddldepartment = new List<SelectListItem>();
                model.OpCode = 2;
                DataSet ds6 = model.GetMasterData();
                if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds6.Tables[0].Rows)
                    {
                        ddldepartment.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddldepartment = ddldepartment;
                #endregion ddldepartment
                #region ddlDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                model.OpCode = 34;
                DataSet ds8 = model.GetMasterData();
                if (ds8 != null && ds8.Tables.Count > 0 && ds8.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds8.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDesignation = ddlDesignation;
                #endregion ddlDesignation
                #region ddlOfficer
                List<SelectListItem> ddlOfficer = new List<SelectListItem>();
                model.OpCode = 38;
                DataSet ds2 = model.GetMasterData();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        ddlOfficer.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlOfficer = ddlOfficer;
                #endregion ddlOfficer


                if (!string.IsNullOrEmpty(Save))
                {
                    if (Save == "Save")
                        obj.OpCode = 1;
                    else
                        obj.OpCode = 2;
                    string hdRows = Request.Form["RowCount"];
                    string pk_DivisionId = "";
                    string divisionName = "";
                    string IsChecked = "";
                    DataTable DivMap = new DataTable();
                    DivMap.Columns.Add("CityId");                   
                    DivMap.Columns.Add("IsChecked");
                    for (int i = 0; i < int.Parse(hdRows); i++)
                    {
                        if (Request.Form["CityName_" + i].ToString() == "on")                        
                            IsChecked = "1";                        
                        else
                            IsChecked = "0";                        
                        DivMap.Rows.Add(Request.Form["CityId_" + i].ToString(), IsChecked);
                    }

                    obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    obj.CityDataTable = DivMap;
                    DataSet ds4 = obj.SaveCityMapping();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        if (ds4.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds4.Tables[0].Rows[0]["Message"].ToString();
                            if (!string.IsNullOrEmpty(obj.Fk_EmpId))
                            {
                                List<CityMapping> lst = new List<CityMapping>();
                                DataSet ds3 = obj.GetCityMapping();
                                if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dr in ds3.Tables[0].Rows)
                                    {
                                        CityMapping _ = new CityMapping()
                                        {
                                            Pk_CityId = dr["Pk_CityId"].ToString(),
                                            Fk_EmpId = dr["Fk_EmployeeId"].ToString(),
                                            CityName = dr["CityName"].ToString(),
                                            Pk_MappingId = dr["Pk_MappingId"].ToString(),
                                            IsChecked = dr["ischecked"].ToString()
                                        };
                                        lst.Add(_);
                                    }
                                }
                                obj.lstData = lst;
                                ViewBag.EmployeeId = obj.Fk_EmpId;
                                ViewBag.Pk_ZoneId = obj.Pk_ZoneId;
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds4.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

    }
}
