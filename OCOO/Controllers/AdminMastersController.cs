using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Filter;
using OCOO.Models;
using OCOO.Models.AdminMasters;
using System.Data;

namespace OCOO.Controllers
{

    public class AdminMastersController : AdminBaseController
    {
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult InspectionMaster(Inspection inspection, string btnSubmit, string btnUpdate)
        {
            try
            {
                #region ddlInspectionType
                List<SelectListItem> ddlInspectionType = new List<SelectListItem>();
                inspection.OpCode = 15;
                DataSet ds = inspection.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlInspectionType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }

                }
                ViewBag.ddlInspectionType = ddlInspectionType;
                #endregion ddlInspectionType
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        inspection.OpCode = 1;
                    }
                    else
                    {
                        inspection.OpCode = 2;
                    }
                    inspection.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = inspection.ManageInspection();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("InspectionMaster");
                }
                else
                {

                    inspection.OpCode = 4;
                    inspection.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = inspection.ManageInspection();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            inspection.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(inspection);

        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteInspection(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Inspection inspection = new Inspection();
                    inspection.PK_InspectionId = Crypto.Decrypt(id);
                    inspection.OpCode = 3;
                    inspection.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = inspection.ManageInspection();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("InspectionMaster");

        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult InspectionAgency(InspectionAgency inspectionAgency, string btnSubmit, string btnUpdate)
        {
            try
            {
                #region
                List<SelectListItem> ddlZone = new List<SelectListItem>();
                inspectionAgency.OpCode = 4;
                DataSet dataSet1 = inspectionAgency.GetMasterData();
                if (dataSet1 != null && dataSet1.Tables.Count > 0)
                {
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet1.Tables[0].Rows)
                        {
                            ddlZone.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                        }
                    }
                }
                ViewBag.ddlZone = ddlZone;
                #endregion

                #region ddlInspectionType
                List<SelectListItem> ddlInspectionType = new List<SelectListItem>();
                inspectionAgency.OpCode = 15;
                DataSet ds = inspectionAgency.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlInspectionType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }

                }
                ViewBag.ddlInspectionType = ddlInspectionType;
                #endregion ddlInspectionType
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        inspectionAgency.OpCode = 1;
                    }
                    else
                    {
                        inspectionAgency.OpCode = 2;
                    }
                    inspectionAgency.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = inspectionAgency.ManageInspectionAgency();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("InspectionAgency");
                }
                else
                {

                    inspectionAgency.OpCode = 4;
                    inspectionAgency.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = inspectionAgency.ManageInspectionAgency();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            inspectionAgency.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
                return View(inspectionAgency);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(inspectionAgency);

        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteInspectionAgency(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    InspectionAgency inspectionAgency = new InspectionAgency();
                    inspectionAgency.Pk_UserId = Crypto.Decrypt(id);
                    inspectionAgency.OpCode = 3;
                    inspectionAgency.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = inspectionAgency.ManageInspectionAgency();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("InspectionAgency");
        }

        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DepartmentMaster(Department department, string btnSubmit, string btnUpdate)
        {
            try
            {
                #region ddlDepartmentType
                List<SelectListItem> ddlDepartmentType = new List<SelectListItem>();
                department.OpCode = 2;
                DataSet ds = department.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDepartmentType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }

                }
                ViewBag.ddlDepartmentType = ddlDepartmentType;
                #endregion ddlDepartmentType
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        department.OpCode = 1;
                    }
                    else
                    {
                        department.OpCode = 2;
                    }
                    department.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = department.ManageDepartment();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("DepartmentMaster");
                }
                else
                {

                    department.OpCode = 4;
                    department.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = department.ManageDepartment();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            department.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(department);
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteDepartment(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Department department = new Department();
                    department.Pk_DepartmentId = Crypto.Decrypt(id);
                    department.OpCode = 3;
                    department.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = department.ManageDepartment();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("DepartmentMaster");
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DesignationMaster(Degination degination, string btnSubmit, string btnUpdate)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        degination.OpCode = 1;
                    }
                    else
                    {
                        degination.OpCode = 2;
                    }
                    degination.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = degination.ManageDegination();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("DesignationMaster");
                }
                else
                {

                    degination.OpCode = 4;
                    degination.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = degination.ManageDegination();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            degination.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
                return View(degination);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(degination);
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteDegination(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Degination degination = new Degination();
                    degination.Pk_DesignationId = Crypto.Decrypt(id);
                    degination.OpCode = 3;
                    degination.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = degination.ManageDegination();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("DesignationMaster");
        }

        //[HttpPost]
        //public ActionResult DeginationLink(Degination degination,string btnsave,string id)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }
        //        DataSet dataSet = new DataSet();
        //        #region ddlDepartmentType
        //        List<SelectListItem> ddlDepartmentType = new List<SelectListItem>();
        //        degination.OpCode = 2;
        //        DataSet ds = degination.GetMasterData();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                ddlDepartmentType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
        //            }

        //        }
        //        ViewBag.ddlDepartmentType = ddlDepartmentType;
        //        #endregion ddlDepartmentType 
        //        if (!string.IsNullOrEmpty(btnsave))
        //        {
        //            dataSet = degination.SaveDegination();
        //            degination.dtDetails = dataSet.Tables[0];
        //        }
        //        return View(degination);

        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        public JsonResult GetEmployeeeZoneData(ZoneData model, string Id)
        {
            List<ZoneData> ddlDesignation = new List<ZoneData>();

            try
            {

                model.Value = Id;

                DataSet ds = model.GetEmpZoneData();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ZoneData zoneData = new ZoneData();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new ZoneData { textZone = row["Name"].ToString(), valueZone = row["Id"].ToString() });
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(ddlDesignation);
        }
        public ActionResult DeginationLink(Degination degination, string? Fk_DepartmentId)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_UserId")))
                {
                    return RedirectToAction("Login", "Home");
                }
                DataSet dataSet = new DataSet();
                #region ddlDepartmentType
                List<SelectListItem> ddlDepartmentType = new List<SelectListItem>();
                degination.OpCode = 2;
                DataSet ds = degination.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDepartmentType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }

                }
                ViewBag.ddlDepartmentType = ddlDepartmentType;
                #endregion ddlDepartmentType 
                //if (!string.IsNullOrEmpty(Fk_DepartmentId))
                //{
                //    dataSet = degination.GetDegination();
                //    degination.dtDetails = dataSet.Tables[0];
                //}
                return View(degination);

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(degination);
        }

        [HttpPost]
        public ActionResult DeginationLink(Degination obj, string btnSubmit, string id)
        {

            try
            {
                ViewBag.Fk_DepartmentId = obj.Fk_DepartmentId;
                //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pk_AdminId")))
                //{
                //    return RedirectToAction("Login", "Home");
                //}
                #region ddlDepartmentType
                List<SelectListItem> ddlDepartmentType = new List<SelectListItem>();
                obj.OpCode = 2;
                DataSet dsDepartmentType = obj.GetMasterData();
                if (dsDepartmentType != null && dsDepartmentType.Tables.Count > 0 && dsDepartmentType.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsDepartmentType.Tables[0].Rows)
                    {
                        ddlDepartmentType.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                ViewBag.ddlDepartmentType = ddlDepartmentType;
                #endregion ddlDepartmentType 
                if (string.IsNullOrEmpty(btnSubmit))
                {

                    List<Degination> lst = new List<Degination>();
                    //obj.OpCode = 3;
                    DataSet ds = obj.GetDegination();
                    // model.dtDetails = ds.Tables[0];
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Degination ob = new Degination();
                            ob.SelectedValue = dr["IsPermited"].ToString();
                            ob.DesignationName = dr["DesignationName"].ToString();
                            ob.Pk_DesignationId = dr["Pk_DesignationId"].ToString();
                            lst.Add(ob);
                        }
                        obj.lstDegination = lst;
                    }


                }
                else
                {

                    string hdrows = Request.Form["hdRows"];
                    string chkSave = "";
                    string chkupdate = "";
                    string chkdelete = "";
                    string chkselect = "";
                    string hdDepartmentId = "";
                    string hdDesignationId = "";
                    string hdloginid = "";
                    DataTable dtDesignation = new DataTable();
                    dtDesignation.Columns.Add("Pk_DesignationId");

                    for (int i = 1; i <= int.Parse(hdrows); i++)
                    {
                        if (Request.Form["chkSelect_" + i].ToString() == "on")
                        {
                            try
                            {
                                hdDesignationId = Request.Form["hdDesignationId_" + i].ToString();
                            }
                            catch
                            {
                                chkselect = "0";
                            }

                            dtDesignation.Rows.Add(hdDesignationId);
                            obj.UserTypeDesignation = dtDesignation;
                            obj.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        }


                    }

                    DataSet ds = obj.SaveDegination();
                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                return View(obj);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(obj);
        }

        public ActionResult Notification(FirmDetails model)
        {
            try
            {
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                model.Fk_UsertypeId = HttpContext.Session.GetString("Fk_UsertypeId");
                // DataSet ds = model.GetBillingdata();
                DataSet ds = model.GetNotification();
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

        public ActionResult UploadDocMaster(UploadDoc model, string btnSubmit)
        {
            try
            {
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
                    DataSet dataSet = new DataSet();
                    dataSet = model.ManageUPloadDoc();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("UploadDocMaster");
                }
                else
                {

                    model.OpCode = 4;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = model.ManageUPloadDoc();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);

        }

        public ActionResult DeleteUploadDoc(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    UploadDoc model = new UploadDoc();
                    model.PK_UploadDocId = Crypto.Decrypt(id);
                    model.OpCode = 3;
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = model.ManageUPloadDoc();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("UploadDocMaster");
        }


        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DivisionMaster(Division division, string btnSubmit, string btnUpdate)
        {
            try
            {

                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        division.OpCode = 1;
                    }
                    else
                    {
                        division.OpCode = 2;
                    }
                    division.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = division.ManageDivision();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("DepartmentMaster");
                }
                else
                {

                    division.OpCode = 4;
                    division.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = division.ManageDivision();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            division.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(division);
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteDivision(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Division division = new Division();
                    division.Pk_DivisinId = Crypto.Decrypt(id);
                    division.OpCode = 3;
                    division.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = division.ManageDivision();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("DepartmentMaster");
        }

        //[ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DistrictMaster(Distric distric, string btnSubmit, string btnUpdate)
        {
            try
            {

                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        distric.OpCode = 1;
                    }
                    else
                    {
                        distric.OpCode = 2;
                    }
                    distric.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = distric.ManageDistric();
                    if (dataSet != null && dataSet.Tables[0] != null)
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
                    return RedirectToAction("DepartmentMaster");
                }
                else
                {

                    distric.OpCode = 4;
                    distric.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = distric.ManageDistric();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            distric.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(distric);
        }
        //[ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult DeleteDistrict(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Distric distric = new Distric();
                    distric.Pk_DistricId = Crypto.Decrypt(id);
                    distric.OpCode = 3;
                    distric.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = distric.ManageDistric();
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("DepartmentMaster");
        }

        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult CityMaster(CityMasterDTO obj)
        {
            try
            {
                DataSet dataSet = new DataSet();
                dataSet = obj.GetCities(Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId")));
                if (dataSet != null && dataSet.Tables[0] != null)
                    obj.dtDetails = dataSet.Tables[0];
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(obj);
        }
    }
}
