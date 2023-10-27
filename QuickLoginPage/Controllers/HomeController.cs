using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using QuickLoginPage.Models;
using QuickLoginPage.Services;
using ServiceStack;
using System.Diagnostics;

namespace QuickLoginPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public HomeController(ILogger<HomeController> logger,LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
        }

        public IActionResult Index()
        {
            ViewBag.Welcome = _localization.GetKey("Welcome");
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}