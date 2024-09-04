using MangaReaderCrawler.Infrastructure.Config;
using MangaReaderDownloader.ConsoleApp.Application.Services.Interfaces;
using MangaReaderDownloader.ConsoleApp.Infrastructure.Extensions;
using MangaReaderDownloader.ConsoleApp.Infrastructure.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MangaReaderDownloader.ConsoleApp.Application.Services
{
    public class DownloaderService : IDownloaderService
    {
        private const string TotalPagesXpath = "#divslide > div.navi-buttons.hoz-controls.hoz-controls-ltr > div.nabu-page > span > span.hoz-total-image";
        private const string NextButton = "hozNextImage()";
        private const string PreviousButton = "hozPrevImage()";
    
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

                // Wait for user input before closing the console
                Console.WriteLine("press any key to exit...");
                Console.Read();
            }
        }

        private static void MangaMiner(ChromeDriver driver)
        {
            var page = 1;
            var totalPages = driver.WaitUntilFindElement(By.CssSelector(TotalPagesXpath), 10).Text;

            while (page <= int.Parse(totalPages))
            {
                Console.WriteLine($"Downloading page {page} of {totalPages}. Please wait...");

                var print = driver.Print(new PrintOptions
                {
                    Orientation = PrintOrientation.Landscape,
                    ScaleFactor = 1.0,
                    OutputBackgroundImages = true,
                });

                // Convert the base64 string to a byte array
                var data = Convert.FromBase64String(print.AsBase64EncodedString);

                // Save each page with a unique name
                var path = AppUtils.CreateDirectoryIfNotExist($"Downloads/{driver.GetFilteredPageTitle()}");
                path = Path.Combine(path, $"{page}.pdf");
                File.WriteAllBytes(path, data);

                Console.WriteLine($"The page {page} has been downloaded successfully.");

                // Move to the next page 
                // and increment the page counter
                page++;
                driver.ExecuteScript(NextButton);
            }
        }
    }
}