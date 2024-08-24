using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MangaReaderCrawler.Application.Helper;

public static class Driver
{
  public static ChromeDriver Build(string url)
  {
    // Chrome in headless mode
    var chromeOptions = new ChromeOptions();
    //chromeOptions.AddArguments("--headless=new");
    var driver = new ChromeDriver(chromeOptions);

    // navigate to the url
    driver.Navigate().GoToUrl(url);

    //configurate cookies
    const string MRSettings = $"{{%22readingMode%22:%22horizontal%22%2C%22readingDirection%22:%22ltr%22%2C%22quality%22:%22high%22%2C%22hozPageSize%22:1%2C%22show_comments_at_home%22:%221%22}}";
    const string PubConsent = "YAAAAAAAAAAA";
    const string EuConsent = "CQDzioAQDzioAAZACBENBDFsAP_gAH_gAAAAKYtV_G__bWlr8X73aftkeY1P9_h77sQxBhfJE-4FzLvW_JwXx2ExNA36tqIKmRIAu3bBIQNlGJDUTVCgaogVryDMak2coTNKJ6BkiFMRO2dYCF5vmwtj-QKY5vr991dx2B-t7dr83dzyz4VHn3a5_2a0WJCdA5-tDfv9bROb-9IOd_x8v4v8_F_rE2_eT1l_tevp7D9-cts7_XW-9_fff79Ln_-uB_--Cl4BJhoVEAZYEhIQaBhBAgBUFYQEUCAAAAEgaICAEwYFOwMAl1hIgBACgAGCAEAAKMgAQAAAQAIRABAAUCAACAQKAAMACAYCAAgYAAQAWAgEAAIDoEKYEECgWACRmREKYEIQCQQEtlQgkAQIK4QhFngAQCImCgAAAAAKwABAWCwOJJASoSCBLiDaAAAgAQCCACoQScmAAIAzZag8GTaMrSANHzBIhpgGAAAA.YAAAAAAAAAAA";

    var CookieSettings = new Cookie("mr_settings", MRSettings);
    driver.Manage().Cookies.AddCookie(CookieSettings);

    var PubContentV2 = new Cookie("pubconsent-v2", PubConsent);
    driver.Manage().Cookies.AddCookie(PubContentV2);

    var EuConsentV2Cookie = new Cookie("euconsent-v2", EuConsent);
    driver.Manage().Cookies.AddCookie(EuConsentV2Cookie);

    // refresh the page
    driver.Navigate().Refresh();

    return driver;
  }
}