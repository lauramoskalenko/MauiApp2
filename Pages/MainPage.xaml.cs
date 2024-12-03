using Microsoft.Maui.Controls;
using System.Xml.Xsl;
using XmlProcessorApp;
using System.Xml;

namespace MauiApp2.Pages
{
    public partial class MainPage : ContentPage
    {
        private string? FilePath { get; set; }
        private readonly XmlProcessingContext _context;

        public MainPage()
        {
            InitializeComponent();
            _context = new XmlProcessingContext(new SaxXmlAnalyzer());
        }

        private async void OnLoadFileClicked(object sender, EventArgs e)
        {
            try
            {
                FilePath = Path.Combine(FileSystem.AppDataDirectory, "scientists.xml");

                if (!File.Exists(FilePath))
                {
                    using var stream = await FileSystem.OpenAppPackageFileAsync("scientists.xml");
                    using var newFile = File.Create(FilePath);
                    await stream.CopyToAsync(newFile);
                }

                await DisplayAlert("File Loaded", $"Loaded file: {FilePath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load file: {ex.Message}", "OK");
            }
        }

        private async void OnAnalyzeClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FilePath))
                {
                    await DisplayAlert("Error", "No file loaded.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(KeywordEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter a keyword.", "OK");
                    return;
                }

                string selectedMethod = AnalyzerPicker.SelectedItem?.ToString() ?? "";
                if (selectedMethod == "SAX")
                    _context.SetAnalyzer(new SaxXmlAnalyzer());
                else if (selectedMethod == "DOM")
                    _context.SetAnalyzer(new DomXmlAnalyzer());
                else if (selectedMethod == "LINQ")
                    _context.SetAnalyzer(new LinqXmlAnalyzer());
                else
                {
                    await DisplayAlert("Error", "Please select an analysis method.", "OK");
                    return;
                }

                string result = _context.ExecuteAnalysis(FilePath, KeywordEntry.Text);
                OutputLabel.Text = result;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Analysis failed: {ex.Message}", "OK");
            }
        }

        private async void OnTransformClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FilePath))
                {
                    await DisplayAlert("Error", "No file loaded.", "OK");
                    return;
                }

                string htmlFilePath = Path.Combine(FileSystem.AppDataDirectory, "output.html");

                using var xsltStream = await FileSystem.OpenAppPackageFileAsync("transform.xsl");
                string xsltFilePath = Path.Combine(FileSystem.AppDataDirectory, "transform.xsl");

                using (var fileStream = File.Create(xsltFilePath))
                {
                    await xsltStream.CopyToAsync(fileStream);
                }

                var xslt = new XslCompiledTransform();
                xslt.Load(xsltFilePath);
                xslt.Transform(FilePath, htmlFilePath);

                await DisplayAlert("Success", $"Transformed to: {htmlFilePath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Transformation failed: {ex.Message}", "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            KeywordEntry.Text = string.Empty;
            OutputLabel.Text = string.Empty;
            AnalyzerPicker.SelectedIndex = -1;
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Exit", "Do you really want to exit?", "Yes", "No");
            if (confirm)
            {
                System.Environment.Exit(0);
            }
        }
    }
}