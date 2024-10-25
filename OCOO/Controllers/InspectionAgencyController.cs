using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using System.Data;
using System.Reflection;


namespace OCOO.Controllers
{
    public class InspectionAgencyController : InspectionAgencyBaseController
    {
        public IActionResult DashBoard(AgencyDashboard dashboard)
        {
            try
            {
                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                dashboard.OpCode = 11;
                DataSet dsSTP = dashboard.GetMasterData();
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
                if (string.IsNullOrEmpty(dashboard.Fk_MonthId))
                {
                    dashboard.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");


                }
                dashboard.Fk_InsAgencyId = HttpContext.Session.GetString("Pk_UserId");
                dashboard.Fk_InspectionTypeId = HttpContext.Session.GetString("InspectionType");
                DataSet dataSet = dashboard.GetAgencyDashboard();
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    dashboard.dtDetails = dataSet.Tables[0];
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(dashboard);
        }
        [HttpPost]
        public ActionResult DashBoard(AgencyDashboard dashboard, string btnSumbit)
        {
            try
            {

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();
                dashboard.OpCode = 11;
                DataSet dsSTP = dashboard.GetMasterData();
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
                if (string.IsNullOrEmpty(dashboard.Fk_MonthId))
                {
                    dashboard.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");


                }
                dashboard.Fk_InsAgencyId = HttpContext.Session.GetString("Pk_UserId");
                dashboard.Fk_InspectionTypeId = HttpContext.Session.GetString("InspectionType");
                dashboard.FromDate = string.IsNullOrEmpty(dashboard.FromDate) ? null : Common.ConvertToSystemDate(dashboard.FromDate, "dd/mm/yyyy");
                dashboard.ToDate = string.IsNullOrEmpty(dashboard.ToDate) ? null : Common.ConvertToSystemDate(dashboard.ToDate, "dd/mm/yyyy");

                DataSet dataSet = dashboard.GetAgencyDashboard();
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    dashboard.dtDetails = dataSet.Tables[0];
                }
                TempData["Fk_STPId"] = dashboard.Fk_STPId;
                TempData["FromDate"] = dashboard.FromDate;
                TempData["Fk_MonthId"] = dashboard.Fk_MonthId;
                TempData["ToDate"] = dashboard.ToDate;
                return View(dashboard);
            }

            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View();
            }
        }
        public ActionResult BindChartData(AgencyDashboard dashboard)
        {
            try
            {

                dashboard.Fk_InsAgencyId = HttpContext.Session.GetString("Pk_UserId");
                dashboard.Fk_InspectionTypeId = HttpContext.Session.GetString("InspectionType");
                //if(!string.IsNullOrEmpty(firmDashboard.FromDate))
                //{

                //firmDashboard.FromDate = Common.ConvertToSystemDate(firmDashboard.FromDate, "dd/mm/yyyy");
                //}
                //if (!string.IsNullOrEmpty(firmDashboard.ToDate))
                //{
                //    firmDashboard.ToDate = Common.ConvertToSystemDate(firmDashboard.ToDate, "dd/mm/yyyy");
                //}


                DataSet ds = dashboard.GetChartData();
                List<ComplaintData> list = new List<ComplaintData>();
                List<LDCountData> listLDCount = new List<LDCountData>();
                if (ds != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        LDCountData piChartLDCount = new LDCountData();
                        piChartLDCount.BODCount = ds.Tables[1].Rows[0]["TotalBODLDCount"].ToString();
                        piChartLDCount.CODCount = ds.Tables[1].Rows[0]["TotalCODLDCount"].ToString();
                        piChartLDCount.TSS = ds.Tables[1].Rows[0]["TotalTSSLDCount"].ToString();
                        piChartLDCount.FC = ds.Tables[1].Rows[0]["TotalFCLDCount"].ToString();
                        piChartLDCount.TotalLDAmount = ds.Tables[1].Rows[0]["TotalLDCount"].ToString();
                        dashboard.PieChartLDCountData = piChartLDCount;
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
                        dashboard.listLDCount = listLDCount;
                    }

                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(dashboard);
        }

        public ActionResult STPBillingList(string Fk_FirmId, String Pk_STPId)
        {

            try
            {
                FirmDetails model = new FirmDetails();
                #region ddlFirm
                List<SelectListItem> ddlFirm = new List<SelectListItem>();
                model.OpCode = 8;
                model.Value = HttpContext.Session.GetString("Pk_ZoneId");
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

                #region ddlSTP
                List<SelectListItem> ddlSTP = new List<SelectListItem>();

                //model.OpCode = 11;
                //DataSet dataSet = model.GetMasterData();
                //if (dataSet != null && dataSet.Tables.Count > 0)
                //{
                //    foreach (DataRow row in dataSet.Tables[0].Rows)
                //    {
                //        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                //    }
                //} 
                ddlSTP.Add(new SelectListItem { Text = "--select", Value = "0" });
                ViewBag.ddlSTP = ddlSTP;
                #endregion
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                model.Fk_FirmId = Fk_FirmId;
                model.Fk_STPId = Pk_STPId;
                DataSet ds = model.GetBillingReport();
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

        [HttpPost]
        public async Task<ActionResult> UpdateBillingStatus(FirmDetails model, string btnSubmit)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    string? foldername = "InspectionDocument";
                    if (HttpContext.Session.GetString("InspectionType") == "1")
                    {
                        if (model.DocsFile != null && model.DocsFile.Length > 0)
                        {
                            string fileLocation = await FileManagement.WriteFiles(model.DocsFile, "InspectionReport", foldername);
                            model.DOCS = fileLocation;
                        }
                        model.OpCode = 1;
                        model.UserId = HttpContext.Session.GetString("Pk_UserId");
                        model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        DataSet dataSet = model.UpdateStatus();
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                    else if (HttpContext.Session.GetString("InspectionType") == "2")
                    {
                        model.OpCode = 2;
                        model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        DataSet dataSet = model.UpdateStatus();
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                    else
                    {
                        model.OpCode = 3;
                        model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                        DataSet dataSet = model.UpdateStatus();
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
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

        public JsonResult GetSTPDDl(string Id)
        {
            #region ddlSTP
            Dashboard dashboard = new Dashboard();
            List<SelectListItem> ddlSTP = new List<SelectListItem>();
            try
            {

                dashboard.Value = Id == "0" ? null : Id;
                dashboard.OpCode = 11;
                DataSet dsSTP = dashboard.GetMasterData();
                if (dsSTP != null && dsSTP.Tables.Count > 0 && dsSTP.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsSTP.Tables[0].Rows)
                    {
                        ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            #endregion
            return Json(ddlSTP);
        }
        public ActionResult ChangePassword(Login login, string BtnChange)
        {
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(BtnChange))
                {
                    login.Fk_UserTypeId = int.Parse(HttpContext.Session.GetString("Fk_UsertypeId"));
                    if (login.Fk_UserTypeId == 3)
                    {
                        login.OpCode = 3;
                    }
                    login.Fk_UserId = Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId"));
                    DataSet dataSet = login.PasswordChange();
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
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
                            //TempData["code"] = dataSet.Tables[0].Rows[0]["Flag"].ToString();
                            //TempData["msg"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            HttpContext.Session.SetString("ProfilePic", profile.Documents);

                            if (HttpContext.Session.GetString("Fk_UsertypeId") == "1")
                            {
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "2")
                            {
                                return RedirectToAction("Dashboard", "Firm");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "3")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                return RedirectToAction("Dashboard", "InspectionAgency");

                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "4")
                            {
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

                return View(profile);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message; return View(profile);
            }
        }


    }

}
