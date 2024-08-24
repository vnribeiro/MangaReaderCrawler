using MangaReaderCrawler.Application.Services;
using MangaReaderCrawler.Application.Services.Interfaces;
using MangaReaderCrawler.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddTransient<ICrawlerService, CrawlerService>()
    .BuildServiceProvider();

var crawler = serviceProvider.GetRequiredService<ICrawlerService>();
var prompt = new Prompt(crawler);
prompt.Run();