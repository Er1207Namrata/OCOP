using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Filter;
using OCOO.Models;
using System.Data;

namespace OCOO.Controllers
{
    public class ApproveBillingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult InvoiceDetails(ApproveBilling model, string FromDate, string ToDate, string Fk_MonthId, string btnSubmit)
        {
            DataSet ds = new DataSet();
            try
            {
                #region ddlFirm
                //List<SelectListItem> ddlFirm = new List<SelectListItem>();
                //model.OpCode = 8;
                //DataSet dataSet1 = model.GetMasterData();
                //if (dataSet1 != null && dataSet1.Tables.Count > 0)
                //{
                //    if (dataSet1.Tables[0].Rows.Count > 0)
                //    {
                //        foreach (DataRow row in dataSet1.Tables[0].Rows)
                //        {
                //            ddlFirm.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                //        }
                //    }
                //}
                //ViewBag.ddlFirm = ddlFirm;


                List<SelectListItem> ddlFirm = new List<SelectListItem>();

                ddlFirm.Add(new SelectListItem { Text = "--select--", Value = "0" });
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
                //if (!string.IsNullOrEmpty(btnSubmit))
                //{
                //    model.Fk_MonthId = Fk_MonthId;

                //}
                //else
                //{

                //    Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                //}
                if (string.IsNullOrEmpty(model.Fk_MonthId))
                {
                    model.Fk_MonthId = HttpContext.Session.GetString("Pk_MonthId");
                }
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
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

        public async Task<ActionResult> ApproveBill(ApproveBilling model, string btnSubmit)
        {
            try
            {
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                if (btnSubmit == "Forward")
                {
                    model.OpCode = 2;
                    model.Remarks = model.ForwardRemarks;
                }
                else
                {
                    model.OpCode = 1;
                    model.ForwardTo = model.ApproveAndForward;
                }
                string? foldername = "ApprovalDocument";
                if (model.ApproveDocuments != null && model.ApproveDocuments.Length > 0)
                {
                    string fileLocation = await FileManagement.WriteFiles(model.ApproveDocuments, "Approval Documents", foldername);
                    model.ApproveDocumentsURL = fileLocation;
                }
                if (model.FuelChargesDocuments != null && model.ApproveDocuments.Length > 0)
                {
                    string fileLocation = await FileManagement.WriteFiles(model.FuelChargesDocuments, "Approval Documents", foldername);
                    model.FuelChargesDocumentsURL = fileLocation;
                }
                if (model.OtherLdChargesDocuments != null && model.ApproveDocuments.Length > 0)
                {
                    string fileLocation = await FileManagement.WriteFiles(model.OtherLdChargesDocuments, "Approval Documents", foldername);
                    model.OtherLdChargesDocumentsURL = fileLocation;
                }
                DataSet ds = model.ApproveBill();
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
            return RedirectToAction("BillApprovalList");
        }

        public JsonResult InvoiceApprovalDetails(string Pk_InvId)
        {
            List<ApproveBilling> appList = new List<ApproveBilling>();
            try
            {
                ApproveBilling model = new ApproveBilling();
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 2;
                model.PK_InvId = Pk_InvId;
                DataSet ds = model.ApproveBill();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ApproveBilling list = new ApproveBilling();
                            list.PK_InvId = ds.Tables[0].Rows[i]["FK_InvId"].ToString();
                            list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            list.Pk_ZoneId = ds.Tables[0].Rows[i]["Zone"].ToString();
                            list.ApproveOrder = ds.Tables[0].Rows[i]["ApprovalOrder"].ToString();
                            list.IsEnabled = ds.Tables[0].Rows[i]["IsEnabled"].ToString();
                            list.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                            list.ApproveDocumentsURL = ds.Tables[0].Rows[i]["Doc_Url"].ToString();
                            list.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                            list.ApprovedDate = ds.Tables[0].Rows[i]["ApprovedDate"].ToString();
                            appList.Add(list);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(appList);

        }

        public JsonResult GetEmployeeddl(string EmpId, string FirmId, string BillId)
        {
            try
            {
                ApproveBilling model = new ApproveBilling();
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = FirmId;
                model.BillId = BillId;
                model.OpCode = 1;
                DataSet ds = model.GetEmpApprovalList();
                List<EmployeeList> employees = new List<EmployeeList>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmployeeList list = new EmployeeList();
                            list.Id = ds.Tables[0].Rows[i]["Id"].ToString();
                            list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            employees.Add(list);
                        }
                    }
                }
                return Json(employees);
            }
            catch (Exception Ex)
            {
                return Json(Ex.Message);
            }
        }

        public JsonResult GetEmployeeddlForReject(string EmpId, string FirmId, string BillId)
        {
            try
            {
                ApproveBilling model = new ApproveBilling();
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = FirmId;
                model.BillId = BillId;
                model.OpCode = 2;
                DataSet ds = model.GetEmpApprovalList();
                List<EmployeeList> employees = new List<EmployeeList>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmployeeList list = new EmployeeList();
                            list.Id = ds.Tables[0].Rows[i]["Id"].ToString();
                            list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            employees.Add(list);
                        }
                    }
                }
                return Json(employees);
            }
            catch (Exception Ex)
            {
                return Json(Ex.Message);
            }
        }
        [ServiceFilter(typeof(MenuAuthorizationFilter))]
        public ActionResult BillApprovalList(ApproveBilling model, string Fk_FirmId, string Fk_STPId, string FromDate, string ToDate, string Fk_MonthId, string Pk_ZoneId, string btnSubmit)
        {
            DataSet ds = new DataSet();
            try
            {
                model.DesignationId = Convert.ToInt32(HttpContext.Session.GetString("DesignationId"));
                model.DepartmentId = Convert.ToInt32(HttpContext.Session.GetString("DepartmentId"));
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
                if (FromDate != null && ToDate != null)
                {
                    model.FromDate = Common.ConvertToSystemDate(FromDate, "dd/mm/yyyy");
                    model.ToDate = Common.ConvertToSystemDate(ToDate, "dd/mm/yyyy");
                }
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
                model.Pk_ZoneId = Pk_ZoneId;
                model.Fk_FirmId = Fk_FirmId;
                model.Fk_STPId = Fk_STPId;
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;

                ds = model.GetBillApprovalList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //ViewBag.Data = ds.Tables[0].Rows[i]["Pk_ZoneId"].ToString();
                        ViewBag.Data1 = ds.Tables[0].Rows[i]["Pk_FirmId"].ToString();
                        //ViewBag.Data2 = ds.Tables[0].Rows[i]["Inv_Month"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }

        public JsonResult BillApprovalHistory(string BillId, string FirmId)
        {
            List<ApproveBilling> appList = new List<ApproveBilling>();
            try
            {
                ApproveBilling model = new ApproveBilling();
                model.Pk_EmployeeId = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 2;
                model.Firm = FirmId;
                model.BillId = BillId;
                DataSet ds = model.ApproveBillHistory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ApproveBilling list = new ApproveBilling();
                            list.BillId = ds.Tables[0].Rows[i]["FK_FbillId"].ToString();
                            list.Pk_EmployeeId = ds.Tables[0].Rows[i]["EMPId"].ToString();
                            list.Fk_UsertypeId = ds.Tables[0].Rows[i]["UserTypeId"].ToString();
                            list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            list.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                            list.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                            list.ApprovedDate = ds.Tables[0].Rows[i]["ApprovedDate"].ToString();
                            list.ApproveDocumentsURL = ds.Tables[0].Rows[i]["Doc_Url"].ToString();
                            list.ToRemark = ds.Tables[0].Rows[i]["ToRemark"].ToString();
                            list.ProcessType = ds.Tables[0].Rows[i]["ProcessType"].ToString();
                            list.ForwardedBy = ds.Tables[0].Rows[i]["ForwardedBy"].ToString();
                            list.Color = ds.Tables[0].Rows[i]["color"].ToString();
                            list.AddedOn = ds.Tables[0].Rows[i]["AddedOn"].ToString();
                            list.ld_DocumentsURL = ds.Tables[0].Rows[i]["LDDocumentsURL"].ToString();
                            appList.Add(list);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(appList);
        }
        public ActionResult BillLog(ApproveBilling model,string abc)
        {
            try
            {
                model.BillId =Crypto.Decrypt(abc);
                DataSet ds = model.getBillLog();
                model.dtDetails = ds.Tables[0];
                
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);

        }
    }
}
