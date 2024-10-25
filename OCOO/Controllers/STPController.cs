using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;

namespace OCOO.Controllers
{
    public class STPController : FirmBaseController
    {
        public ActionResult StpList(STPModel model,string Id)
        {
            try
            {
                DataSet dataset = new DataSet();
                
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
                TempData["firm"] = model.Fk_FirmId;
                TempData["stp"] = model.Fk_STPId;
                model.Pk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetSTPList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
               
                return View(model);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;return View();
            }

        }

        public ActionResult StpRegistration(int Id)
        {

            try
            {
                STPModel model = new STPModel();

                #region ddlPumpingStationCount
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
                #endregion ddlPumpingStationCount

                #region ddlCity


                List<SelectListItem> ddlCity = new List<SelectListItem>();
                model.OpCode = 21;
                model.Value = HttpContext.Session.GetString("Pk_ZoneId");
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

                string tokenNo = "0001";
                 DataSet dsTken = model.GetFirmRegistrationToken();
                if (dsTken.Tables[0].Rows.Count > 0)
                {
                    tokenNo=dsTken.Tables[0].Rows[0]["Token"].ToString();
                }
                ViewBag.TOkenNo = tokenNo;

                    model.OpCode = 3;
                model.Pk_STPId = Id;
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
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
                        model.IsFC = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFC"]);
                        model.IsCOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCOD"]);
                        model.IsBOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBOD"]);
                        model.IsTSS = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTSS"]);
                        model.IsInhouse = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsInhouse"]);
                        model.IsUPPCB = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsUPPCB"]);
                        model.IsTPI = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTPI"]);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; return View();
            }

        }

        #region
        //[HttpPost]
        //public ActionResult StpRegistration(STPModel model, string btnSubmit)
        //{
        //    try
        //    {
        //        #region ddlPumpingStationCount
        //        List<SelectListItem> PumpingStationCount = new List<SelectListItem>();
        //        model.OpCode = 13;
        //        DataSet dataset1 = model.GetMasterData();
        //        if (dataset1 != null && dataset1.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dataset1.Tables[0].Rows)
        //            {
        //                PumpingStationCount.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
        //            }
        //        }
        //        ViewBag.PumpingStationCount = PumpingStationCount;
        //        #endregion ddlPumpingStationCount

        //        #region ddlCity
        //        List<SelectListItem> ddlCity = new List<SelectListItem>();
        //        model.OpCode = 21;
        //        model.Value = HttpContext.Session.GetString("Pk_ZoneId");
        //        DataSet dataset = model.GetMasterData();
        //        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dataset.Tables[0].Rows)
        //            {
        //                ddlCity.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
        //            }
        //        }
        //        ViewBag.ddlCity = ddlCity;
        //        #endregion ddlCity

        //        if (!string.IsNullOrEmpty(btnSubmit) && btnSubmit == "SaveSTP")
        //        {
        //            model.OpCode = 1;
        //            model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
        //            if (model.IsFC == false)
        //            {
        //                model.FC = "0";
        //            }
        //            if (model.IsTSS == false)
        //            {
        //                model.IsTSS = true;
        //            }
        //            if (model.IsBOD == false)
        //            {
        //                model.IsBOD = true;
        //            }
        //            if (model.IsCOD == false)
        //            {
        //                model.IsCOD = true;
        //            }
        //            if (model.IsInhouse == false)
        //            {
        //                model.IsInhouse = true;
        //            }
        //            if (model.IsUPPCB == false)
        //            {
        //                model.IsUPPCB = true;

        //            }
        //            if (model.IsTPI == false)
        //            {
        //                model.IsTPI = true;
        //            }
        //            DataSet ds = model.StpRegistration();

        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
        //                {
        //                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    if (!string.IsNullOrEmpty(model.PumpingStation) && model.PumpingStation != "0")
        //                    {

        //                        for (int i = 1; i <= Convert.ToInt32(Request.Form["Count"]); i++)
        //                        {

        //                            model.SPSName = Request.Form["SPSName_" + i].ToString();
        //                            model.ElectricityMeterLoad = Request.Form["PumpStationMeterLoad_" + i].ToString();
        //                            model.PumpingStatisonAccountNo = Request.Form["PumpingStatisonAccountNo_" + i].ToString();
        //                            model.BillingCycle = Request.Form["PumpingBillCycle_" + i].ToString();
        //                            model.DrainageName = Request.Form["DrainageName_" + i].ToString();
        //                            model.SewerLength = Request.Form["SewerLength_" + i].ToString();
        //                            model.PeekDesignedDischarge = Request.Form["PeekDesignedDischarge_" + i].ToString();
        //                            model.PeekDesignedFactor = Request.Form["PeekDesignedFactor_" + i].ToString();
        //                            model.Pk_STPId = int.Parse(ds.Tables[0].Rows[0]["Pk_STPId"].ToString());
        //                            model.OpCode = 2;
        //                            if (!string.IsNullOrEmpty(model.SPSName))
        //                            {
        //                                model.IsSPS = true;
        //                                DataSet ds1 = model.StpRegistration();
        //                            }
        //                        }

        //                    }

        //                }
        //                else
        //                {
        //                    TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    return View(model);
        //                }
        //            }
        //        }

        //        else
        //        {
        //            model.OpCode = 7;
        //            model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
        //            if (model.IsFC == false)
        //            {
        //                model.FC = "0";
        //            }
        //            if (model.IsTSS == false)
        //            {
        //                model.IsTSS = true;
        //            }
        //            if (model.IsBOD == false)
        //            {
        //                model.IsBOD = true;
        //            }
        //            if (model.IsCOD == false)
        //            {
        //                model.IsCOD = true;
        //            }
        //            if (model.IsInhouse == false)
        //            {
        //                model.IsInhouse = true;
        //            }
        //            if (model.IsUPPCB == false)
        //            {
        //                model.IsUPPCB = true;

        //            }
        //            if (model.IsTPI == false)
        //            {
        //                model.IsTPI = true;
        //            }
        //            DataSet ds = model.StpRegistration();
        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
        //                {
        //                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();

        //                }
        //                else
        //                {

        //                    TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
        //                    return View(model);

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        TempData["ErrorMessage"] = Ex.Message;
        //    }
        //    return Json(model);
        //}
        #endregion

        [HttpPost]
        public ActionResult StpRegistration(STPModel model, string btnSubmit)
        {
            try
            {
                #region ddlCity 
                List<SelectListItem> ddlCity = new List<SelectListItem>();
                model.OpCode = 21;
                model.Value = HttpContext.Session.GetString("Pk_ZoneId");
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

                model.OpCode = 1;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                //if (model.IsFC == false)
                //{
                //    model.FC = "0";
                //}

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
                //if (model.IsFC == false)
                //{
                //    model.IsFC = true;
                //}
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
                ////if (model.IsBillingBOD == "No")
                //{
                //    model.BOD = model.NewBOD;
                //}
                //if (model.IsBillingCOD == "No")
                //{
                //    model.COD = model.NewCOD;
                //}
                //if (model.IsBillingTSS == "No")
                //{
                //    model.TSS = model.NewTSS;
                //}
                DataSet ds = model.StpRegistration();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        //if (!string.IsNullOrEmpty(model.PumpingStation) && model.PumpingStation != "0")
                        //{
                        //    for (int i = 1; i <= Convert.ToInt32(Request.Form["Count"]); i++)
                        //    {
                        //        model.SPSName = Request.Form["SPSName_" + i].ToString();
                        //        model.ElectricityMeterLoad = Request.Form["PumpStationMeterLoad_" + i].ToString();
                        //        model.PumpingStatisonAccountNo = Request.Form["PumpingStatisonAccountNo_" + i].ToString();
                        //        model.BillingCycle = Request.Form["PumpingBillCycle_" + i].ToString();
                        //        model.DrainageName = Request.Form["DrainageName_" + i].ToString();
                        //        model.SewerLength = Request.Form["SewerLength_" + i].ToString();
                        //        model.PeekDesignedDischarge = Request.Form["PeekDesignedDischarge_" + i].ToString();
                        //        model.PeekDesignedFactor = Request.Form["PeekDesignedFactor_" + i].ToString();
                        //        model.Pk_STPId = int.Parse(ds.Tables[0].Rows[0]["Pk_STPId"].ToString());
                        //        model.OpCode = 2;
                        //        if (!string.IsNullOrEmpty(model.SPSName))
                        //        {
                        //            model.IsSPS = true;
                        //            DataSet ds1 = model.StpRegistration();
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return RedirectToAction("StpList");
        }

        [HttpPost]
        public ActionResult SaveMainPumpingStation(string PumpingStationName, string Name, string MeterLoad, string AccountNo, string BillCycle, string PeakDischarge, string PeakFactor, string DrainageName, string SewerLength,string hddnTokenNo,bool IsMain,string STPName)
        {
            try
            {
                STPModel model = new STPModel();
                model.STPName = STPName;
                model.Name = Name;
                model.Fk_ParentId = PumpingStationName;
                model.PumpStationMeterLoad = MeterLoad;
                model.PumpingStatisonAccountNo = AccountNo;
                model.BillingCycle = BillCycle;
                model.PeakDischarge = PeakDischarge;
                model.PeakFactor = PeakFactor;
                model.DrainageName = DrainageName;
                model.SewerLength = SewerLength; 
                model.hddnTokenNo = hddnTokenNo;
                model.IsMain = IsMain;
                model.OpCode = 1;
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.SavePumpingStation();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
                return Json(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; return View();
            }

        }

        [HttpPost]
        public JsonResult SPSRegistration(string SPSName, string MeterLoad, string AccountNo, string BillCycle, string DrainageName, string SewerLength, string PeakDesign, string PeakFactor)
        {
            STPModel model = new STPModel();
            try
            {
                model.SPSName = SPSName;
                model.ElectricityMeterLoad = MeterLoad;
                model.PumpingStatisonAccountNo = AccountNo;
                model.BillingCycle = BillCycle;
                model.DrainageName = DrainageName;
                model.SewerLength = SewerLength;
                model.PeekDesignedDischarge = PeakDesign;
                model.PeekDesignedFactor = PeakFactor;
                DataSet ds1 = model.SaveSPS();
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        TempData["Message"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public JsonResult GetPumpingStationDropDown(string TokenNo)
        {
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            try
            {
                FirmDetails model = new FirmDetails();
               // model.Value = HttpContext.Session.GetString("Pk_UserId");
                model.OpCode = 22;
                model.Value = TokenNo;// HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.GetMasterData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }
                }
                return Json(ddlDesignation);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(ddlDesignation);

        }
        public JsonResult PumpingStationDropDown(string Data)
        {
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            try
            {
                FirmDetails model = new FirmDetails();
                model.Value = Data;
                model.OpCode = 23;
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


        public JsonResult PumpStationList(string Id,string TokenNo)
        {
            List<STPModel> dtList = new List<STPModel>();
            try
            {
                STPModel model = new STPModel();
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                model.hddnTokenNo = TokenNo;
                DataSet ds = model.GetPumpingStationList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        STPModel list = new STPModel();
                        list.Pk_Id = ds.Tables[0].Rows[i]["Pk_Id"].ToString();
                        list.PumpingStationName = ds.Tables[0].Rows[i]["PName"].ToString();
                        list.SPSName = ds.Tables[0].Rows[i]["Name"].ToString();
                        list.ElectricityMeterLoad = ds.Tables[0].Rows[i]["MeterLoad"].ToString();
                        list.ElectricityAccountNo = ds.Tables[0].Rows[i]["ElectricityAccountNumber"].ToString();
                        list.BillingCycle = ds.Tables[0].Rows[i]["ElectricityBillCycle"].ToString();
                        list.SewerLength = ds.Tables[0].Rows[i]["FlowFromSewer"].ToString();
                        list.DrainageName = ds.Tables[0].Rows[i]["FlowFromDrain"].ToString();
                        list.PeekDesignedDischarge = ds.Tables[0].Rows[i]["PeakDesignDischarge"].ToString();
                        list.PeekDesignedFactor = ds.Tables[0].Rows[i]["PeakFactor"].ToString();
                        list.IsMain =Convert.ToBoolean( ds.Tables[0].Rows[i]["IsMain"]);

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

        public JsonResult DeletePumpingStation(string Id)
        {

            try
            {
            STPModel model = new STPModel();
            model.Pk_Id = Id;
            DataSet ds = model.DeletePumpingStation();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Json(TempData["Message"]);
        }

        public ActionResult DeleteSTP(string Id)
        {

            try
            {
                STPModel model = new STPModel();
                model.Pk_Id = Id;
                DataSet ds = model.DeleteSTP();
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
            
            return RedirectToAction("StpList");
        }


        public ActionResult GetStpByID(string ID)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetSTPDDl2(string Id)
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
    }
}
