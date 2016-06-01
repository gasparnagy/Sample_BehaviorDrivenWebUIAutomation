using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public class Menu : IUiConcept
    {
        private string key;
        private readonly IElementLocator elementLocator;

        public Menu(string key, IElementLocator elementLocator)
        {
            this.key = key;
            this.elementLocator = elementLocator;
        }

        protected IWebElement MenuDivElement
        {
            get { return elementLocator.LocateElement(LocatorConventions.GetLocators(key)); }
        }

        public MenuItem GetChild(string label)
        {
            var menuDivElement = MenuDivElement;

            var childItemLI = menuDivElement.WaitForVisible(ctx => ctx.FindElement(By.XPath(string.Format("div/ul/li[a/span/text() = '{0}']", label))));
            return new MenuItem(childItemLI);
        }
    }

    public class MenuItem
    {
        private readonly IWebElement listItemElement;

        public MenuItem(IWebElement listItemElement)
        {
            this.listItemElement = listItemElement;
        }

        private void Hover()
        {
            var driver = ((IWrapsDriver)listItemElement).WrappedDriver;
            new Actions(driver).MoveToElement(GetLinkElement()).Build().Perform();
        }

        public MenuItem GetChild(string label)
        {
            Hover();

            var subItemLI = listItemElement.WaitForVisible(ctx => ctx.FindElement(By.XPath(string.Format("div/ul/li[a/span/text() = '{0}']", label))));
            return new MenuItem(subItemLI);
        }

        private IWebElement GetLinkElement()
        {
            return listItemElement.FindElement(By.TagName("a"));
        }

        public void SelectMenuItem()
        {
            GetLinkElement().Click();
        }
    }
}
