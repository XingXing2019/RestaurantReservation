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
            string firstName = "Timothy";
            string lastName = "Xing";
            string password = "Member@123";
            string passwordConfirm = "Member@123";
            try
            {
                //testVector = GenerateTestVector(email, mobile, firstName, lastName, password, passwordConfirm);
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

        private static TestVector GenerateTestVector(string email, string mobile, string guestName)
            => new TestVector
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
            };
    }
}
