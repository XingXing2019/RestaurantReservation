using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using NUnit.Framework;
using T3RMSWS.Data;

namespace UnitTest.UnitTest
{
    public class ReservationServiceTest : TestBase
    {
        private ReservationService _service;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public ReservationServiceTest()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            var context = new ApplicationDbContext(optionBuilder.Options);
            _service = new ReservationService(context);
        }

        [SetUp]
        public void SetUp()
        {
            try
            {
                RemoteSetupDatabase("TestSampleDataSetup_Reservation.sql");
                RemoteSetupDatabase("TestSampleDataSetup_User.sql");
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
                RemoteSetupDatabase("MainSampleDataTearDown.sql");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task ReservationDeleteTest_InvalidReservationId()
        {
            var id = 39;
            try
            {
                await _service.DeleteReservation(id);
                var selectQuery = $"select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                //Should be three records in database, no one was deleted
                Assert.AreEqual(3, result.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task ReservationDeleteTest_Success()
        {
            var id = 40;
            try
            {
                await _service.DeleteReservation(id);
                var selectQuery = $"select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                //Should be two records left in database
                Assert.AreEqual(2, result.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task ReservationGetReservationTest_InvalidReservationId()
        {
            var id = 39;
            try
            {
                var reservation = await _service.GetOneReservation(id);

                //Reservation should be null as there is reservation in DB with id = 39
                Assert.IsNull(reservation);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task ReservationGetReservationTest_Success()
        {
            var id = 40;
            try
            {
                var reservation = await _service.GetOneReservation(id);

                Assert.IsNotNull(reservation);
                Assert.AreEqual(5, reservation.NumberOfGuest);
                Assert.AreEqual("2020-05-31 08:00:00", reservation.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Close to Window", reservation.Requirement);
                Assert.AreEqual(2, (int) reservation.ReservationSource);
                Assert.AreEqual("Tim Xing", reservation.GuestName);
                Assert.AreEqual(0, (int) reservation.SittingType);
                Assert.AreEqual(2, (int) reservation.Duration);
                Assert.AreEqual("test1@e.com", reservation.Email);
                Assert.AreEqual("62938ec0-8666-4697-aabc-55a4c70f541f", reservation.ReferenceNo.ToString());
                Assert.AreEqual("0425124578", reservation.Mobile);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GeUserTest_InvalidPersonId()
        {
            var personId = "676858bc-7cf1-44f1-8e4f-fd503b3e4a87";
            try
            {
                var user = await _service.GetUser(personId);
                Assert.IsNull(user);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GeUserTest_Success()
        {
            var personId = "376068bc-7cf1-44f1-8e4f-fd503b3c3a87";
            try
            {
                var user = await _service.GetUser(personId);

                Assert.IsNotNull(user);
                Assert.AreEqual("test1@e.com", user.UserName);
                Assert.AreEqual("test1@e.com", user.Email);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationForWalkInCustomerTest_ReferenceNo_InvalidReferenceNo()
        {
            var referenceNo = Guid.Parse("62578ec0-8666-4697-aabc-63a4c70f541f");
            try
            {
                var reservation = await _service.GetReservationForWalkInCustomer(referenceNo);
                Assert.IsNull(reservation);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        [Test]
        public async Task GetReservationForWalkInCustomerTest_ReferenceNo_Success()
        {
            var referenceNo = Guid.Parse("62938ec0-8666-4697-aabc-55a4c70f541f");
            try
            {
                var reservation = await _service.GetReservationForWalkInCustomer(referenceNo);

                Assert.IsNotNull(reservation);
                Assert.AreEqual(40, reservation.Id);
                Assert.AreEqual(5, reservation.NumberOfGuest);
                Assert.AreEqual("2020-05-31 08:00:00", reservation.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Close to Window", reservation.Requirement);
                Assert.AreEqual("Tim Xing", reservation.GuestName);
                Assert.AreEqual(2, (int)reservation.Duration);
                Assert.AreEqual("test1@e.com", reservation.Email);
                Assert.AreEqual("62938ec0-8666-4697-aabc-55a4c70f541f", reservation.ReferenceNo.ToString());
                Assert.AreEqual("0425124578", reservation.Mobile);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationForWalkInCustomerTest_Email_InvalidEmail()
        {
            var email = "test3@e.com";
            try
            {
                var reservations = await _service.GetReservationForWalkInCustomer(email);
                Assert.IsEmpty(reservations);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        [Test]
        public async Task GetReservationForWalkInCustomerTest_Email_Success()
        {
            var email = "test2@e.com";
            try
            {
                var reservations = await _service.GetReservationForWalkInCustomer(email);

                Assert.AreEqual(2, reservations.Count);

                Assert.AreEqual(41,reservations[0].Id);
                Assert.AreEqual(6,reservations[0].NumberOfGuest);
                Assert.AreEqual("2020-06-01 08:00:00", reservations[0].StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Away from Window", reservations[0].Requirement);
                Assert.AreEqual("Angela Dai", reservations[0].GuestName);
                Assert.AreEqual(2,(int) reservations[0].Duration);
                Assert.AreEqual("test2@e.com", reservations[0].Email);
                Assert.AreEqual("58938ec0-8666-4607-aabc-55a4c70f541f", reservations[0].ReferenceNo.ToString());
                Assert.AreEqual("0425135485", reservations[0].Mobile);

                Assert.AreEqual(42, reservations[1].Id);
                Assert.AreEqual(8, reservations[1].NumberOfGuest);
                Assert.AreEqual("2020-06-02 08:00:00", reservations[1].StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Big eater", reservations[1].Requirement);
                Assert.AreEqual("Angela Dai", reservations[1].GuestName);
                Assert.AreEqual(3, (int)reservations[1].Duration);
                Assert.AreEqual("test2@e.com", reservations[1].Email);
                Assert.AreEqual("c472e156-ff32-46dd-84d3-e8cffdcf112e", reservations[1].ReferenceNo.ToString());
                Assert.AreEqual("0425135485", reservations[1].Mobile);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}