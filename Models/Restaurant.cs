using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public string CustomerName { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumberOfGuests { get; set; }
        public bool Confirmed { get; set; }
    }
}
