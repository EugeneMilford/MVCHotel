using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{

    public class Fitness
    {
            [Key]
            public int FitnessID { get; set; }
            public string MemberName { get; set; }
            public DateTime BookingTime { get; set; }
            public int DurationInMinutes { get; set; }
            public bool Confirmed { get; set; }
    }
}
