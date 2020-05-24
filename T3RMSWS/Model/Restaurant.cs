using System.Collections.Generic;

namespace T3RMSWS.Data
{
    public class Restaurant
    {
        public Restaurant()
        {
            People = new List<Person>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Person> People { get; set; }
    }
}
