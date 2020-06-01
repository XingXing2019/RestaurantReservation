using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T3RMSWS.Data;

namespace T3RMSWS.Controllers
{
    public class Reservation : Controller
    {
        private readonly ReservationService _service;
        public Reservation(ReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _service.AddSittings();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationRequest reservation)
        {
            if (ModelState.IsValid)
            {
                var isVaildReservation = await _service.IsValidReservation(reservation);
                if (!isVaildReservation)
                {
                    ViewBag.AvailableTimeMsg = "Sorry, the reservation end time is outside the sitting period";
                    return View(reservation);
                }
                var user = await _service.GetUser(reservation.PersonId);
                var userId = user?.Id;
                var isSuccess = await _service.CreateReservation(reservation, userId);
                if (isSuccess)
                    return RedirectToAction(nameof(Success), reservation);
                else
                {
                    var availableTimeRange = await _service.GetAvailableTimeRange(reservation);
                    if (availableTimeRange == null)
                        ViewBag.AvailableTimeMsg = "Sorry, there is no available time on selected date, please select another date";
                    else
                    {
                        var startTime = availableTimeRange[0];
                        ViewBag.AvailableTimeMsg =
                            $"There is no vacancy on selected time, please select {reservation.StartDateTime.ToString("yyyy-MM-dd")} {startTime}";
                    }
                    return View(reservation);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(ReservationRequest reservation, int reservationId)
        {
            if (reservation.ReferenceNo == Guid.Empty)
                reservation = await _service.GetOneReservation(reservation.Id);
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Details(ReservationRequest reservation)
        {
            var referenceNo = reservation.ReferenceNo;
            reservation = await _service.GetReservationForWalkInCustomer(referenceNo);
            if (reservation == null)
                return RedirectToAction(nameof(Create));
            return View(reservation);
        }

        public IActionResult Success(ReservationRequest reservation)
        {
            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var reservation = await _service.GetOneReservation(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservationRequest reservation)
        {
            if (id != reservation.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                var isVaildReservation = await _service.IsValidReservation(reservation);
                if (!isVaildReservation)
                {
                    ViewBag.AvailableTimeMsg = "Sorry, the reservation end time is outside the sitting period";
                    return View(reservation);
                }
                try
                {
                    var user = await _service.GetUser(reservation.PersonId);
                    var userId = user?.Id;
                    var isSuccess = await _service.EditReservation(reservation, userId);
                    if (isSuccess)
                        return RedirectToAction(nameof(Details), reservation);
                    else
                    {
                        var availableTimeRange = await _service.GetAvailableTimeRange(reservation);
                        if (availableTimeRange == null)
                            ViewBag.AvailableTimeMsg = "Sorry, there is no available time on selected date, please select another date";
                        else
                        {
                            var startTime = availableTimeRange[0];
                            ViewBag.AvailableTimeMsg =
                                $"There is no vacancy on selected time, please select {reservation.StartDateTime.ToString("yyyy-MM-dd")} {startTime}";
                        }
                        return View(reservation);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.IsReservationExist(id))
                        return NoContent();
                    else
                        throw;
                }
            }
            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var reservation = await _service.GetOneReservation(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteReservation(id);
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public async Task<ActionResult> ReservationbyEmail(string email)
        {
            var reservations = await _service.GetReservationForWalkInCustomer(email);
            if (reservations.Count == 0)
                return NotFound();
            var result = reservations.Select(r =>
                new
                {
                    r.Id,
                    Reservation = new
                    {
                        r.ReferenceNo,
                        r.StartDateTime,
                        r.Duration,
                        r.NumberOfGuest,
                        r.ReservationSource,
                        r.Requirement,
                    },
                    Person = new
                    {
                        r.PersonId,
                        r.GuestName,
                        r.Email,
                        r.Mobile
                    }
                });
            return Ok(result);
        }
    }
}