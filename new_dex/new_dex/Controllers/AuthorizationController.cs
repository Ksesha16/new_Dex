using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;

public class AuthorizationController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthorizationController(IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _hostingEnvironment = hostingEnvironment;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(string login, string password)
    {
        // Путь к файлу Excel
        string filePath = @"D:\dex\new_dex\Tabel\User.xlsx";

        // Установка контекста лицензирования
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Открытие пакета Excel
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Регистрация"];
            if (worksheet == null)
            {
                // Если лист не существует, перенаправление на страницу регистрации
                return RedirectToAction("RegistrationWindow", "Home");
            }

            // Получение последней заполненной строки в Excel
            int lastRow = worksheet.Dimension.Rows;
            login = "5";
            password = "5";

            // Проверка логина и пароля
            for (int row = 2; row <= lastRow; row++) // начинаем с 2, потому что 1 строка - это заголовки
            {
                if (worksheet.Cells[row, 1].Value.ToString() == login &&
                    worksheet.Cells[row, 2].Value.ToString() == password)
                {
                    // Пользователь найден, перенаправление на страницу UserProfile
                    return RedirectToAction("UserProfile", "Home");
                }
            }
        }

        // Пользователь не найден, возвращаемся на страницу входа с сообщением об ошибке
        ModelState.AddModelError("", "Неверный логин или пароль");
        return View();
    }





}
