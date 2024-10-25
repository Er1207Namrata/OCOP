
using DTOs.APIDTO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Nancy.Session;
using NuGet.Protocol.Plugins;
using OCOO.Filter;
using OCOO.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Xml.Serialization;
using static iText.IO.Image.Jpeg2000ImageData;
using static OCOO.Models.Login;
namespace OCOO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPdf()
        {
            byte[] bytes = new byte[] { };

            string htmlFilePath = Path.Combine(_hostEnvironment.WebRootPath, "Payout Statement.html");
            string htmlContent = System.IO.File.ReadAllText(htmlFilePath);

            StringReader sr = new StringReader(htmlContent);
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
            return File(bytes, "application/pdf", DateTime.Now.Ticks.ToString() + ".pdf");

        }
        public ActionResult Login(Login login, string btnLogin)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnLogin))
                {
                    if (string.IsNullOrEmpty(login.LoginId) || string.IsNullOrEmpty(login.Password))
                    {
                        TempData["LoginMessage"] = "LoginId and password must be required.";
                        return RedirectToAction("Login");
                    }
                    IPHostEntry iPHostEntry = new IPHostEntry();
                    iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    login.IpAddress = iPHostEntry.AddressList[iPHostEntry.AddressList.Length - 1].ToString();
                    login.LoginId = login.LoginId.Trim();
                    login.Password = login.Password.Trim();
                    DataSet ds = login.CheckLogin();
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                HttpContext.Session.SetString("Pk_MonthId", ds.Tables[0].Rows[0]["Pk_MonthId"].ToString());
                                HttpContext.Session.SetString("Pk_UserId", ds.Tables[0].Rows[0]["Pk_UserId"].ToString());
                                HttpContext.Session.SetString("Fk_UsertypeId", ds.Tables[0].Rows[0]["Fk_UsertypeId"].ToString());
                                HttpContext.Session.SetString("LoginId", ds.Tables[0].Rows[0]["LoginId"].ToString());
                                HttpContext.Session.SetString("Zone", ds.Tables[0].Rows[0]["Zone"].ToString());
                                HttpContext.Session.SetString("ZoneId", ds.Tables[0].Rows[0]["ZoneId"].ToString());
                                HttpContext.Session.SetString("Name", ds.Tables[0].Rows[0]["Name"].ToString());
                                HttpContext.Session.SetString("FilePath", ds.Tables[0].Rows[0]["SubPath"].ToString());
                                HttpContext.Session.SetString("ShowFilePath", ds.Tables[0].Rows[0]["ShowPath"].ToString());
                                HttpContext.Session.SetString("Fk_DistrictId", ds.Tables[0].Rows[0]["Fk_CityId"].ToString());
                                HttpContext.Session.SetString("Fk_CityId", ds.Tables[0].Rows[0]["Fk_CityId"].ToString());
                                HttpContext.Session.SetString("Name", ds.Tables[0].Rows[0]["Name"].ToString());
                                HttpContext.Session.SetString("FilePath", ds.Tables[0].Rows[0]["SubPath"].ToString());
                                HttpContext.Session.SetString("ShowFilePath", ds.Tables[0].Rows[0]["ShowPath"].ToString());
                                HttpContext.Session.SetString("ProfilePic", ds.Tables[0].Rows[0]["ProfilePic"].ToString());
                                HttpContext.Session.SetString("Password", ds.Tables[0].Rows[0]["Password"].ToString());
                                HttpContext.Session.SetString("disabledSave_Update", ds.Tables[0].Rows[0]["disabledSave_Update"].ToString());

                                if (HttpContext.Session.GetString("Fk_UsertypeId") == "1" || HttpContext.Session.GetString("Fk_UsertypeId") == "4")
                                {
                                    HttpContext.Session.SetString("DesignationId", ds.Tables[0].Rows[0]["DesignationId"].ToString());
                                    HttpContext.Session.SetString("DepartmentId", ds.Tables[0].Rows[0]["DepartmentId"].ToString());
                                    HttpContext.Session.SetString("IsAccountVerify", ds.Tables[0].Rows[0]["IsAccountVerify"].ToString());
                                    HttpContext.Session.SetString("ZoneID", ds.Tables[0].Rows[0]["ZoneId"].ToString());

                                    if (HttpContext.Session.GetString("IsAccountVerify") == "0")
                                    {
                                        HttpContext.Session.Clear();
                                        HttpContext.Session.SetString("Pk_UserId", ds.Tables[0].Rows[0]["Pk_UserId"].ToString());
                                        HttpContext.Session.SetString("Name", ds.Tables[0].Rows[0]["Name"].ToString());
                                        HttpContext.Session.SetString("LoginId", ds.Tables[0].Rows[0]["LoginId"].ToString());
                                        HttpContext.Session.SetString("Password", ds.Tables[0].Rows[0]["Password"].ToString());
                                        return RedirectToAction("AccountVerification", "Home");
                                    }
                                    AdminRenderMenu();
                                    return RedirectToAction("Dashboard", "Admin");
                                }
                                else if (HttpContext.Session.GetString("Fk_UsertypeId") == "2")
                                {
                                    HttpContext.Session.SetString("IsGstUpdated", ds.Tables[0].Rows[0]["IsGstUpdated"].ToString());
                                    HttpContext.Session.SetString("IsDetailsUpdateded", ds.Tables[0].Rows[0]["IsDetailsUpdated"].ToString());
                                    HttpContext.Session.SetString("Pk_ZoneId", ds.Tables[0].Rows[0]["Pk_ZoneId"].ToString());
                                    AdminRenderMenu();
                                    return RedirectToAction("Dashboard", "Firm");
                                }
                                else if (HttpContext.Session.GetString("Fk_UsertypeId") == "3")
                                {
                                    HttpContext.Session.SetString("InspectionType", ds.Tables[0].Rows[0]["FK_InspectionId"].ToString());
                                    HttpContext.Session.SetString("Pk_ZoneId", ds.Tables[0].Rows[0]["Pk_ZoneId"].ToString());
                                    AdminRenderMenu();
                                    return RedirectToAction("Dashboard", "InspectionAgency");
                                }
                                else
                                {
                                    return RedirectToAction("Login");
                                    //AdminRenderMenu();
                                    //return RedirectToAction("GetFirm", "STC");
                                }
                            }
                            else
                            {
                                TempData["LoginMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                        else
                        {
                            TempData["LoginMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
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

        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                // HttpContext.Session.SetString("Pk_UserId", "");
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Directory()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult Event()
        {
            return View();
        }

        public ActionResult StpRegistrationValidation(FirmDetails model)
        {
            try
            {
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                DataSet ds = model.StpValidation();
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

        public ActionResult ChangePassword(Login login, string BtnChange)
        {
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(BtnChange))
                {

                    //login.OldPassword = Crypto.Encrypt(login.OldPassword);
                    //login.NewPassword = Crypto.Encrypt(login.NewPassword);
                    login.Fk_UserTypeId = int.Parse(HttpContext.Session.GetString("Fk_UsertypeId"));
                    if (login.Fk_UserTypeId == 18)
                    {
                        login.OpCode = 1;
                    }
                    else if (login.Fk_UserTypeId == 17)
                    {
                        login.OpCode = 2;
                    }
                    else
                    {
                        login.OpCode = 3;
                    }

                    login.Fk_UserId = Convert.ToInt32(HttpContext.Session.GetString("Pk_UserId"));
                    DataSet dataSet = login.PasswordChange();
                    login.dtDetails = dataSet.Tables[0];
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        TempData["ChangePassword"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {

                        TempData["ChangePassword"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        return RedirectToAction("ChangePassword", "Home");
                    }
                }

            }
            catch (Exception)
            {
                throw;
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
                        string fileName = await FileManagement.WriteFiles(profile.ProfilePic, "ProfilePic", "ProfilePic");
                        profile.Documents = fileName;
                    }

                    DataSet dataSet = profile.ProfileUpdate();
                    if (dataSet != null)
                    {
                        profile.dtDetails = dataSet.Tables[0];
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["code"] = dataSet.Tables[0].Rows[0]["Flag"].ToString();
                            TempData["msg"] = dataSet.Tables[0].Rows[0]["Message"].ToString();

                            if (HttpContext.Session.GetString("Fk_UsertypeId") == "18")
                            {
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (HttpContext.Session.GetString("Fk_UsertypeId") == "17")
                            {
                                return RedirectToAction("Dashboard", "Firm");
                            }
                            else
                            {
                                return RedirectToAction("Dashboard", "InspectionAgency");

                            }
                        }
                        else
                        {
                            TempData["code"] = dataSet.Tables[0].Rows[0]["Flag"].ToString();
                            TempData["msg"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
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
                    profile.GSTNo = dataset.Tables[0].Rows[0]["GSTNo"].ToString();
                }

                return View(profile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult BindMenu(string Division)
        {
            SetPermission permission = new SetPermission();
            try
            {
                permission.OpCode = 1;
                permission.Fk_MemId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = permission.BindMenuDetails();
                List<SetPermission> list = new List<SetPermission>();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                    {
                        SetPermission listdata = new SetPermission();
                        listdata.Text = dataSet.Tables[0].Rows[j]["Name"].ToString();
                        listdata.Id = dataSet.Tables[0].Rows[j]["Id"].ToString();

                        list.Add(listdata);
                    }
                }
                permission.BindList = list;
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(permission);
        }

        public ActionResult BindMenu1(string Division)
        {
            SetPermission permission = new SetPermission();
            try
            {
                permission.OpCode = 2;
                permission.Fk_MemId = HttpContext.Session.GetString("Pk_UserId");
                DataSet dataSet = permission.BindMenuDetails();
                List<SetPermission> list = new List<SetPermission>();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                    {
                        SetPermission listdata = new SetPermission();
                        listdata.Text = dataSet.Tables[0].Rows[j]["Name"].ToString();
                        listdata.Id = dataSet.Tables[0].Rows[j]["Id"].ToString();

                        list.Add(listdata);
                    }
                }
                permission.BindList = list;
            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(permission);
        }

        public ActionResult AdminRenderMenu()
        {
            Menu model = new Menu();
            try
            {
                List<Menu> list = new List<Menu>();
                List<Menu> list1 = new List<Menu>();
                DataSet dataset = new DataSet();
                if (HttpContext.Session.GetString("Fk_UsertypeId") == "1" || HttpContext.Session.GetString("Fk_UsertypeId") == "4")
                {
                    model.Fk_MemId = HttpContext.Session.GetString("Pk_UserId");
                    model.UserType = "Admin";
                }
                if (HttpContext.Session.GetString("Fk_UsertypeId") == "2")
                {

                    model.Fk_MemId = HttpContext.Session.GetString("Pk_UserId");
                    model.UserType = "Firm";
                }
                if (HttpContext.Session.GetString("Fk_UsertypeId") == "3")
                {
                    model.Fk_MemId = HttpContext.Session.GetString("Pk_UserId");
                    model.UserType = "InspectionAgency";
                }
                model.OpCode = 3;
                dataset = model.GetMenuDetails();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < dataset.Tables[0].Rows.Count; i++)
                    {
                        Menu ListData = new Menu();
                        ListData.MenuName = dataset.Tables[0].Rows[i]["MenuName"].ToString();
                        ListData.MenuId = dataset.Tables[0].Rows[i]["MenuId"].ToString();
                        ListData.Url = dataset.Tables[0].Rows[i]["Url"].ToString();
                        ListData.IsDropdown = dataset.Tables[0].Rows[i]["IsDropdown"].ToString();
                        ListData.Icon = dataset.Tables[0].Rows[i]["Icon"].ToString();
                        list.Add(ListData);
                    }
                    for (int j = 0; j <= dataset.Tables[1].Rows.Count - 1; j++)
                    {
                        Menu SubList = new Menu();
                        SubList.SubMenuName = dataset.Tables[1].Rows[j]["SubMenuName"].ToString();
                        //SubList.MenuName = dataset.Tables[1].Rows[j]["MenuName"].ToString();
                        SubList.Fk_MenuId = dataset.Tables[1].Rows[j]["Fk_MenuId"].ToString();
                        SubList.SubMenuId = int.Parse(dataset.Tables[1].Rows[j]["SubMenuId"].ToString());
                        SubList.Url = dataset.Tables[1].Rows[j]["Url"].ToString();

                        list1.Add(SubList);
                    }

                }
                model.menuList = list;
                model.subMenuList = list1;
                HttpContext.Session.SetObjectAsJson("List_of_menu", list);
                HttpContext.Session.SetObjectAsJson("List_of_submenu", list1);
                HttpContext.Session.SetObjectAsJson("_menu", model);
                HttpContext.Session.SetObjectAsJson("_submenu", model);

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return PartialView("_PartialLayout");
        }
        public JsonResult PumpStationDetails(string Id)
        {
            PumpingStationReport model = new PumpingStationReport();
            List<FirmDetails> dtList = new List<FirmDetails>();
            try
            {
                model.Fk_STPId = Id;
                DataSet ds = model.GetPumpingStationDetails();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FirmDetails list = new FirmDetails();
                        list.Pk_Id = ds.Tables[0].Rows[i]["Pk_Id"].ToString();
                        //list.STPName = ds.Tables[0].Rows[i]["StpName"].ToString();
                        //list.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        //list.PumpStationMeterLoad = ds.Tables[0].Rows[i]["MeterLoad"].ToString();
                        //list.AccountNo = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                        //list.BillingCycle = ds.Tables[0].Rows[i]["BillingCycle"].ToString();
                        list.PumpingStationName = ds.Tables[0].Rows[i]["PumpingStationName"].ToString();
                        list.SewerLength = ds.Tables[0].Rows[i]["SewerLength"].ToString();
                        list.DrainageName = ds.Tables[0].Rows[i]["DrainageName"].ToString();
                        list.PeekDesignedDischarge = ds.Tables[0].Rows[i]["PeekDesignedDischarge"].ToString();
                        list.PeekDesignedFactor = ds.Tables[0].Rows[i]["PeekDesignedFactor"].ToString();

                        dtList.Add(list);
                    }
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(dtList);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public JsonResult GetNotification()
        {
            Notification model = new Notification();
            List<Notification> dtList = new List<Notification>();
            try
            {
                model.Fk_userTypeId = HttpContext.Session.GetString("Fk_UsertypeId");
                //model.Pk_AdminId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_FirmId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
                model.Fk_ZoneId = HttpContext.Session.GetString("Pk_ZoneId");
                DataSet ds = model.GetNotification();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Notification list = new Notification();
                        list.Firm = ds.Tables[0].Rows[i]["Firm"].ToString();
                        list.Message = ds.Tables[0].Rows[i]["Message"].ToString();
                        list.STP = ds.Tables[0].Rows[i]["STP"].ToString();
                        list.Capacity = ds.Tables[0].Rows[i]["Capacity"].ToString();
                        list.Zone = ds.Tables[0].Rows[i]["zone"].ToString();
                        list.Time = ds.Tables[0].Rows[i]["date"].ToString();
                        list.DesignCapacity = ds.Tables[0].Rows[i]["DesignCapacity"].ToString();
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            list.Row_count = ds.Tables[1].Rows[0]["row_count"].ToString();
                            list.Fk_userTypeId = HttpContext.Session.GetString("Fk_UsertypeId");
                        }
                        dtList.Add(list);
                    }
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(dtList);
        }
        public JsonResult BillingDetails(string Id)
        {
            FirmBilldet model = new FirmBilldet();
            try
            {
                model.Pk_BillingId = Id;
                DataSet ds = model.GetBilling();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        model.BillingDate = ds.Tables[0].Rows[0]["BillingDate"].ToString();
                        model.STPName = ds.Tables[0].Rows[0]["StpName"].ToString();
                        model.WaterDischarge = ds.Tables[0].Rows[0]["Actual_Achieved"].ToString();

                        model.InHouseBOD = ds.Tables[0].Rows[0]["InHouseBOD"].ToString();
                        model.InHouseCOD = ds.Tables[0].Rows[0]["InHouseCOD"].ToString();
                        model.InHouseTSS = ds.Tables[0].Rows[0]["InHouseTSS"].ToString();
                        model.InHouseFC = ds.Tables[0].Rows[0]["InHouseFC"].ToString();

                        model.ThirdPartyBOD = ds.Tables[0].Rows[0]["ThirdPartyBOD"].ToString();
                        model.ThirdPartyCOD = ds.Tables[0].Rows[0]["ThirdPartyCOD"].ToString();
                        model.ThirdPartyTSS = ds.Tables[0].Rows[0]["ThirdPartyTSS"].ToString();
                        model.ThirdPartyFC = ds.Tables[0].Rows[0]["ThirdPartyFC"].ToString();

                        model.UPPCBBOD = ds.Tables[0].Rows[0]["UPPCBBOD"].ToString();
                        model.UPPCBCOD = ds.Tables[0].Rows[0]["UPPCBCOD"].ToString();
                        model.UPPCBTSS = ds.Tables[0].Rows[0]["UPPCBTSS"].ToString();
                        model.UPPCBFC = ds.Tables[0].Rows[0]["UPPCBFC"].ToString();

                        model.IsInhouse = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsInhouse"]);
                        model.IsTPI = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTPI"]);
                        model.IsUPPCB = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsUPPCB"]);

                        model.InHouseBODColor = ds.Tables[0].Rows[0]["InHouseBODColor"].ToString();
                        model.InHouseCODColor = ds.Tables[0].Rows[0]["InHouseCODColor"].ToString();
                        model.InHouseFCColor = ds.Tables[0].Rows[0]["InHouseFCColor"].ToString();
                        model.InHouseTSSColor = ds.Tables[0].Rows[0]["InHouseTSSColor"].ToString();
                        model.TPIBODColor = ds.Tables[0].Rows[0]["TPIBODColor"].ToString();
                        model.TPICODColor = ds.Tables[0].Rows[0]["TPICODColor"].ToString();
                        model.TPIFCColor = ds.Tables[0].Rows[0]["TPIFCColor"].ToString();
                        model.TPITSSColor = ds.Tables[0].Rows[0]["TPITSSColor"].ToString();
                        model.UPPCBBODColor = ds.Tables[0].Rows[0]["UPPCBBODColor"].ToString();
                        model.UPPCBCODColor = ds.Tables[0].Rows[0]["UPPCBCODColor"].ToString();
                        model.UPPCBFCColor = ds.Tables[0].Rows[0]["UPPCBFCColor"].ToString();
                        model.UPPCBTSSColor = ds.Tables[0].Rows[0]["UPPCBTSSColor"].ToString();

                    }
                }

            }
            catch (Exception Ex)
            {
                TempData["ErrorMessage"] = Ex.Message;
            }
            return Json(model);
        }

        public ActionResult OfficeOfDirectorateOCOP()
        {
            return View();
        }
        public ActionResult MinistryAndSecretariat()
        {
            return View();
        }

        public ActionResult GetVisitorCount()
        {
            string? VisitorCount = "00001";
            try
            {

                Login login = new Login();
                DataSet ds = login.GetVisitorCount();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        VisitorCount = ds.Tables[0].Rows[0]["VisitorCounter"].ToString();
                    }
                }
                return Json(VisitorCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult AccountVerification(FirmDetails model)
        {
            DataSet ds = new DataSet();
            model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
            model.LoginId = HttpContext.Session.GetString("LoginId");
            model.Name = HttpContext.Session.GetString("Name");
            return View(model);
        }

        public JsonResult UpdateAccountVerification(string LoginId, string Password, string NewPassword)
        {
            DataSet ds = new DataSet();
            FirmDetails model = new FirmDetails();
            model.Fk_EmpId = HttpContext.Session.GetString("Pk_UserId");
            model.LoginId = LoginId;
            //model.EmailId = LoginId;
            model.Password = Password;
            model.NewPassword = NewPassword;
            ds = model.GetVeifyAccount();
            string message = "";
            int flag = 0;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    message = ds.Tables[0].Rows[0]["Message"].ToString();
                    flag = 1;
                }
                else
                {
                    message = ds.Tables[0].Rows[0]["Message"].ToString();
                    flag = 0;
                }
            }
            return Json(new { message = message, flag = flag });

        }

        public void ManualInsert()
        {
            UPJNDataService _UPJNDataService = new UPJNDataService();
            DataSet ds = new DataSet();
            ds = new FirmDetails().Gettype();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string xmlString = item["RequestBody"].ToString(); //"<UPJNAPIDTO xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><stationId>15</stationId><data><UPJNDataDTO><deviceId>1</deviceId><parameters><ParameterDTO><parameter>cod</parameter><value>77.41</value><unit>mg/l</unit><timestamp>1712169039</timestamp><flag>U</flag></ParameterDTO><ParameterDTO><parameter>BOD</parameter><value>27.29</value><unit>mg/l</unit><timestamp>1712169039</timestamp><flag>U</flag></ParameterDTO><ParameterDTO><parameter>tss</parameter><value>29.07</value><unit>mg/l</unit><timestamp>1712169039</timestamp><flag>U</flag></ParameterDTO><ParameterDTO><parameter>ph</parameter><value>7.7</value><unit>ph</unit><timestamp>1712169039</timestamp><flag>U</flag></ParameterDTO><ParameterDTO><parameter>flow</parameter><value>160.32</value><unit>m3</unit><timestamp>1712169039</timestamp><flag>U</flag></ParameterDTO></parameters></UPJNDataDTO></data></UPJNAPIDTO>";
                    List<STPsOCEMSDTO> _ObjectOfOCEMS = new List<STPsOCEMSDTO>();
                    XmlSerializer serializer = new XmlSerializer(typeof(_UPJNAPIDTO));
                    using (StringReader reader = new StringReader(xmlString))
                    {
                        _UPJNAPIDTO upjnDto = (_UPJNAPIDTO)serializer.Deserialize(reader);
                        STPsOCEMSDTO sTPsOCEMSDTO = new STPsOCEMSDTO();
                        sTPsOCEMSDTO.StationId = Convert.ToInt32(upjnDto.stationId);                        
                        sTPsOCEMSDTO.BOD = (float)upjnDto.data.UPJNDataDTO.parameters.Where(y => y.parameter.ToLower() == "bod").Select(x=>x.value).FirstOrDefault();
                        sTPsOCEMSDTO.COD = (float)upjnDto.data.UPJNDataDTO.parameters.Where(y => y.parameter.ToLower() == "cod").Select(x => x.value).FirstOrDefault();
                        sTPsOCEMSDTO.TSS = (float)upjnDto.data.UPJNDataDTO.parameters.Where(y => y.parameter.ToLower() == "tss").Select(x => x.value).FirstOrDefault();
                        sTPsOCEMSDTO.PH = (float)upjnDto.data.UPJNDataDTO.parameters.Where(y => y.parameter.ToLower() == "ph").Select(x => x.value).FirstOrDefault();
                        sTPsOCEMSDTO.Flow = (float)upjnDto.data.UPJNDataDTO.parameters.Where(y => y.parameter.ToLower() == "flow").Select(x => x.value).FirstOrDefault();
                        sTPsOCEMSDTO.ReportedDate = upjnDto.data.UPJNDataDTO.parameters.FirstOrDefault().timestamp;
                        _ObjectOfOCEMS.Add(sTPsOCEMSDTO);
                        string SingleXMLdata = _UPJNDataService.SerializeToXml(_ObjectOfOCEMS);
                        string XMLdata = _UPJNDataService.SerializeToXml(upjnDto);

                       // DataSet data = _UPJNDataService.SaveUPJN_Data(XMLdata, SingleXMLdata);
                    }
                }
            }
        }
    }
}
