using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;
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
    [Binding, Scope(Feature = "StackClone")] //HACK: normally step definitions are global and don't need to be scoped to a feature
    public class StackCloneSteps
    {
        private readonly SeleniumContext seleniumContext;

        public StackCloneSteps(SeleniumContext seleniumContext)
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

        class QuestionData
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Tags { get; set; }

            public QuestionData()
            {
                Tags = "SpecFlow, Selenium";
            }
        }

        private QuestionData lastQuestion;

        [When(@"I ask a new question with")]
        public void WhenIAskANewQuestionWith(Table table)
        {
            var question = table.CreateInstance<QuestionData>();

            var wait = new WebDriverWait(seleniumContext.Driver, new TimeSpan(0, 0, 5));

            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/#/");
            wait.Until(d => d.FindElement(By.LinkText("Home")));
            Assert.AreEqual("Stackoverflow | Stamplay app", seleniumContext.Driver.Title);
            seleniumContext.Driver.FindElement(By.LinkText("Login")).Click();
            Thread.Sleep(2000);
            seleniumContext.Driver.FindElement(By.LinkText("Ask a question")).Click();
            Thread.Sleep(500);
            seleniumContext.Driver.FindElement(By.XPath("//input[@type='text']")).Clear();
            seleniumContext.Driver.FindElement(By.XPath("//input[@type='text']")).SendKeys(question.Title);

            seleniumContext.Driver.FindElement(By.ClassName("ta-text")).FindElement(By.ClassName("ta-bind")).Click();
            seleniumContext.Driver.FindElement(By.ClassName("ta-text")).FindElement(By.ClassName("ta-bind")).SendKeys(question.Body);

            seleniumContext.Driver.FindElement(By.XPath("(//input[@type='text'])[2]")).Clear();
            seleniumContext.Driver.FindElement(By.XPath("(//input[@type='text'])[2]")).SendKeys(question.Tags);
            seleniumContext.Driver.FindElement(By.XPath("//div[4]/div/div")).Click();
            Thread.Sleep(2000);

            // remember last added question
            lastQuestion = question;
        }

        [Then(@"the question should appear in the question list with todays date as")]
        public void ThenTheQuestionShouldAppearInTheQuestionListWithTodaysDateAs(Table table)
        {
            var expectedQuestion = table.CreateInstance<QuestionData>();

            Assert.AreEqual("Stackoverflow | Stamplay app", seleniumContext.Driver.Title);
            Thread.Sleep(1500);
            var wait = new WebDriverWait(seleniumContext.Driver, new TimeSpan(0, 0, 5));
            var item = wait.Until(d => d.FindElement(By.LinkText(expectedQuestion.Title)));

            var todayString = DateTime.Today.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual(string.Format("asked {0} by John Doe", todayString), item.FindElement(By.XPath("ancestor::li/div/div[2]/div[2]/div[2]")).Text);
        }

        [Given(@"there is a question added with")]
        public void GivenThereIsAQuestionAddedWith(Table table)
        {
            WhenIAskANewQuestionWith(table);
        }

        [When(@"I navigate to the question details from the main page")]
        public void WhenINavigateToTheQuestionDetailsFromTheMainPage()
        {
            Assert.AreEqual("Stackoverflow | Stamplay app", seleniumContext.Driver.Title);
            Thread.Sleep(1500);
            var wait = new WebDriverWait(seleniumContext.Driver, new TimeSpan(0, 0, 5));
            var item = wait.Until(d => d.FindElement(By.LinkText(lastQuestion.Title)));

            item.Click();
            Thread.Sleep(1500);
        }

        [Then(@"the question details should be visible")]
        public void ThenTheQuestionDetailsShouldBeVisible(Table table)
        {
            var expectedQuestion = table.CreateInstance<QuestionData>();

            Assert.AreEqual(expectedQuestion.Title, seleniumContext.Driver.FindElement(By.CssSelector("h1.answer-title.ng-binding")).Text);
            Assert.AreEqual(expectedQuestion.Body, seleniumContext.Driver.FindElement(By.CssSelector("p")).Text);
        }
    }
}
