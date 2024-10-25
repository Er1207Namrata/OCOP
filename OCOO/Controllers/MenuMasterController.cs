using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using System.Data;
using System.Reflection;

namespace OCOO.Controllers
{
    public class MenuMasterController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        #region Menu Master
        public ActionResult MenuMaster(string Id)
        {
            try
            {
                DataSet ds = new DataSet();
                Menu model = new Menu();
                if (Id != null)
                {

                    model.MenuId = Crypto.Decrypt(Id);
                }
                model.OpCode = 4;

                ds = model.GetMenuMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.MenuId = ds.Tables[0].Rows[0]["MenuId"].ToString();
                    model.MenuName = ds.Tables[0].Rows[0]["MenuName"].ToString();
                }

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveMenuMaster(Menu model, string btnSubmit)
        {
            try
            {
                DataSet ds = new DataSet();
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
                    ds = model.GetMenuMaster();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                {
                                    TempData["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                    return RedirectToAction("MenuMaster");
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = ds.Tables[0].Rows[0]["Message"].ToString();
                                    return RedirectToAction("MenuMaster");
                                }
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                
            }
            return RedirectToAction("MenuMaster");
        }

        public ActionResult MenuList(Menu model)
        {
            try
            {
                DataSet ds = new DataSet();
                if (model.Page == 0 || model.Page == null)
                {
                    model.Page = 1;
                }
                model.Size = SessionManager.Size;
                model.OpCode = 4;
                ds = model.GetMenuMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                int totalRecords = 0;
                if (model.dtDetails.Rows.Count > 0)
                {
                    totalRecords = Convert.ToInt32(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult DeleteMenu(string Id)
        {
            DataSet ds = new DataSet();
            Menu model = new Menu();
            try
            {
               
                model.OpCode = 3;
                model.MenuId = Crypto.Decrypt(Id);
                model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                ds = model.GetMenuMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                
            }
            return RedirectToAction("MenuMaster");
        }
        #endregion

        #region Sub Menu Master
        public ActionResult SubMenuMaster(string Id)
        {
            SubMenu model = new SubMenu();
            try
            {
                #region ddlMenu
                List<SelectListItem> ddlMenu = new List<SelectListItem>();
                Menu obj = new Menu();
                DataSet ds = new DataSet();
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
                #endregion

                
                if (Id != null)
                {
                    model.SubMenuId = int.Parse(Crypto.Decrypt(Id));
                    model.OpCode = 5;
                    ds = model.SubMenuMaster();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        model.SubMenuId = int.Parse(ds.Tables[0].Rows[0]["SubMenuId"].ToString());
                        model.MenuId = ds.Tables[0].Rows[0]["MenuId"].ToString();
                        model.SubMenuName = ds.Tables[0].Rows[0]["SubMenuName"].ToString();
                    }
                }
                model.OpCode = 4;
                ds = model.SubMenuMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveSubMenuMaster(SubMenu model, string btnSubmit)
        {
            try
            {
                #region ddlMenu
                List<SelectListItem> ddlMenu = new List<SelectListItem>();
                Menu obj = new Menu();
                DataSet ds = new DataSet();
                obj.OpCode = 4;
                ds = obj.GetMenuMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMenu.Add(new SelectListItem { Text = "Select Menu", Value = "0" });
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        ddlMenu.Add(new SelectListItem { Text = item["MenuName"].ToString(), Value = item["MenuId"].ToString() });
                    }
                }
                ViewBag.ddlMenu = ddlMenu;
                #endregion

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
                    ds = model.SubMenuMaster();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
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
               
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                
            }
            return RedirectToAction("SubMenuMaster");
        }

        public ActionResult SubMenuList(SubMenu model)
        {
            try
            {
                DataSet ds = new DataSet();
                if (model.Page == 0 || model.Page == null)
                {
                    model.Page = 1;
                }
                model.OpCode = 4;
                model.Size = SessionManager.Size;
                ds = model.SubMenuMaster(); 
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.dtDetails = ds.Tables[0];
                }
                int totalRecords = 0;
                if (model.dtDetails.Rows.Count > 0)
                {
                    totalRecords = Convert.ToInt32(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult DeleteSubMenu(string Id)
        {
            SubMenu model = new SubMenu();
            DataSet ds = new DataSet();
            try
            {
                if (Id != null)
                {
                    
                    
                    model.OpCode = 3;
                    model.SubMenuId = int.Parse(Crypto.Decrypt(Id));
                    model.AddedBy = HttpContext.Session.GetString("Pk_UserId");
                    ds = model.SubMenuMaster();
                    if (ds != null)
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
                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View("SubMenuMaster");
            }
            return RedirectToAction("SubMenuMaster");
        }
        #endregion



    }
}
