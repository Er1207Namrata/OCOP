using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using System.Data;
using System.Reflection;

namespace OCOO.Controllers
{
    public class BillVerificationController : BaseController
    {
        public IActionResult Index(MeasurementAcceptance measurement, string abc)
        {

            #region ddlSTP
            List<SelectListItem> ddlSTP = new List<SelectListItem>();
            Dashboard dashboard = new Dashboard();
            dashboard.OpCode = 27;
            dashboard.Value = HttpContext.Session.GetString("Pk_UserId");

            DataSet dsSTP = dashboard.GetMasterData();
            if (dsSTP != null && dsSTP.Tables.Count > 0 && dsSTP.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsSTP.Tables[0].Rows)
                {
                    ddlSTP.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                }
            }
            ViewBag.ddlSTP = ddlSTP;
            #endregion

            #region ddlStatus
            List<SelectListItem> ddlStatus = new List<SelectListItem>();

            dashboard.OpCode = 26;
            DataSet dsStatus = dashboard.GetMasterData();
            if (dsStatus != null && dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsStatus.Tables[0].Rows)
                {
                    ddlStatus.Add(new SelectListItem { Text = row["Name"].ToString(), Value = row["Id"].ToString() });
                }
            }
            ViewBag.ddlStatus = ddlStatus;
            #endregion
            measurement.Pk_BillingId = Crypto.Decrypt(abc);
            return View(measurement);
        }

        public ActionResult ViewInvoiceDetail(MeasurementAcceptance model)
        {
            try
            {
                DataSet ds = new DataSet();
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                ds = model.GetViewBillDetail();
                List<ViewInvoiceDetails> lst = new List<ViewInvoiceDetails>();
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow items in ds.Tables[0].Rows)
                        {

                            ViewInvoiceDetails invoiceData = new ViewInvoiceDetails();
                             invoiceData.Pk_BillingId = items["Pk_BillingId"].ToString();

                            invoiceData.BillingDate = items["Dates"].ToString();
                            invoiceData.STPName = items["StpName"].ToString();
                            invoiceData.IsSame = items["IsSame"].ToString();
                            invoiceData.HeaderText = items["HeaderText"].ToString();
                            invoiceData.AmountOfMLDSTP = items["AmountOfMLDSTP"].ToString();
                            invoiceData.Capacity = items["Capacity"].ToString();
                            invoiceData.IsFlowUpdated = items["IsFlowUpdated"].ToString();
                            invoiceData.UpdatedColor = items["UpdatedColor"].ToString();
                            invoiceData.FlowUpdatedDate = items["FlowUpdatedDate"].ToString();
                            invoiceData.FlowUpdatedBy = items["FlowUpdatedBy"].ToString();
                            invoiceData.Waterdischarge = items["Waterdischarge"].ToString();
                            invoiceData.Treated_for_Payement = items["Treated_for_Payement"].ToString();
                            invoiceData.AsPerCBFixCharges_60 = items["AsPerCBFixCharges_60"].ToString();
                            invoiceData.IsFlowLD = items["IsFlowLD"].ToString();
                            invoiceData.FlowLDAmount = items["FlowLDAmount"].ToString();
                            invoiceData.AsPerActual_FixCharges_60 = items["AsPerActual_FixCharges_60"].ToString();

                            invoiceData.AsPerCBFixCharges_40 = items["AsPerCBFixCharges_40"].ToString();
                            invoiceData.AsPerActual_FixCharges_40 = items["AsPerActual_FixCharges_40"].ToString();
                            invoiceData.IsBODUpdated = items["IsBODUpdated"].ToString();
                            invoiceData.BODUpdatedDate = items["BODUpdatedDate"].ToString();
                            invoiceData.BODUpdatedBy = items["BODUpdatedBy"].ToString();
                            invoiceData.BODAmount = items["BODAmount"].ToString();
                            invoiceData.BODLDAmount = items["BODLDAmount"].ToString();
                            invoiceData.IsBODLD = items["IsBODLD"].ToString();
                            invoiceData.BODReportedValue = items["BODReportedValue"].ToString();
                            invoiceData.BODAmount = items["BODAmount"].ToString();
                            invoiceData.IsCODUpdated = items["IsCODUpdated"].ToString();
                            invoiceData.CODReportedValue = items["CODReportedValue"].ToString();
                            invoiceData.CODUpdatedDate = items["CODUpdatedDate"].ToString();
                            invoiceData.CODUpdatedBy = items["CODUpdatedBy"].ToString();
                            invoiceData.CODAmount = items["CODAmount"].ToString();
                            invoiceData.CODLDAmount = items["CODLDAmount"].ToString();
                            invoiceData.IsTSSUpdated = items["IsTSSUpdated"].ToString();
                            invoiceData.TSSReportedValue = items["TSSReportedValue"].ToString();
                            invoiceData.TSSUpdatedDate = items["TSSUpdatedDate"].ToString();
                            invoiceData.TSSUpdatedBy = items["TSSUpdatedBy"].ToString();
                            invoiceData.TSSAmount = items["TSSAmount"].ToString();
                            invoiceData.TSSLDAmount = items["TSSLDAmount"].ToString();
                            invoiceData.IsTSSLD = items["IsTSSLD"].ToString();
                      
                            invoiceData.IsFCUpdated = items["IsFCUpdated"].ToString();
                            invoiceData.FCReportedValue = items["FCReportedValue"].ToString();
                            invoiceData.FCUpdatedDate = items["FCUpdatedDate"].ToString();
                            invoiceData.FCUpdatedBy = items["FCUpdatedBy"].ToString();
                            invoiceData.FCAmount = items["FCAmount"].ToString();
                            invoiceData.FCLDAmount = items["FCLDAmount"].ToString();
                            invoiceData.IsFCLD = items["IsFCLD"].ToString();
                            invoiceData.ComplaintReceived = items["ComplaintReceived"].ToString();
                            invoiceData.ComplaintResolved = items["ComplaintResolved"].ToString();
                            invoiceData.IsCRUpdated = items["IsCRUpdated"].ToString();
                            invoiceData.ComplaintLDAmount = items["ComplaintLDAmount"].ToString();
                            invoiceData.CRUpdatedBy = items["CRUpdatedBy"].ToString();
                            invoiceData.TotalVerifiedAmount = items["TotalVerifiedAmount"].ToString();
                            invoiceData.TotalLDAmount = items["TotalLDAmount"].ToString();
                            invoiceData.TotalBillAmount = items["TotalBillAmount"].ToString();
                            invoiceData.Pk_BillingId = items["Pk_BillingId"].ToString();
                            invoiceData.TestingAgency = items["TestingAgency"].ToString();
                            invoiceData.TestingCountColor = items["TestingCountColor"].ToString();
                            invoiceData.TestingCount = items["TestingCount"].ToString();
                            lst.Add(invoiceData);

                        }

                    }
                }


                return Json(lst);
            }
            catch (Exception Ex)
            {
                throw;
            }

        }
        public JsonResult GetSTPDDl(string Id)
        {
            List<SelectListItem> ddlSTP = new List<SelectListItem>();
            try
            {
                #region ddlSTP
                Dashboard dashboard = new Dashboard();
                dashboard.OpCode = 27;
                dashboard.Value = Id;
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

        public async Task<JsonResult> FinalSubmit(MeasurementAcceptance model)
        {
           
            try
            {

                if (model.Documents != null)
                {
                    
                    model.DocumentName = await FileManagement.WriteFiles(model.Documents, "", "MeasurementAcceptance");
                }
                DataSet ds = new DataSet();
            
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                ds = model.VerifyPendingBill();
                model.msg= ds.Tables[0].Rows[0]["msg"].ToString();
                model.flag= ds.Tables[0].Rows[0]["flag"].ToString();
                return Json(model); 

            }
            catch (Exception Ex)
            {
                throw;
            }

        }
        public ActionResult BindVerifiedMonthlyBill(BillVerificationDetails model)
        {

            try
            {
                DataSet ds = new DataSet();
                ds = model.GetFirmVerificationDetails();
                List<BillVerificationDetails> lstBillVerificationDetails = new List<BillVerificationDetails>();
                if(ds!=null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count>0)
                    {
                        foreach (DataRow items in ds.Tables[0].Rows)
                        {
                            BillVerificationDetails billVerificationDetails = new BillVerificationDetails();
                            billVerificationDetails.HeaderText = items["HeaderText"].ToString();
                            billVerificationDetails.StpName = items["StpName"].ToString();
                            billVerificationDetails.Capacity = items["Capacity"].ToString();
                            billVerificationDetails.Status = items["Status"].ToString();
                            billVerificationDetails.VerifiedRemarks = items["VerifiedRemarks"].ToString();
                            billVerificationDetails.VerifiedDoc = items["VerifiedDoc"].ToString();
                            billVerificationDetails.DocFormat = items["DocFormat"].ToString();
                            billVerificationDetails.UserType = items["DocFormat"].ToString();
                            lstBillVerificationDetails.Add(billVerificationDetails);
                        }
                    }
                }
                return Json(lstBillVerificationDetails);
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
    }
    
}
