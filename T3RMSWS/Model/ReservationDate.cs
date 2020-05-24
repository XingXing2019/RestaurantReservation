using System;
using System.Collections.Generic;

namespace T3RMSWS.Data
{
    public class ReservationDate
    {
        public ReservationDate()
        {
            Reservations = new List<ReservationRequest>();
            Tables = new List<Table>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ReservationRequest> Reservations { get; set; }
        public List<Table> Tables { get; set; }
    }
}
