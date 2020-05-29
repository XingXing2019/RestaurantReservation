using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NLog;

using T3RMSWS.ViewModel;

namespace T3RMSWS.Data
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ReservationService(ApplicationDbContext context)
        {
            try
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add sittings in database if there is no sitting.
        /// </summary>
        /// <returns></returns>
        public async Task AddSittings()
        {
            try
            {
                if (!_context.Sittings.Any())
                {
                    var breakfast = new Sitting
                    {
                        StartTime = new DateTime(2020, 01, 01, 07, 00, 00),
                        EndTime = new DateTime(2020, 01, 01, 11, 00, 00),
                        Capacity = 15,
                        SittingType = SittingType.Breakfast
                    };
                    var lunch = new Sitting
                    {
                        StartTime = new DateTime(2020, 01, 01, 11, 00, 00),
                        EndTime = new DateTime(2020, 01, 01, 16, 00, 00),
                        Capacity = 15,
                        SittingType = SittingType.Lunch
                    };
                    var dinner = new Sitting
                    {
                        StartTime = new DateTime(2020, 01, 01, 16, 00, 00),
                        EndTime = new DateTime(2020, 01, 01, 23, 00, 00),
                        Capacity = 15,
                        SittingType = SittingType.Dinner
                    };
                    _context.Sittings.AddRange(breakfast, lunch, dinner);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Save reservation in database.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task<bool> CreateReservation(ReservationRequest reservation, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return await CreateReservationInDataBase(reservation, user);
        }

        /// <summary>
        /// Delete specific reservation from database based on reservation id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteReservation(int id)
        {
            try
            {
                var reservation = await _context.ReservationRequests.FindAsync(id);
                if(reservation == null)
                    return;
                _context.ReservationRequests.Remove(reservation);
                var table = await _context.Tables.FirstOrDefaultAsync(t => t.ReservationId.Equals(reservation.Id));
                if (table != null)
                    _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edit reservation in database.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task<bool> EditReservation(ReservationRequest reservation, string userId)
        {

            var user = await _context.Users.FindAsync(userId);
            return await EditReservationInDataBase(reservation, user);
        }

        /// <summary>
        /// Get specific reservation from database based on the input reservation id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReservationRequest> GetOneReservation(int? id)
        {
            try
            {
                var reservation = await _context.ReservationRequests.FindAsync(id);
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IdentityUser> GetUser(string personId)
        {
            if (personId != null)
                return await _context.Users.FindAsync(personId);
            return null;
        }

        /// <summary>
        /// Get reservation for walking in customers with their reservation reference.
        /// </summary>
        /// <param name="referenceNo"></param>
        /// <returns></returns>
        public async Task<ReservationRequest> GetReservationForWalkInCustomer(Guid referenceNo)
        {
            try
            {
                var reservation = await _context.ReservationRequests.FirstOrDefaultAsync(r => r.ReferenceNo.Equals(referenceNo));
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all reservations for walking in customers with their email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<ReservationRequest>> GetReservationForWalkInCustomer(string email)
        {
            try
            {
                var reservations = await _context.ReservationRequests.Where(r => r.Email.Equals(email)).ToListAsync();
                return reservations;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get reservations only belong to this login member.
        /// </summary>
        /// <returns></returns>
        public async Task<ReservationIndexViewModel> GetReservationsForMember(string userId)
        {
            try
            {
                //Get reservations only belong to this person and order these reservations by their start date.
                var reservations = await _context.ReservationRequests
                    .Where(r => r.PersonId.Equals(userId))
                    .OrderBy(r => r.StartDateTime)
                    .ToListAsync();

                //Save the reservations in the reservationIndexViewModel and return it to view.
                var reservationIndexViewModel = new ReservationIndexViewModel { Reservations = reservations };
                return reservationIndexViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all reservations for manager.
        /// </summary>
        /// <returns></returns>
        public async Task<ReservationIndexViewModel> GetReservationsForManager()
        {
            try
            {
                //Get all reservations from database.
                var reservations = await _context.ReservationRequests
                    .OrderBy(r => r.StartDateTime)
                    .ToListAsync();

                //Group the reservations based on their start date.
                var reservationByDate = new Dictionary<DateTime, List<ReservationRequest>>();
                foreach (var reservation in reservations)
                {
                    if (!reservationByDate.ContainsKey(reservation.StartDateTime.Date))
                        reservationByDate[reservation.StartDateTime.Date] = new List<ReservationRequest> { reservation };
                    else
                        reservationByDate[reservation.StartDateTime.Date].Add(reservation);
                }

                //Save the grouped reservation in the reservationIndexViewModel and return it to view.
                var reservationIndexViewModel = new ReservationIndexViewModel { Reservations = reservations, ReservationsByDate = reservationByDate };
                return reservationIndexViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Recommend available time slot based on the number of guest of reservation duration and the time range of the reservation.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task<TimeSpan[]> GetAvailableTimeRange(ReservationRequest reservation)
        {
            try
            {
                //Get existing reservations in the same date and same sitting of the input reservation.
                var currentReservations = await _context.ReservationRequests
                    .AsNoTracking()
                    .Where(r => r.StartDateTime.Date.Equals(reservation.StartDateTime.Date) && r.SittingType.Equals(reservation.SittingType))
                    .OrderBy(r => r.StartDateTime)
                    .ToListAsync();

                //Check if the input reservation is already in database. This only happens if current operation is edit, but does not happen if the current operation is create.
                var oldReservation = currentReservations.Find(r => r.Id.Equals(reservation.Id));

                //If the current reservation already exists (current operation is edit), should remove it as it will cause duplicate calculation of the total number of guest.
                if (oldReservation != null)
                    currentReservations.Remove(oldReservation);
                var sitting =
                    await _context.Sittings.FirstOrDefaultAsync(s => s.SittingType.Equals(reservation.SittingType));

                //Create dictionary and initialize the time slot based on the start time and end time of each reservation
                var dict = new Dictionary<TimeSpan, List<int?>> { { sitting.StartTime.TimeOfDay, new List<int?>() }, { sitting.EndTime.TimeOfDay, new List<int?>() } };
                foreach (var r in currentReservations)
                {
                    if (!dict.ContainsKey(r.StartDateTime.TimeOfDay))
                        dict[r.StartDateTime.TimeOfDay] = new List<int?>();
                    if (!dict.ContainsKey(r.EndDateTime.TimeOfDay))
                        dict[r.EndDateTime.TimeOfDay] = new List<int?> { r.NumberOfGuest };
                    else
                        dict[r.EndDateTime.TimeOfDay].Add(r.NumberOfGuest);
                }

                //Record how many guest in each time slot.
                foreach (var record in dict)
                {
                    foreach (var r in currentReservations)
                    {
                        if (record.Key > r.StartDateTime.TimeOfDay && record.Key < r.EndDateTime.TimeOfDay)
                            dict[record.Key].Add(r.NumberOfGuest);
                    }
                }

                //Group time slot and number of guest into two arrays, the length of these arrays will be same.
                var timeSlot = dict.OrderBy(record => record.Key).Select(record => record.Key).ToArray();
                var guestNo = dict.OrderBy(record => record.Key).Select(record => record.Value.Sum(v => v)).ToArray();

                //Traversal the time slot array. For each time point, try to find if there is any slot after this time point that current reservation can fit in.
                //Criteria:
                //1. Number of guest already in current slot + number of guest of the input reservation is less than the sitting capacity.
                //2. The found slot has enough time for the input reservation(The reservation will not excess the end time of the sitting).
                for (int i = 0; i < timeSlot.Length; i++)
                {
                    bool foundSlot = true;
                    int index = i + 1;
                    while (index < timeSlot.Length)
                    {
                        //If this time slot does not has enough space for the number of guest in input reservation, set foundSlot to false.
                        //Jump i to index - 1 as there must be no valid time point between the previous i and index.
                        //break the while loop for next loop of outer for loop.
                        if (guestNo[index] + reservation.NumberOfGuest > sitting.Capacity)
                        {
                            i = index - 1;
                            foundSlot = false;
                            break;
                        }

                        //If the while loop was not broken by last if statement, that means there is time slot between time point of timeSlot[i] and timeSlot[index] can hold the number of input reservation.
                        //Then we need to check if this time slot if long enough to hold the duration of the input reservation. If it can, also break the while loop, but the foundSlot is true this time.
                        if (timeSlot[i].Add(TimeSpan.FromHours((double)reservation.Duration / 2)) <= timeSlot[index])
                            break;
                        index++;
                    }

                    //If foundSlot is true and the reservation will not excess the end time of sitting. return this available start time.
                    if (foundSlot && timeSlot[i].Add(TimeSpan.FromHours((double)reservation.Duration / 2)) <= sitting.EndTime.TimeOfDay)
                        return new[] { timeSlot[i] };
                };

                //Return null if no valid time slot.
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Check if the reservation is already existing in database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsReservationExist(int id)
        {
            try
            {
                return _context.ReservationRequests.Any(reservation => reservation.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Check if the input reservation is valid.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task<bool> IsValidReservation(ReservationRequest reservation)
        {
            try
            {
                var sitting = await SetSittingType(reservation);
                if (reservation.EndDateTime.TimeOfDay > sitting.EndTime.TimeOfDay)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }




        /// <summary>
        /// Set sitting type for reservation based on its start time.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private async Task<Sitting> SetSittingType(ReservationRequest reservation)
        {
            try
            {
                //Get the start time of input reservation.
                var reservationTime = reservation.StartDateTime.TimeOfDay;

                //Get all the sittings from database.
                var sittings = _context.Sittings.ToList();

                //Traversal the sittings, search the correct sitting type and set it to reservation.
                for (int i = 0; i < sittings.Count; i++)
                {
                    if (reservationTime >= sittings[i].StartTime.TimeOfDay &&
                        reservationTime < sittings[i].EndTime.TimeOfDay)
                    {
                        reservation.SittingType = (SittingType)i;
                        break;
                    }
                }
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.SittingType.Equals(reservation.SittingType));
                sitting.Reservations.Add(reservation);
                return sitting;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edit reservation in database, this method can be used for edit only.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private async Task<bool> EditReservationInDataBase(ReservationRequest reservation, IdentityUser user)
        {
            try
            {
                var sitting = await SetSittingType(reservation);

                //Get all reservations from database, use AsNoTracking method to avoid exception.
                var currentReservations = await _context.ReservationRequests.AsNoTracking().ToListAsync();
                var oldReservation = currentReservations.Find(r => r.Id.Equals(reservation.Id));

                //Check if the input reservation can be made using CanMakeReservation method.
                var canMakeReservation = CanMakeReservation(currentReservations, oldReservation, reservation, sitting);
                if (!canMakeReservation)
                    return false;

                //Check is the current operator the manager or member.
                var isManager = false;
                if (reservation.Email == "m@e.com" && reservation.GuestName == "default" && reservation.Mobile == "0400000000")
                {
                    var roles = await _context.Roles.ToListAsync();
                    var userRoles = await _context.UserRoles.ToListAsync();
                    var manager = roles.Find(r => r.Name.ToLower().Equals("manager"));
                    isManager = userRoles.Find(u => u.UserId.Equals(user.Id)).RoleId == manager.Id;
                }

                //Keep the current reservation same as old reservation.
                reservation.ReservationSource = oldReservation.ReservationSource;
                reservation.TimeStamp = oldReservation.TimeStamp;
                reservation.ReferenceNo = oldReservation.ReferenceNo;
                reservation.PersonId = oldReservation.PersonId;
                reservation.GuestName = oldReservation.GuestName;
                reservation.Email = oldReservation.Email;
                reservation.Mobile = oldReservation.Mobile;

                var reservationDate =
                    await _context.ReservationDates.FirstOrDefaultAsync(r => r.Date.Date.Equals(reservation.StartDateTime.Date));
                if (reservationDate == null)
                {
                    reservationDate = new ReservationDate { Date = reservation.StartDateTime.Date };
                    _context.ReservationDates.Add(reservationDate);
                }

                //If table is unassigned, create one and save it to database, else update the existing one.
                var table = await _context.Tables.FirstOrDefaultAsync(t => t.ReservationId.Equals(reservation.Id));
                if (table == null)
                {
                    table = new Table
                    {
                        TableType = reservation.TableType,
                        ReservationDateId = reservation.Id,
                        ReservationDate = reservationDate,
                        Reservation = reservation
                    };
                    reservation.Table = table;
                    reservation.TableId = table.Id;
                    _context.Tables.Add(table);
                }
                else
                {
                    //If current operator is manager, he is allowed to change the table type in the table entity.
                    //Otherwise, the customer should only follow the table type read from database.
                    if (isManager)
                        table.TableType = reservation.TableType;
                    else
                        reservation.TableType = table.TableType;
                    reservation.Table = table;
                    reservation.TableId = table.Id;
                }

                reservationDate.Reservations.Add(reservation);
                _context.ReservationRequests.Update(reservation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Create reservation into database, this method can be used for create only.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private async Task<bool> CreateReservationInDataBase(ReservationRequest reservation, IdentityUser user)
        {
            try
            {
                var sitting = await SetSittingType(reservation);

                //Get all reservations from database, use AsNoTracking method to avoid exception.
                var currentReservations = await _context.ReservationRequests.AsNoTracking().ToListAsync();

                //Check if the input reservation can be made using CanMakeReservation method.
                var canMakeReservation = CanMakeReservation(currentReservations, null, reservation, sitting);
                if (!canMakeReservation)
                    return false;

                if (reservation.Email == "m@e.com" && reservation.GuestName == "default" && reservation.Mobile == "0400000000")
                {
                    if (user != null)
                    {
                        var guest = await _context.People.FindAsync(user.Id);
                        reservation.PersonId = user.Id;
                        reservation.Email = user.Email;
                        reservation.GuestName = guest.FullName;
                        reservation.Mobile = guest.Mobile;
                    }
                }
                reservation.ReservationSource = ReservationSource.Online;
                reservation.TimeStamp = DateTime.Now;
                reservation.ReferenceNo = Guid.NewGuid();

                var reservationDate =
                    await _context.ReservationDates.FirstOrDefaultAsync(r => r.Date.Date.Equals(reservation.StartDateTime.Date));
                if (reservationDate == null)
                {
                    reservationDate = new ReservationDate { Date = reservation.StartDateTime.Date };
                    _context.ReservationDates.Add(reservationDate);
                }
                reservationDate.Reservations.Add(reservation);
                _context.ReservationRequests.Add(reservation);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Check if the reservation can be made
        /// </summary>
        /// <param name="currentReservations"></param>
        /// <param name="oldReservation"></param>
        /// <param name="reservation"></param>
        /// <param name="sitting"></param>
        /// <returns></returns>
        private bool CanMakeReservation(List<ReservationRequest> currentReservations, ReservationRequest oldReservation, ReservationRequest reservation, Sitting sitting)
        {
            try
            {
                //Remove old reservation from current reservations as it will cause duplicate calculation of number of guest.
                if (oldReservation != null)
                    currentReservations.Remove(oldReservation);

                //Find the reservations with same date and sitting of the input reservation.
                var reservations = currentReservations
                    .Where(r => r.StartDateTime.Date.Equals(reservation.StartDateTime.Date) &&
                                r.SittingType.Equals(reservation.SittingType) &&
                                (r.StartDateTime.TimeOfDay < reservation.EndDateTime.TimeOfDay && r.StartDateTime.TimeOfDay >= reservation.StartDateTime.TimeOfDay ||
                                r.EndDateTime.TimeOfDay > reservation.StartDateTime.TimeOfDay && r.EndDateTime.TimeOfDay <= reservation.EndDateTime.TimeOfDay))
                    .OrderBy(r => r.StartDateTime)
                    .ToList();

                //Create dictionary and initialize the time slot based on the start time and end time of each reservation. 
                //Should consider the overlapped reservations as well.
                var dict = new Dictionary<TimeSpan, List<int?>>
                {
                    {reservation.StartDateTime.TimeOfDay, new List<int?>()},
                    {reservation.EndDateTime.TimeOfDay, new List<int?>()}
                };
                foreach (var r in reservations)
                {
                    if (!dict.ContainsKey(r.StartDateTime.TimeOfDay))
                        dict[r.StartDateTime.TimeOfDay] = new List<int?>();
                    if (r.EndDateTime.TimeOfDay > reservation.EndDateTime.TimeOfDay)
                        continue;
                    if (!dict.ContainsKey(r.EndDateTime.TimeOfDay))
                        dict[r.EndDateTime.TimeOfDay] = new List<int?> { r.NumberOfGuest };
                    else
                        dict[r.EndDateTime.TimeOfDay].Add(r.NumberOfGuest);
                }

                //Add number of guest of input reservation into the corresponding time points.
                foreach (var record in dict)
                {
                    record.Value.Add(reservation.NumberOfGuest);
                    foreach (var r in reservations)
                        if (record.Key > r.StartDateTime.TimeOfDay && record.Key < r.EndDateTime.TimeOfDay)
                            dict[record.Key].Add(r.NumberOfGuest);
                }

                //Check if all the time points can hold the number of guest of input reservation, and return the result.
                var guestNo = dict.OrderBy(record => record.Key).Select(record => record.Value.Sum(v => v)).ToArray();
                return guestNo.All(number => number <= sitting.Capacity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReservationIndexViewModel> GetReservationsForStaff(string mobile)
        {
            try
            {
                //Get all reservations from database.
                var reservations = await _context.ReservationRequests
                    .OrderBy(r => r.StartDateTime)
                    .ToListAsync();

                if (!String.IsNullOrEmpty(mobile))
                {
                    reservations = reservations.Where(s => s.Mobile.Contains(mobile)).ToList();
                }

                //Group the reservations based on their start date.
                var reservationByDate = new Dictionary<DateTime, List<ReservationRequest>>();
                foreach (var reservation in reservations)
                {
                    if (!reservationByDate.ContainsKey(reservation.StartDateTime.Date))
                        reservationByDate[reservation.StartDateTime.Date] = new List<ReservationRequest> { reservation };
                    else
                        reservationByDate[reservation.StartDateTime.Date].Add(reservation);
                }

                //Save the grouped reservation in the reservationIndexViewModel and return it to view.
                var reservationIndexViewModel = new ReservationIndexViewModel {
                    Reservations = reservations, 
                    ReservationsByDate = reservationByDate 
                };
                return reservationIndexViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
