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
        string filePath = @"C:\Users\antox\source\repos\new_Dex\new_dex\Tabel\User.xlsx";

        // Чтение данных из файла Excel
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet != null)
            {
                // Поиск соответствия логина и пароля в файле Excel
                int rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++) // Начинаем с 2 строки, так как первая строка содержит заголовки
                {
                    string excelLogin = worksheet.Cells[row, 1]?.Value?.ToString();
                    string excelPassword = worksheet.Cells[row, 2]?.Value?.ToString();

                    if (excelLogin == login && excelPassword == password)
                    {
                        // Логин и пароль найдены, перенаправляем на главное меню
                        return RedirectToAction("MainMenu", "Home");
                    }
                }
            }
        }

        // Логин и пароль не найдены, отображаем ошибку
        ViewBag.ErrorMessage = "Неверный логин или пароль";
        return View("Login");
    }



}
