using System;
using System.Threading;

using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using UnitTest;

namespace UITesting
{
    [TestFixture]
    public class RegisterUITest : TestBase
    {
        private IWebDriver _driver;
        private string _registerURL;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private TestVector _testVector;

        [SetUp]
        public void SetUp()
        {
            string email = "test1@e.com";
            string mobile = "0425123214";
            string firstName = "Timothy";
            string lastName = "Xing";
            string password = "Member@123";
            string passwordConfirm = "Member@123";
            try
            {
                _testVector = GenerateTestVector(email, mobile, firstName, lastName, password, passwordConfirm);
                _driver = new ChromeDriver();
                _registerURL = "https://localhost:44309/Identity/Account/Register";
                _driver.Navigate().GoToUrl(_registerURL);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                _driver.Quit();
                RemoteSetupDatabase("MainSampleDataTearDown.sql");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private class TestVector
        {
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string PasswordConfirm { get; set; }
        }

        private static TestVector GenerateTestVector(string email, string mobile, string firstName, string lastName, string password, string passwordConfirm)
            => new TestVector
            {
                Email = email,
                Mobile = mobile,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                PasswordConfirm = passwordConfirm
            };

        [Test]
        public void RegisterTest_InvalidEmailAddress()
        {
            try
            {
                //Invalid email address
                var invalidEmail = "m@@e.com";
                _driver.FindElement(By.Id("registerEmail")).SendKeys(invalidEmail);

                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Mobile);
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);
                _driver.FindElement(By.Id("registerPassword")).SendKeys(_testVector.Password);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(_testVector.PasswordConfirm);
                _driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = _driver.FindElement(By.Id("registerEmailErrorMsg")).Text;
                var expectedErrorMsg = "The Email field is not a valid e-mail address.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = _driver.FindElement(By.Id("registerEmailErrorMsg")).Text;
                expectedErrorMsg = "The Email field is not a valid e-mail address.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidMobile()
        {
            try
            {
                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);

                //Invalid mobile
                _driver.FindElement(By.Id("registerMobile")).SendKeys("");
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);
                _driver.FindElement(By.Id("registerPassword")).SendKeys(_testVector.Password);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(_testVector.PasswordConfirm);
                _driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = _driver.FindElement(By.Id("registerMobileErrorMsg")).Text;
                var expectedErrorMsg = "The Mobile field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = _driver.FindElement(By.Id("registerErrorMsg")).Text;
                expectedErrorMsg = "The Mobile field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidUsername()
        {
            try
            {
                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);
                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Email);

                //Invalid mobile
                _driver.FindElement(By.Id("registerFirstName")).SendKeys("");
                _driver.FindElement(By.Id("registerLastName")).SendKeys("");

                _driver.FindElement(By.Id("registerPassword")).SendKeys(_testVector.Password);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(_testVector.PasswordConfirm);
                _driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = _driver.FindElement(By.Id("registerFirstNameErrorMsg")).Text;
                var expectedErrorMsg = "The First Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = _driver.FindElement(By.Id("registerLastNameErrorMsg")).Text;
                expectedErrorMsg = "The Last Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = _driver.FindElement(By.Id("registerErrorMsg")).Text;
                expectedErrorMsg = "The First Name field is required.\r\nThe Last Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InconsistentPassword()
        {
            try
            {
                string inconsistentPassword = "Member@1234";

                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);
                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Mobile);
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);
                _driver.FindElement(By.Id("registerPassword")).SendKeys(_testVector.Password);

                //Inconsistent password
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(inconsistentPassword);
                _driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = _driver.FindElement(By.Id("registerPasswordConfirmErrorMsg")).Text;
                var expectedErrorMsg = "The password and confirmation password do not match.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidPassword_TooShort()
        {
            try
            {
                string tooShortPassword = "1234";

                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);
                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Mobile);
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);

                //Invalid password
                _driver.FindElement(By.Id("registerPassword")).SendKeys(tooShortPassword);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(tooShortPassword);
                _driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = _driver.FindElement(By.Id("registerPasswordErrorMsg")).Text;
                var expectedErrorMsg = "The Password must be at least 6 and at max 100 characters long.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = _driver.FindElement(By.Id("registerErrorMsg")).Text;
                expectedErrorMsg = "The Password must be at least 6 and at max 100 characters long.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidPassword_WrongFormat()
        {
            try
            {
                string wrongFormatPassword = "123456";

                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);
                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Mobile);
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);

                //Invalid password
                _driver.FindElement(By.Id("registerPassword")).SendKeys(wrongFormatPassword);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(wrongFormatPassword);
                _driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = _driver.FindElement(By.Id("registerErrorMsg")).Text;
                var expectedErrorMsg = "Passwords must have at least one non alphanumeric character.\r\nPasswords must have at least one lowercase ('a'-'z').\r\nPasswords must have at least one uppercase ('A'-'Z').";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_ExistingUser()
        {
            try
            {
                //Insert user with email test@e.com into DB
                RemoteSetupDatabase("TestSampleDataSetup_User.sql");

                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);
                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Mobile);
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);
                _driver.FindElement(By.Id("registerPassword")).SendKeys(_testVector.Password);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(_testVector.PasswordConfirm);
                _driver.FindElement(By.Id("register")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = _driver.FindElement(By.Id("registerErrorMsg")).Text;
                var expectedErrorMsg = "User name 'test1@e.com' is already taken.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_Success()
        {
            try
            {
                _driver.FindElement(By.Id("registerEmail")).SendKeys(_testVector.Email);
                _driver.FindElement(By.Id("registerMobile")).SendKeys(_testVector.Mobile);
                _driver.FindElement(By.Id("registerFirstName")).SendKeys(_testVector.FirstName);
                _driver.FindElement(By.Id("registerLastName")).SendKeys(_testVector.LastName);
                _driver.FindElement(By.Id("registerPassword")).SendKeys(_testVector.Password);
                _driver.FindElement(By.Id("registerPasswordConfirm")).SendKeys(_testVector.PasswordConfirm);
                _driver.FindElement(By.Id("register")).Click();
                Thread.Sleep(500);

                //Check record in database
                var selectQuery = $"select * from People where Email='{_testVector.Email}'";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);
                Assert.AreEqual(_testVector.Email, result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual(_testVector.FirstName, result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual(_testVector.LastName, result.Rows[0].ItemArray[4].ToString());
                Assert.AreEqual(_testVector.Mobile, result.Rows[0].ItemArray[5].ToString());

                //Check redirect url
                var actualUrl = _driver.Url;
                var expectedUrl = "https://localhost:44309/Member/";
                Assert.AreEqual(expectedUrl, actualUrl);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}