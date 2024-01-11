namespace HotelManagement.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int Amount { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
