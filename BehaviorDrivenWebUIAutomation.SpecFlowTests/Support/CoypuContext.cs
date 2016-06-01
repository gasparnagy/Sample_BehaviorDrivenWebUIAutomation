using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using Coypu;
using TechTalk.SpecFlow;

namespace BehaviorDrivenWebUIAutomation.SpecFlowTests.Support
{
    [Binding]
    public class CoypuHooks
    {
        private readonly ObjectContainer _objectContainer;
        private Uri _baseUrl;
        private BrowserSession _browser;

        public CoypuHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer as ObjectContainer;
        }

        [BeforeScenario]
        public void Before()
        {
            var appName = FeatureContext.Current.FeatureInfo.Title; //HACK: we get the application name from the feature title. Normally you only test one application from a single project
            _baseUrl =  new Uri(ConfigurationManager.AppSettings[appName + ".URL"]);
            _objectContainer.RegisterFactoryAs<BrowserSession>(CreateBrowserSession);
        }

        private BrowserSession CreateBrowserSession()
        {
            _browser = new BrowserSession(new SessionConfiguration { AppHost = _baseUrl.Host, Port = _baseUrl.Port });
            return _browser;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_browser != null)
            {
                _browser.Dispose();
                _browser = null;
            }
        }
    }
}
