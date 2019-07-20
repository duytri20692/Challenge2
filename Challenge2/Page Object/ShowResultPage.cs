using OpenQA.Selenium;
using Challenge2.Helper;
using System.Collections.ObjectModel;

namespace Challenge2.Page_Object
{
    class ShowResultPage
    {
        IWebDriver driver;

        By cardFull = By.ClassName("card-full");
        By FPGInsuranceCheckbox = By.CssSelector("div[data-filter-name='FPG Insurance']");
        By cardName = By.CssSelector(".card-full .card-brand .name");
        By sortPriceLowToHighCheckbox = By.XPath("//label[@for='gb_radio_3']");
        By cardPrice = By.CssSelector(".card-full .card .policy-price .value");
        By detailSingleTrip = By.XPath("//label[@for='gb_radio_9']");
        By detailAnnualTrip = By.XPath("//label[@for='gb_radio_10']");
        By collapseSeemoreButton = By.Id("collapseSeemoreBtn");
        By filterTripCancelation = By.XPath("//div[@data-filter-by='filter-coverage-c2f71bf6-7256-4428-8920-fd7b15859c2f']");
        
        public ShowResultPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public int totalCardOnPage()
        {
            // Make sure at least one card is present on the page before counting
            WebDriverExtensions.FindElement(driver, cardFull, 120);

            // Count all the cards on page
            return driver.FindElements(cardFull).Count;
        }

        public void FPGInsuranceCheckboxClick()
        {
            WebDriverExtensions.FindElement(driver, FPGInsuranceCheckbox, 120).Click();
        }

        public bool checkAllCardNameAreEqual(string cardNameExpect)
        {
            // Find all card name on the page
            driver.FindElements(cardName);
            foreach (var eachCardName in driver.FindElements(cardName))
            {
                // If have any card name does not match, return false.
                if (!eachCardName.Text.Trim().Equals(cardNameExpect))
                {
                    break;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void sortPriceLowToHighClick()
        {
            WebDriverExtensions.FindElement(driver, sortPriceLowToHighCheckbox, 120).Click();
        }

        public bool checkCardPriceIsAscending()
        {
            // Find all card price on page
            ReadOnlyCollection<IWebElement> allCardPrice = driver.FindElements(cardPrice);
            for(int i = 0; i < allCardPrice.Count; i++)
            {
                if (int.Parse(allCardPrice[i].Text) > int.Parse(allCardPrice[i+1].Text))
                {
                    break;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void detailSingleTripClick()
        {
            WebDriverExtensions.FindElement(driver, detailSingleTrip, 120).Click();
        }

        public void detailAnnualTripClick()
        {
            WebDriverExtensions.FindElement(driver, detailAnnualTrip, 120).Click();
        }

        public void collapseSeemoreButtonClick()
        {
            WebDriverExtensions.FindElement(driver, collapseSeemoreButton, 120).Click();
        }

        public bool checkIfAnElementIsVisible(By byElement)
        {
            IWebElement element = null;
            WebElementExtensions eleExtensions = new WebElementExtensions();
            if(eleExtensions.TryFindElement(driver, byElement, out element))
            {
                bool visible = eleExtensions.IsElementVisible(element);
                if (visible)
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkIfListSeeMoreIsVisible()
        {
            // Randomly select an element in the list Seemore and check if it is displaying 
            // in this case, we are check the filter 'Trip Cancelation'
            if (checkIfAnElementIsVisible(filterTripCancelation))
            {
                return true;
            }
            return false;
        }
    }
}
