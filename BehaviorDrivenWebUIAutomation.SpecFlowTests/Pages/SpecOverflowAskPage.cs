using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages
{
    public class SpecOverflowAskPage
    {
        private readonly SeleniumContext seleniumContext;

        [FindsBy(How = How.Id)]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.Id)]
        public IWebElement Body { get; set; }

        [FindsBy(How = How.Id, Using = "submitask")]
        public IWebElement SubmitButton { get; set; }

        public SpecOverflowAskPage(SeleniumContext seleniumContext)
        {
            this.seleniumContext = seleniumContext;
            PageFactory.InitElements(seleniumContext.Driver, this);
        }

        public void Ask(string title, string body)
        {
            Title.Clear();
            Title.SendKeys(title);

            Body.Clear();
            Body.SendKeys(body);

            SubmitButton.Click();
        }

        public void GoTo()
        {
            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/");
            seleniumContext.Driver.FindElement(By.LinkText("Ask Question")).Click();
        }
    }
}
