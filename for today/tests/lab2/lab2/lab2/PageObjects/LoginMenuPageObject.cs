using OpenQA.Selenium;

namespace lab2.PageObjects
{
    class LoginMenuPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _loginInput = By.Name("username");
        private readonly By _passwordInput = By.XPath("//input[@name='password']");
        private readonly By _loginButton = By.XPath("//input[@name='signon']");
        private readonly By _registrationButton = By.XPath("//a[text()='Register Now!']");
        public bool CheckAnything(string any) =>
            _webDriver.PageSource.Contains(any);
        public LoginMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public LoginMenuPageObject LogInFill(string login, string password)
        {
            _webDriver.FindElement(_loginInput).SendKeys(login);
            _webDriver.FindElement(_passwordInput).Clear();
            _webDriver.FindElement(_passwordInput).SendKeys(password);
            return new LoginMenuPageObject(_webDriver);
        }
        public MainMenuPageObject LogInButton()
        {
            _webDriver.FindElement(_loginButton).Click();
            return new MainMenuPageObject(_webDriver);
        }
        public LoginMenuPageObject LogInButtonFail()
        {
            _webDriver.FindElement(_loginButton).Click();
            return new LoginMenuPageObject(_webDriver);
        }
        public RegistrationMenuPageObject SignOnButton()
        {
            _webDriver.FindElement(_registrationButton).Click();
            return new RegistrationMenuPageObject(_webDriver);
        }
    }
}
