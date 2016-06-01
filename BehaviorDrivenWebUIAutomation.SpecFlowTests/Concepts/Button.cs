using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public class Button : IUiConcept
    {
        private string key;
        private readonly IElementLocator elementLocator;

        public Button(string key, IElementLocator elementLocator)
        {
            this.key = key;
            this.elementLocator = elementLocator;
        }

        protected IWebElement InputElement
        {
            get { return elementLocator.LocateElement(LocatorConventions.GetLocators(key)); }
        }

        public void Click()
        {
            InputElement.Click();
        }
    }
}