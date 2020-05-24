using System;
using System.Threading;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            DurationLength duration = DurationLength.OneHour;
            int numberOfGuest = 12;
            string requirement = "Close to window";
            try
            {
                testVector = GenerateTestVector(email, mobile, guestName, startDateTime, duration, numberOfGuest, requirement);
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
            public string GuestName { get; set; }
            public DateTime StartDateTime { get; set; }
            public DurationLength Duration { get; set; }
            public int NumberOfGuest { get; set; }
            public string Requirement { get; set; }
        }

        private static TestVector GenerateTestVector(
            string email,
            string mobile,
            string guestName,
            DateTime startDateTime, 
            DurationLength duration, 
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
        public void WalkInReservationTest_Success()
        {

        }
    }
}
