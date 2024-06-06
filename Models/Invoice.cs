namespace HotelManagement.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoicePayer { get; set; }
        public string HotelPackage { get; set; }
        public int Amount { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
