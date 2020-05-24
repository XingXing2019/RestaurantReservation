using System;
using System.Collections.Generic;

namespace T3RMSWS.Data
{
    public class Sitting
    {
        public Sitting()
        {
            Reservations = new List<ReservationRequest>();
        }
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public SittingType SittingType { get; set; }
        public List<ReservationRequest> Reservations { get; set; }
    }
}
