using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages
{
    public class OrbitButtonLocator : By
    {
        private readonly string buttonText;

        public OrbitButtonLocator(string buttonText)
        {
            const string postfix = "Button";
            if (buttonText.EndsWith(postfix))
                buttonText = buttonText.Substring(0, buttonText.Length - postfix.Length);

            this.buttonText = buttonText;
            this.Description = string.Format("By.Button: {0}", buttonText);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return context.FindElements(By.XPath(string.Format("//input[@value = '{0}']", buttonText)));
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var result = FindElements(context).FirstOrDefault();
            if (result == null)
                throw new NoSuchElementException(string.Format("Button '{0}' could not be found", buttonText));

            return result;
        }
    }
}
