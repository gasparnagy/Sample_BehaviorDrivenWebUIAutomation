using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace BehaviorDrivenWebUIAutomation.UITests
{
    [TestClass]
    public class OrbitTests : SeleniumTestBase
    {
        public OrbitTests() : base("Orbit")
        {
        }

        [TestMethod]
        public void Login()
        {
            driver.Navigate().GoToUrl(baseURL + "/GuestsDefault.aspx");
            driver.FindElement(By.CssSelector("span.rmText")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).SendKeys("admin");
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).SendKeys("admin");
            driver.FindElement(By.Name("ctl00$Orbit_Content$ctl04")).Click();
            Assert.AreEqual("Orbit", driver.Title);
            Assert.IsTrue(driver.Url.EndsWith("/ProjectOverview.aspx"));
            Assert.AreEqual("admin", driver.FindElement(By.CssSelector("span.loggedInUserName")).Text);
            driver.FindElement(By.LinkText("Logout:")).Click();
            driver.FindElement(By.CssSelector("span.rmText")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).SendKeys("admin");
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).SendKeys("wrong");
            driver.FindElement(By.Name("ctl00$Orbit_Content$ctl04")).Click();
            Assert.AreEqual("Invalid user name or wrong password (or user/ customer association not enabled by orf)", driver.FindElement(By.CssSelector("span.warningLabel")).Text);
        }

        [TestMethod]
        public void ProductDetails()
        {
            driver.Navigate().GoToUrl(baseURL + "/GuestsDefault.aspx");
            driver.FindElement(By.CssSelector("span.rmText")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).SendKeys("admin");
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).SendKeys("admin");
            driver.FindElement(By.Name("ctl00$Orbit_Content$ctl04")).Click();
            driver.FindElement(By.LinkText("Test WM 2012")).Click();

            Thread.Sleep(1000);
            var menu = driver.FindElement(By.Id("ctl00_projectmenu"));
            new Actions(driver).MoveToElement(menu.FindElement(By.LinkText("Manage Project"))).Build().Perform();
            Thread.Sleep(100);
            new Actions(driver).MoveToElement(menu.FindElement(By.LinkText("Project Details"))).Click().Build().Perform();

            driver.FindElement(By.XPath("//div[@id='ctl00_Orbit_Content_ProjectDetailsTabStrip']/div/ul/li[2]/a/span/span/span")).Click();
            var rows = driver.FindElements(By.CssSelector("#ctl00_Orbit_Content_VenuesTab > table tr")).Skip(5);
            Assert.IsTrue(rows.Any());
            Assert.AreEqual("Schladming", driver.FindElement(By.Id("ctl00_Orbit_Content_ProjectVenues1_Repeater1_ctl01_Hyperlink1")).Text);
            driver.FindElement(By.XPath("//div[@id='ctl00_Orbit_Content_ProjectDetailsTabStrip']/div/ul/li[3]/a/span/span/span")).Click();
            Assert.AreEqual("Add New Product", driver.FindElement(By.Name("ctl00$Orbit_Content$ProductList1$ctl12")).GetAttribute("value"));
            driver.FindElement(By.XPath("//div[@id='ctl00_Orbit_Content_ProjectDetailsTabStrip']/div/ul/li[4]/a/span/span/span")).Click();
            Assert.AreEqual("No records to display.", driver.FindElement(By.CssSelector("tr.rgNoRecords > td > div")).Text);
        }

        [TestMethod]
        public void AddRemoveLinesOfNewProduct()
        {
            driver.Navigate().GoToUrl(baseURL + "/GuestsDefault.aspx");
            driver.FindElement(By.CssSelector("span.rmText")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_UserNameTextBox")).SendKeys("admin");
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_PasswordTextBox")).SendKeys("admin");
            driver.FindElement(By.Name("ctl00$Orbit_Content$ctl04")).Click();
            driver.FindElement(By.LinkText("Test WM 2012")).Click();
            Thread.Sleep(1000);
            var menu = driver.FindElement(By.Id("ctl00_projectmenu"));
            new Actions(driver).MoveToElement(menu.FindElement(By.LinkText("New Order"))).Build().Perform();
            Thread.Sleep(100);
            new Actions(driver).MoveToElement(menu.FindElement(By.LinkText("GenericDropDown_A"))).Click().Build().Perform();
            Thread.Sleep(500);
            new SelectElement(driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_CustomerDropDown"))).SelectByText("ARD");
            //driver.FindElement(By.CssSelector("option[value=\"f5f88a29-45c9-4686-b517-f384b7bdc6ef\"]")).Click();
            new SelectElement(driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_VenueList"))).SelectByText("Kitzbühel");
            driver.FindElement(By.CssSelector("option[value=\"Kitzbühel\"]")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).SendKeys("1");
            new SelectElement(driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl01_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Sessel");
            new SelectElement(driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_CurrentStateOrderControl_FieldDefinitionRepeater_ctl02_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Empfang");
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_addButton")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl00_OrderControl_FieldControlHolder_ctl06_ValueTextBox_ValueTextBox")).SendKeys("2");
            new SelectElement(driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl01_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Sessel");
            new SelectElement(driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl01_CurrentStateOrderControl_FieldDefinitionRepeater_ctl02_OrderControl_FieldControlHolder_ctl06_ValueListBox"))).SelectByText("Siegerehrung");
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_addButton")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl00_RemoveCheckBox")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_OrderListRepeater_ctl02_RemoveCheckBox")).Click();
            driver.FindElement(By.Id("ctl00_Orbit_Content_Orderdetails1_removeButton")).Click();
            driver.FindElement(By.Name("ctl00$Orbit_Content$Orderdetails1$ctl30")).Click();
        }
    }
}
