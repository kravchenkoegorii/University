using OpenQA.Selenium;

namespace lab2.PageObjects
{
    class ErrorPageObject
    {
        private IWebDriver _webDriver;

        public ErrorPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
    }
}
