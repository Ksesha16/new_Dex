using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;

public class RegistrationController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public RegistrationController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost]
    public IActionResult Register(string login, string password, string email)
    {
        // Путь к файлу Excel
        string filePath = @"D:\dex\new_dex\Tabel\User.xlsx";

        // Установка контекста лицензирования
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Создание нового пакета Excel
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                // Если лист не существует, создаем новый
                worksheet = package.Workbook.Worksheets.Add("Users");
                // Добавляем заголовки столбцов
                worksheet.Cells[1, 1].Value = "Логин";
                worksheet.Cells[1, 2].Value = "Пароль";
                worksheet.Cells[1, 3].Value = "Email";
            }

            // Получение последней заполненной строки в Excel
            int lastRow = worksheet.Dimension.Rows;

            // Определение следующей строки для записи данных
            int nextRow = lastRow + 1;

            // Запись данных в Excel
            worksheet.Cells[nextRow, 1].Value = login;
            worksheet.Cells[nextRow, 2].Value = password;
            worksheet.Cells[nextRow, 3].Value = email;

            // Сохранение пакета Excel
            package.Save();
        }

        // Дополнительный код после сохранения данных

        return RedirectToAction("Index", "Home"); // Перенаправление на другую страницу
    }

}

