using OpenQA.Selenium;

namespace lab2.PageObjects
{
    class CategoryPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _petType = By.XPath("//a[text()='FI-SW-01']");
        private readonly By _addToCartButton = By.XPath("//tbody/tr[2]/td[5]/a[1]");
        public CategoryPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public CategoryPageObject SelectPetButton()
        {
            _webDriver.FindElement(_petType).Click();
            return new CategoryPageObject(_webDriver);
        }
        public ShoppingCartPageObject AddToCartButton()
        {
            _webDriver.FindElement(_addToCartButton).Click();
            return new ShoppingCartPageObject(_webDriver);
        }
    }
}
