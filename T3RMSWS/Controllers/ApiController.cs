using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T3RMSWS.Data;

namespace T3RMSWS.Controllers
{
    public class ApiController : Controller
    {
        private readonly ReservationService _service;

        public ApiController(ReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> ReservationByEmail(string email)
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
