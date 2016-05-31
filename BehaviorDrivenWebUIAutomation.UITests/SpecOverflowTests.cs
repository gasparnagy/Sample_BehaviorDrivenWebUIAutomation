using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace BehaviorDrivenWebUIAutomation.UITests
{
    [TestClass]
    public class SpecOverflowTests : SeleniumTestBase
    {
        public SpecOverflowTests() : base("SpecOverflow")
        {
        }

        [TestMethod]
        public void AskQuestion()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Ask Question")).Click();
            driver.FindElement(By.Id("Title")).Clear();
            driver.FindElement(By.Id("Title")).SendKeys("This is a question");
            driver.FindElement(By.Id("Body")).Clear();
            driver.FindElement(By.Id("Body")).SendKeys("With some details");
            driver.FindElement(By.Id("submitask")).Click();
            Assert.AreEqual("Home Page - SpecOverflow", driver.Title);
            Assert.AreEqual("This is a question", driver.FindElement(By.XPath("//ul[@id='questions']/li[last()]/div/div[3]/div")).Text);
        }

        [TestMethod]
        public void About()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("About")).Click();
            Assert.AreEqual("About - SpecOverflow", driver.Title);
            Assert.IsTrue(IsElementPresent(By.LinkText("StackOverflow")));
            Assert.IsTrue(IsElementPresent(By.LinkText("SpecFlow")));
            Assert.IsTrue(IsElementPresent(By.LinkText("http://gasparnagy.com")));
        }

        [TestMethod]
        public void AskQuestionFromFrontPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            try
            {
                Assert.AreEqual("Ask Question", driver.FindElement(By.CssSelector("legend")).Text);
            }
            catch (AssertFailedException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("Title")).Clear();
            driver.FindElement(By.Id("Title")).SendKeys("Another question");
            driver.FindElement(By.Id("Body")).Clear();
            driver.FindElement(By.Id("Body")).SendKeys("with some details");
            driver.FindElement(By.Id("submitask")).Click();
            Assert.AreEqual("Home Page - SpecOverflow", driver.Title);
            Assert.AreEqual("Another question", driver.FindElement(By.XPath("//ul[@id='questions']/li[last()]/div/div[3]/div")).Text);
            Assert.AreEqual("0", driver.FindElement(By.XPath("//ul[@id='questions']/li[last()]/div/div/span")).Text);
            Assert.AreEqual("0", driver.FindElement(By.XPath("//ul[@id='questions']/li[last()]/div/div[2]/span")).Text);
        }
    }
}
