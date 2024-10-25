using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using OCOO.Models.FirmMasters;
using System.Data;
using System.Reflection;

namespace OCOO.Controllers
{
    public class DeductionReleaseController : FirmBaseController
    {
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

                //model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
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

        public ActionResult AddDeduction(Deduction model, string Id, string STPID)
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

                if (!string.IsNullOrEmpty(Id))
                {
                    model.Pk_DeductionId = Crypto.Decrypt(Id);
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet ds = model.GetDeduction();
                    if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Pk_DeductionId = ds.Tables[0].Rows[0]["Pk_DeductionId"].ToString();
                        model.DeductionDate = ds.Tables[0].Rows[0]["DeductionDate"].ToString();
                        model.ElectricityCutStartTime = ds.Tables[0].Rows[0]["ElectricityCutStartTime"].ToString();
                        model.ElectricityCutEndTime = ds.Tables[0].Rows[0]["ElectricityCutEndTime"].ToString();
                        model.MeterReading = ds.Tables[0].Rows[0]["MeterReading"].ToString();
                        model.DGStartTime = ds.Tables[0].Rows[0]["DGStartTime"].ToString();
                        model.DGEndTime = ds.Tables[0].Rows[0]["DGEndTime"].ToString();
                        model.DieselConsumes = ds.Tables[0].Rows[0]["DieselConsumes"].ToString();
                        model.DieselRateInMonth = ds.Tables[0].Rows[0]["DieselRateInMonth"].ToString();
                        model.Pk_STPId = ds.Tables[0].Rows[0]["Fk_STPId"].ToString();
                        model.PK_IPS = ds.Tables[0].Rows[0]["FK_MainPumpingStationId"].ToString();
                        model.TotalDGDuration = ds.Tables[0].Rows[0]["TotalDGDuration"].ToString();
                        model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                        TempData["DGSubmit"] = "Update";
                        #region ddlips
                        model.OpCode = 23;
                        model.Value = model.Pk_STPId;
                        DataSet ds1 = model.GetMasterData();
                        List<SelectListItem> ddlIPS1 = new List<SelectListItem>();

                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    ddlIPS1.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                                }
                            }
                        }
                        ViewBag.ddlIPS = ddlIPS1;
                        #endregion

                    }
                    return View(model);
                }
                else
                {
                    model.Pk_STPId = string.IsNullOrEmpty(STPID) ? "0" : Crypto.Decrypt(STPID);
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddDeduction(Deduction model, string btnSubmit)
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

                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                    model.DeductionDate = Common.ConvertToSystemDate(model.DeductionDate, "dd/mm/yyyy");
                    if (btnSubmit == "Submit")
                    {
                        DataSet ds = model.SaveDeduction();
                        if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                return RedirectToAction("AddDeduction", new { STPID = Crypto.Encrypt(model.Pk_STPId) });
                            }
                            else
                            {
                                TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                return View();
                            }
                        }
                    }
                    else
                    {
                        DataSet ds1 = model.UpdateDeduction();
                        if (ds1?.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["Message"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["DGSubmit"] = "Update";
                                TempData["ErrorMessage"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                                return View();
                            }
                        }
                    }


                }
                else
                {
                    return RedirectToAction("GetDeductionList");
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("GetDeductionList");
        }
        public ActionResult DeleteDeduction(string Id, string STPID)
        {
            try
            {
                Deduction model = new Deduction();
                model.Pk_DeductionId = Crypto.Decrypt(Id);
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.DeleteDeduction();
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
                return RedirectToAction("GetDeductionList", new { id = Crypto.Encrypt(STPID) });
                // return RedirectToAction("GetDeductionList");
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("GetDeductionList", new { id = Crypto.Encrypt(STPID) });

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

                //model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
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
        public ActionResult DeleteElectricity(string Id, string StpId)
        {
            try
            {
                Deduction model = new Deduction();
                model.PK_ElectricityId = Crypto.Decrypt(Id);
                model.OpCode = 3;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetElectricity();
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("GetelectricityBillList", new { id = Crypto.Encrypt(StpId) });
        }
        public ActionResult GetSTPMonthDate(Deduction model)
        {
            try
            {
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetSTPDate();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        model.Months = ds.Tables[0].Rows[0]["UPPCLBillCycle"].ToString();
                    }
                    if (ds?.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        model.EndMeterReading = ds.Tables[1].Rows[0]["MonthEndMeterReading"].ToString();
                        model.MonthEndDate = ds.Tables[1].Rows[0]["MonthEndDate"].ToString();
                    }
                }
                else
                {

                    model.Status = "0";
                    model.Message = "Something went wrong";
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }
        public ActionResult AddElectricityBill(Deduction model, string Id, string STPID)
        {
            try
            {
                DataSet ds1 = new DataSet();
                model.Pk_STPId = Crypto.Decrypt(STPID);
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
                model.Pk_STPId= Crypto.Decrypt(STPID);
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
                if (!string.IsNullOrEmpty(Id))
                {
                    model.PK_ElectricityId = Crypto.Decrypt(Id);
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.OpCode = 5;
                    DataSet ds = model.GetElectricity();
                    if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.PK_ElectricityId = ds.Tables[0].Rows[0]["Pk_ElectricityBillId"].ToString();
                        model.Pk_STPId = ds.Tables[0].Rows[0]["Fk_STPId"].ToString();
                        model.PK_IPS = ds.Tables[0].Rows[0]["FK_MainPumpingStationId"].ToString();
                        model.MonthStartDate = ds.Tables[0].Rows[0]["MonthStartDate"].ToString();
                        model.MonthEndDate = ds.Tables[0].Rows[0]["MonthEndDate"].ToString();
                        model.StartMeterReading = ds.Tables[0].Rows[0]["MonthStartMeterReading"].ToString();
                        model.EndMeterReading = ds.Tables[0].Rows[0]["MonthEndMeterReading"].ToString();
                        model.OtherCharges = ds.Tables[0].Rows[0]["OtherCharges"].ToString();
                        model.UnitRate = ds.Tables[0].Rows[0]["UnitRate"].ToString();
                        model.FixCharges = ds.Tables[0].Rows[0]["FixCharges"].ToString();
                        model.Penalty = ds.Tables[0].Rows[0]["Penalty"].ToString();
                        model.ElectricityUnit = ds.Tables[0].Rows[0]["TotalMeterReading"].ToString();
                        model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                        TempData["ElectricitySubmit"] = "Update";
                        #region ddlips
                        List<SelectListItem> ddlIPS1 = new List<SelectListItem>();
                        model.OpCode = 23;
                        model.Value = model.Pk_STPId;
                        DataSet dataSet1 = model.GetMasterData();
                        if (dataSet1 != null && dataSet1.Tables.Count > 0)
                        {
                            if (dataSet1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in dataSet1.Tables[0].Rows)
                                {
                                    ddlIPS1.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                                }
                            }
                        }
                        ViewBag.ddlIPS = ddlIPS1;
                        #endregion

                    }
                    else
                    {
                        model.Pk_STPId = string.IsNullOrEmpty(STPID) ? "0" : Crypto.Decrypt(STPID);
                    }
                    return View(model);
                }

                TempData["ElectricitySubmit"] = "Save";
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddElectricityBill(Deduction model, string btnSubmit)
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

                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        model.OpCode = 1;
                        TempData["ElectricitySubmit"] = "Save";
                    }
                    else
                    {
                        model.OpCode = 2;
                        TempData["ElectricitySubmit"] = "Update";
                    }
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                    model.MonthStartDate = Common.ConvertToSystemDate(model.MonthStartDate, "dd/mm/yyyy");
                    model.MonthEndDate = Common.ConvertToSystemDate(model.MonthEndDate, "dd/mm/yyyy");

                    //string[] datessp = model.MonthStartDate.Split("-");
                    //string _MonthStartDate = model.MonthStartDate + "-01";
                    //string _MonthEndDate = Convert.ToDateTime(_MonthStartDate).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                    //model.MonthStartDate = _MonthStartDate.Trim(); //Common.ConvertToSystemDate(_MonthStartDate, "dd/mm/yyyy");
                    //model.MonthEndDate = _MonthEndDate.Trim(); //Common.ConvertToSystemDate(_MonthEndDate, "dd/mm/yyyy");


                    DataSet ds = model.GetElectricity();
                    if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("AddElectricityBill", new { STPID = Crypto.Encrypt(model.Pk_STPId) });
                        }
                        else
                        {
                            TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("AddElectricityBill", new { STPID = Crypto.Encrypt(model.Pk_STPId) });
                        }
                    }

                }
                else
                {
                    return RedirectToAction("GetelectricityBillList");
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("GetelectricityBillList");
        }
        public ActionResult CheckValiddate(Deduction model)
        {
            try
            {
                model.MonthStartDate = Common.ConvertToSystemDate(model.MonthStartDate, "dd/mm/yyyy");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetValidDate();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                else
                {

                    model.Status = "0";
                    model.Message = "Something went wrong";
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

    }
}
