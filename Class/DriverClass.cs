using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_WebDriver_Test.Class
{
    public class DriverClass
    {
        private static IWebDriver driver;
        private static readonly object lockObject = new object();

        public static IWebDriver GetDriver()
        {
            lock (lockObject)
            {
                if (driver == null)
                {
                    ChromeOptions options = new ChromeOptions();
                    driver = new ChromeDriver();
                }
                return driver;
            }
        }
    }
}
