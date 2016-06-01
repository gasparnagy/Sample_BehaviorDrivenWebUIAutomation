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
    [Binding, Scope(Feature = "Orbit")] //HACK: normally step definitions are global and don't need to be scoped to a feature
    public class OrbitSteps
    {
        private readonly SeleniumContext seleniumContext;
        private readonly OrbitLoginPage loginPage;

        public OrbitSteps(SeleniumContext seleniumContext, OrbitLoginPage loginPage)
        {
            this.seleniumContext = seleniumContext;
            this.loginPage = loginPage;
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

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            loginPage.GoTo();
        }

        [When(@"I am logged in as user '(.*)' with password '(.*)'")]
        public void WhenIAmLoggedInAsUserWithPassword(string userName, string password)
        {
            loginPage.Login(userName, password);
        }

        [Then(@"there should be a login error displayed: '(.*)'")]
        public void ThenThereShouldBeALoginErrorDisplayed(string expectedMessage)
        {
            seleniumContext.Driver.FindElement(By.CssSelector("span.warningLabel")).Text
                .Should().Contain(expectedMessage);
        }

        [Then(@"the project overview page should be displayed")]
        public void ThenTheProjectOverviewPageShouldBeDisplayed()
        {
            seleniumContext.Driver.Title.Should().Be("Orbit");
            seleniumContext.Driver.Url.Should().EndWithEquivalent("/ProjectOverview.aspx");
        }

        [Given(@"there is a project '(.*)' with Venues")]
        public void GivenThereIsAProjectWithVenues(string projectName, Table venuesTable)
        {
            //NOP: this is already in the database for now
        }

        [Given(@"I am logged in as admin")]
        public void GivenIAmLoggedInAsAdmin()
        {
            seleniumContext.Driver.Navigate().GoToUrl(seleniumContext.BaseURL + "/GuestsDefault.aspx");
            seleniumContext.Driver.FindElement(By.CssSelector("span.rmText")).Click();

            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).Clear();
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).SendKeys("admin");
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).Clear();
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).SendKeys("admin");
            seleniumContext.Driver.FindElement(By.Name("ctl00$Orbit_Content$ctl04")).Click();
        }

        [Given(@"I work on project '(.*)'")]
        public void GivenIWorkOnProject(string projectName)
        {
            seleniumContext.Driver.FindElement(By.LinkText("Test WM 2012")).Click();
            Thread.Sleep(1000);
        }

        [When(@"I try to manage project venues")]
        public void WhenITryToManageProjectVenues()
        {
            var menu = seleniumContext.Driver.FindElement(By.Id("ctl00_projectmenu"));
            new Actions(seleniumContext.Driver).MoveToElement(menu.FindElement(By.LinkText("Manage Project"))).Build().Perform();
            Thread.Sleep(100);
            new Actions(seleniumContext.Driver).MoveToElement(menu.FindElement(By.LinkText("Project Details"))).Click().Build().Perform();

            seleniumContext.Driver.FindElement(By.XPath("//div[@id='ctl00_Orbit_Content_ProjectDetailsTabStrip']/div/ul/li[2]/a/span/span/span")).Click();
        }

        [Then(@"the following venues are listed")]
        public void ThenTheFollowingVenuesAreListed(Table table)
        {
            var rows = seleniumContext.Driver.FindElements(By.CssSelector("#ctl00_Orbit_Content_VenuesTab > table tr")).Skip(5);
            Assert.IsTrue(rows.Any());
        }

        [When(@"I try to manage project orders")]
        public void WhenITryToManageProjectOrders()
        {
            var menu = seleniumContext.Driver.FindElement(By.Id("ctl00_projectmenu"));
            new Actions(seleniumContext.Driver).MoveToElement(menu.FindElement(By.LinkText("Manage Project"))).Build().Perform();
            Thread.Sleep(100);
            new Actions(seleniumContext.Driver).MoveToElement(menu.FindElement(By.LinkText("Project Details"))).Click().Build().Perform();

            seleniumContext.Driver.FindElement(By.XPath("//div[@id='ctl00_Orbit_Content_ProjectDetailsTabStrip']/div/ul/li[4]/a/span/span/span")).Click();
        }

        [Then(@"there should be a warning displayed in the table: '(.*)'")]
        public void ThenThereShouldBeAWarningDisplayedInTheTable(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, seleniumContext.Driver.FindElement(By.CssSelector("tr.rgNoRecords > td > div")).Text);
        }

        [Given(@"have started placing a new order for '(.*)'")]
        public void GivenHaveStartedPlacingANewOrderFor(string productName)
        {
            var menu = seleniumContext.Driver.FindElement(By.Id("ctl00_projectmenu"));
            new Actions(seleniumContext.Driver).MoveToElement(menu.FindElement(By.LinkText("New Order"))).Build().Perform();
            Thread.Sleep(300);
            new Actions(seleniumContext.Driver).MoveToElement(menu.FindElement(By.LinkText(productName))).Click().Build().Perform();

            // select header fields
            new SelectElement(seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_CustomerDropDown"))).SelectByText("ARD");
            seleniumContext.Driver.FindElement(By.CssSelector("option[value=\"f5f88a29-45c9-4686-b517-f384b7bdc6ef\"]")).Click();
            new SelectElement(seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_VenueList"))).SelectByText("Kitzbühel");
            seleniumContext.Driver.FindElement(By.CssSelector("option[value=\"Kitzbühel\"]")).Click();

            // fill out first line
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).Clear();
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).SendKeys("1");
            new SelectElement(seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl01_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Sessel");
            new SelectElement(seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl02_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Empfang");
        }

        [When(@"I add a new order line")]
        public void WhenIAddANewOrderLine()
        {
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_addButton")).Click();
        }

        [Then(@"there should be (.*) order lines added")]
        public void ThenThereShouldBeOrderLinesAdded(int expectedOrderLineCount)
        {
            var table = seleniumContext.Driver.FindElement(By.Id("GenericTable"));

            var actualRowCount = table.FindElements(By.TagName("tr")).Count - 2; // there is a header and a footer line
            actualRowCount.Should().Be(expectedOrderLineCount);
        }

        [Then(@"it should be possible to fill out the added line")]
        public void ThenItShouldBePossibleToFillOutTheAddedLine()
        {
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).Clear();
            seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).SendKeys("2");
            new SelectElement(seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl01_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Sessel");
            new SelectElement(seleniumContext.Driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl02_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Siegerehrung");
        }

    }
}
