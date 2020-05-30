using System;
using System.Linq;
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
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private string email = "test@e.com";
        private string mobile = "0425123214";
        private string guestName = "Timothy Xing";
        private DateTime startDateTime = new DateTime(2020, 05, 25, 08, 00, 00);
        private DurationLength duration = DurationLength.OneHour;
        private int numberOfGuest = 12;
        private string requirement = "Close to window";
        private string userId = "688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57";

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
                RemoteSetupDatabase("TestSampleDataSetup_User.sql");
                RemoteSetupDatabase("TestSampleDataSetup_Reservation.sql");
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
                RemoteSetupDatabase("MainSampleDataTearDown.sql");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task AddSittingTest_Success()
        {
            try
            {
                var deleteQuery = "delete from Sittings";
                RemoteQueryDatabase(deleteQuery);
                var selectQuery = "select * from Sittings";
                var result = RemoteQueryDatabase(selectQuery);
                Assert.AreEqual(0, result.Rows.Count);

                await _service.AddSittings();
                result = RemoteQueryDatabase(selectQuery);

                //Should be three records in database
                Assert.AreEqual(3, result.Rows.Count);

                //Check details of the records
                Assert.AreEqual("1/01/2020 7:00:00 AM", result.Rows[0].ItemArray[1].ToString());
                Assert.AreEqual("1/01/2020 11:00:00 AM", result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual(15, (int)result.Rows[0].ItemArray[3]);
                Assert.AreEqual(0, (int)result.Rows[0].ItemArray[4]);

                Assert.AreEqual("1/01/2020 11:00:00 AM", result.Rows[1].ItemArray[1].ToString());
                Assert.AreEqual("1/01/2020 4:00:00 PM", result.Rows[1].ItemArray[2].ToString());
                Assert.AreEqual(15, (int)result.Rows[1].ItemArray[3]);
                Assert.AreEqual(1, (int)result.Rows[1].ItemArray[4]);

                Assert.AreEqual("1/01/2020 4:00:00 PM", result.Rows[2].ItemArray[1].ToString());
                Assert.AreEqual("1/01/2020 11:00:00 PM", result.Rows[2].ItemArray[2].ToString());
                Assert.AreEqual(15, (int)result.Rows[2].ItemArray[3]);
                Assert.AreEqual(2, (int)result.Rows[2].ItemArray[4]);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task CreateReservationTest_WalkIn_Success()
        {
            var reservation = new ReservationRequest
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
                StartDateTime = startDateTime,
                Duration = duration,
                NumberOfGuest = numberOfGuest,
                Requirement = requirement
            };

            try
            {
                var deleteQuery = "delete from ReservationRequests";
                RemoteQueryDatabase(deleteQuery);
                await _service.CreateReservation(reservation, "");
                var selectQuery = $"select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);

                Assert.AreEqual(12, (int)result.Rows[0].ItemArray[1]);
                Assert.AreEqual("25/05/2020 8:00:00 AM", result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual("Close to window", result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual("Timothy Xing", result.Rows[0].ItemArray[5].ToString());
                Assert.AreEqual(2, (int)result.Rows[0].ItemArray[9]);
                Assert.AreEqual("test@e.com", result.Rows[0].ItemArray[11].ToString());
                Assert.AreEqual(0, (int)result.Rows[0].ItemArray[15]);
                Assert.AreEqual("0425123214", result.Rows[0].ItemArray[16].ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task CreateReservationTest_Member_Success()
        {
            var reservation = new ReservationRequest
            {
                Email = "m@e.com",
                Mobile = "0400000000",
                GuestName = "default",
                StartDateTime = startDateTime,
                Duration = duration,
                NumberOfGuest = numberOfGuest,
                Requirement = requirement
            };

            try
            {
                var deleteQuery = "delete from ReservationRequests";
                RemoteQueryDatabase(deleteQuery);
                await _service.CreateReservation(reservation, userId);
                var selectQuery = $"select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);

                Assert.AreEqual(12, (int)result.Rows[0].ItemArray[1]);
                Assert.AreEqual("25/05/2020 8:00:00 AM", result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual("Close to window", result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual("Angela Dai", result.Rows[0].ItemArray[5].ToString());
                Assert.AreEqual("688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57", result.Rows[0].ItemArray[8].ToString());
                Assert.AreEqual(2, (int)result.Rows[0].ItemArray[9]);
                Assert.AreEqual("test2@e.com", result.Rows[0].ItemArray[11].ToString());
                Assert.AreEqual(0, (int)result.Rows[0].ItemArray[15]);
                Assert.AreEqual("0457843658", result.Rows[0].ItemArray[16].ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task DeleteTest_InvalidReservationId()
        {
            var id = 39;
            try
            {
                await _service.DeleteReservation(id);
                var selectQuery = $"select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(9, result.Rows.Count);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task DeleteTest_Success()
        {
            var id = 40;
            try
            {
                await _service.DeleteReservation(id);
                var selectQuery = $"select * from ReservationRequests";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(8, result.Rows.Count);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task EditReservationTest_WalkIn_Success()
        {
            var reservationId = 44;
            var reservation = await _service.GetOneReservation(reservationId);
            try
            {
                reservation.Requirement = "Away from window";
                reservation.StartDateTime = new DateTime(2020, 06, 02, 20, 00, 00);
                reservation.Duration = DurationLength.OneAndHalfHours;
                reservation.NumberOfGuest = 9;
                await _service.EditReservation(reservation, "");

                var selectQuery = $"select * from ReservationRequests where Id={reservationId}";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);
                Assert.AreEqual(9, (int)result.Rows[0].ItemArray[1]);
                Assert.AreEqual("2/06/2020 8:00:00 PM", result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual("Away from window", result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual(3, (int)result.Rows[0].ItemArray[9]);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task EditReservationTest_Member_Success()
        {
            var reservationId = 41;
            try
            {
                var reservation = await _service.GetOneReservation(reservationId);

                reservation.Requirement = "Away from window";
                reservation.StartDateTime = new DateTime(2020, 06, 01, 21, 00, 00);
                reservation.Duration = DurationLength.OneAndHalfHours;
                reservation.NumberOfGuest = 9;
                await _service.EditReservation(reservation, userId);

                var selectQuery = $"select * from ReservationRequests where Id={reservationId}";
                var result = RemoteQueryDatabase(selectQuery);

                Assert.AreEqual(1, result.Rows.Count);
                Assert.AreEqual(9, (int)result.Rows[0].ItemArray[1]);
                Assert.AreEqual("1/06/2020 9:00:00 PM", result.Rows[0].ItemArray[2].ToString());
                Assert.AreEqual("Away from window", result.Rows[0].ItemArray[3].ToString());
                Assert.AreEqual("Timothy Xing", result.Rows[0].ItemArray[5].ToString());
                Assert.AreEqual(3, (int)result.Rows[0].ItemArray[9]);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationTest_InvalidReservationId()
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
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationTest_Success()
        {
            var id = 40;
            try
            {
                var reservation = await _service.GetOneReservation(id);

                Assert.IsNotNull(reservation);
                Assert.AreEqual(5, reservation.NumberOfGuest);
                Assert.AreEqual("2020-05-31 08:00:00", reservation.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Close to Window", reservation.Requirement);
                Assert.AreEqual(2, (int)reservation.ReservationSource);
                Assert.AreEqual("Timothy Xing", reservation.GuestName);
                Assert.AreEqual(0, (int)reservation.SittingType);
                Assert.AreEqual(2, (int)reservation.Duration);
                Assert.AreEqual("test1@e.com", reservation.Email);
                Assert.AreEqual("62938ec0-8666-4697-aabc-55a4c70f541f", reservation.ReferenceNo.ToString());
                Assert.AreEqual("0425124578", reservation.Mobile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
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
                _logger.Error(ex.Message);
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
                _logger.Error(ex.Message);
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
                _logger.Error(ex.Message);
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
                Assert.AreEqual("Timothy Xing", reservation.GuestName);
                Assert.AreEqual(2, (int)reservation.Duration);
                Assert.AreEqual("test1@e.com", reservation.Email);
                Assert.AreEqual("62938ec0-8666-4697-aabc-55a4c70f541f", reservation.ReferenceNo.ToString());
                Assert.AreEqual("0425124578", reservation.Mobile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationForWalkInCustomerTest_Email_InvalidEmail()
        {
            var email = "test4@e.com";
            try
            {
                var reservations = await _service.GetReservationForWalkInCustomer(email);
                Assert.IsEmpty(reservations);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
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

                Assert.AreEqual(42, reservations[0].Id);
                Assert.AreEqual(6, reservations[0].NumberOfGuest);
                Assert.AreEqual("2020-06-01 08:00:00", reservations[0].StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Away from Window", reservations[0].Requirement);
                Assert.AreEqual("Angela Dai", reservations[0].GuestName);
                Assert.AreEqual(2, (int)reservations[0].Duration);
                Assert.AreEqual("test2@e.com", reservations[0].Email);
                Assert.AreEqual("58938ec0-8666-4607-aabc-55a4c70f541f", reservations[0].ReferenceNo.ToString());
                Assert.AreEqual("0425135485", reservations[0].Mobile);

                Assert.AreEqual(43, reservations[1].Id);
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
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationsForMember_Success()
        {
            try
            {
                var reservations = (await _service.GetReservationsForMember(userId)).Reservations.ToList();

                Assert.AreEqual(2, reservations.Count);

                Assert.AreEqual(42, reservations[0].Id);
                Assert.AreEqual(6, reservations[0].NumberOfGuest);
                Assert.AreEqual("2020-06-01 08:00:00", reservations[0].StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual("Away from Window", reservations[0].Requirement);
                Assert.AreEqual("Angela Dai", reservations[0].GuestName);
                Assert.AreEqual(2, (int)reservations[0].Duration);
                Assert.AreEqual("test2@e.com", reservations[0].Email);
                Assert.AreEqual("58938ec0-8666-4607-aabc-55a4c70f541f", reservations[0].ReferenceNo.ToString());
                Assert.AreEqual("0425135485", reservations[0].Mobile);

                Assert.AreEqual(43, reservations[1].Id);
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
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetReservationsForManager_Success()
        {
            try
            {
                var results = (await _service.GetReservationsForManager()).ReservationsByDate;

                Assert.AreEqual(4, results.Count);

                var reservations1 = results[new DateTime(2020, 5, 31, 0, 0, 0)];
                Assert.AreEqual(1, reservations1.Count);
                Assert.AreEqual("62938ec0-8666-4697-aabc-55a4c70f541f", reservations1[0].ReferenceNo.ToString());

                var reservations2 = results[new DateTime(2020, 6, 1, 0, 0, 0)];
                Assert.AreEqual(2, reservations2.Count);
                Assert.AreEqual("58938ec0-8666-4607-aabc-55a4c70f541f", reservations2[0].ReferenceNo.ToString());
                Assert.AreEqual("09eda8b2-6cac-46c8-b811-2b6d124a849b", reservations2[1].ReferenceNo.ToString());

                var reservations3 = results[new DateTime(2020, 6, 2, 0, 0, 0)];
                Assert.AreEqual(2, reservations3.Count);
                Assert.AreEqual("e8afbdf8-829a-4ef7-a13b-02e86c7625a0", reservations3[0].ReferenceNo.ToString());
                Assert.AreEqual("c472e156-ff32-46dd-84d3-e8cffdcf112e", reservations3[1].ReferenceNo.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Test]
        public async Task GetAvailableTimeRange_HasAvailableTime()
        {
            var reservation = new ReservationRequest
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
                StartDateTime = new DateTime(2020, 06, 03, 10, 00, 00),
                SittingType = SittingType.Breakfast,
                Duration = DurationLength.OneHour,
                NumberOfGuest = 5,
                Requirement = requirement
            };
            var time = await _service.GetAvailableTimeRange(reservation);
            Assert.IsNotNull(time);
            Assert.AreEqual(new TimeSpan(7, 0, 0), time[0]);
        }

        [Test]
        public async Task GetAvailableTimeRange_NoAvailableTime()
        {
            var reservation = new ReservationRequest
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
                StartDateTime = new DateTime(2020, 06, 03, 10, 00, 00),
                SittingType = SittingType.Breakfast,
                Duration = DurationLength.OneHour,
                NumberOfGuest = 10,
                Requirement = requirement
            };
            var time = await _service.GetAvailableTimeRange(reservation);
            Assert.IsNull(time);
        }

        [Test]
        public void IsReservationExist_Exist()
        {
            var reservationId = 40;
            var isExist = _service.IsReservationExist(reservationId);
            Assert.IsTrue(isExist);
        }

        [Test]
        public void IsReservationExist_NotExist()
        {
            var reservationId = 39;
            var isExist = _service.IsReservationExist(reservationId);
            Assert.IsFalse(isExist);
        }

        [Test]
        public async Task IsValidReservation_Vaild()
        {
            var reservation = new ReservationRequest
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
                StartDateTime = new DateTime(2020, 05, 30, 07, 00, 00),
                Duration = DurationLength.ThreeHour,
                NumberOfGuest = numberOfGuest,
                Requirement = requirement
            };
            var isValid = await _service.IsValidReservation(reservation);
            Assert.IsTrue(isValid);
        }

        [Test]
        public async Task IsValidReservation_Invaild()
        {
            var reservation = new ReservationRequest
            {
                Email = email,
                Mobile = mobile,
                GuestName = guestName,
                StartDateTime = new DateTime(2020, 05, 30, 10, 00, 00),
                Duration = DurationLength.ThreeHour,
                NumberOfGuest = numberOfGuest,
                Requirement = requirement
            };
            var isValid = await _service.IsValidReservation(reservation);
            Assert.IsFalse(isValid);
        }
    }
}