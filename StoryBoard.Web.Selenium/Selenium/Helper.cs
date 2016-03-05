using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Selenium
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, TimeSpan timeout)
        {
            if (TimeSpan.Equals(timeout, TimeSpan.Zero)) return driver.FindElement(by);

            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(d => d.FindElement(by));
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, TimeSpan timeout)
        {
            if (TimeSpan.Equals(timeout, TimeSpan.Zero)) return driver.FindElements(by);

            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(d => (d.FindElements(by).Count > 0) ? d.FindElements(by) : null);
        }

    }
}
