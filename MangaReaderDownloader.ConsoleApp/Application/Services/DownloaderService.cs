using MangaReaderCrawler.Infrastructure.Config;
using MangaReaderDownloader.ConsoleApp.Application.Services.Interfaces;
using MangaReaderDownloader.ConsoleApp.Infrastructure.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MangaReaderDownloader.ConsoleApp.Application.Services
{
    public class DownloaderService : IDownloaderService
    {
        private const string TotalPagesXpath = "#divslide > div.navi-buttons.hoz-controls.hoz-controls-ltr > div.nabu-page > span > span.hoz-total-image";

        public void Download(string url)
        {
            var driver = Driver.Build(url);

            Console.WriteLine("Downloading...");

            try
            {
                MangaMiner(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }

        private static void MangaMiner(ChromeDriver driver)
        {
            var page = 1;
            var retryAttempts = 0;
            var maxAttempts = 30;

            var totalPages = driver.WaitForElement(By.CssSelector(TotalPagesXpath), 5).Text;
            
            while (page <= int.Parse(totalPages))
            {
                Console.WriteLine($"Downloading page {page} of {totalPages}. Please wait...");

                var print = driver.Print(new PrintOptions
                {
                    Orientation = PrintOrientation.Landscape,
                    ScaleFactor = 2.0,
                    OutputBackgroundImages = true,
                });

                // Convert the base64 string to a byte array
                var pdfData = Convert.FromBase64String(print.AsBase64EncodedString);

                // Save each page with a unique name
                var fileName = $"printed_page_{page}.pdf";
                var currentDirectory = Directory.GetCurrentDirectory();
                var path = Path.Combine(currentDirectory, "volumes", fileName);
                File.WriteAllBytes(fileName, pdfData);

                Console.WriteLine($"Page {page} has been downloaded as {fileName}.");

                // Move to the next page
                page++;
            }
        }
    }
}