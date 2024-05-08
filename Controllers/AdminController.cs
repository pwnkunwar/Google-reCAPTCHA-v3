using Microsoft.AspNetCore.Mvc;

namespace GoogleReCaptcha.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
