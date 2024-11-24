using System.Xml.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Microsoft.Maui.Controls;
using System.Collections;
using System.IO;
using Microsoft.Maui.Graphics.Text;

namespace SuperLaba
{
    public partial class MainPage : ContentPage
    {
        private XmlParserContext _parserContext;
        public string SelectedFilePath { get; set; }
        public MainPage()
        {
            InitializeComponent();
            _parserContext = new XmlParserContext(new SaxXmlParser());

        }
        private async void OnConvertToHtmlClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SelectedFilePath))
                {
                    await DisplayAlert("Помилка", "Спочатку оберіть XML файл.", "ОК");
                    return;
                }

                
                var htmlFilePath = Path.ChangeExtension(SelectedFilePath, ".html");

               
                TransformHTML.TransformXmlToHtml(SelectedFilePath, htmlFilePath);

                await DisplayAlert("Успіх", $"Файл перетворено в HTML: {htmlFilePath}", "ОК");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"Трапилась помилка : {ex.Message}", "ОК");
            }
        }//конвертація в HTML
        private void OnParserChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!e.Value) return;

            var selectedParser = (sender as RadioButton)?.Content.ToString();
            switch (selectedParser)
            {
                case "SAX":
                    _parserContext.SetParser(new SaxXmlParser());
                    break;
                case "DOM":
                    _parserContext.SetParser(new DomXmlParser());
                    break;
                case "LINQ to XML":
                    _parserContext.SetParser(new LinqToXmlParser());
                    break;
            }
        }//вибір парсеру
        private async void OnParseButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var attributeInput = AttributeEntry.Text;
                var keywordInput = KeywordEntry.Text;

                if (string.IsNullOrWhiteSpace(attributeInput) || string.IsNullOrWhiteSpace(keywordInput))
                {
                    await DisplayAlert("Помилка", "Потрібно ввести хоча б один атрибут та кейворд", "ОК");
                    return;
                }

               
                var attributes = attributeInput.Split(',').Select(a => a.Trim()).ToList();
                var keywords = keywordInput.Split(',').Select(k => k.Trim()).ToList();

                if (string.IsNullOrWhiteSpace(SelectedFilePath))
                {
                    await DisplayAlert("Помилка", "Спочатку оберіть файл для аналізу", "ОК");
                    return;
                }

               
                var results = _parserContext.ExecuteParse(SelectedFilePath, attributes, keywords);

                ResultLabel.Text = results.Any()
                    ? string.Join("\n\n", results)
                    : "Нічого не знайдено.";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"Трапилась помилка: {ex.Message}", "ОК");
            }
        }//парсинг документу 
        private async void OnLoadDataClicked(object sender, EventArgs e)
        {
            var filePicker = new FilePickerHelper(this);
            bool isFilePicked = await filePicker.PickXmlFileAsync();

        }//метод для вибору файлу XML через провідник
        private void OnClearClicked(object sender, EventArgs e)
        {
            ResultLabel.Text = string.Empty;

        }//метод для очистки результату
        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool shouldExit = await DisplayAlert("Підтвердження виходу", "Ви впевнені, що хочете вийти?", "Так", "Ні");

            if (shouldExit)
            {
                Application.Current.Quit(); 
            }
        } //метод для виходу з програми

    }
}


