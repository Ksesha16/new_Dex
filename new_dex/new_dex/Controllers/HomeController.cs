using Microsoft.AspNetCore.Mvc;
using new_dex.Models;
using System.Diagnostics;

namespace new_dex.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Переход на страницу Регистриации
        public IActionResult RegistrationWindow()
        {
            return View("RegistrationWindow"); // Переход на представление "Registration"
        }

        //Переход на страницу Авторизации
        public IActionResult GoToSign()
        {
            return View("Index"); // Переход на действие "Index" (страницу "Авторизация")
        }
        public IActionResult MainMenu()
        {
            return View("MainMenu"); // Переход на действие "Index" (страницу "Авторизация")
        }
        public IActionResult ShopMenu()
        {
            return View("ShopMenu"); // Переход на действие "Index" (страницу "Авторизация")
        }
        public IActionResult UserProfile()
        {
            return View("UserProfile"); // Переход на действие "Index" (страницу "Авторизация")
        }
    }
}