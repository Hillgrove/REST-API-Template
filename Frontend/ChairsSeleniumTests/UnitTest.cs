using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ChairsSeleniumTests
{
    [TestClass]
    public class ChairsSeleniumTests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void Setup()
        {
            //_driver = new ChromeDriver(); // if driver is in SYSTEM PATH
            _driver = new ChromeDriver(@"C:\REST API Template\Driver");
            string path = "C:\\REST API Template\\Frontend\\index.html";
            _driver.Navigate().GoToUrl($"file:///{path}");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void TestHomePageTitle()
        {
            Assert.AreEqual("REST API Template", _driver.Title);
        }

        [TestMethod]
        public void TestAddChair()
        {
            var modelInput = _driver.FindElement(By.Id("addModel"));
            var maxWeightInput = _driver.FindElement(By.Id("addMaxWeight"));
            var hasPillowCheckbox = _driver.FindElement(By.Id("addHasPillow"));
            var addButton = _driver.FindElement(By.Id("addButton"));

            string model = "Test Chair";
            int maxWeight = 150;
            bool hasPillow = true;

            modelInput.SendKeys(model);
            maxWeightInput.SendKeys(maxWeight.ToString());
            if (hasPillow)
            {
                hasPillowCheckbox.Click();
            }

            addButton.Click();

            var chairList = _driver.FindElement(By.Id("chairsList"));
            var addedChair = chairList.FindElement(By.XPath($"//li[contains(text(), '{model}') and contains(text(), '{maxWeight}') and contains(text(), '{(hasPillow ? "Yes" : "No")}')]"));
            Assert.IsNotNull(addedChair, "The chair was not added to the list.");
        }
    }
}
