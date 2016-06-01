using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public class RadioButtons : IUiConcept
    {
        private string key;
        private readonly IElementLocator elementLocator;

        public RadioButtons(string key, IElementLocator elementLocator)
        {
            this.key = key;
            this.elementLocator = elementLocator;
        }

        public string SelectedValue
        {
            get
            {
                try
                {
                    var selectedRadioElement = elementLocator.LocateElement(new[] {By.CssSelector(string.Format("input[type='radio'][name='{0}']:checked", key))});
                    return selectedRadioElement.GetAttribute("value");
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public void SelectValue(string value)
        {
            var radioElement = elementLocator.LocateElement(new[] { By.CssSelector(string.Format("input[type='radio'][name='{0}'][value='{1}']", key, value)) });
            radioElement.Click();
        }
    }
}
