using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{

    public class Fitness
    {
            [Key]
            public int FitnessID { get; set; }
            [Display(Name = "Name")]
            public string MemberName { get; set; }
            [Display(Name = "Date")]
            public DateTime BookingTime { get; set; }
            public bool Confirmed { get; set; }
    }
}
