using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Concepts
{
    public abstract class Page : ContainerConcept
    {
        protected Page(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
