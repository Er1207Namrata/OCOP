using Microsoft.AspNetCore.Mvc;
using OCOO.Models;
using System.Data;

namespace OCOO.Controllers
{
    public class FirmBillingDetailController : FirmBaseController
    {
        public ActionResult ChangePassword(Login login, string BtnChange)
        {
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(BtnChange))
                {
                    login.Fk_UserTypeId = int.Parse(HttpContext.Session.GetString("Fk_UsertypeId"));
                    if (login.Fk_UserTypeId == 2)
                    {
                        login.OpCode = 2;
                    }

                    login.Fk_UserId = Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId"));
                    DataSet dataSet = login.PasswordChange();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

                    DataSet dataSet = profile.FirmProfileUpdate();
                    if (dataSet != null)
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            profile.dtDetails = dataSet.Tables[0];
                        }
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (!string.IsNullOrEmpty(profile.Documents))
                            {
                                HttpContext.Session.SetString("ProfilePic", profile.Documents);
                            }
                            if (!string.IsNullOrEmpty(profile.GSTNo))
                            {
                                HttpContext.Session.SetString("IsGstUpdated", "1");
                            }
                            if (HttpContext.Session.GetString("Fk_UsertypeId") == "1")
                            {
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "2")
                            {
                                TempData["Message"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                return RedirectToAction("Dashboard", "Firm");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "3")
                            {
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
                DataSet dataset = profile.FirmProfileUpdate();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    profile.Name = dataset.Tables[0].Rows[0]["Name"].ToString();
                    profile.MobileNo = dataset.Tables[0].Rows[0]["MobileNo"].ToString();
                    profile.Email = dataset.Tables[0].Rows[0]["Email"].ToString();
                    profile.Documents = dataset.Tables[0].Rows[0]["ProfilePic"].ToString();
                    profile.Pk_AdminId = dataset.Tables[0].Rows[0]["Pk_AdminId"].ToString();
                    profile.GSTNo = dataset.Tables[0].Rows[0]["GSTNo"].ToString();

                }

                return View(profile);
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return View(profile);
        }

    }
}
