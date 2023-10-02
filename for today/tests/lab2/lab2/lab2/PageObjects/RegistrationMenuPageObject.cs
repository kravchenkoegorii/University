using OpenQA.Selenium;
using System;

namespace lab2.PageObjects
{
    class RegistrationMenuPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _userId = By.Name("username");
        private readonly By _newPassword = By.XPath("//input[@name='password']");
        private readonly By _repeatPassword = By.XPath("//input[@name='repeatedPassword']");
        private readonly By _firstName = By.XPath("//input[@name='account.firstName']");
        private readonly By _lastName = By.XPath("//input[@name='account.lastName']");
        private readonly By _email = By.XPath("//input[@name='account.email']");
        private readonly By _phone = By.XPath("//input[@name='account.phone']");
        private readonly By _address1 = By.XPath("//input[@name='account.address1']");
        private readonly By _address2 = By.XPath("//input[@name='account.address2']");
        private readonly By _city = By.XPath("//input[@name='account.city']");
        private readonly By _state = By.XPath("//input[@name='account.state']");
        private readonly By _zip = By.XPath("//input[@name='account.zip']");
        private readonly By _country = By.XPath("//input[@name='account.country']");
        private readonly By _saveButton = By.XPath("//input[@name='newAccount']");
        public RegistrationMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public RegistrationMenuPageObject Registration(string id, string password, string repeatPassword, string firstName,
            string lastName, string email, string phone,
            string address, string city, string state,
            string zip, string country)
        {
            _webDriver.FindElement(_userId).SendKeys(id);
            _webDriver.FindElement(_newPassword).SendKeys(password);
            _webDriver.FindElement(_repeatPassword).SendKeys(repeatPassword);
            _webDriver.FindElement(_firstName).SendKeys(firstName);
            _webDriver.FindElement(_lastName).SendKeys(lastName);
            _webDriver.FindElement(_email).SendKeys(email);
            _webDriver.FindElement(_phone).SendKeys(phone);
            _webDriver.FindElement(_address1).SendKeys(address);
            _webDriver.FindElement(_address2).SendKeys(address);
            _webDriver.FindElement(_city).SendKeys(city);
            _webDriver.FindElement(_state).SendKeys(state);
            _webDriver.FindElement(_zip).SendKeys(zip);
            _webDriver.FindElement(_country).SendKeys(country);
            return new RegistrationMenuPageObject(_webDriver);
        }

        internal void Test()
        {
            throw new NotImplementedException();
        }

        public MainMenuPageObject RegisterButton()
        {
            _webDriver.FindElement(_saveButton).Click();
            return new MainMenuPageObject(_webDriver);
        }
        public ErrorPageObject RegisterButtonError()
        {
            _webDriver.FindElement(_saveButton).Click();
            return new ErrorPageObject(_webDriver);
        }
        public RegistrationMenuPageObject RegisterButtonFail()
        {
            _webDriver.FindElement(_saveButton).Click();
            return new RegistrationMenuPageObject(_webDriver);
        }
        public RegistrationMenuPageObject FailedRegistration(string id, string password)
        {
            _webDriver.FindElement(_userId).SendKeys(id);
            _webDriver.FindElement(_newPassword).SendKeys(password);
            _webDriver.FindElement(_repeatPassword).SendKeys(password);
            return new RegistrationMenuPageObject(_webDriver);
        }
    }
}
