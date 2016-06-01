using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public class Button : IUiConcept
    {
        private string key;
        private readonly IElementLocator elementLocator;

        public Button(string key, IElementLocator elementLocator)
        {
            this.key = key;
            this.elementLocator = elementLocator;
        }

        public void Click()
        {
            //TODO: implement "click" on button concept!
            throw new NotImplementedException();
        }
    }
}