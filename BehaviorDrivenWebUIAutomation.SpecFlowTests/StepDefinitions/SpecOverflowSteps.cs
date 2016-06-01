using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages;
using BehaviorDrivenWebUIAutomation.SpecFlowTests.Support;
using Coypu;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.StepDefinitions
{
    [Binding, Scope(Feature = "SpecOverflow")] //HACK: normally step definitions are global and don't need to be scoped to a feature
    public class SpecOverflowSteps
    {
        private readonly SeleniumContext seleniumContext;
        private readonly SpecOverflowAskPage askPage;

        public SpecOverflowSteps(SeleniumContext seleniumContext, SpecOverflowAskPage askPage)
        {
            this.seleniumContext = seleniumContext;
            this.askPage = askPage;
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

        // define a class that represents the table structure
        class QuestionData
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public int Views { get; set; }
            public int Votes { get; set; }
        }

        [When(@"I ask a new question with")]
        public void WhenIAskANewQuestionWith(Table table)
        {
            // convert the table to an instance of the class with CreateInstance<T>();
            var question = table.CreateInstance<QuestionData>();

            askPage.GoTo();
            askPage.Ask(question.Title, question.Body);
        }

        [Then(@"the question should appear at the end of the question list as")]
        public void ThenTheQuestionShouldAppearAtTheEndOfTheQuestionListAs(Table table)
        {
            var expectedQuestion = table.CreateInstance<QuestionData>();

            Assert.AreEqual("Home Page - SpecOverflow", seleniumContext.Driver.Title);
            Assert.AreEqual(expectedQuestion.Title, seleniumContext.Driver.FindElement(By.XPath("//ul[@id='questions']/li[last()]/div/div[3]/div")).Text);
        }

        [Given(@"the following questions are registered")]
        public void GivenTheFollowingQuestionsAreRegistered(Table table)
        {
            //NOP: these are already in the database for now
        }

        [When(@"I go to the home page")]
        public void WhenIGoToTheHomePage()
        {
            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/");
        }

        [Then(@"the following questions should be displayed in this order")]
        public void ThenTheFollowingQuestionsShouldBeDisplayedInThisOrder(Table table)
        {
            var actualQuestions = new List<QuestionData>();
            var questionLIs = seleniumContext.Driver.FindElements(By.XPath("//ul[@id='questions']/li"));
            foreach (var questionLI in questionLIs)
            {
                var questionData = new QuestionData
                {
                    Title = questionLI.FindElement(By.XPath("div/div[3]/div")).Text,
                    Views = int.Parse(questionLI.FindElement(By.XPath("div/div[2]/span")).Text),
                    Votes = int.Parse(questionLI.FindElement(By.XPath("div/div[1]/span")).Text)
                };
                //HACK: we skip the questions with 0 view count to avoid the data entered by the other scenarios
                if (questionData.Views == 0)
                    continue;
                actualQuestions.Add(questionData);
            }

            table.CompareToSet(actualQuestions);
        }

    }
}
