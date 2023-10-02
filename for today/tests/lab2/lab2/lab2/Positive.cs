using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using lab2.PageObjects;

namespace lab2
{
    public class Positive
    {
        private IWebDriver _webDriver;
        private readonly string _url = "https://petstore.octoperf.com/";
        private readonly string _cartItem = "EST-1";
        private readonly string _logOutText = "Sign In";
        public bool CheckAnything(string any) =>
            _webDriver.PageSource.Contains(any);

        [OneTimeSetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver(Environment.CurrentDirectory);
            _webDriver.Navigate().GoToUrl(_url);
        }
        [Test]
        public void Registration()
        {
            WelcomePageObject reg = new WelcomePageObject(_webDriver);
            reg
                .EnterShop()
                .SignInButton()
                .SignOnButton()
                .Registration(TestData.RandomUserId, TestData.Password, TestData.Password,
                TestData.FirstName, TestData.LastName, TestData.Email,
                TestData.Phone, TestData.Address, TestData.City,
                TestData.State, TestData.Zip, TestData.Country)
                .RegisterButton();
        }
        [Test]
        public void LogIn()
        {
            WelcomePageObject log = new WelcomePageObject(_webDriver);
            log
                .EnterShop()
                .SignInButton()
                .LogInFill(TestData.ExistedUserId, TestData.Password)
                .LogInButton();
        }
        [Test]
        public void AddItemToCart()
        {
            WelcomePageObject add = new WelcomePageObject(_webDriver);
            add
                .EnterShop()
                .SignInButton()
                .LogInFill(TestData.ExistedUserId, TestData.Password)
                .LogInButton()
                .CategoryButton()
                .SelectPetButton()
                .AddToCartButton();

            Assert.IsTrue(CheckAnything(_cartItem));
        }
        [Test]
        public void RemoveCartItem()
        {
            WelcomePageObject add = new WelcomePageObject(_webDriver);
            add
                .EnterShop()
                .SignInButton()
                .LogInFill(TestData.ExistedUserId, TestData.Password)
                .LogInButton()
                .CategoryButton()
                .SelectPetButton()
                .AddToCartButton()
                .RemoveItemButton();

            Assert.IsFalse(CheckAnything(_cartItem));
        }
        [Test]
        public void LogOut()
        {
            WelcomePageObject logout = new WelcomePageObject(_webDriver);
            logout
                .EnterShop()
                .SignInButton()
                .LogInFill(TestData.ExistedUserId, TestData.Password)
                .LogInButton()
                .LogOutButton();

            Assert.IsTrue(CheckAnything(_logOutText));
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