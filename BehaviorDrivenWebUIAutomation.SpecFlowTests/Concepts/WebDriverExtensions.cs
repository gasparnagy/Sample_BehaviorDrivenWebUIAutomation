using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitFor(this ISearchContext searchContext, Func<ISearchContext, IWebElement> condition, double seconds = 10)
        {
            var wait = new DefaultWait<ISearchContext>(searchContext) { Timeout = TimeSpan.FromSeconds(seconds) };
            return wait.Until(condition);
        }

        public static IWebElement WaitForVisible(this ISearchContext searchContext, Func<ISearchContext, IWebElement> condition, double seconds = 10)
        {
            var wait = new DefaultWait<ISearchContext>(searchContext) { Timeout = TimeSpan.FromSeconds(seconds) };
            var result = wait.Until(condition);
            wait.Until(d => result.Displayed);
            return result;
        }
    }
}