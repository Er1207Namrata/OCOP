using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OCOO.Controllers
{
    public class FirmBaseController : Controller
    {
        //
        // GET: /Base/

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // code involving this.Session // edited to simplify

           // If the browser session or authentication session has expired...
            if (HttpContext.Session.GetString("Fk_UsertypeId") != "2" || HttpContext.Session.GetString("Pk_UserId") == "0")
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }
            if (HttpContext.Session.GetString("Pk_UserId") == "0" || HttpContext.Session.GetString("Pk_UserId") == null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}
