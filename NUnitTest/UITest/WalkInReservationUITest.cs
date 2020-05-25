using System;
using System.Threading;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using T3RMSWS.Data;

namespace UnitTest.UITest
{
    class WalkInReservationUITest : TestBase
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
            string guestName = "Timothy Xing";
            DateTime startDateTime = new DateTime(2020, 05, 25, 08, 00, 00);
            string duration = "1.0 hour";
            int numberOfGuest = 12;
            string requirement = "Close to window";
            try
            {
                testVector = GenerateTestVector(email, mobile, guestName, startDateTime, duration, numberOfGuest, requirement);
                driver = new ChromeDriver();
                registerURL = "https://localhost:44309/Reservation/Create";
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
            public string GuestName { get; set; }
            public DateTime StartDateTime { get; set; }
            public string Duration { get; set; }
            public int NumberOfGuest { get; set; }
            public string Requirement { get; set; }
        }

        private static TestVector GenerateTestVector(
            string email,
            string mobile,
            string guestName,
            DateTime startDateTime, 
            string duration, 
            int numberOfGuest, 
            string requirement
            ) => new TestVector
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
                StartDateTime = startDateTime,
                Duration = duration,
                NumberOfGuest = numberOfGuest,
                Requirement = requirement
            };


        [Test]
        public void WalkInReservationTest_InvalidEmail()
        {
            try
            {
                //Invalid email address
                var invalidEmail = "m@@e.com";
                driver.FindElement(By.Id("walkInEmail")).SendKeys(invalidEmail);

                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys(testVector.NumberOfGuest.ToString());
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInEmailErrorMsg")).Text;
                var expectedErrorMsg = "Invalid Email Address";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
                expectedErrorMsg = "Invalid Email Address";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void WalkInReservationTest_EmptyEmail()
        {
            try
            {
                //Empty email address
                driver.FindElement(By.Id("walkInEmail")).SendKeys("");

                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys(testVector.NumberOfGuest.ToString());
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInEmailErrorMsg")).Text;
                var expectedErrorMsg = "The Email field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
                expectedErrorMsg = "The Email field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void WalkInReservationTest_EmptyMobile()
        {
            try
            {
                driver.FindElement(By.Id("walkInEmail")).SendKeys(testVector.Email);

                //Empty mobile
                driver.FindElement(By.Id("walkInMobile")).SendKeys("");
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys(testVector.NumberOfGuest.ToString());
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInMobileErrorMsg")).Text;
                var expectedErrorMsg = "The Mobile field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
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
        public void WalkInReservationTest_EmptyGuestName()
        {
            try
            {
                driver.FindElement(By.Id("walkInEmail")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);

                //Empty mobile
                driver.FindElement(By.Id("walkInGuestName")).SendKeys("");
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys(testVector.NumberOfGuest.ToString());
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInGuestNameErrorMsg")).Text;
                var expectedErrorMsg = "The Guest Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
                expectedErrorMsg = "The Guest Name field is required.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        
        [Test]
        public void WalkInReservationTest_EmptyNumberOfGuest()
        {
            try
            {
                driver.FindElement(By.Id("walkInEmail")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);

                //Empty number of guest
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys("");
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInNumberOfGuestErrorMsg")).Text;
                var expectedErrorMsg = "Please select number of guests.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
                expectedErrorMsg = "Please select number of guests.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void WalkInReservationTest_LowNumberOfGuest()
        {
            try
            {
                driver.FindElement(By.Id("walkInEmail")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);

                //Low number of guest
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys("0");
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInNumberOfGuestErrorMsg")).Text;
                var expectedErrorMsg = "Please enter between 1 and 15.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
                expectedErrorMsg = "Please enter between 1 and 15.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void WalkInReservationTest_HighNumberOfGuest()
        {
            try
            {
                driver.FindElement(By.Id("walkInEmail")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate"))
                    .SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);

                //Low number of guest
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys("16");
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                var actualErrorMsg = driver.FindElement(By.Id("walkInNumberOfGuestErrorMsg")).Text;
                var expectedErrorMsg = "Please enter between 1 and 15.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);

                actualErrorMsg = driver.FindElement(By.Id("walkInErrorMsg")).Text;
                expectedErrorMsg = "Please enter between 1 and 15.";
                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public void WalkInReservationTest_Success()
        {
            try
            {
                driver.FindElement(By.Id("walkInEmail")).SendKeys(testVector.Email);
                driver.FindElement(By.Id("walkInMobile")).SendKeys(testVector.Mobile);
                driver.FindElement(By.Id("walkInGuestName")).SendKeys(testVector.GuestName);
                driver.FindElement(By.Id("walkInStartDate")).SendKeys(testVector.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
                new SelectElement(driver.FindElement(By.Id("walkInDuration"))).SelectByText(testVector.Duration);
                driver.FindElement(By.Id("walkInNumberOfGuest")).SendKeys(testVector.NumberOfGuest.ToString());
                driver.FindElement(By.Id("walkInRequirement")).SendKeys(testVector.Requirement);
                driver.FindElement(By.Id("confirm")).Click();
                Thread.Sleep(500);

                //Check record in database
                var selectQuery = "select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);
                Assert.AreEqual(testVector.NumberOfGuest, (int)result.Rows[0].ItemArray[1]);
                Assert.AreEqual(testVector.StartDateTime, (DateTime)result.Rows[0].ItemArray[2]);
                Assert.AreEqual(testVector.Requirement, result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual(testVector.GuestName, result.Rows[0].ItemArray[5].ToString());
                Assert.AreEqual(DurationLength.OneHour, (DurationLength)(int)result.Rows[0].ItemArray[9]);
                Assert.AreEqual(testVector.Email, result.Rows[0].ItemArray[11].ToString());
                Assert.AreEqual(testVector.Mobile, result.Rows[0].ItemArray[16].ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                //Remove test data from database
                var deleteQuery = @"delete from ReservationRequests; 
                                    delete from ReservationDates;";
                RemoteQueryDatabase(deleteQuery);
            }
        }
    }
}
