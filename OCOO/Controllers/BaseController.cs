﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OCOO.Controllers
{
    public class BaseController : Controller
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Session.GetString("Pk_UserId") == "0" || HttpContext.Session.GetString("Pk_UserId") == null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }
            if (HttpContext.Session.GetString("Fk_UsertypeId") == "0" || HttpContext.Session.GetString("Fk_UsertypeId") ==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                  new { action = "Login", Controller = "Home" }));

            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}