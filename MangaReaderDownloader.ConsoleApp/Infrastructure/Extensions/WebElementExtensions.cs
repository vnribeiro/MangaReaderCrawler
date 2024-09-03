using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MangaReaderDownloader.ConsoleApp.Infrastructure.Extensions;

public static class WebElementExtensions
{
    /// <summary>
    /// Finds the first element in the current web driver's page that matches the specified <paramref name="by"/> condition.
    /// </summary>
    /// <param name="driver">The web driver instance.</param>
    /// <param name="by">The condition used to find the element.</param>
    /// <param name="timeoutInSeconds">The maximum time to wait for the element to be found, in seconds.</param>
    /// <returns>The first <see cref="IWebElement"/> that matches the specified condition.</returns>
    public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeoutInSeconds, int maxAttempts = 10)
    {
        while (maxAttempts > 0)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            catch (WebDriverTimeoutException)
            {
                maxAttempts--;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        throw new NoSuchElementException($"Element with locator {by} was not found after multiple attempts.");
    }
}