using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using T3RMSWS.Data;

namespace T3RMSWS.ViewModel
{
    public class ReservationIndexViewModel
    {
        public IEnumerable<ReservationRequest> Reservations { get; set; }
        public int TotalGuest { get; set; }
        public Dictionary<DateTime, List<ReservationRequest>> ReservationsByDate { get; set; }

        [Display(Name = "Table")]
        public TableType TableType { get; set; }
    }
}
