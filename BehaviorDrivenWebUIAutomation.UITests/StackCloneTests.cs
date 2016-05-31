using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace BehaviorDrivenWebUIAutomation.UITests
{
    [TestClass]
    public class StackCloneTests : SeleniumTestBase
    {
        public StackCloneTests() : base("StackClone")
        {
        }

        [TestMethod]
        public void AskAQuestion()
        {
            string questionTitle = "Test question " + Guid.NewGuid().ToString("N");

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            driver.Navigate().GoToUrl(baseURL + "/#/");
            wait.Until(d => d.FindElement(By.LinkText("Home")));
            Assert.AreEqual("Stackoverflow | Stamplay app", driver.Title);
            driver.FindElement(By.LinkText("Login")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Ask a question")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//input[@type='text']")).Clear();
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys(questionTitle);

            driver.FindElement(By.ClassName("ta-text")).FindElement(By.ClassName("ta-bind")).Click();
            driver.FindElement(By.ClassName("ta-text")).FindElement(By.ClassName("ta-bind")).SendKeys("With some details");

            driver.FindElement(By.XPath("(//input[@type='text'])[2]")).Clear();
            driver.FindElement(By.XPath("(//input[@type='text'])[2]")).SendKeys("SpecFlow, Selenium");
            driver.FindElement(By.XPath("//div[4]/div/div")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual("Stackoverflow | Stamplay app", driver.Title);
            Thread.Sleep(1500);
            var item = wait.Until(d => d.FindElement(By.LinkText(questionTitle)));

            var todayString = DateTime.Today.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual(string.Format("asked {0} by John Doe", todayString), item.FindElement(By.XPath("ancestor::li/div/div[2]/div[2]/div[2]")).Text);
            var actions = new Actions(driver);
            actions.MoveToElement(item.FindElement(By.XPath("ancestor::li"))).Perform();
            driver.FindElement(By.LinkText(questionTitle)).Click();
            Thread.Sleep(1500);
            Assert.AreEqual(questionTitle, driver.FindElement(By.CssSelector("h1.answer-title.ng-binding")).Text);
            Assert.AreEqual("With some details", driver.FindElement(By.CssSelector("p")).Text);
            driver.FindElement(By.LinkText("Home")).Click();
        }
    }
}
