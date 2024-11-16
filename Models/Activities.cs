using HotelManagement.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Activities
    {
        [Key]
        public int ActivityID { get; set; }
        [Display(Name = "Name")]
        public string GuestName { get; set; }
        [Display(Name = "Number of People")]
        public int NumberofPeople { get; set; }
        public DateTime BookingTime { get; set; }
        [Display(Name = "Activity")]
        public string ActivityName { get; set; }
        public bool Confirmed { get; set; }
        public string UserId { get; set; }
        public HotelUser User { get; set; } // Link to HotelUser
    }
}
