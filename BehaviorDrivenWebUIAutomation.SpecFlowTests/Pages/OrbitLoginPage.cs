using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Support;
using OpenQA.Selenium;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages
{
    public class OrbitLoginPage
    {
        private readonly SeleniumContext seleniumContext;

        public TextBox UserName { get; set; }
        public TextBox Password { get; set; }
        public Button LoginButton { get; set; }

        public OrbitLoginPage(SeleniumContext seleniumContext)
        {
            this.seleniumContext = seleniumContext;
            ConceptFactory.InitElements(seleniumContext.Driver, this);
        }

        public void Login(string userName, string password)
        {
            UserName.FillInWith(userName);
            Password.FillInWith(password);

            LoginButton.Click();
        }

        public void GoTo()
        {
            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/GuestsDefault.aspx");
            seleniumContext.Driver.FindElement(By.CssSelector("span.rmText")).Click();
        }
    }
}
