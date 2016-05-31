using System;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.StepDefinitions
{
    [Binding]
    public class SpecOverflowSteps
    {
        private readonly SeleniumContext seleniumContext;

        public SpecOverflowSteps(SeleniumContext seleniumContext)
        {
            this.seleniumContext = seleniumContext;
        }

        [When(@"I navigate to the home page")]
        public void WhenINavigateToTheHomePage()
        {
            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/");
        }
        
        [Then(@"the title should be '(.*)'")]
        public void ThenTheTitleShouldBe(string expectedTitle)
        {
            Assert.AreEqual(expectedTitle, seleniumContext.Driver.Title);
        }
    }
}
