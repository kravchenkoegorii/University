using OpenQA.Selenium;

namespace lab2.PageObjects
{
    class ShoppingCartPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _removeButton = By.XPath("//a[text()='Remove']");
        public ShoppingCartPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public void RemoveItemButton()
        {
            _webDriver.FindElement(_removeButton).Click();
        }
    }
}
