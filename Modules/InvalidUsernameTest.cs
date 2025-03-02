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
using Newtonsoft.Json;
using System.IO;

public class SuiteInvalidUsernameTests : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<String, Object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }
    private dynamic credentials;

    public SuiteInvalidUsernameTests(bool isTestAll = false)
    {
        if (!isTestAll)
        {
            try
            {
                driver = DriverClass.GetDriver();
                js = (IJavaScriptExecutor)driver;
                vars = new Dictionary<String, Object>();
                driver.Manage().Window.Maximize();
                LoadCredentials();
            }
            catch (Exception ex)
            {
                StatusParam.code = 200;
                StatusParam.errorMessage = ex.Message.ToString();
                Console.WriteLine("failed " + StatusParam.errorMessage);
            }

        }
        RunTests();
    }

    private void LoadCredentials()
    {
        string json = File.ReadAllText("credentials.json");
        credentials = JsonConvert.DeserializeObject<dynamic>(json);
    }

    public void Dispose()
    {
        driver.Quit();
    }
    [Fact]

    public void RunTests()
    {
        Console.WriteLine("Testing invalid credentials...");
        List<string> loginMessages = new List<string>();

        foreach (var cred in credentials.invalidUsername)
        {
            InvalidUsername((string)cred.username, (string)cred.password);
            loginMessages.Add(StatusParam.login);
        }
        if (loginMessages.Count > 0)
        {
            StatusParam.login = string.Join("\n", loginMessages);
        }
    }

    private void InvalidUsername(string username, string password)
    {
        try
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"username\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).Click();
            driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).SendKeys(username);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"password\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).Click();
            driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).SendKeys(password);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"login-button\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"login-button\"]")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("*[data-test=\"error\"]")));
            driver.FindElement(By.CssSelector("*[data-test=\"error\"]")).Click();

            try
            {
                Assert.Equal(driver.FindElement(By.CssSelector("*[data-test=\"error\"]")).Text, "Epic sadface: Username and password do not match any user in this service");
                Console.WriteLine($"Passed: Access not granted for  Credetials: Username: {username} Password: {password}");
                StatusParam.code = 100;
                StatusParam.login = $"Access not granted for Credetials: Username: {username} Password: {password}";
            }
            catch
            {
                StatusParam.code = 200;
                StatusParam.errorMessage = $"Invalid Credetials: Username {username} Password {password} - Access Granted";
                StatusParam.login = $"Failed: Access not Granted for Credetials: Username: {username} Password: {password}";
                Console.WriteLine("Failed " + StatusParam.errorMessage);
                Console.WriteLine($"Failed: Access not Granted for Credetials: Username: {username} Password: {password}");
            }
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
