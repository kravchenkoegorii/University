using OpenQA.Selenium;

namespace lab2.PageObjects
{
    class MainMenuPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _signInButton = By.XPath("//a[text()='Sign In']");
        private readonly By _categoryButton = By.XPath("//img[@src='../images/fish_icon.gif']");
        private readonly By _logOutButton = By.XPath("//a[text()='Sign Out']");
        public bool CheckAnything(string any) =>
            _webDriver.PageSource.Contains(any);
        public MainMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public LoginMenuPageObject SignInButton()
        {
            _webDriver.FindElement(_signInButton).Click();
            return new LoginMenuPageObject(_webDriver);
        }
        public CategoryPageObject CategoryButton()
        {
            _webDriver.FindElement(_categoryButton).Click();
            return new CategoryPageObject(_webDriver);
        }
        public MainMenuPageObject LogOutButton()
        {
            if (CheckAnything("Sign Out") == true) _webDriver.FindElement(_logOutButton).Click();
            return new MainMenuPageObject(_webDriver);
        }
    }
}
