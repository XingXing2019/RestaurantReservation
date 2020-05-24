using System.Collections.Generic;

namespace T3RMSWS.Data
{
    public class Table
    {
        public int Id { get; set; }
        public TableType TableType { get; set; }

        public int ReservationDateId { get; set; }
        public ReservationDate ReservationDate { get; set; }

        public int ReservationId { get; set; }
        public ReservationRequest Reservation { get; set; }
    }
}