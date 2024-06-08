using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Cinema
    {
        public int CinemaID { get; set; }
        [Display(Name = "Name")]
        public string GuestName { get; set; }
        [Display(Name = "Date")]
        public DateTime BookingTime { get; set; }
        [Display(Name = "Movie")]
        public string MovieTitle { get; set; }
        [Display(Name = "Number of Tickets")]
        public int NumberOfTickets { get; set; }
        public bool Confirmed { get; set; }
    }
}
