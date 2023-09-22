using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab1
{
    public class Tests
    {
        private ChromeDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        [Test, Order(1)]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://pastebin.com/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postform-text")));
            driver.FindElement(By.Id("postform-text")).SendKeys("ada");

            driver.FindElement(By.XPath("//*[@id='select2-postform-expiration-container']")).Click();
            driver.FindElement(By.XPath("/html/body/span[2]/span/span[2]/ul/li[3]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postform-text")));
            driver.FindElement(By.Id("postform-name")).SendKeys("ada2");

            driver.FindElement(By.CssSelector(".btn.-big")).Click();

            Assert.Pass();
        }

        [Test, Order(2)]
        public void Test2()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Books")));
            driver.FindElement(By.LinkText("Books")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Under 25.00")));
            driver.FindElement(By.LinkText("Under 25.00")).Click();

            driver.FindElement(By.XPath("/html/body/div[4]/div[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div[3]/select/option[1]")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Computing and Internet")));
            driver.FindElement(By.LinkText("Computing and Internet")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("add-to-cart-button-13")));
            driver.FindElement(By.Id("add-to-cart-button-13")).Click();

            Thread.Sleep(3000);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("topcartlink")));
            driver.FindElement(By.Id("topcartlink")).Click();

            Assert.Pass();
        }

        [TearDown]
        public void Exit()
        {
            driver.Quit();
        }
    }
}