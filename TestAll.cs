using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OfficeOpenXml;
using OpenQA.Selenium;
using System.Web;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium_WebDriver_Test;
using SeleniumExtras.WaitHelpers;
using System.Net.Mime;
using Xunit;
using Selenium_WebDriver_Test.Class;
using System.Net.Mail;

public class SuiteTestsAll : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<String, Object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }

    public SuiteTestsAll()
    {

        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Template\SauceDemoResult.xlsx");
        FileInfo templateFile = new FileInfo(templatePath);
        FileInfo file = new FileInfo(templatePath);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelPackage excelPackage = new OfficeOpenXml.ExcelPackage(file);
        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
        string outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Output");
        Directory.CreateDirectory(outputFile);
        string newPathLoc = Path.Combine(outputFile, DateTime.Now.ToString("yyyyMMdd") + " SauceDemoResult.xlsx");

        int validateCount = 0;
        int startRow = 0;

        string[] modules = { "LOGIN", "INVALIDUSERNAME", "INVALIDPASSWORD", "VALIDUSERNAMEANDPASSWORD", "LOGOUT"};

        for (int i = 0; i < modules.Length; i++)
        {

            switch (modules[i])
            {
                case "LOGIN":
                    SuiteLoginPageTests loginPageVerification = new SuiteLoginPageTests(false);
                    if (StatusParam.code == 100)
                    {
                        startRow = 2;
                        worksheet.Cells["D" + startRow].Value = "Working as expected";
                        worksheet.Cells["E" + startRow].Value = "Passed";
                        worksheet.Cells["F" + startRow].Value = DateTime.Now.ToString("yyyy/MM/dd");
                        startRow++;
                    }
                    else
                    {
                        startRow = 2;
                        worksheet.Cells["D" + startRow].Value = "Not working as expected - " + StatusParam.errorMessage; ;
                        worksheet.Cells["E" + startRow].Value = "Failed";
                        worksheet.Cells["F" + startRow].Value = DateTime.Now.ToString("yyyy/MM/dd");
                        startRow++;
                    }
                    break;
                case "INVALIDUSERNAME":
                    SuiteInvalidUsernameTests verifyInvalidUsername = new SuiteInvalidUsernameTests();
                    break;
                case "INVALIDPASSWORD":
                    SuiteInvalidPasswordTests verifyinvalidPassword = new SuiteInvalidPasswordTests();
                    startRow++;
                    break;
                case "VALIDUSERNAMEANDPASSWORD":
                    SuiteValidUsernameandPasswordTests verifyValidUsernameandPassword = new SuiteValidUsernameandPasswordTests();
                    startRow++;
                    break;
                case "LOGOUT":
                    SuitelogoutTests verifyLogoutButton = new SuitelogoutTests();
                    startRow++;
                    break;
            }
            if (StatusParam.code == 100)
            {
                worksheet.Cells["D" + startRow].Value = "Working as expected - " + StatusParam.login;
                worksheet.Cells["E" + startRow].Value = "Passed";
                worksheet.Cells["F" + startRow].Value = DateTime.Now.ToString("yyyy/MM/dd");
            }
            else
            {
                worksheet.Cells["D" + startRow].Value = "Not working as expected - " + StatusParam.errorMessage;
                worksheet.Cells["E" + startRow].Value = "Failed";
                worksheet.Cells["F" + startRow].Value = DateTime.Now.ToString("yyyy/MM/dd");
            }
        }

        excelPackage.SaveAs(new FileInfo(newPathLoc));
    }
    public void Dispose()
    {
        driver.Quit();
    }
}