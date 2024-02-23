using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class PlayArea
    {
        [Key]
        public int PlayAreaID { get; set; }
        public string GuardianName { get; set; }
        public DateTime BookingTime { get; set; }
        public int DurationInHours { get; set; }
        public int NumberOfChildren { get; set; }
        public bool Confirmed { get; set; }
    }
}
