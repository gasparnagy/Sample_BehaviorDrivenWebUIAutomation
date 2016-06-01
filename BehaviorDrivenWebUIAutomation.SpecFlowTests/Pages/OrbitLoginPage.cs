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
    public class OrbitLoginPage
    {
        private readonly SeleniumContext seleniumContext;

        [FindsBy(How = How.Id, Using = "ctl00_Orbit_Content_UserNameTextBox")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_Orbit_Content_PasswordTextBox")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Name, Using = "ctl00$Orbit_Content$ctl04")]
        public IWebElement LoginButton { get; set; }

        public OrbitLoginPage(SeleniumContext seleniumContext)
        {
            this.seleniumContext = seleniumContext;
            PageFactory.InitElements(seleniumContext.Driver, this);
        }

        public void Login(string userName, string password)
        {
            UserName.Clear();
            UserName.SendKeys(userName);

            Password.Clear();
            Password.SendKeys(password);

            LoginButton.Click();
        }

        public void GoTo()
        {
            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/GuestsDefault.aspx");
            seleniumContext.Driver.FindElement(By.CssSelector("span.rmText")).Click();
        }
    }
}
