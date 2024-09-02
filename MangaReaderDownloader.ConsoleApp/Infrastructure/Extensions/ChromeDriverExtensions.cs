using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MangaReaderDownloader.ConsoleApp.Infrastructure.Extensions;

public static class ChromeDriverExtensions
{
    /// <summary>
    /// Finds the first element in the current web driver's page that matches the specified <paramref name="by"/> condition.
    /// </summary>
    /// <param name="driver">The web driver instance.</param>
    /// <param name="by">The condition used to find the element.</param>
    /// <param name="timeoutInSeconds">The maximum time to wait for the element to be found, in seconds.</param>
    /// <returns>The first <see cref="IWebElement"/> that matches the specified condition.</returns>
    public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
    {
        if (timeoutInSeconds > 0)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => drv.FindElement(by));
        }
        
        return driver.FindElement(by);
    }
}