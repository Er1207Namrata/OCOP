using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OCOO.Controllers
{
    public class STCBaseController : AdminBaseController
    {
        //
        // GET: /Base/

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // code involving this.Session // edited to simplify

            // If the browser session or authentication session has expired...
            if (HttpContext.Session.GetString("Fk_UsertypeId") == "18" || HttpContext.Session.GetString("Fk_UsertypeId") == "17" || HttpContext.Session.GetString("Pk_UserId") == "0")
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Logout", Controller = "Home" }));
            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}
