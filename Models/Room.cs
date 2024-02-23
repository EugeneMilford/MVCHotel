namespace HotelManagement.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string Occupant { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsBooked { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string HotelPackage { get; set; }
    }
}
