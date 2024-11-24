using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SuperLaba
{
    public class FilePickerHelper
    {
        private readonly MainPage _mainPage;

        /// <summary>
        /// Конструктор, принимающий экземпляр MainPage.
        /// </summary>
        /// <param name="mainPage">Экземпляр MainPage для обновления пути.</param>
        public FilePickerHelper(MainPage mainPage)
        {
            _mainPage = mainPage;
        }

        /// <summary>
        /// Метод для вызова проводника и выбора XML файла.
        /// </summary>
        /// <returns>True, если файл успешно выбран и валиден, иначе False.</returns>
        public async Task<bool> PickXmlFileAsync()
        {
            try
            {
                // Создаем параметры для выбора только XML файлов
                var xmlFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.xml" } },  // iOS поддерживает UTType
                    { DevicePlatform.Android, new[] { "application/xml" } }, // MIME-тип
                    { DevicePlatform.WinUI, new[] { ".xml" } }, // Расширения файлов
                    { DevicePlatform.MacCatalyst, new[] { "public.xml" } }
                });

                var pickOptions = new PickOptions
                {
                    PickerTitle = "Оберіть XML файл",
                    FileTypes = xmlFileType
                };

                var result = await FilePicker.Default.PickAsync(pickOptions);

                if (result == null)
                {
                    
                    await Application.Current.MainPage.DisplayAlert("Відміна", "Файл не був обраний.", "ОК");
                    return false;
                }

                
                if (Path.GetExtension(result.FullPath).ToLower() != ".xml")
                {
                    await Application.Current.MainPage.DisplayAlert("Помилка", "Обраний файл не є XML.", "ОК");
                    return false;
                }

                
                if (!File.Exists(result.FullPath))
                {
                    await Application.Current.MainPage.DisplayAlert("Помилка", "Обраний файл не знайдено.", "ОК");
                    return false;
                }

                if (!IsValidXml(result.FullPath))
                {
                    await Application.Current.MainPage.DisplayAlert("Помилка", "Обраний файл містить не коректний XML.", "ОК");
                    return false;
                }

                // Передаем путь в MainPage
                _mainPage.SelectedFilePath = result.FullPath;
                await Application.Current.MainPage.DisplayAlert("Успіх", "Файл обрано успішно.", "ОК"); 

                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Помилка", $"Трапилась помилка: {ex.Message}", "ОК");
                return false;
            }
        }

        
        private bool IsValidXml(string filePath)
        {
            try
            {
                var xmlContent = File.ReadAllText(filePath);
                System.Xml.Linq.XDocument.Parse(xmlContent); 
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
