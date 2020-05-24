using System.Collections.Generic;

namespace T3RMSWS.Data
{
    public class Person
    {
        public Person()
        {
            Reservations = new List<ReservationRequest>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<ReservationRequest> Reservations { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
