using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OCOO.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult Notauthorized()
        {
            return View();
        }
    }
}
