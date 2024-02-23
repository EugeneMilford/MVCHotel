namespace HotelManagement.Models
{
    public class Cinema
    {
        public int CinemaID { get; set; }
        public string GuestName { get; set; }
        public DateTime BookingTime { get; set; }
        public string MovieTitle { get; set; }
        public int NumberOfTickets { get; set; }
        public bool Confirmed { get; set; }
    }
}
