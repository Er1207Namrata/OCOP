using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Filter;
using OCOO.Models;
using System.Data;
using System.Reflection;

namespace OCOO.Controllers
{
    
    public class SecurityController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult SetPermission()
        {
            try
            {
                ViewBag.FK_EmployeeId = 0;
                ViewBag.Fk_MenuId = 0;
                ViewBag.Fk_ZoneId = 0;
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                DataSet DS = new DataSet();
                SetPermission obj = new SetPermission();
                List<SelectListItem> ddlEmployee = new List<SelectListItem>();
                DataSet ds = new DataSet();
                obj.OpCode = 17;
                ds = obj.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        ddlEmployee.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                    }
                }
                ViewBag.ddlEmployee = ddlEmployee;
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
                ViewBag.ddlMenu = ddlMenu;
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
                TempData["Emp"] = obj.Fk_EmpId;
                TempData["Menu"] =  obj.Fk_MemId;
                #endregion
                return View();

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
        }

        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        [HttpPost]
        public ActionResult SetPermission(SetPermission obj, string GetDetails)
        {
            try
            {
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

                #region ddl Menu
                List<SelectListItem> ddlMenu = new List<SelectListItem>();
                obj.OpCode = 18;
                DataSet ds1 = new DataSet();
                ds1 = obj.GetMasterData();
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds1.Tables[0].Rows)
                    {
                        ddlMenu.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                    }
                }
                ViewBag.ddlMenu = ddlMenu;
                #endregion


                ViewBag.FK_EmployeeId = obj.Fk_EmpId;
                ViewBag.Fk_MenuId = obj.MenuId;
                ViewBag.Fk_ZoneId = obj.Fk_ZoneId;

                if (!string.IsNullOrEmpty(GetDetails))
                {
                    SetPermission model = new SetPermission();
                    //    ViewBag.Fk_DistrictId = obj.Fk_EmpId;

                    List<SetPermission> lst = new List<SetPermission>();
                    obj.OpCode = 1;
                    DataSet ds = obj.GetFormPermission();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SetPermission ob = new SetPermission();
                            ob.MenuId = dr["MenuId"].ToString();
                            ob.IsSelectValue = Convert.ToBoolean(dr["IsPermited"].ToString());
                            if (ob.IsSelectValue == false)
                            {
                                ob.SelectedValue = "";
                            }
                            else
                            {
                                ob.SelectedValue = "checked";
                            }
                            ob.IsSaveValue = Convert.ToBoolean(dr["FormSave"].ToString());
                            ob.IsUpdateValue = Convert.ToBoolean(dr["FormUpdate"].ToString());
                            ob.IsDeleteValue = Convert.ToBoolean(dr["FormDelete"].ToString());
                            ob.SubMenuId = int.Parse(dr["SubMenuId"].ToString());
                            ob.SubMenuName = dr["SubMenuName"].ToString();
                            ob.MenuId = dr["MenuId"].ToString();
                            ob.Fk_EmpId = dr["EmpId"].ToString();
                            lst.Add(ob);
                        }
                        model.lstPermission = lst;
                    }
                    //DataSet ds1 = new DataSet();
                    #region ddl Employee
                    Permission obj1 = new Permission();
                    List<SelectListItem> ddlEmployee = new List<SelectListItem>();
                    obj.OpCode = 17;
                    ds1 = obj.GetMasterData();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds1.Tables[0].Rows)
                        {
                            ddlEmployee.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                        }
                    }
                    ViewBag.ddlEmployee = ddlEmployee;
                    #endregion

                    TempData["Emp"] = obj.Fk_EmpId;
                    TempData["Menu"] = obj.Fk_MemId;
                    return View(model);
                }
                else
                {
                    // DataSet ds1 = new DataSet();
                    #region ddl Employee
                    Permission obj1 = new Permission();
                    List<SelectListItem> ddlEmployee = new List<SelectListItem>();
                    DataSet ds = new DataSet();
                    obj.OpCode = 17;
                    ds = obj.GetMasterData();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            ddlEmployee.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                        }
                    }
                    ViewBag.ddlEmployee = ddlEmployee;
                    #endregion



                    string hdrows = Request.Form["hdRows"];
                    string chkSave = "";
                    string chkupdate = "";
                    string chkdelete = "";
                    string chkselect = "";
                    string hdmenuid = "";
                    string hdsubmenuid = "";
                    string hdloginid = "";
                    DataTable dtpermission = new DataTable();

                    dtpermission.Columns.Add("MenuId");
                    dtpermission.Columns.Add("SubMenuId");
                    dtpermission.Columns.Add("IsSave");
                    dtpermission.Columns.Add("IsUpdate");
                    dtpermission.Columns.Add("IsDelete");
                    dtpermission.Columns.Add("IsSelect");
                    for (int i = 0; i < int.Parse(hdrows); i++)
                    {
                        if (Request.Form["chkSelect_" + i].ToString() == "on")
                        {
                            try
                            {
                                chkselect = Request.Form["chkselect_" + i].ToString();
                            }
                            catch
                            {
                                chkselect = "0";
                            }
                            hdmenuid = Request.Form["hdmenuid_" + i].ToString();
                            hdsubmenuid = Request.Form["hdsubmenuid_" + i].ToString();
                            hdloginid = Request.Form["hdloginid_" + i].ToString();

                            dtpermission.Rows.Add(hdmenuid, hdsubmenuid, "0", "0", "0", chkselect == "1" ? "0" : "1");
                        }
                        else
                        {
                            hdmenuid = Request.Form["hdmenuid_" + i].ToString();
                            hdsubmenuid = Request.Form["hdsubmenuid_" + i].ToString();
                            hdloginid = Request.Form["hdloginid_" + i].ToString();

                            obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                            obj.Fk_EmpId = hdloginid;
                            obj.MenuId = hdmenuid;
                            obj.OpCode = 2;
                            DataSet dsDelete = obj.DeletePermission();
                        }
                    }
                    obj.UserTypeFormPermission = dtpermission;
                    obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    obj.Fk_EmpId = hdloginid;
                    obj.MenuId = hdmenuid;
                    DataSet ds2 = obj.SavePermission();
                    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    {
                        if (ds2.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds2.Tables[0].Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds2.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                    TempData["Emp"] = obj.Fk_EmpId;
                    TempData["Menu"] = obj.Fk_MemId;
                    return View(obj);

                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult SetRoll()
        {
            SetPermission obj = new SetPermission();
            try
            {
                ViewBag.FK_EmployeeId = 0;
                ViewBag.Fk_MenuId = 0;
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                DataSet DS = new DataSet();

                List<SelectListItem> ddlUser = new List<SelectListItem>();
                DataSet ds = new DataSet();
                obj.OpCode = 3;
                ds = obj.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        ddlUser.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                    }
                }
                ViewBag.ddlUser = ddlUser;
                #region ddl Menu
                List<SelectListItem> ddlMenu = new List<SelectListItem>();
                obj.OpCode = 18;
                DataSet ds1 = new DataSet();
                ds1 = obj.GetMasterData();
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds1.Tables[0].Rows)
                    {
                        ddlMenu.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                    }
                }
                ViewBag.ddlMenu = ddlMenu;
                #endregion
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
            return View();
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        [HttpPost]
        public ActionResult SetRoll(SetPermission obj, string GetDetails)
        {
            try
            {
                #region ddl Menu
                List<SelectListItem> ddlMenu = new List<SelectListItem>();
                obj.OpCode = 18;
                DataSet ds1 = new DataSet();
                ds1 = obj.GetMasterData();
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds1.Tables[0].Rows)
                    {
                        ddlMenu.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                    }
                }
                ViewBag.ddlMenu = ddlMenu;
                #endregion

                ViewBag.FK_EmployeeId = obj.Fk_EmpId;
                ViewBag.FK_MenuId = obj.MenuId;
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                if (!string.IsNullOrEmpty(GetDetails))
                {

                    SetPermission model = new SetPermission();
                    List<SetPermission> lst = new List<SetPermission>();
                    obj.OpCode = 3;
                    DataSet ds = obj.GetFormPermission();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SetPermission ob = new SetPermission();
                            ob.MenuId = dr["MenuId"].ToString();
                            ob.IsSelectValue = Convert.ToBoolean(dr["IsPermited"].ToString());
                            if (ob.IsSelectValue == false)
                            {
                                ob.SelectedValue = "";
                            }
                            else
                            {
                                ob.SelectedValue = "checked";
                            }
                            ob.IsSaveValue = Convert.ToBoolean(dr["FormSave"].ToString());
                            ob.IsUpdateValue = Convert.ToBoolean(dr["FormUpdate"].ToString());
                            ob.IsDeleteValue = Convert.ToBoolean(dr["FormDelete"].ToString());
                            ob.SubMenuId = int.Parse(dr["SubMenuId"].ToString());
                            ob.SubMenuName = dr["SubMenuName"].ToString();
                            ob.MenuId = dr["MenuId"].ToString();
                            ob.Fk_EmpId = dr["EmpId"].ToString();
                            lst.Add(ob);
                        }
                        model.lstPermission = lst;
                    }
                    DataSet ds3 = new DataSet();
                    #region ddl User
                    Permission obj1 = new Permission();
                    List<SelectListItem> ddlUser = new List<SelectListItem>();
                    obj1.OpCode = 3;
                    ds3 = obj1.GetMasterData();
                    if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds3.Tables[0].Rows)
                        {
                            ddlUser.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                        }
                    }
                    ViewBag.ddlUser = ddlUser;
                    #endregion

                    return View(model);
                }
                else
                {
                    DataSet ds2 = new DataSet();
                    #region ddl User
                    Permission obj1 = new Permission();
                    List<SelectListItem> ddlUser = new List<SelectListItem>();
                    obj1.OpCode = 3;
                    ds2 = obj1.GetMasterData();
                    if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds2.Tables[0].Rows)
                        {
                            ddlUser.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Id"].ToString() });
                        }
                    }
                    ViewBag.ddlUser = ddlUser;
                    #endregion


                    string hdrows = Request.Form["hdRows"];
                    string chkSave = "";
                    string chkupdate = "";
                    string chkdelete = "";
                    string chkselect = "";
                    string hdmenuid = "";
                    string hdsubmenuid = "";
                    string hdloginid = "";
                    DataTable dtpermission = new DataTable();

                    dtpermission.Columns.Add("MenuId");
                    dtpermission.Columns.Add("SubMenuId");
                    dtpermission.Columns.Add("IsSave");
                    dtpermission.Columns.Add("IsUpdate");
                    dtpermission.Columns.Add("IsDelete");
                    dtpermission.Columns.Add("IsSelect");
                    for (int i = 0; i < int.Parse(hdrows); i++)
                    {
                        if (Request.Form["chkSelect_" + i].ToString() == "on")
                        {
                            try
                            {
                                chkselect = Request.Form["chkselect_" + i].ToString();
                            }
                            catch
                            {
                                chkselect = "0";
                            }
                            hdmenuid = Request.Form["hdmenuid_" + i].ToString();
                            hdsubmenuid = Request.Form["hdsubmenuid_" + i].ToString();
                            hdloginid = Request.Form["hdloginid_" + i].ToString();

                            dtpermission.Rows.Add(hdmenuid, hdsubmenuid, "0", "0", "0", chkselect == "1" ? "0" : "1");
                        }
                        else
                        {
                            hdmenuid = Request.Form["hdmenuid_" + i].ToString();
                            hdsubmenuid = Request.Form["hdsubmenuid_" + i].ToString();
                            hdloginid = Request.Form["hdloginid_" + i].ToString();

                            obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                            obj.Fk_EmpId = hdloginid;
                            obj.MenuId = hdmenuid;
                            obj.OpCode = 4;
                            DataSet dsDelete = obj.DeletePermission();
                        }

                    }
                    obj.UserTypeFormPermission = dtpermission;
                    obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    obj.Fk_EmpId = hdloginid;
                    obj.MenuId = hdmenuid;
                    DataSet ds = obj.UserRollPermission();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
                    return View(obj);
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;return View();
            }
        }
    }
}
