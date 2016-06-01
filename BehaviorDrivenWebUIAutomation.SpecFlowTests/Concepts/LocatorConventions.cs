using System.Collections.Generic;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages;
using OpenQA.Selenium;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public class LocatorConventions
    {
        public static IEnumerable<By> GetLocators(string locator)
        {
            //TODO: list all locators (in order of priority) that your app needs
            yield return By.Id(locator);
            yield return new OrbitTableFieldLocator(locator);
            yield return new OrbitButtonLocator(locator);
        }
    }
}