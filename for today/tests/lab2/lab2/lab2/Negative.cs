using lab2.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace lab2
{
    class Negative
    {
        private IWebDriver _webDriver;
        private readonly string _url = "https://petstore.octoperf.com/";
        private readonly string _registrationText = "User Information";
        private readonly string _error = "HTTP Status 500 – Internal Server Error";
        private readonly string _loginText = "Please enter your username and password.";
        private readonly string _loginError = "Invalid username or password.";
        public bool CheckAnything(string any) =>
            _webDriver.PageSource.Contains(any);

        [OneTimeSetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver(Environment.CurrentDirectory);
            _webDriver.Navigate().GoToUrl(_url);
        }
        
        [Test]
        public void EmptyRegistration()
        {
            WelcomePageObject ereg = new WelcomePageObject(_webDriver);
            ereg
                .EnterShop()
                .SignInButton()
                .SignOnButton()
                .RegisterButton();

            Assert.IsTrue(CheckAnything(_registrationText));
        }
        [Test]
        public void RegistrationFailed()
        {
            WelcomePageObject freg = new WelcomePageObject(_webDriver);
            freg
                .EnterShop()
                .SignInButton()
                .SignOnButton()
                .FailedRegistration(TestData.RandomUserId, TestData.Password)
                .RegisterButtonError();

            Assert.IsTrue(CheckAnything(_error));
        }
        [Test]
        public void EmptyLogin()
        {
            WelcomePageObject el = new WelcomePageObject(_webDriver);
            el
                .EnterShop()
                .SignInButton()
                .LogInButton();
            Assert.IsTrue(CheckAnything(_loginText));
        }
        [Test]
        public void OnlyLogin()
        {
            WelcomePageObject ol = new WelcomePageObject(_webDriver);
            ol
                .EnterShop()
                .SignInButton()
                .LogInFill(TestData.ExistedUserId, "")
                .LogInButton();
            Assert.IsTrue(CheckAnything(_loginText));
        }
        [Test]
        public void WrongPassword()
        {
            WelcomePageObject wp = new WelcomePageObject(_webDriver);
            wp
                .EnterShop()
                .SignInButton()
                .LogInFill(TestData.ExistedUserId, TestData.WrongPassword)
                .LogInButtonFail();
            Assert.IsTrue(CheckAnything(_loginError));
        }
        [Test]
        public void RegistrationExistedUser()
        {
            WelcomePageObject reu = new WelcomePageObject(_webDriver);
            reu
                .EnterShop()
                .SignInButton()
                .SignOnButton()
                .Registration(TestData.ExistedUserId, TestData.Password, TestData.Password,
                TestData.FirstName, TestData.LastName, TestData.Email,
                TestData.Phone, TestData.Address, TestData.City,
                TestData.State, TestData.Zip, TestData.Country)
                .RegisterButtonError();
            Assert.IsTrue(CheckAnything(_error));
        }
        [TearDown]
        public void EndTest()
        {
            MainMenuPageObject main = new MainMenuPageObject(_webDriver);
            main.LogOutButton();
            _webDriver.Navigate().GoToUrl(_url);
        }
    }
}
