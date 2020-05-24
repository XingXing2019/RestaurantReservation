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
        private IWebDriver driver;
        private string registerURL;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private TestVector testVector;

        [SetUp]
        public void SetUp()
        {
            string email = "test@e.com";
            string mobile = "0425123214";
            string firstName = "Timothy";
            string lastName = "Xing";
            string password = "Member@123";
            string passwordConfirm = "Member@123";
            try
            {
                testVector = GenerateTestVector(email, mobile, firstName, lastName, password, passwordConfirm);
                driver = new ChromeDriver();
                registerURL = "https://localhost:44309/Identity/Account/Register";
                driver.Navigate().GoToUrl(registerURL);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private class TestVector
        {
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string FirstName { get; set; }
            public int NumberOfGuest { get; set; }
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
                var invalidEmial = "m@@e.com";
                driver.FindElement(By.Id("email")).SendKeys(invalidEmial);

                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);
                driver.FindElement(By.Id("password")).SendKeys(testVector.Password);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(testVector.PasswordConfirm);
                driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = driver.FindElement(By.Id("emailErrorMsg")).Text;
                var expectedErrorMsg = "The Email field is not a valid e-mail address.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("emailErrorMsg")).Text;
                expectedErrorMsg = "The Email field is not a valid e-mail address.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidMobile()
        {
            try
            {
                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);

                //Invalid mobile
                driver.FindElement(By.Id("mobile")).SendKeys("");
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);
                driver.FindElement(By.Id("password")).SendKeys(testVector.Password);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(testVector.PasswordConfirm);
                driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = driver.FindElement(By.Id("mobileErrorMsg")).Text;
                var expectedErrorMsg = "The Mobile field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("errorMsg")).Text;
                expectedErrorMsg = "The Mobile field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidUsername()
        {
            try
            {
                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Email);

                //Invalid mobile
                driver.FindElement(By.Id("firstName")).SendKeys("");
                driver.FindElement(By.Id("lastName")).SendKeys("");

                driver.FindElement(By.Id("password")).SendKeys(testVector.Password);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(testVector.PasswordConfirm);
                driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = driver.FindElement(By.Id("firstNameErrorMsg")).Text;
                var expectedErrorMsg = "The First Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("lastNameErrorMsg")).Text;
                expectedErrorMsg = "The Last Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("errorMsg")).Text;
                expectedErrorMsg = "The First Name field is required.\r\nThe Last Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InconsistentPassword()
        {
            try
            {
                string inconsistentPassword = "Member@1234";

                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);
                driver.FindElement(By.Id("password")).SendKeys(testVector.Password);

                //Inconsistent password
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(inconsistentPassword);
                driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = driver.FindElement(By.Id("passwordConfirmErrorMsg")).Text;
                var expectedErrorMsg = "The password and confirmation password do not match.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidPassword_TooShort()
        {
            try
            {
                string tooShortPassword = "1234";

                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);

                //Invalid password
                driver.FindElement(By.Id("password")).SendKeys(tooShortPassword);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(tooShortPassword);
                driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = driver.FindElement(By.Id("passwordErrorMsg")).Text;
                var expectedErrorMsg = "The Password must be at least 6 and at max 100 characters long.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("errorMsg")).Text;
                expectedErrorMsg = "The Password must be at least 6 and at max 100 characters long.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_InvalidPassword_WrongFormat()
        {
            try
            {
                string wrongFormatPassword = "123456";

                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);

                //Invalid password
                driver.FindElement(By.Id("password")).SendKeys(wrongFormatPassword);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(wrongFormatPassword);
                driver.FindElement(By.Id("register")).Click();

                var actualErrorMsg = driver.FindElement(By.Id("errorMsg")).Text;
                var expectedErrorMsg = "Passwords must have at least one non alphanumeric character.\r\nPasswords must have at least one lowercase ('a'-'z').\r\nPasswords must have at least one uppercase ('A'-'Z').";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void RegisterTest_ExistingUser()
        {
            try
            {
                //Insert user with email test@e.com into DB
                RemoteSetupDatabase("TestSampleDataSetup.sql");

                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);
                driver.FindElement(By.Id("password")).SendKeys(testVector.Password);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(testVector.PasswordConfirm);
                driver.FindElement(By.Id("register")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("errorMsg")).Text;
                var expectedErrorMsg = "User name 'test@e.com' is already taken.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                var deleteQuery = $@"delete from People where Email='{testVector.Email}';
                                delete from AspNetUsers where Email='{testVector.Email}';";
                RemoteQueryDatabase(deleteQuery);
            }
        }

        [Test]
        public void RegisterTest_Success()
        {
            try
            {
                driver.FindElement(By.Id("email")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("mobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("firstName")).SendKeys(testVector.FirstName);
                driver.FindElement(By.Id("lastName")).SendKeys(testVector.LastName);
                driver.FindElement(By.Id("password")).SendKeys(testVector.Password);
                driver.FindElement(By.Id("passwordConfirm")).SendKeys(testVector.PasswordConfirm);
                driver.FindElement(By.Id("register")).Click();
                Thread.Sleep(500);

                //Check record in database
                var selectQuery = $"select * from People where Email='{testVector.Email}'";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);
                Assert.AreEqual(testVector.Email, result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual(testVector.FirstName, result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual(testVector.LastName, result.Rows[0].ItemArray[4].ToString());
                Assert.AreEqual(testVector.Mobile, result.Rows[0].ItemArray[5].ToString());

                //Check redirect url
                var actualUrl = driver.Url;
                var expectedUrl = "https://localhost:44309/Member/";
                Assert.AreEqual(expectedUrl, actualUrl);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                //Remove test data from database
                var deleteQuery = $@"delete from People where Email='{testVector.Email}';
                                delete from AspNetUsers where Email='{testVector.Email}';";
                RemoteQueryDatabase(deleteQuery);
            }
        }
    }
}