﻿using System;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Support;
using FluentAssertions;
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
            // This is an assertion using the "FluentAssertions" library, that defines the "Should()" extension method
            // in order to be able to express expectations.
            // The line below is equivalent to (but mutch nicer :-)
            // Assert.AreEqual(expectedTitle, seleniumContext.Driver.Title);

            seleniumContext.Driver.Title.Should().Be(expectedTitle);
        }
    }
}
