using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;

namespace BehaviorDrivenWebUIAutomation.UITests
{
    public abstract class SeleniumTestBase
    {
        protected readonly string appName;
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;
        protected bool acceptNextAlert = true;

        protected SeleniumTestBase(string appName)
        {
            this.appName = appName;
        }

        [TestInitialize]
        public void SetupTest()
        {
            driver = CreateWebDriver();
            baseURL = ConfigurationManager.AppSettings[appName + ".URL"];
            verificationErrors = new StringBuilder();
        }

        private static IWebDriver CreateWebDriver()
        {
            switch ((ConfigurationManager.AppSettings["Browser"] ?? "Firefox").ToLowerInvariant())
            {
                case "firefox":
                    return new FirefoxDriver();
                case "ie":
                    return new InternetExplorerDriver();
                case "chrome":
                    return new ChromeDriver();
                case "phantomjs":
                    return new PhantomJSDriver();
            }

            return new FirefoxDriver();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        protected bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        protected string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
