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
using TechTalk.SpecFlow;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Support
{
    public class SeleniumContext : IDisposable
    {
        private IWebDriver driver;
        public string AppName { get; private set; }
        public string BaseURL { get; private set; }

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    driver = CreateWebDriver();
                return driver;
            }
        }

        public SeleniumContext()
        {
            AppName = FeatureContext.Current.FeatureInfo.Title; //HACK: we get the application name from the feature title. Normally you only test one application from a single project
            BaseURL = ConfigurationManager.AppSettings[AppName + ".URL"];
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

        public void Dispose()
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver = null;
                }
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
