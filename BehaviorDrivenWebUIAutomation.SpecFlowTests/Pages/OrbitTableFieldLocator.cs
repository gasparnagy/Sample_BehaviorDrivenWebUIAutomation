using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages
{
    public class OrbitTableFieldLocator : By
    {
        private readonly string label;

        public OrbitTableFieldLocator(string label)
        {
            this.label = label;
            this.Description = string.Format("By.TableField: {0}", label);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var expectedLabel = label.Replace(" ", "") + ":";
            var labelTds = context.FindElements(By.CssSelector("td.propertyName")).Where(td => td.Text.Replace(" ", "") == expectedLabel);
            var result = new List<IWebElement>();
            foreach (var labelTd in labelTds)
            {
                try
                {
                    var input = labelTd.FindElement(By.XPath("following-sibling::td/input"));
                    result.Add(input);
                }
                catch (NoSuchElementException)
                {
                }
            }

            return new ReadOnlyCollection<IWebElement>(result);
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var result = FindElements(context).FirstOrDefault();
            if (result == null)
                throw new NoSuchElementException(string.Format("Table field '{0}' could not be found", label));

            return result;
        }
    }
}