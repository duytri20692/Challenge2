using OpenQA.Selenium;

namespace Challenge2.Helper
{
    public class WebElementExtensions
    {       
        public bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public bool TryFindElement(IWebDriver driver, By by, out IWebElement element)
        {
            try
            {
                element = driver.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                element = null;
                return false;
            }
            return true;
        }
    }
}
