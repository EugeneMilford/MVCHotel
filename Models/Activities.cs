using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Activities
    {
        [Key]
        public int ActivityID { get; set; }
        public string GuestName { get; set; }
        public int NumberofPeople { get; set; }
        public DateTime BookingTime { get; set; }
        public string ActivityName { get; set; }
        public bool Confirmed { get; set; }
    }
}
