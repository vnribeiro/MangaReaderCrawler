using MangaReaderCrawler.Application.Services.Interfaces;
using MangaReaderCrawler.Infrastructure.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MangaReaderCrawler.Application.Services;

public class CrawlerService : ICrawlerService
{
    public void Crawl(string url)
    {
        var driver = Driver.Build(url);

        Console.WriteLine("Downloading...");

        Download(driver);
    }

    private void Download(ChromeDriver driver)
    {
        // Download the manga
        var page = 1;
        var LastUrl = driver.Url;
        var retryAttempts = 0;
        var maxAttempts = 30;

        var totalPages = driver
        .FindElement(By.CssSelector("#divslide > div.navi-buttons.hoz-controls.hoz-controls-ltr > div.nabu-page > span > span.hoz-total-image")).Text;

        while (page < int.Parse(totalPages))
        {
            Console.WriteLine($"Downloading page {page} of {totalPages}. Please wait...");

            var print = driver.Print(new PrintOptions
            {
                Orientation = PrintOrientation.Landscape,
                ScaleFactor = 100,
                OutputBackgroundImages = true,
            });

            // convert the base64 string to a byte array
            var pdfData = Convert.FromBase64String(print.AsBase64EncodedString);
            File.WriteAllBytes("printed_page.pdf", pdfData);

            Console.WriteLine($"The page {page} has been downloaded.");
        }
    }
}