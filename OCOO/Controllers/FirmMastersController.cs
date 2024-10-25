using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using System.Data;

namespace OCOO.Controllers
{
    public class FirmMastersController : FirmBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult CallCenterMaster(CallCenter callCenter, string btnSubmit)
        {
            try
            {
                #region ddlStp
                List<SelectListItem> ddlStp = new List<SelectListItem>();
                callCenter.OpCode = 11;
                DataSet ds = callCenter.GetMasterData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ddlStp.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                    }

                }
                ViewBag.ddlStp = ddlStp;
                #endregion ddlStp
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        callCenter.OpCode = 2;
                    }
                    else
                    {
                        callCenter.OpCode = 3;
                    }
                    callCenter.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = callCenter.ManageCallCenter();
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
                        return RedirectToAction("CallCenterMaster");
                    }
                }
                else
                {

                    callCenter.OpCode = 1;
                    callCenter.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = callCenter.ManageCallCenter();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            callCenter.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(callCenter);
        }
        public ActionResult DeleteCallCenter(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    CallCenter callCenter = new CallCenter();
                    callCenter.Pk_CallCenterId = Crypto.Decrypt(id);
                    callCenter.OpCode = 4;
                    callCenter.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = callCenter.ManageCallCenter();
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
            return RedirectToAction("CallCenterMaster");
        }
        public ActionResult PumpStationMaster(PumpingStation pumpingStation, string btnSubmit)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSubmit))
                {
                    if (btnSubmit == "Submit")
                    {
                        pumpingStation.OpCode = 1;
                    }
                    else
                    {
                        pumpingStation.OpCode = 2;
                    }
                    pumpingStation.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    pumpingStation.FK_FirmId = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = pumpingStation.ManagePumpingStation();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("PumpStationMaster");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
                else
                {

                    pumpingStation.OpCode = 4;
                    pumpingStation.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = pumpingStation.ManagePumpingStation();
                    if (dataSet != null && dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            pumpingStation.dtDetails = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(pumpingStation);
        }
        public ActionResult DeletePumpingStation(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    PumpingStation pumpingStation = new PumpingStation();
                    pumpingStation.Pk_PumpingStationId = Crypto.Decrypt(id);
                    pumpingStation.OpCode = 3;
                    pumpingStation.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    DataSet dataSet = new DataSet();
                    dataSet = pumpingStation.ManagePumpingStation();
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
            return RedirectToAction("PumpStationMaster");
        }
      
    }
}
