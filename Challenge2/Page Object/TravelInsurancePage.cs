using OpenQA.Selenium;
using Challenge2.Helper;

namespace Challenge2.Page_Object
{
    class TravelInsurancePage
    {
        IWebDriver driver;

        By insuranceTab = By.XPath("//a[@href='#Insurance']");
        By travelTab = By.XPath("//a[@href='#Travel']");
        By showMyResultButton = By.Name("product-form-submit");

        public TravelInsurancePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void insuranceTabClick()
        {
            WebDriverExtensions.FindElement(driver, insuranceTab, 120).Click();
        }

        public void travelTabClick()
        {
            WebDriverExtensions.FindElement(driver, travelTab, 120).Click();
        }

        public void showMyResultButtonClick()
        {
            WebDriverExtensions.FindElement(driver, showMyResultButton, 120).Click();
        }
    }
}
