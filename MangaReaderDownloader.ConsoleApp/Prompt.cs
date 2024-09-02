using System.Text.RegularExpressions;
using MangaReaderDownloader.ConsoleApp.Application.Services.Interfaces;

namespace MangaReaderDownloader.ConsoleApp;

public class Prompt(IDownloaderService downloaderService)
{
    private const string _UrlRegex = @"^https?:\/\/mangareader\.to\/.*";
    private readonly IDownloaderService _downloader = downloaderService;

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

        // Call the download method of the DownloaderService
        _downloader.Download(imputUrl);
    }
}
