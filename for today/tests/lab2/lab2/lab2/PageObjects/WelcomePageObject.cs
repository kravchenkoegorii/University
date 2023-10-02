using OpenQA.Selenium;

namespace lab2.PageObjects
{
    class WelcomePageObject
    {
        private IWebDriver _webDriver;
        private readonly By _enterButton = By.XPath("//a[text()='Enter the Store']");
        public WelcomePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public MainMenuPageObject EnterShop()
        {
            _webDriver.FindElement(_enterButton).Click();
            return new MainMenuPageObject(_webDriver);
        }
    }
}
