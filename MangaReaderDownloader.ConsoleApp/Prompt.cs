using System.Text.RegularExpressions;
using MangaReaderCrawler.Application.Services.Interfaces;

namespace MangaReaderCrawler.ConsoleApp;

public class Prompt(ICrawlerService crawlerService)
{
  private const string _UrlRegex = @"^https?:\/\/mangareader\.to\/.*";
  private readonly ICrawlerService _crawler = crawlerService;

  public void Run()
  {
    var imputUrl = string.Empty;

    while (true)
    {
      Console.Write("Enter the URL of the manga you want to download: ");
      imputUrl = Console.ReadLine()!;

      // Validate input using Regex
      if (Regex.IsMatch(imputUrl, _UrlRegex))
      {
        Console.WriteLine($"You entered a valid URL: {imputUrl}");
        break; // Exit the loop if input is valid
      }
      else
      {
        Console.WriteLine("Invalid URL. Please try again.");
      }
    }

    // Call the Crawl method of the CrawlerService
    _crawler.Crawl(imputUrl);
  }
}
