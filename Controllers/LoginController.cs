using GoogleReCaptcha.Configuration;
using GoogleReCaptcha.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleReCaptchaConfig.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly GoogleCaptchaService _captchaService;
        public LoginController(GoogleCaptchaService captchaService )
        {
            _captchaService = captchaService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            var captchaResult = await _captchaService.VerifyToken(login.Token);
            if (!captchaResult)
            {
                return BadRequest();
            }
            else if (login.Email == "viper@gmail.com" && login.Password =="Password" && captchaResult == true)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

       
    }
}
