using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OCOO.Models;

namespace OCOO.Filter
{
    public class MenuAuthorizationFilter : IActionFilter
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public MenuAuthorizationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsAjaxRequest())
            {
                if (_session != null)
                {
                    if (_session.GetString("List_of_menu") != null && _session.GetString("List_of_submenu") != null)
                    {
                        List<Menu>? menuList = _session.GetObjectFromJson<List<Menu>>("List_of_menu");
                        List<Menu>? subMenuList = _session.GetObjectFromJson<List<Menu>>("List_of_submenu");
                        string ActionUrl = "/" + context.ActionDescriptor.RouteValues["controller"].ToString() + "/" + context.ActionDescriptor.RouteValues["action"].ToString();
                        if (!subMenuList.Where(x => x.Url.ToLower() == ActionUrl.ToLower()).Any())
                        {
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "Error",
                                action = "Notauthorized",
                                returnUrl = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value
                                : context.HttpContext.Request.PathBase.Value
                           }));
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        private bool IsAjaxRequest()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }

}
