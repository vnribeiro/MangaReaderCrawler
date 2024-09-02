using MangaReaderDownloader.ConsoleApp;
using MangaReaderDownloader.ConsoleApp.Application.Services;
using MangaReaderDownloader.ConsoleApp.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddTransient<IDownloaderService, DownloaderService>()
    .BuildServiceProvider();

var crawler = serviceProvider.GetRequiredService<IDownloaderService>();
var prompt = new Prompt(crawler);
prompt.Run();