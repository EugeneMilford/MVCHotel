using HotelManagement.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class PlayArea
    {
        [Key]
        public int PlayAreaID { get; set; }
        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }
        [Display(Name = "Date")]
        public DateTime BookingTime { get; set; }
        [Display(Name = "Number of Children")]
        public int NumberOfChildren { get; set; }
        public bool Confirmed { get; set; }
        public string UserId { get; set; }
        public HotelUser User { get; set; } // Link to HotelUser
    }
}
