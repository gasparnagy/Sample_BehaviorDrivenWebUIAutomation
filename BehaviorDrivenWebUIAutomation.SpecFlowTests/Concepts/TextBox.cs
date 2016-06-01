using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public class TextBox : IUiConcept
    {
        private string key;
        private readonly IElementLocator elementLocator;

        public TextBox(string key, IElementLocator elementLocator)
        {
            this.key = key;
            this.elementLocator = elementLocator;
        }

        protected IWebElement InputElement
        {
            get { return elementLocator.LocateElement(LocatorConventions.GetLocators(key)); }
        }

        public void FillInWith(string value)
        {
            var inputElement = InputElement;
            inputElement.Clear();
            inputElement.SendKeys(value);
        }
    }
}