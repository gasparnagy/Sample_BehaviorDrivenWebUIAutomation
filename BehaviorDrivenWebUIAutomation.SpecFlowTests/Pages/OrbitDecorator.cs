using System;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Pages
{
    public class OrbitDecorator : IPageObjectMemberDecorator
    {
        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            var customizedResult = new DefaultPageObjectMemberDecorator().Decorate(member, locator);
            if (customizedResult != null)
                return customizedResult;

            var property = member as PropertyInfo;
            if (property == null || !property.CanWrite || !property.GetMethod.IsPublic)
                return null;

            var bys = new List<By>
            {
                By.Id(member.Name),
                new OrbitButtonLocator(member.Name),
                new OrbitTableFieldLocator(member.Name)
            };

            bool cache = ShouldCacheLookup(member);
            return CreateProxyObject(property.PropertyType, locator, bys, cache);
        }

        private object CreateProxyObject(Type propertyType, IElementLocator locator, IEnumerable<By> bys, bool cache)
        {
            var createProxyObjectMethod = typeof(DefaultPageObjectMemberDecorator).GetMethod("CreateProxyObject", BindingFlags.Static | BindingFlags.NonPublic);
            return createProxyObjectMethod.Invoke(null, new object[] { propertyType, locator, bys, cache });
        }

        private static bool ShouldCacheLookup(MemberInfo member)
        {
            var shouldCacheLookupMethod = typeof (DefaultPageObjectMemberDecorator).GetMethod("ShouldCacheLookup", BindingFlags.Static | BindingFlags.NonPublic);
            return (bool)shouldCacheLookupMethod.Invoke(null, new object[] {member});
        }
    }
}