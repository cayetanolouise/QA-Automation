using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Xunit;
using Selenium_WebDriver_Test.Class;
public class SuiteLoginPageTests : IDisposable {
  public IWebDriver driver {get; private set;}
  public IDictionary<String, Object> vars {get; private set;}
  public IJavaScriptExecutor js {get; private set;}
  public SuiteLoginPageTests(bool isTestAll = false)
  {
        if (!isTestAll)
        {
            try
            {
                driver = DriverClass.GetDriver();
                js = (IJavaScriptExecutor)driver;
                vars = new Dictionary<String, Object>();
                driver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {
                StatusParam.code = 200;
                StatusParam.errorMessage = ex.Message.ToString();
                Console.WriteLine("failed " + StatusParam.errorMessage);
            }

        }
        LoginPage();
    }
  public void Dispose()
  {
    driver.Quit();
  }
  [Fact]
  public void LoginPage()
    {
        try
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".login_wrapper-inner")));
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".login_wrapper-inner"));
                Assert.True(elements.Count > 0);
            }
            Console.WriteLine("passed");
            StatusParam.code = 100;
        }
        catch (Exception ex)
        {
            StatusParam.code = 200;
            StatusParam.errorMessage = ex.Message.ToString();
            StatusParam.login = "failed";
            Console.WriteLine("failed " + StatusParam.errorMessage);
        }

    }
}
