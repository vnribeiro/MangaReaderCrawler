using MangaReaderCrawler.Application.Helper;
using MangaReaderCrawler.Application.Services.Interfaces;

namespace MangaReaderCrawler.Application.Services;

public class CrawlerService : ICrawlerService
{

    public void Crawl(string url)
    {
       var driver = Driver.Build(url);
    }
}