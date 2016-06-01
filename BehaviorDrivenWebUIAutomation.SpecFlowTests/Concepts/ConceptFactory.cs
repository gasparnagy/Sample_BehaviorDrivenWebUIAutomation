using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public static class ConceptFactory
    {
        public static void InitElements(ISearchContext searchContext, object obj)
        {
            var locator = new DefaultElementLocator(searchContext);
            InitElements(locator, obj);
        }

        public static void InitElements(IElementLocator locator, object obj)
        {
            var properties = obj.GetType().GetProperties().Where(p => typeof (IUiConcept).IsAssignableFrom(p.PropertyType));
            foreach (var propertyInfo in properties)
            {
                var concept = Activator.CreateInstance(propertyInfo.PropertyType, new object[] {propertyInfo.Name, locator});
                propertyInfo.SetValue(obj, concept);
            }
        }
    }
}
