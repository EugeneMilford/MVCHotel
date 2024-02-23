using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Spa
    {
        [Key]
        public int SpaId { get; set; }
        public string SpaCustomerName { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SpaPackage { get; set; }
        public string Service { get; set; }
        public bool IsConfirmed { get; set; }
        public decimal Price { get; set; }
    }
}
