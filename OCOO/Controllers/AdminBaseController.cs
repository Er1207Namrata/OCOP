




using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OCOO.Filter;

namespace OCOO.Controllers
{
    public class AdminBaseController : Controller
    {
        //
        // GET: /Base/

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Session.GetString("Pk_UserId") == "0" || HttpContext.Session.GetString("Pk_UserId") == null || HttpContext.Session.GetString("Fk_UsertypeId") == null || HttpContext.Session.GetString("Fk_UsertypeId") == "0")
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }
            if (HttpContext.Session.GetString("Fk_UsertypeId") == "3" || HttpContext.Session.GetString("Fk_UsertypeId") == "2")
            {
                 filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                 new { action = "Login", Controller = "Home" }));

            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}
