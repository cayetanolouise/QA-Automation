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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
public class SuitelogoutTests : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<String, Object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }
    public SuitelogoutTests(bool isTestAll = false)
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
        Logout();
    }
    public void Dispose()
    {
        driver.Quit();
    }
    [Fact]
    public void Logout()
    {
        try
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"username\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).Click();
            driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).SendKeys("standard_user");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"password\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).Click();
            driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).SendKeys("secret_sauce");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"login-button\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"login-button\"]")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("contents_wrapper")));
            driver.FindElement(By.Id("contents_wrapper")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"inventory-container\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"inventory-container\"]")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("react-burger-menu-btn")));
            driver.FindElement(By.Id("react-burger-menu-btn")).Click();

            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".bm-item-list")));
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".bm-item-list"));
                Assert.True(elements.Count > 0);
            }

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"logout-sidebar-link\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"logout-sidebar-link\"]")).Click();

            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".login_wrapper-inner")));
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".login_wrapper-inner"));
                Assert.True(elements.Count > 0);
            }
            Console.WriteLine("passed");
            StatusParam.code = 100;
            StatusParam.login = $"User logged out successfully.";
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
