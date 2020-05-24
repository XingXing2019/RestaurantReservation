using System;
using System.ComponentModel.DataAnnotations;

namespace T3RMSWS.Data
{
    public class ReservationRequest
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }


        [Display(Name = "Book For")]
        public SittingType? SittingType { get; set; }


        [Required]
        [Display(Name = "Guest Name")]
        public string GuestName { get; set; }

        
        [Required(ErrorMessage = "Please select number of guests")]
        [Display(Name = "Number of Guests")]
        [Range(1.0, 15.0, ErrorMessage = "Please enter between 1 and 15")]
        public int NumberOfGuest { get; set; }


        [Required(ErrorMessage = "Please select start date and time")]
        [Display(Name = "Start")]
        public DateTime StartDateTime { get; set; }


        [Required(ErrorMessage = "Please enter duration")]
        [Display(Name = "Duration")]
        public DurationLength Duration { get; set; }


        [Display(Name = "End")] 
        public DateTime EndDateTime => StartDateTime.AddHours((double)Duration / 2);


        [Display(Name = "Requirement")]
        public string Requirement { get; set; }


        [Display(Name = "Reservation Source")]
        public ReservationSource? ReservationSource { get; set; }


        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Display(Name = "Reservation Date")]
        public DateTime TimeStamp { get; set; }


        [Display(Name = "Reference No")]
        public Guid ReferenceNo { get; set; }

        
        [Display(Name = "Table")]
        public TableType TableType { get; set; }
    }
}
