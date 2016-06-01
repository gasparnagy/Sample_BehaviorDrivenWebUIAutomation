using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public interface IUiContainerConcept
    {
        ISearchContext SearchContext { get; }
    }

    public abstract class ContainerConcept : IUiContainerConcept
    {
        private class LazyElementLocator : IElementLocator
        {
            private readonly Lazy<ISearchContext> searchContext;

            public ISearchContext SearchContext { get { return searchContext.Value; } }

            public LazyElementLocator(Lazy<ISearchContext> searchContext)
            {
                this.searchContext = searchContext;
            }

            public IWebElement LocateElement(IEnumerable<By> bys)
            {
                return new DefaultElementLocator(SearchContext).LocateElement(bys);
            }

            public ReadOnlyCollection<IWebElement> LocateElements(IEnumerable<By> bys)
            {
                return new DefaultElementLocator(SearchContext).LocateElements(bys);
            }
        }

        private readonly Lazy<ISearchContext> searchContext;

        protected ContainerConcept(string key, IElementLocator elementLocator)
        {
            searchContext = new Lazy<ISearchContext>(() => elementLocator.LocateElement(LocatorConventions.GetLocators(key)));
            ConceptFactory.InitElements(new LazyElementLocator(searchContext), this);
        }

        protected ContainerConcept(ISearchContext searchContext)
        {
            this.searchContext = new Lazy<ISearchContext>(() => searchContext);
            ConceptFactory.InitElements(searchContext, this);
        }

        public ISearchContext SearchContext { get { return searchContext.Value; } }
    }
}