using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using T3RMSWS.Data;
using T3RMSWS.Models;

namespace T3RMSWS.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Manager")]
    public class Reservation : ReservationMethodsRepository
    {
        public Reservation(UserManager<IdentityUser> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }
        public async Task<IActionResult> Index()
        {
            var reservationIndexViewModel = await GetReservationsForManager();
            return View(reservationIndexViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var reservation = await GetOneReservation(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var reservation = await GetOneReservation(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservationRequest reservation)
        {
            if (ModelState.IsValid)
            {
                var isVaildReservation = await IsValidReservation(reservation);
                if (!isVaildReservation)
                {
                    ViewBag.AvailableTimeMsg = "Sorry, the reservation end time is outside the sitting period";
                    return View(reservation);
                }
                try
                {
                    var isSuccess = await EditReservation(reservation);
                    if(isSuccess)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        var availableTimeRange = await GetAvailableTimeRange(reservation);
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
                    if (!IsReservationExist(reservation.Id))
                        return NoContent();
                    else
                        throw;
                }
               
            }
            return View(reservation);
        }

      

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var reservation = await GetOneReservation(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}