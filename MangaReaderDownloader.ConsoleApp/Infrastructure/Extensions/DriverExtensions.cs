using MangaReaderDownloader.ConsoleApp.Infrastructure.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MangaReaderDownloader.ConsoleApp.Infrastructure.Extensions;

public static class DriverExtensions
{
    /// <summary>
    /// Finds the first element in the current web driver's page that matches the specified <paramref name="by"/> condition.
    /// </summary>
    /// <param name="driver">The web driver instance.</param>
    /// <param name="by">The condition used to find the element.</param>
    /// <param name="timeoutInSeconds">The maximum time to wait for the element to be found, in seconds.</param>
    /// <returns>The first <see cref="IWebElement"/> that matches the specified condition.</returns>
    public static IWebElement WaitUntilFindElement(this IWebDriver driver, By by, int timeoutInSeconds, int maxAttempts = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        return wait.Until(ExpectedConditions.ElementIsVisible(by));
    }

    /// Retrieves the filtered page title from the specified <see cref="IWebDriver"/> instance.
    /// </summary>
    /// <param name="driver">The <see cref="IWebDriver"/> instance.</param>
    /// <returns>The filtered page title.</returns>
    public static string GetFilteredPageTitle(this IWebDriver driver)
    {
        var mangaTitle = driver.Title;
        mangaTitle = mangaTitle.Replace("Read ", "");
        var title = mangaTitle.Replace(" in English Online Free", "");
        return AppUtils.FilterPath(title);
    }
}