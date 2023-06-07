using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;

namespace new_dex.Controllers
{
    public class UserProfileController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SaveProfile(string name, string bankCard, string address, string email, string telephone, IFormFile photo)
        {
            // Путь к файлу Excel
            string filePath = @"D:\dex\new_dex\Tabel\User.xlsx";

            // Установка контекста лицензирования
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Открытие пакета Excel
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Создаем новый лист только если он еще не существует
                string newSheetName = "Личный кабинет пользователей";
                ExcelWorksheet worksheet = package.Workbook.Worksheets[newSheetName];

                if (worksheet == null)
                {
                    // Если лист не существует, создаем новый и добавляем заголовки столбцов
                    worksheet = package.Workbook.Worksheets.Add(newSheetName);
                    worksheet.Cells[1, 1].Value = "Имя";
                    worksheet.Cells[1, 2].Value = "Банковская Карта";
                    worksheet.Cells[1, 3].Value = "Адрес";
                    worksheet.Cells[1, 4].Value = "Email";
                    worksheet.Cells[1, 5].Value = "Телефон";
                }

                // Получение последней заполненной строки в Excel
                int lastRow = worksheet.Dimension?.Rows ?? 1;

                // Определение следующей строки для записи данных
                int nextRow = (worksheet.Dimension == null) ? 2 : lastRow + 1;

                // Записываем данные
                worksheet.Cells[nextRow, 1].Value = name;
                worksheet.Cells[nextRow, 2].Value = bankCard;
                worksheet.Cells[nextRow, 3].Value = address;
                worksheet.Cells[nextRow, 4].Value = email;
                worksheet.Cells[nextRow, 5].Value = telephone;

                // Сохраняем пакет Excel
                package.Save();
            }

            if (photo != null)
            {
                // Определить путь сохранения файла
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", photo.FileName);

                // Сохранить файл на диск
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                // Путь к файлу Excel для сохранения пути к файлу
                string picturePath = @"C:\Users\antox\source\repos\new_Dex\new_dex\Tabel\Picture.xlsx";

                // Открыть или создать новый файл Excel
                using (ExcelPackage picturePackage = new ExcelPackage(new FileInfo(picturePath)))
                {
                    string pictureSheetName = "Пути к фотографиям";
                    ExcelWorksheet pictureWorksheet = picturePackage.Workbook.Worksheets[pictureSheetName];

                    if (pictureWorksheet == null)
                    {
                        // Если лист не существует, создаем новый и добавляем заголовок столбца
                        pictureWorksheet = picturePackage.Workbook.Worksheets.Add(pictureSheetName);
                        pictureWorksheet.Cells[1, 1].Value = "Путь к файлу";
                    }

                    // Получение последней заполненной строки в Excel
                    int lastRow = pictureWorksheet.Dimension?.Rows ?? 1;

                    // Определение следующей строки для записи данных
                    int nextRow = (pictureWorksheet.Dimension == null) ? 2 : lastRow + 1;

                    // Записываем данные
                    pictureWorksheet.Cells[nextRow, 1].Value = savePath;

                    // Сохраняем пакет Excel
                    picturePackage.Save();
                }
            }

            // Установка сообщения об успешном сохранении данных
            TempData["Message"] = "Данные успешно сохранены";

            // Перенаправление на другую страницу
            return RedirectToAction("UserProfile", "Home");
        }
    }

}
