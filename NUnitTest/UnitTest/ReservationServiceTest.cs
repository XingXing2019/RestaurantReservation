using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Internal;
using T3RMSWS.Data;
using Logger = NLog.Logger;

namespace UnitTest.UnitTest
{
    public class ReservationServiceTest : TestBase
    {
        private ReservationService _service;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private ReservationRequest _reservation;
        public ReservationServiceTest()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            var context = new ApplicationDbContext(optionBuilder.Options);
            _service = new ReservationService(context);
            _reservation = new ReservationRequest
            {
                Email = "test@e.com",
                Mobile = "0425123214",
                GuestName = "Timothy Xing",
                StartDateTime = new DateTime(2020, 05, 25, 08, 00, 00),
                SittingType = SittingType.Breakfast,
                Duration = DurationLength.OneHour,
                NumberOfGuest = 12,
                Requirement = "Close to window"
            };
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


        private static object[] CreateReservationTestCases =
        {
            new object[] { true, "688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57"},
            new object[] { false, "" }
        };
        [Test, TestCaseSource(nameof(CreateReservationTestCases))]
        public async Task TestCreateReservation(bool isMember, string userId)
        {
            if (isMember)
            {
                _reservation.Email = "m@e.com";
                _reservation.Mobile = "0400000000";
                _reservation.GuestName = "default";
            }
            try
            {
                var deleteQuery = "delete from ReservationRequests";
                RemoteQueryDatabase(deleteQuery);
                await _service.CreateReservation(_reservation, userId);
                var selectQuery = $"select * from ReservationRequests";
                var record = RemoteQueryDatabase(selectQuery);
                ValidateResults(record, new List<ReservationRequest> {_reservation});
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.InnerException.Message);
            }
        }

        
        private static object[] DeleteReservationTestCases =
        {
            new object[] { true, 40 },
            new object[] { false, 39 }
        };
        [Test, TestCaseSource(nameof(DeleteReservationTestCases))]
        public async Task TestDeleteReservation(bool isSuccessful, int id)
        {
            try
            {
                var selectQuery = $"select * from ReservationRequests";
                var before = RemoteQueryDatabase(selectQuery);
                await _service.DeleteReservation(id);
                var after = RemoteQueryDatabase(selectQuery);

                if (isSuccessful)
                    Assert.AreEqual(before.Rows.Count - 1, after.Rows.Count);
                else
                    Assert.AreEqual(before.Rows.Count, after.Rows.Count);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.InnerException.Message);
            }
        }


        private static object[] EditReservationTestCases =
        {
            new object[] { 41, "Away from window", new DateTime(2020, 06, 01, 21, 00, 00), DurationLength.OneAndHalfHours, 9, "Timothy Xing"},
            new object[] { 44, "Kosher", new DateTime(2020, 06, 02, 20, 00, 00), DurationLength.HalfHour, 2, "Peter Austin"}
        };
        [Test, TestCaseSource(nameof(EditReservationTestCases))]
        public async Task TestEditReservation(int reservationId, string requirement, DateTime startDateTime, DurationLength duration, int numberOfGuest, string guestName)
        {
            var userId = "688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57";
            try
            {
                var reservation = await _service.GetOneReservation(reservationId);

                reservation.Requirement = requirement;
                reservation.StartDateTime = startDateTime;
                reservation.Duration = duration;
                reservation.NumberOfGuest = numberOfGuest;
                await _service.EditReservation(reservation, userId);

                var selectQuery = $"select * from ReservationRequests where Id={reservationId}";
                var record = RemoteQueryDatabase(selectQuery);

                ValidateResults(record, new List<ReservationRequest>{reservation});
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.InnerException.Message);
            }
        }


        private static object[] GetReservationTestCases =
        {
            new object[] { true, 40 },
            new object[] { false, 39 }
        };
        [Test, TestCaseSource(nameof(GetReservationTestCases))]
        public async Task TestGetReservation(bool hasReservation, int id)
        {
            try
            {
                var reservations = new List<ReservationRequest> {await _service.GetOneReservation(id)};

                var selectQuery = $"select * from ReservationRequests where Id = '{id}'";
                var record = RemoteQueryDatabase(selectQuery);

                if(hasReservation)
                    ValidateResults(record, reservations);
                else
                    Assert.IsNull(reservations[0]);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        private static object[] GetUserTestCases =
        {
            new object[] { true, "376068bc-7cf1-44f1-8e4f-fd503b3c3a87" },
            new object[] { false, "676858bc-7cf1-44f1-8e4f-fd503b3e4a87" }
        };
        [Test, TestCaseSource(nameof(GetUserTestCases))]
        public async Task TestGetUser(bool hasUser, string personId)
        {
            try
            {
                var user = await _service.GetUser(personId);

                var selectQuery = $"select * from AspNetUsers where Id = '{personId}'";
                var record = RemoteQueryDatabase(selectQuery);

                if (hasUser)
                {
                    Assert.IsNotNull(user);
                    Assert.AreEqual(record.Rows[0]["UserName"], user.UserName);
                    Assert.AreEqual(record.Rows[0]["Email"], user.Email);
                }
                else
                    Assert.IsNull(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        

        private static object[] GetReservationForWalkInCustomerByReferenceNoTestCases =
        {
            new object[] { true, Guid.Parse("62938ec0-8666-4697-aabc-55a4c70f541f") },
            new object[] { false, Guid.Parse("62578ec0-8666-4697-aabc-63a4c70f541f") }
        };
        [Test, TestCaseSource(nameof(GetReservationForWalkInCustomerByReferenceNoTestCases))]
        public async Task TestGetReservationForWalkInCustomerTestByReferenceNo(bool hasReservation, Guid referenceNo)
        {
            try
            {
                var reservations = new List<ReservationRequest> {await _service.GetReservationForWalkInCustomer(referenceNo)};
                
                var selectQuery = $"select * from ReservationRequests where ReferenceNo = '{referenceNo}'";
                var record = RemoteQueryDatabase(selectQuery);

                if(hasReservation)
                    ValidateResults(record, reservations);
                else
                    Assert.IsNull(reservations[0]);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        private static object[] GetReservationForWalkInCustomerByEmailTestCases =
        {
            new object[] { true, "test2@e.com" },
            new object[] { false, "test4@e.com" }
        };
        [Test, TestCaseSource(nameof(GetReservationForWalkInCustomerByEmailTestCases))]
        public async Task TestGetReservationForWalkInCustomerByEmail(bool hasReservations, string email)
        {
            try
            {
                var reservations = await _service.GetReservationForWalkInCustomer(email);
                var selectQuery = $"select * from ReservationRequests where Email = '{email}'";
                var record = RemoteQueryDatabase(selectQuery);

                if (hasReservations)
                    ValidateResults(record, reservations);
                else
                    Assert.IsEmpty(reservations);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [Test]
        public async Task TestGetReservationsForMember_Success()
        {
            var userId = "688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57";
            try
            {
                var reservations = (await _service.GetReservationsForMember(userId)).Reservations.ToList();
                var selectQuery = $"select * from ReservationRequests where PersonId = '{userId}' order by PersonId";
                var record = RemoteQueryDatabase(selectQuery);
                ValidateResults(record, reservations);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [Test]
        public async Task TestGetReservationsForManager()
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


        private static object[] GetAvailableTimeRangeTestCases =
        {
            new object[] { true, 5 },
            new object[] { false, 10 }
        };
        [Test, TestCaseSource(nameof(GetAvailableTimeRangeTestCases))]
        public async Task TestGetAvailableTimeRange(bool hasAvailableTimeRange, int numberOfGuest)
        {
            _reservation.StartDateTime = new DateTime(2020, 06, 03, 10, 00, 00);
            _reservation.NumberOfGuest = numberOfGuest;
            var time = await _service.GetAvailableTimeRange(_reservation);
            if (hasAvailableTimeRange)
            {
                Assert.IsNotNull(time);
                Assert.AreEqual(new TimeSpan(7, 0, 0), time[0]);
            }
            else
                Assert.IsNull(time);
        }


        private static object[] IsReservationExistTestCases =
        {
            new object[] { true, 40 },
            new object[] { false, 39 }
        };
        [Test, TestCaseSource(nameof(IsReservationExistTestCases))]
        public void TestIsReservationExist(bool expected, int reservationId)
        {
            var isExist = _service.IsReservationExist(reservationId);
            Assert.AreEqual(expected, isExist);
        }


        private static object[] IsValidReservationTestCases =
        {
            new object[] {true, new DateTime(2020, 05, 30, 07, 00, 00), DurationLength.OneHour},
            new object[] {false, new DateTime(2020, 05, 30, 10, 00, 00), DurationLength.ThreeHour}
        };
        [Test, TestCaseSource(nameof(IsValidReservationTestCases))]
        public async Task TestIsValidReservation(bool expected, DateTime startDateTime, DurationLength duration)
        {
            _reservation.StartDateTime = startDateTime;
            _reservation.Duration = duration;
            var isValid = await _service.IsValidReservation(_reservation);
            Assert.AreEqual(expected, isValid);
        }


        private void ValidateResults(DataTable record, List<ReservationRequest> reservations)
        {
            try
            {
                Assert.AreEqual(record.Rows.Count, reservations.Count);

                for (int i = 0; i < record.Rows.Count; i++)
                {
                    Assert.AreEqual(record.Rows[i]["Id"], reservations[i].Id);
                    Assert.AreEqual(record.Rows[i]["NumberOfGuest"], reservations[i].NumberOfGuest);
                    Assert.AreEqual(record.Rows[i]["StartDateTime"], reservations[i].StartDateTime);
                    Assert.AreEqual(record.Rows[i]["Requirement"], reservations[i].Requirement);
                    Assert.AreEqual(record.Rows[i]["GuestName"], reservations[i].GuestName);
                    Assert.AreEqual(record.Rows[i]["Duration"], (int)reservations[i].Duration);
                    Assert.AreEqual(record.Rows[i]["Email"], reservations[i].Email);
                    Assert.AreEqual(record.Rows[i]["ReferenceNo"], reservations[i].ReferenceNo);
                    Assert.AreEqual(record.Rows[i]["Mobile"], reservations[i].Mobile);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}