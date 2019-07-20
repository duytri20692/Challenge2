using NUnit.Framework;
using System;
using System.Threading;
using Challenge2.Page_Object;
using Challenge2.Helper;
using Challenge2.Reports;

namespace Challenge2
{
    public class TravelInsuranceTest : SeleniumExtentReport
    {
        string URL = "https://www.gobear.com/ph?x_session_type=UAT";
        
        int totalCardOnPage;
        int currentCardOnPage;
        int totalCardAfterUncheck;
        int totalSingleCard;
        int totalAnnualCard;

        [Test]
        public void Test01_AtLeast3CardsAreBeingDisplayed()
        {
            ShowResultPage showResultPage = new ShowResultPage(driver);
            // Go to the test site
            driver.Url = URL;

            // Wait for page load
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));

            // Go to Travel section
            TravelInsurancePage travelInsurancePage = new TravelInsurancePage(driver);
            travelInsurancePage.insuranceTabClick();
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));
            travelInsurancePage.travelTabClick();

            // Go to the Travel results page
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));
            travelInsurancePage.showMyResultButtonClick();
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));
        
            // Make sure at least 3 cards are being displayed                              
            totalCardOnPage = showResultPage.totalCardOnPage();
            Assert.GreaterOrEqual(totalCardOnPage, 3);
        }

        // Make sure the left side menu categories are functional
        #region Basic goal
        [Test]
        public void Test02_FilterOptionIsWorking()
        {
            ShowResultPage showResultPage = new ShowResultPage(driver);
            //// Category 01: Filter => Click on checkbox 'FPG Insurance'
            showResultPage.FPGInsuranceCheckboxClick();
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));

            // Check 01: The total number of cards displayed on the page is less than or equal the total number of cards before the filter is executed
            currentCardOnPage = showResultPage.totalCardOnPage();
            Assert.LessOrEqual(currentCardOnPage, totalCardOnPage);
            // Check 02: Make sure all cards are displaying is belong to 'FPG Insurance' 
            Assert.IsTrue(showResultPage.checkAllCardNameAreEqual("FPG Insurance"));
            // Check 03: Uncheck 'FPG Insurance' checkbox, the total number of cards displayed on the page should be equal the total number of cards before the filter is executed 
            showResultPage.FPGInsuranceCheckboxClick();
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));
            totalCardAfterUncheck = showResultPage.totalCardOnPage();
            Assert.AreEqual(totalCardAfterUncheck, totalCardOnPage);
        }

        [Test]
        public void Test03_SortOptionIsWorking()
        {
            ShowResultPage showResultPage = new ShowResultPage(driver);
            //// Category 02: Sort => Click on checkbox 'Price: Low to high'
            showResultPage.sortPriceLowToHighClick();
            // Check 01: Make sure the price of card is ascending
            Assert.IsTrue(showResultPage.checkCardPriceIsAscending());
        }

        [Test]
        public void Test04_DetailsOptionIsWorking()
        {
            ShowResultPage showResultPage = new ShowResultPage(driver);
            //// Category 03: Details
            // Check total cards are displayed with 'Single trip'
            showResultPage.detailSingleTripClick();
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));
            totalSingleCard = showResultPage.totalCardOnPage();

            // Check total cards are displayed with 'Annual trip'
            showResultPage.detailAnnualTripClick();
            WaitForPageLoad.WaitPageLoad(driver, TimeSpan.FromSeconds(120));
            //Wait for the list card is changed 
            Thread.Sleep(1000);
            totalAnnualCard = showResultPage.totalCardOnPage();

            // Check 01: The total number of cards on the page should be different between two categories: single trip and annual trip
            Assert.AreNotEqual(totalSingleCard, totalAnnualCard);
        }
        #endregion

        #region Stretch goal*: write a test to ensure the left side menu is functional
        [Test]
        public void Test05_SeeMoreOptionIsWorking()
        {
            ShowResultPage showResultPage = new ShowResultPage(driver);
            
            #region MyRegion
            // Before checking on SEE MORE button, the list does not show (cannot see filter 'Trip Cancelation')
            Assert.IsFalse(showResultPage.checkIfListSeeMoreIsVisible());

            // Click on SEE MORE button & the list will be shown
            showResultPage.collapseSeemoreButtonClick();
            //Wait for the list seemore is displayed 
            Thread.Sleep(1000);
            Assert.IsTrue(showResultPage.checkIfListSeeMoreIsVisible());
            #endregion
        }
        #endregion        
    }
}
