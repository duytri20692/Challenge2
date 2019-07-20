using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Challenge2.Helper
{
    class WaitForPageLoad
    {
        public void WaitForReady(IWebDriver driver, TimeSpan seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, seconds);
            wait.Until(d =>
            {
                return WaitPageLoad(driver, seconds);
            });
        }

        public static bool WaitPageLoad(IWebDriver driver, TimeSpan waitTime)
        {
            WaitForDocumentReady(driver, waitTime);
            bool ajaxReady = WaitForAjaxReady(driver, waitTime);
            WaitForDocumentReady(driver, waitTime);
            return ajaxReady;
        }

        private static void WaitForDocumentReady(IWebDriver driver, TimeSpan waitTime)
        {
            var wait = new WebDriverWait(driver, waitTime);
            var javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        private static bool WaitForAjaxReady(IWebDriver driver, TimeSpan waitTime)
        {
            System.Threading.Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, waitTime);
            return wait.Until<bool>((d) =>
            {
                return driver.FindElements(By.CssSelector(".waiting, .tb-loading")).Count == 0;
            });
        }
    }
}
